using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject options, saveSlots;

    public void StartBtn()
    {
        if (!saveSlots.activeSelf)
        {
            saveSlots.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void BackBtn()
    {
        gameObject.SetActive(true);
        if (saveSlots.activeSelf)
            saveSlots.SetActive(false);
        if (options.activeSelf)
            options.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        if (!options.activeSelf)
        {
            options.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
