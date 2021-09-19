using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Image _transitionScreen;
    [SerializeField] private float _transitionTime;
    [field: SerializeField] public List<GameObject> Maps { get; private set; }

    public static GameManager Instance { get; private set; }

    public int CurrentMapIndex { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
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

        Maps[CurrentMapIndex++].SetActive(false);
        Maps[CurrentMapIndex].SetActive(true);

        Player.Controller.ControlsEnabled = true;
        yield return null;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}