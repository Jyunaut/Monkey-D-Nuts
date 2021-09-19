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

    private void OnEnable()
    {
        GameEvent.OnLevelComplete += RefreshLmao;
    }

    private void OnDisable()
    {
        GameEvent.OnLevelComplete -= RefreshLmao;
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        Debug.Log(_doTransitionLevel);
        if(_doTransitionLevel != null)
            _doTransitionLevel();
    }

    private void TransitionLevel()
    {
        _doTransitionLevel = null;
        GameEvent.CompleteLevel();
        EffectsManager.Instance.ScreenShake(0f, 3f, 90f, 5f, 8f);
        Stop();
    }

    private void RefreshLmao()
    {
        _doTransitionLevel = TransitionLevel;
    }
}