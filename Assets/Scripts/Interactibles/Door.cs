using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TriggerObject
{
    public override void Trigger()
    {
        Debug.Log("OPEN");
    }
}
