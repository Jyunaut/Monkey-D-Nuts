using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Box : Interactible
{
    private Rigidbody2D Rigidbody2D;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.isKinematic = true;
    }

    public override void DoBehaviour()
    {
        Rigidbody2D.MovePosition(Actor.GetComponent<Rigidbody2D>().position);
    }

    public override void Stop()
    {
        base.Stop();
        Rigidbody2D.velocity = Vector2.zero;
    }
}
