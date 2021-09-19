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
            // Controller.Animator.Play("Idle");
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
            Interactible interactible = null;
            IEnumerator DoInteract(State state)
            {
                yield return new WaitForSeconds(0.15f);
                if (interactible != null)
                    Controller.SetState(state);
                else
                    Controller.SetState(new Idle());
            }

            float radius = 1.5f;
            Collider2D[] hit = Physics2D.OverlapCircleAll(Controller.transform.position, radius);
            foreach (Collider2D obj in hit)
            {
                if (obj.transform.tag == "Box")
                {
                    interactible = obj.transform.GetComponent<Interactible>();
                    if (interactible.CanInteract)
                    {
                        if(interactible.gameObject.CompareTag("Box"))
                        {
                            if(interactible.IsActive)
                                Controller.StartCoroutine(DoInteract(new Drop(interactible)));
                            else
                                Controller.StartCoroutine(DoInteract(new PickUp(interactible)));
                        }
                        else if(interactible.gameObject.CompareTag("Switch"))
                            Controller.StartCoroutine(DoInteract(new Kick(interactible)));
                        return;
                    }
                }
            }
            Controller.SetState(new Idle());
        }
    }
    
    class Kick : State
    {
        private Interactible _interactible;

        public Kick(Interactible interactible = null)
        {
            _interactible = interactible;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Controller.StartCoroutine(DoDrop());
            IEnumerator DoDrop()
            {
                Controller.Animator.Play("Kick");
                _interactible.Interact(Controller);
                yield return new WaitForSeconds(0.15f);
                if(_interactible != null)
                    Controller.SetState(new Idle());

            }
        }
    }

    class PickUp : State
    {
        private Interactible _interactible;

        public PickUp(Interactible interactible = null)
        {
            _interactible = interactible;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Controller.StartCoroutine(DoDrop());
            IEnumerator DoDrop()
            {
                Controller.Animator.Play("Pickup");
                _interactible.Interact(Controller);
                yield return new WaitForSeconds(0.3f);
                if(_interactible != null)
                    Controller.SetState(new Idle());

            }
        }
    }

    class Drop : State
    {
        private Interactible _interactible;

        public Drop(Interactible interactible = null)
        {
            _interactible = interactible;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Controller.StartCoroutine(DoDrop());
            _interactible.Interact(Controller);
            IEnumerator DoDrop()
            {
                Controller.Animator.Play("Drop");
                yield return new WaitForSeconds(0.5f);
                Controller.Animator.Play("Idle");
                Controller.SetState(new Idle());
            }
        }
    }
}