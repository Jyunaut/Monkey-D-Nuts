using System.Collections;
using System.Collections.Generic;
using Player;
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
        transform.parent = null;
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        actorPosition = player.GetComponent<Rigidbody2D>().position;
        Vector2 curPosition = new Vector2(offset.x + actorPosition.x, offset.y + actorPosition.y);
        Rigidbody2D.MovePosition(curPosition);
    }

    // public override void Interact()
    // {
    //     if(action != null && player.HeldItem != null) 
    //     { 
    //         Stop(); // Drop box
            
    //     }
    //     else if(player.HeldItem == null)
    //     { 
    //         Debug.Log(1);
    //         base.Interact(); 
    //     }
    // }

    public override void Stop()
    {
        base.Stop();
        Rigidbody2D.MovePosition(new Vector2(actorPosition.x, actorPosition.y -0.61f));
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        // Actor.SetSpeedMultiplier(0.30f); // NOTE: slowing the player does not make sense since there is nothing to exploit the vulnerableness
    }
    protected override void OnExit()
    {
        base.OnExit();
        // Actor.ResetSpeedMultiplier(); // NOTE: slowing the player does not make sense since there is nothing to exploit the vulnerableness
        Rigidbody2D.velocity = Vector2.zero;
    }
}
