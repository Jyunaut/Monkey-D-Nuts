using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public GameObject TripTarget;
    public ButtonReceiver TriggerTarget;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == TripTarget.gameObject)
        {
            TriggerTarget.Trigger();
        }
    }
}
