using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

public abstract class Interactible : MonoBehaviour
{
    public Controller Actor;
    public Action Action;
    public bool IsActive = false;
    public bool CanInteract = true;

    protected virtual void Awake() { Action = null; }

    protected virtual void Update() { if(Action != null) Action(); }

    public virtual void Interact(Controller actor)
    {
        Actor = actor;
        OnEnter();
    }

    public virtual void Stop() 
    {
        Action = null; 
        OnExit();
    }

    public virtual void DoBehaviour() { IsActive = true; }
    protected virtual void OnEnter() { Action = DoBehaviour; }
    protected virtual void OnExit() { IsActive = false; }
}
