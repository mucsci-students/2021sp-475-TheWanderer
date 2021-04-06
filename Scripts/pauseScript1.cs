using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class pauseScript1 : MonoBehaviour
{
    public GameObject PauseMenu;
    private bool pausedGame = false;

    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausedGame == false)
            {
                PauseGame();
                pausedGame = true;
            }
            else
            {
                ResumeGame();
                pausedGame = false;
            }
        }
    }

    void PauseGame()
    {
    
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartScreen()    
    {        
        ResumeGame();
        SceneManager.LoadScene("StartScreen"); 
        Time.timeScale = 1;
        
    }

    public void Retry(string SceneName)    
    {   
        Time.timeScale = 1;
        ResumeGame();     
        SceneManager.LoadScene(SceneName); 
        
    }

    public void Exit()    
    {        
        ResumeGame();
        print("Okay exiting...");
        Application.Quit();
    }
}
