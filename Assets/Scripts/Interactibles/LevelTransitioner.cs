using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : ButtonReceiver
{
    public override void Trigger()
    {
        GameEvent.CompleteLevel();
        EffectsManager.Instance.ScreenShake(0f, 3f, 90f, 5f, 8f);
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}