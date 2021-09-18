using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
            Controller.Velocity = (Controller.Speed * Controller.Direction) * Time.deltaTime;
            Controller.Rigidbody2d.MovePosition(Controller.Rigidbody2d.position + Controller.Velocity);
        }

        public override void OnTransition()
        {
            if (!Inputs.IsPressingMovement)
                Controller.SetState(new Idle());
        }
    }

    class Interact : State
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Interact");
        }
    }
}