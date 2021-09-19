using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    [SerializeField] private Camera _camera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _camera = FindObjectOfType<Camera>();
    }

    private Coroutine _screenShake;
    public void ScreenShake(float xMagnitude, float yMagnitude, float frequency, float duration, float damping)
    {
        if (_screenShake != null)
        {
            StopCoroutine(_screenShake);
            _screenShake = null;
        }
        _screenShake = StartCoroutine(ShakeScreen());

        IEnumerator ShakeScreen()
        {
            float time = 0f;
            while (time < duration)
            {
                float x = xMagnitude * Mathf.Exp(-damping * time) * Mathf.Sin(frequency * time + Mathf.PI * time / duration);
                float y = yMagnitude * Mathf.Exp(-damping * time) * Mathf.Sin(frequency * time - Mathf.PI * time / duration);
                _camera.transform.localPosition = new Vector3(x, y, 0f);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            _camera.transform.localPosition = Vector3.zero;
        }
    }

    public void ShakeSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
