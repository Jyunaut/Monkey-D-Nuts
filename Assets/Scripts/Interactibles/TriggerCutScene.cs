using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TriggerCutScene : Interactible
{
    private PlayableDirector _playableDirector;
    private bool _triggered;

    protected override void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    public override void DoBehaviour()
    {
        base.DoBehaviour();
        if (!_triggered)
        {
            Player.Controller.ControlsEnabled = false;
            _playableDirector.Play();
            _triggered = true;
        }
    }
}
