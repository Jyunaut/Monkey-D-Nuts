using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitioner : ButtonReceiver
{
    public override void Trigger()
    {
        GameEvent.CompleteLevel();
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}