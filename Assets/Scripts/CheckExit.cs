using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckExit : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        if(this.name == "Exit") OffPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPanel();
        }
    }

    public void OnPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void OffPanel()
    {
        if(panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void Exit()
    {
        if(this.name == "NoButton")
        {
            OffPanel();
            return;
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("https://github.com/beachsandle/MessengerBreaker");
#else
        Application.Quit();
#endif
    }
}
