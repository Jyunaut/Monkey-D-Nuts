using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image _transitionScreen;
    [SerializeField] private float _transitionTime;

    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        _transitionScreen.rectTransform.sizeDelta = new Vector2(Screen.width + 20f, Screen.height + 20f);
        _transitionScreen.gameObject.SetActive(false);
    }

    public static void LoadScene(int index)
    {
        Instance.StartCoroutine(Instance.SceneTransition(index));
    }

    private IEnumerator SceneTransition(int index)
    {
        _transitionScreen.gameObject.SetActive(true);

        for (float i = 0f; i < 1f; i += Time.deltaTime / _transitionTime)
        {
            _transitionScreen.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, i));
            yield return null;
        }

        AsyncOperation ao = SceneManager.LoadSceneAsync(index);

        while (!ao.isDone)
            yield return null;

        for (float i = 0f; i < 1f; i += Time.deltaTime / _transitionTime)
        {
            _transitionScreen.color = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, i));
            yield return null;
        }

        _transitionScreen.gameObject.SetActive(false);
    }
}