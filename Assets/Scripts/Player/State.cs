using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player
{
    public abstract class State : StateMachine
    {
        protected static Controller Controller { get; private set; }

        public static void SetController(Controller controller)
        {
            Controller = controller;
        }

        public override void OnEnter()
        {
            Controller.Animator.Play(GetType().Name, 0, 0f);
        }
    }

    class Idle : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Idle");
        }

        public override void OnTransition()
        {
            if (Inputs.IsPressingMovement)
                Controller.SetState(new Move());
        }
    }

    class Move : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Move");
        }

        public override void OnFixedUpdate()
        {
            if (Inputs.IsPressingMovement)
                Controller.Direction = new Vector2(Inputs.Horizontal, Inputs.Vertical).normalized;
            
            Controller.Velocity = Controller.Speed * Controller.Direction;
            Controller.Rigidbody2d.MovePosition(Controller.Rigidbody2d.position + (Controller.Velocity * Time.deltaTime));
        }

        public override void OnTransition()
        {
            if (!Inputs.IsPressingMovement)
                Controller.SetState(new Idle());
        }
    }

    class Interact : State
    {
        private Action DoWait;
        public override void OnEnter()
        {
            base.OnEnter();
            DoWait = () =>
            {
                Controller.StartCoroutine(wait()); DoWait = null;
                IEnumerator wait()
                {
                    yield return new WaitForSeconds(0.15f);
                    Controller.SetState(new Idle());
                }
            };

            if(Controller.Object != null)
            {
                Controller.Object.Stop();
                Controller.Object = null;
                return;
            }

            float radius = 1f;
            Collider2D[] hit = Physics2D.OverlapCircleAll(Controller.transform.position, radius);
            foreach(Collider2D obj in hit)
            {
                if(obj.transform.tag == "Respawn")
                {
                    Controller.SetInteractible(obj.transform.GetComponent<Interactible>());
                    break;
                }
            }
        }

        public override void OnTransition()
        {
            base.OnTransition();
            if (DoWait != null)
                DoWait();
        }
    }
}