using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }
}
