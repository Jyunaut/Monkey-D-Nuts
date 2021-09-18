using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorOpen : ButtonReceiver
{
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    public override void Trigger()
    {
        Debug.Log("OPEN");
        _boxCollider2D.enabled = false;
    }
}
