using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCon : MonoBehaviour
{
    public bool IsPaused = false;
    public Text PausedText;

    private void Start()
    {
        PausedText.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        PauseSc();
    }

    void PauseSc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPaused)
            {
                Time.timeScale = 0;
                PausedText.gameObject.SetActive(true);
                IsPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                PausedText.gameObject.SetActive(false);
                IsPaused = false;
            }
        }
    }
}
