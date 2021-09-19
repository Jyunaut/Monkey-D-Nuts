using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class CameraIntro : MonoBehaviour
{
    public Vector3 CameraStartOffset;
    public float CameraStartSize = 0.05f;
    public float ZoomLerpSpeed = 0.015f;
    public float StartZoomSpeed = 0.015f;
    public float EndZoomSpeed = 0.15f;

    private Controller _player;
    private Camera _mainCamera;
    private Vector3 _cameraEndPosition;
    private float _cameraEndSize;
    private float _curSpeed;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraEndPosition = _mainCamera.gameObject.transform.position;
        _cameraEndSize = _mainCamera.orthographicSize;
        _curSpeed = StartZoomSpeed;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();

        _mainCamera.gameObject.GetComponent<CameraMovement>().enabled = false;
        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Set camera position
        _mainCamera.gameObject.transform.position = new Vector3(player.x + CameraStartOffset.x, player.y + CameraStartOffset.y, _mainCamera.gameObject.transform.position.z);
        _mainCamera.orthographicSize = CameraStartSize;
        // _player.control
    }

    private void Update()
    {
        if(_mainCamera.orthographicSize >= _cameraEndSize - 0.2f)
        {
            _mainCamera.gameObject.GetComponent<CameraMovement>().enabled = true;
            this.gameObject.SetActive(false);
            return;
        }

        _curSpeed = Mathf.Lerp(_curSpeed, EndZoomSpeed, ZoomLerpSpeed * Time.deltaTime);
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _cameraEndSize, _curSpeed * Time.deltaTime);
    }
    
}
