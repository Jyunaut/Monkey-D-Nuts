using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : Interactible
{
    public GameObject TriggerTarget;

    private Interactible _triggerTarget;

    protected override void Awake()
    {
        base.Awake();
        _triggerTarget = TriggerTarget.GetComponent<Interactible>();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.CompareTag("Box"))
        {
            Interactible TripTarget = col.GetComponent<Interactible>();
            if(!TripTarget.IsActive)
            {
                TripTarget.transform.position = this.transform.position;
                _triggerTarget.DoBehaviour();
            }
        }
    }
}
