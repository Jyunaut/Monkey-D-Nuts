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
        _triggerTarget = TriggerTarget.GetComponent<Interactible>();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.CompareTag("Box"))
        {
            if(!_tripTarget.IsActive)
            {
                TripTarget.transform.position = this.transform.position;
                _triggerTarget.DoBehaviour();
            }
        }
    }
}
