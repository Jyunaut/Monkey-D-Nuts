using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : Interactible
{
    public GameObject[] TriggerTarget;

    private List<Interactible> _triggerTarget;

    protected override void Awake()
    {
        base.Awake();

        TriggerTarget = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
        if (TriggerTarget.Length > 0)
        {
            _triggerTarget = new List<Interactible>();
            for (int i = 0; i < TriggerTarget.Length; i++)
                _triggerTarget.Add(TriggerTarget[i].GetComponent<Interactible>());
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        Interactible TripTarget = col.GetComponent<Interactible>();
        if (!TripTarget.IsActive)
        {
            TripTarget.transform.position = this.transform.position;
            for (int i = 0; i < TriggerTarget.Length; i++)
                _triggerTarget[i].DoBehaviour();
        }
    }
}

