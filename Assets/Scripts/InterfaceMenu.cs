using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceMenu : MonoBehaviour
{
    public GameObject panelOpt;

    private void Start()
    {
        CloseOption(); 
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenOptions()
    {
        panelOpt.SetActive(true);
    }

    public void CloseOption()
    {
        panelOpt.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
