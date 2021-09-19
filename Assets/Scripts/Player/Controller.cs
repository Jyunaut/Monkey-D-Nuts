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
        [SerializeField, Range(0f,40f)] private float _speed = 1f;
        [field: SerializeField] public Interactible HeldItem { get; set; }
        [SerializeField] private AudioClip[] _audioClips;
        
        private float _speedMultiplier = 1f;
        public float Speed => _speed * _speedMultiplier;
        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = Mathf.Clamp(multiplier, 0.1f, 4f);
        public void ResetSpeedMultiplier() => _speedMultiplier = 1f;

        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2d { get; private set; }
        public Collider2D Collider2d { get; private set; }
        public AudioSource AudioSource { get; private set; }

        public State State { get; set; }

        public static bool ControlsEnabled { get; set; } = true;
        public static bool CanMove { get; set; } = true;
        public static bool CanInteract { get; set; } = true;

        public Vector2 Velocity { get; set; }
        public Vector2 Direction 
        { 
            get { return _direction; } 
            set { _direction = value; SetDirection(); } 
        }
        private Vector2 _direction = Vector2.one;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
            Collider2d = GetComponent<Collider2D>();
            AudioSource = GetComponent<AudioSource>();
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
            State?.OnEnter();
        }

        public void PlayAudioClip(int index)
        {
            if (index >= _audioClips.Length)
                return;
            float scale = 1f;
            if (index == 0) scale = 0.5f;
            AudioSource.PlayOneShot(_audioClips[index], scale);
        }

        private void SetDirection()
        {
            if ((_direction.x < 0 && transform.localScale.x > 0) || _direction.x > 0 && transform.localScale.x < 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}