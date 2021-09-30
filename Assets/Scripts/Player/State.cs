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
    }

    class Idle : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            if(Controller.HeldItem != null)
            {
                Controller.Animator.Play("IdlePickup");
                return;
            }
            Controller.Animator.Play("Idle");
        }
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
        public override void OnEnter()
        {
            base.OnEnter();
            if(Controller.HeldItem != null)
            {
                Controller.Animator.Play("MovePickup");
                return;
            }
            Controller.Animator.Play("Move");
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
            if (Inputs.InteractAPress)
                Controller.SetState(new Interact());
        }
    }

    class Interact : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            if(Controller.HeldItem != null)
            {
                Controller.SetState(new Drop());
                return;
            }

            float radius = 1.5f;
            Collider2D[] hit = Physics2D.OverlapCircleAll(Controller.transform.position, radius);
            foreach (Collider2D obj in hit)
            {
                if (obj.transform.tag == "Box") // NOTE: Create a tag called interactible
                {
                    Interactible interactible = obj.transform.GetComponent<Interactible>();

                    if (interactible.CanInteract)
                    {
                        // Controller.StartCoroutine(DoInteract(new PickUp(interactible)));
                        Controller.SetState(new PickUp(interactible));
                    }
                    return;
                }
            }
            Controller.SetState(new Idle());
        }
    }

    class PickUp : State
    {
        private Interactible _interactible;

        public PickUp(Interactible interactible = null)
        {
            _interactible = interactible;
            Controller.HeldItem = _interactible;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Controller.StartCoroutine(DoDrop());
            IEnumerator DoDrop()
            {
                Controller.GetComponent<AudioSource>().Play();
                Controller.Animator.Play("Pickup");
                _interactible.Interact();
                yield return new WaitForSeconds(0.3f);
                if(_interactible != null)
                    Controller.SetState(new Idle());

            }
        }
    }

    class Drop : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Controller.StartCoroutine(DoDrop());
            Controller.HeldItem.Stop();
            Controller.HeldItem = null;
            IEnumerator DoDrop()
            {
                Controller.Animator.Play("Drop");
                yield return new WaitForSeconds(0.5f);
                if (!Inputs.IsPressingMovement)
                    Controller.SetState(new Idle());
                if (Inputs.IsPressingMovement)
                    Controller.SetState(new Move());
            }
        }
    }
}