using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class Controller : MonoBehaviour
    {
        [SerializeField, Range(0,5)] private float _speed = 1f;
        private float _speedMultiplier = 1f;
        public float Speed => _speed * _speedMultiplier;
        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = Mathf.Clamp(multiplier, 0.1f, 4f);
        public void ResetSpeedMultiplier() => _speedMultiplier = 1f;

        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2d { get; private set; }
        public Collider2D Collider2d { get; private set; }

        public State State { get; set; }

        public static bool ControlsEnabled { get; private set; } = true;
        public static bool CanMove { get; private set; } = true;
        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        [field: SerializeField] public Interactible Object { get; set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2d = GetComponent<Collider2D>();
            State.SetController(this);
            SetState(new Idle());
        }

        private void Update()
        {
            State.OnUpdate();
            State.OnTransition();
        }

        private void FixedUpdate()
        {
            State.OnFixedUpdate();
        }

        public void SetState(State state)
        {
            State?.OnExit();
            State = state;
            State.OnEnter();
        }

        public void SetInteractible(Interactible obj)
        {
            Object?.OnExit();
            Object = obj;
            Object.Interact(this.gameObject);
            Object.OnEnter();
        }
    }
}