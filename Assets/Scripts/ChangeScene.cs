using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ChangeUpgradeScene()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("https://github.com/beachsandle/MessengerBreaker");
#else
        Application.Quit();
#endif
    }
}
