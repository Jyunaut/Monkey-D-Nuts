using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CameraShakeEvent : UnityEvent<float, float, float, float> {}

public class CameraEffect : MonoBehaviour
{
    public CameraShakeEvent cameraShakeEvent;

    public void Start()
    {
        if (cameraShakeEvent == null)
            cameraShakeEvent = new CameraShakeEvent();

        cameraShakeEvent.AddListener(ShakeScreen);
    }

    public void ShakeScreen(float xMagnitude, float yMagnitude, float frequency, float duration)
    {
        EffectsManager.Instance.ScreenShake(xMagnitude, yMagnitude, frequency, duration, 0f);
    }
}
