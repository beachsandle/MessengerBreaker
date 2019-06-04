using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject panel;

    bool isPause = false;

    void Start()
    {
        Debug.Log($"Start() in {this.name}");
        if (this.name == "PauseButton") OffPanel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseBtn();
        }
    }

    void OnPanel()
    {
        Debug.Log($"OnPanel() in {this.name}");

        Time.timeScale = 0;

        if (panel != null)
        {
            panel.SetActive(true);
            isPause = true;
        }
    }

    public void OffPanel()
    {
        Debug.Log($"OffPanel() in {this.name}");
        if (panel != null)
        {
            panel.SetActive(false);
            isPause = false;
        }

        Time.timeScale = 1;
    }

    public void OnPauseBtn()
    {
        if (panel.activeSelf != isPause) isPause = panel.activeSelf;

        isPause = !isPause;

        Debug.Log($"OnPauseBtn(){isPause}  in {this.name}");

        if (isPause)
        {
            OnPanel();
        }
        else
        {
            OffPanel();
        }
    }
}