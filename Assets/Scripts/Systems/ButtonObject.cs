using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

public class ButtonObject : Interactible
{
    public GameObject[] TriggerTarget;

    private Controller _player;
    private List<Interactible> _triggerTarget;
    [SerializeField] private Interactible _tripTarget;
    private Action _doDisableTriggerTarget;

    [SerializeField] private GameObject _levelTransitioner;

    protected override void Awake()
    {
        base.Awake();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        TriggerTarget = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
        _tripTarget = null;
        if (TriggerTarget.Length > 0)
        {
            _triggerTarget = new List<Interactible>();
            for (int i = 0; i < TriggerTarget.Length; i++)
                _triggerTarget.Add(TriggerTarget[i].GetComponent<Interactible>());
        }
    }

    protected override void Update()
    {
        base.Update();
        if (_doDisableTriggerTarget != null)
            _doDisableTriggerTarget();
    }
    private bool _triggered;

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col != null)
        {
            if (_levelTransitioner != null && col.gameObject.CompareTag("Box") && !_triggered && IsActive)
            {
                var levelTransition = _levelTransitioner.GetComponent<TriggerCutScene>();
                if (levelTransition != null)
                {
                    levelTransition.PlayCutscene();
                    _triggered = true;
                }
            }
            if(_doDisableTriggerTarget == null)
            {
                _tripTarget = col.GetComponent<Interactible>();
                if (!_tripTarget.IsActive && _player.HeldItem == null)
                {
                    _tripTarget.transform.position = this.transform.position;
                    for (int i = 0; i < TriggerTarget.Length; i++)
                    {
                        IsActive = true;
                        _doDisableTriggerTarget = TriggerTargetCallback;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(_tripTarget == null)
            _tripTarget = col.GetComponent<Interactible>();
        if (_doDisableTriggerTarget == null && col != null && !_tripTarget.IsActive)
            this.GetComponent<AudioSource>().Play();
    }

    private void TriggerTargetCallback()
    {
        if(_tripTarget.IsActive && _tripTarget == _player.HeldItem)
        {
            IsActive = false;
            _doDisableTriggerTarget = null;
        }
    }
}

