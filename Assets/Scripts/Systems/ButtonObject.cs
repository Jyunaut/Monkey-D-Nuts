using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : Interactible
{
    public GameObject TripTarget;
    public GameObject TriggerTarget;

    private Interactible _tripTarget;
    private Interactible _triggerTarget;

    protected override void Awake()
    {
        base.Awake();
        _tripTarget = TripTarget.GetComponent<Interactible>();
        _triggerTarget = TripTarget.GetComponent<Interactible>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == TripTarget.gameObject)
        {
            if(!_tripTarget.IsActive)
                _triggerTarget.DoBehaviour();
        }
    }
}
