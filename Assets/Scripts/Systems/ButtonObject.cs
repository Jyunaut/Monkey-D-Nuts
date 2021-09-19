using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonObject : Interactible
{
    public GameObject[] TriggerTarget;

    private List<Interactible> _triggerTarget;
    private Interactible _tripTarget;
    private Action _doDisableTriggerTarget;

    protected override void Awake()
    {
        base.Awake();

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

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col != null)
        {
            if(_doDisableTriggerTarget == null)
                _tripTarget = col.GetComponent<Interactible>();
            if (!_tripTarget.IsActive)
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

    private void TriggerTargetCallback()
    {
        if(_tripTarget.IsActive)
        {
            IsActive = false;
            _doDisableTriggerTarget = null;
        }
    }
}

