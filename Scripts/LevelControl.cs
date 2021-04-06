using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelControl : MonoBehaviour
{
    public string SceneName;

    public GameObject Spawner;
    public GameObject wanderer;
    public GameObject archer;
    public GameObject unknownSoldier;

    public GameObject gameover;
    public GameObject healthUI;

    spawn spawner;
    Wanderer wan;
    Archer arc;
    UnknownSoldier us;
    
    private int char1;
    private int char2;
    private int char3;


    void Start()
    {
        char1 = PlayerPrefs.GetInt("wandererSelect");
        char2 = PlayerPrefs.GetInt("archerSelect");
        char3 = PlayerPrefs.GetInt("unknownSoldierSelect");

        if(char1 == 1)
        {
            wanderer.SetActive(true);
            archer.SetActive(false);
            unknownSoldier.SetActive(false);
        }
        else if (char2 == 1)
        {
            archer.SetActive(true);
            wanderer.SetActive(false);
            unknownSoldier.SetActive(false);
        }
        else if(char3 == 1)
        {
            unknownSoldier.SetActive(true);
            wanderer.SetActive(false);
            archer.SetActive(false);
        }

        spawner = Spawner.GetComponent<spawn>();
        wan = wanderer.GetComponent<Wanderer>();
        arc = archer.GetComponent<Archer>();
        us = unknownSoldier.GetComponent<UnknownSoldier>();
         Time.timeScale = 1;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(wan.HealthPoints <= 0 || arc.HealthPoints <= 0 || us. HealthPoints <= 0)
        {
            Debug.Log("You lose");
                
            healthUI.SetActive(false);
            gameover.SetActive(true);
            Time.timeScale = 0;
            
        
        }

        if(SceneName == "Level-1")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
              
                PlayerPrefs.SetInt("Level-2", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-2")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
            
                PlayerPrefs.SetInt("Level-3", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-3")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
             
                PlayerPrefs.SetInt("Level-4", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-4")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
               
                PlayerPrefs.SetInt("Level-5", 1);
                PlayerPrefs.SetInt("archer_Unlock", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-5")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
           
                PlayerPrefs.SetInt("Level-6", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-6")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
             
                PlayerPrefs.SetInt("Level-7", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-7")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
            
                PlayerPrefs.SetInt("Level-8", 1);
                PlayerPrefs.SetInt("unknownSoldier_Unlock", 1);
                PlayerPrefs.Save();
            } 
        }   
        else if(SceneName == "Level-8")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
             
                PlayerPrefs.SetInt("Level-9", 1);
                PlayerPrefs.Save();
            } 
        }
        else if(SceneName == "Level-9")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
           
                PlayerPrefs.SetInt("Level-10", 1);
                PlayerPrefs.Save();
            } 

            if(Input.GetKey(KeyCode.Alpha1))
            {
                wanderer.SetActive(true);
                archer.SetActive(false);
                unknownSoldier.SetActive(false);
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                archer.SetActive(true);
                unknownSoldier.SetActive(false);
                wanderer.SetActive(false);
            }
            else if(Input.GetKey(KeyCode.Alpha3))
            {
                unknownSoldier.SetActive(true);
                archer.SetActive(false);
                wanderer.SetActive(false);
            }
        }
        else if(SceneName == "Level-10")
        {
            if(spawner.finalWave == spawner.waves.Length)
            {
                Debug.Log("Level Completed");
                PlayerPrefs.SetInt("GameDone", 1);
                PlayerPrefs.Save();
            } 

            if(Input.GetKey(KeyCode.Alpha1))
            {
                wanderer.SetActive(true);
                archer.SetActive(false);
                unknownSoldier.SetActive(false);
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                archer.SetActive(true);
                unknownSoldier.SetActive(false);
                wanderer.SetActive(false);
            }
            else if(Input.GetKey(KeyCode.Alpha3))
            {
                unknownSoldier.SetActive(true);
                archer.SetActive(false);
                wanderer.SetActive(false);
            }
        }
        
    }
}
