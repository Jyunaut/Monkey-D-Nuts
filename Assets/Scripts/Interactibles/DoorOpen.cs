using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorOpen : Interactible
{
    private BoxCollider2D _boxCollider2D;
    public GameObject[] Switches;
    private SpriteRenderer _spriteRenderer;

    protected override void Awake()
    {
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        _boxCollider2D.enabled = !CheckSwitches();
        if (!_boxCollider2D.enabled)
            _spriteRenderer.material.color = new Color(_spriteRenderer.material.color.r,
                                                    _spriteRenderer.material.color.g,
                                                    _spriteRenderer.material.color.b,
                                                    0.5f);
        else
            _spriteRenderer.material.color = new Color(_spriteRenderer.material.color.r,
                                                    _spriteRenderer.material.color.g,
                                                    _spriteRenderer.material.color.b,
                                                    1f);
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
        Debug.Log("alpha up");
        Color newColor = this.GetComponent<SpriteRenderer>().material.color;
        newColor.a = 0.5f;
        this.GetComponent<SpriteRenderer>().material.color = newColor;
    }
    public override void Stop()
    {
        base.Stop();
        _boxCollider2D.enabled = true;
        Debug.Log("alpha down");
        Color newColor = this.GetComponent<SpriteRenderer>().material.color;
        newColor.a = 1f;
        this.GetComponent<SpriteRenderer>().material.color = newColor;
    }
}
