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

    public void PlayCutscene()
    {
        if (!_triggered)
        {
            Camera.main.GetComponentInParent<CameraMovement>().enabled = false;
            Player.Controller.ControlsEnabled = false;
            _playableDirector.Play();
            _triggered = true;
        }
    }
}
