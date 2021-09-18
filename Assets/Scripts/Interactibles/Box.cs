using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Box : Interactible
{
    public Vector2 offset;
    private Rigidbody2D Rigidbody2D;
    private Vector2 actorPosition;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.isKinematic = true;
    }

    public override void DoBehaviour()
    {
        Vector2 actor = Actor.GetComponent<Rigidbody2D>().position;
        Vector2 curPosition = new Vector2(offset.x + actor.x, offset.y + actor.y);
        Rigidbody2D.MovePosition(curPosition);
    }

    public override void Stop()
    {
        base.Stop();
        Rigidbody2D.velocity = Vector2.zero;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        // Actor.SetSpeedMultiplier(0.5f); // NOTE: slowing the player does not make sense since there is nothing to exploit the vulnerableness
    }
    protected override void OnExit()
    {
        base.OnEnter();
        // Actor.ResetSpeedMultiplier();
    }
}
