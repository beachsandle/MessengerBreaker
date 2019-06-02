using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject panel;

    bool isPause = false;

    void Start()
    {
        OffPanel();
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
        Time.timeScale = 0;

        if (panel != null)
        {
            panel.SetActive(true);
            isPause = true;
        }
    }

    void OffPanel()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            isPause = false;
        }

        Time.timeScale = 1;
    }

    public void OnPauseBtn()
    {
        isPause = !isPause;

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