using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _maps;
    [SerializeField] private Image _transitionScreen;
    [SerializeField] private float _transitionTime;

    public static GameManager Instance { get; private set; }

    public int CurrentMapIndex { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        // _transitionScreen.rectTransform.sizeDelta = new Vector2(Screen.width + 20f, Screen.height + 20f);
        // _transitionScreen.gameObject.SetActive(false);        
    }

    private void OnEnable()
    {
        GameEvent.OnLevelComplete += ChangeLevel;
    }

    private void OnDisable()
    {
        GameEvent.OnLevelComplete -= ChangeLevel;
    }

    private void ChangeLevel()
    {
        StartCoroutine(LevelTransition());
    }

    private IEnumerator LevelTransition()
    {
        Player.Controller.ControlsEnabled = false;
        // _transitionScreen.gameObject.SetActive(true);
        // for (float i = 0f; i < 1f; i += Time.deltaTime / _transitionTime)
        // {
        //     _transitionScreen.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, i));
        //     yield return null;
        // }

        // Do transition stuff
        _maps[CurrentMapIndex++].SetActive(false);
        _maps[CurrentMapIndex].SetActive(true);

        // for (float i = 0f; i < 1f; i += Time.deltaTime / _transitionTime)
        // {
        //     _transitionScreen.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, i));
        //     yield return null;
        // }
        // _transitionScreen.gameObject.SetActive(false);
        Player.Controller.ControlsEnabled = true;
        yield return null;
    }
}