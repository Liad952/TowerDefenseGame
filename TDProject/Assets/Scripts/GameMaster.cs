using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject endCanvas;
    public GameObject uiCanvas;  

    public static int playerHealth = 10;
    public Text waves;
    public static int tempWaveNum;
    public static bool GameOver;


    private void Start()
    {
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }

        if (playerHealth <= 0)
        {
            EndGame();
        }

        waves.text = tempWaveNum.ToString();

    }

    


    private void EndGame()
    {
        GameOver = true;
        uiCanvas.SetActive(false);
        endCanvas.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
