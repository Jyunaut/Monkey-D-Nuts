using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelTransitioner : Interactible
{
    private Action _doTransitionLevel;

    public GameObject Switch;

    protected override void Awake()
    {
        base.Awake();
        _doTransitionLevel = TransitionLevel;
    }

    protected override void Update()
    {
        if(_doTransitionLevel != null && Switch && Switch.GetComponent<Interactible>().IsActive)
        {
            Destroy(Switch);
            _doTransitionLevel();
        }
    }

    private void OnEnable()
    {
        GameEvent.OnLevelComplete += RefreshLmao;
    }

    private void OnDisable()
    {
        GameEvent.OnLevelComplete -= RefreshLmao;
    }

    private void TransitionLevel()
    {
        EffectsManager.Instance.ShakeSound();
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