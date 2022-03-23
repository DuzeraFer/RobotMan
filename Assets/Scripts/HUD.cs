using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textTime;
    public float timerSec;
    public int timerMin;
    public GameObject panelMenu;

    // Start is called before the first frame update
    void Start()
    {
        panelMenu.SetActive(false);
        timerSec = 00;
        timerMin = 00;
    }

    // Update is called once per frame
    void Update()
    {
        textCoin.text = "= " + Player.coins.ToString();

        timerSec += Time.deltaTime;

        if (timerSec > 60)
        {
            timerSec = 0;
            timerMin += 1;
        }

        if (timerMin < 10)
        {
            if (timerSec < 10)
            {
                textTime.text = "0" + timerMin + ":0" + (int)timerSec;
            }
            else
                textTime.text = "0" + timerMin + ":" + (int)timerSec;
        }
        else
            textTime.text = timerMin + ":" + (int)timerSec;
    }

    public void ActivePanelMenu()
    {
        panelMenu.SetActive(!panelMenu.activeSelf);

        if (panelMenu.activeSelf == true)
        {
            Time.timeScale = 00;
        }
        else
        {
            Time.timeScale = 01;
        }
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void MoveButton()
    {
        Player.move = !Player.move;
    }
}
