using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using System;

public class CameraIntro : MonoBehaviour
{
    public Vector3 CameraStartOffset;
    public float CameraStartSize = 0.05f;
    public float ZoomLerpSpeed = 0.015f;
    public float StartZoomSpeed = 0.015f;
    public float EndZoomSpeed = 0.15f;

    private Action _doIntro;
    private Controller _player;
    private Camera _mainCamera;
    private Vector3 _cameraEndPosition;
    private float _cameraEndSize;
    private float _curSpeed;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        _mainCamera = Camera.main;
        _cameraEndPosition = _player.transform.position;
        _cameraEndSize = _mainCamera.orthographicSize;
        _curSpeed = StartZoomSpeed;

        // _mainCamera.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = false;
        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Set camera position
        _mainCamera.gameObject.transform.position = new Vector3(player.x + CameraStartOffset.x, player.y + CameraStartOffset.y, _mainCamera.gameObject.transform.position.z);
        _mainCamera.orthographicSize = CameraStartSize;
        Player.Controller.ControlsEnabled = false;
        DoIntro();
    }

    public void DoIntro()
    {
        _doIntro = () =>
        {
            if (_mainCamera.orthographicSize >= _cameraEndSize - 0.3f)
            {
                _doIntro = null;
                _mainCamera.transform.parent.gameObject.GetComponent<CameraMovement>().enabled = true;
                Player.Controller.ControlsEnabled = true;
                this.gameObject.SetActive(false);
                return;
            }

            _curSpeed = Mathf.Lerp(_curSpeed, EndZoomSpeed, ZoomLerpSpeed * Time.deltaTime);
            // _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _cameraEndPosition, _curSpeed);
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _cameraEndSize, _curSpeed * Time.deltaTime);
        };
    }

    private void Update()
    {
        if (_doIntro != null)
            _doIntro();
    }

}
