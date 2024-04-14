using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUi.SetActive(true);
            //if (GameIsPaused)
            //{
            //    FollowCam();
            //}
            //else
            //{
            //    Interact();
            //}
        }

    }


   

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
       
        Time.timeScale = 1f;
        SceneManager.LoadScene("Login");

    }



}
