using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject TripTarget;
    public TriggerObject TriggerTarget;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == TripTarget.gameObject)
        {
            TriggerTarget.Trigger();
        }
    }
}
