using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Interactible : MonoBehaviour
{
    public GameObject Actor;
    public Action Action;

    protected virtual void Awake() { Action = null; }

    protected virtual void Update()
    {
        if(Action != null) Action();
    }

    public virtual void Interact(GameObject actor)
    {
        Actor = actor;
        Action = DoBehaviour;
    }

    public virtual void OnEnter() { }

    public virtual void OnExit() { Stop(); }

    public virtual void DoBehaviour() { }

    public virtual void Stop() { Action = null; }
}
