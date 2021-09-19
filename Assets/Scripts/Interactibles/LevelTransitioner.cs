using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : Interactible
{
    public override void DoBehaviour()
    {
        base.DoBehaviour();
        GameEvent.CompleteLevel();
        EffectsManager.Instance.ScreenShake(0f, 3f, 90f, 5f, 8f);
        Stop();
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}