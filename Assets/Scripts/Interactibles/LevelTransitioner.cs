using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelTransitioner : Interactible
{
    private Action _doTransitionLevel;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _doTransitionLevel = TransitionLevel;
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        if(_doTransitionLevel != null)
            _doTransitionLevel();
    }

    private void TransitionLevel()
    {
        GameEvent.CompleteLevel();
        EffectsManager.Instance.ScreenShake(0f, 3f, 90f, 5f, 8f);
        _doTransitionLevel = null;
        Stop();
    }
}