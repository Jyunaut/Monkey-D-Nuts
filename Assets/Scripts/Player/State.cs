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
        public override void OnTransition()
        {
            if (Inputs.IsPressingMovement)
                Controller.SetState(new Move());
            if(Inputs.InteractAPress)
                Controller.SetState(new Interact());
        }
    }

    class Move : State
    {
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
        private Action _doOnExit; // NOTE: Place holder until pick-up animation is implemented
        public override void OnEnter()
        {
            base.OnEnter();
            
            float radius = 1f;
            Collider2D[] hit = Physics2D.OverlapCircleAll(Controller.transform.position, radius);
            foreach (Collider2D obj in hit)
            {
                if (obj.transform.tag == "Respawn")
                {
                    Interactible interactible = obj.transform.GetComponent<Interactible>();
                    if (interactible.CanInteract)
                    {
                        interactible.Interact(Controller);
                        break;
                    }
                }
            }
            _doOnExit = () =>
            {
                float waitTime = 0.15f;
                _doOnExit = null;
                Controller.StartCoroutine(wait());
                IEnumerator wait()
                {
                    yield return new WaitForSeconds(waitTime);
                    Controller.SetState(new Idle());
                }
            };

        }

        public override void OnTransition()
        {
            base.OnTransition();
            if (_doOnExit != null)
                _doOnExit();
        }
    }
}