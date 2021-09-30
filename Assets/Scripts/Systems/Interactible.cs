using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

public abstract class Interactible : MonoBehaviour
{
    protected Controller player;
    protected Action action;
    public bool IsActive = false;
    public bool CanInteract = true;

    protected virtual void Awake() 
    { 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        action = null; 
    }

    protected virtual void Update() { if(action != null) action(); }

    public virtual void Interact()
    {
        OnEnter();
    }

    public virtual void Stop() 
    {
        action = null; 
        OnExit();
    }

    public virtual void DoBehaviour() { IsActive = true; }
    protected virtual void OnEnter() { action = DoBehaviour; }
    protected virtual void OnExit() { IsActive = false; }
}
