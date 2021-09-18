using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

public abstract class Interactible : MonoBehaviour
{
    public Controller Actor;
    public Action Action;

    protected virtual void Awake() { Action = null; }

    protected virtual void Update() { if(Action != null) Action(); }

    public virtual void Interact(Controller actor)
    {
        Actor = actor;
        Action = DoBehaviour;
        OnEnter();
    }

    public virtual void Stop() 
    {
        Action = null; 
        OnExit();
    }

    public virtual void DoBehaviour() { }
    protected virtual void OnExit() { }
    protected virtual void OnEnter() { }
}
