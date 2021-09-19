using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorOpen : Interactible
{
    private BoxCollider2D _boxCollider2D;

    protected override void Awake()
    {
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        _boxCollider2D.enabled = false;
    }
    public override void Stop()
    {
        base.Stop();
        _boxCollider2D.enabled = true;
    }
}
