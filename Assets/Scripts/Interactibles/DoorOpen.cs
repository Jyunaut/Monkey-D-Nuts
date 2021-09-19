using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorOpen : Interactible
{
    private BoxCollider2D _boxCollider2D;
    public GameObject[] Switches;

    protected override void Awake()
    {
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        base.Update();
        _boxCollider2D.enabled = !CheckSwitches();
    }

    private bool CheckSwitches()
    {
        if(Switches.Length > 0)
            for(int i = 0; i < Switches.Length; i++)
            {
                if(Switches[i].GetComponent<Interactible>().IsActive)
                    return true;
            }
        return false;
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
