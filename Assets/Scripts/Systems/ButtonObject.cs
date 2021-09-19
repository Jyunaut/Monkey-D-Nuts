using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : Interactible
{
    public List<GameObject> TriggerTarget = new List<GameObject>();

    private List<Interactible> _triggerTarget;

    protected override void Awake()
    {
        base.Awake();
        if(TriggerTarget.Count > 0)
        {
            _triggerTarget = new List<Interactible>();
            for(int i = 0; i < TriggerTarget.Count; i++)
                _triggerTarget.Add(TriggerTarget[i].GetComponent<Interactible>());
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Box"))
        {
            Interactible TripTarget = col.GetComponent<Interactible>();
            if(!TripTarget.IsActive)
            {
                TripTarget.transform.position = this.transform.position;
                for(int i = 0; i < TriggerTarget.Count; i++)
                {
                    _triggerTarget[i].DoBehaviour();
                }
            }
        }
    }
}
