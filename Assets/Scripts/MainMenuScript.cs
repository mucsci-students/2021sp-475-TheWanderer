using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    #region Menu Dialogs
    [Header("Menu Components")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private GameObject LevelMenu;
    [SerializeField] private GameObject CharacterMenu;

    [SerializeField] private GameObject Level_2;
    [SerializeField] private GameObject Level_3;
    [SerializeField] private GameObject Level_4;
    [SerializeField] private GameObject Level_5;
    [SerializeField] private GameObject Level_6;
    [SerializeField] private GameObject Level_7;
    [SerializeField] private GameObject Level_8;
    [SerializeField] private GameObject Level_9;
    [SerializeField] private GameObject Level_10;

    [SerializeField] private GameObject kangi_1;
    [SerializeField] private GameObject kangi_2;
    [SerializeField] private GameObject kangi_3;
    [SerializeField] private GameObject kangi_4;
    [SerializeField] private GameObject kangi_5;
    [SerializeField] private GameObject kangi_6;
    [SerializeField] private GameObject kangi_7;
    [SerializeField] private GameObject kangi_8;
    [SerializeField] private GameObject kangi_9;
    [SerializeField] private GameObject kangi_10;

    [SerializeField] private GameObject archerBlank;
    [SerializeField] private GameObject archer;
    [SerializeField] private GameObject unknownSoldierBlank;
    [SerializeField] private GameObject unknownSoldier;
    #endregion

    private int check2, check3, check4, check5, check6, check7, check8, check9, check10, checkGame;
    private int unlock1, unlock2;

    void Start()
    {
        check2 = PlayerPrefs.GetInt("Level-2");
        check3 = PlayerPrefs.GetInt("Level-3");
        check4 = PlayerPrefs.GetInt("Level-4");
        check5 = PlayerPrefs.GetInt("Level-5");
        check6 = PlayerPrefs.GetInt("Level-6");
        check7 = PlayerPrefs.GetInt("Level-7");
        check8 = PlayerPrefs.GetInt("Level-8");
        check9 = PlayerPrefs.GetInt("Level-9");
        check10 = PlayerPrefs.GetInt("Level-10");
        checkGame = PlayerPrefs.GetInt("GameDone");

        unlock1 = PlayerPrefs.GetInt("archer_Unlock");
        unlock2 = PlayerPrefs.GetInt("unknownSoldier_Unlock");
    }
    // Start is called before the first frame update
    void Update()
    { 
        if(Input.GetKey(KeyCode.Escape))
        {
            if(OptionsMenu.gameObject.activeInHierarchy == true)
            {
                OptionsMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
            else if(LevelMenu.gameObject.activeInHierarchy == true)
            {
                LevelMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
            else if (CharacterMenu.gameObject.activeInHierarchy == true)
            {
                CharacterMenu.SetActive(false);
                LevelMenu.SetActive(true);
            }
        }

        checkCompletion();
    }

    public void checkCompletion()
    {
    
        if(check2 == 1)
        {
            Level_2.SetActive(true);
            kangi_1.SetActive(true);
        }

        if(check3 == 1)
        {
            Level_3.SetActive(true);
            kangi_2.SetActive(true);
        }

        if(check4 == 1)
        {
            Level_4.SetActive(true);
            kangi_3.SetActive(true);
        }

        if(check5 == 1)
        {
            Level_5.SetActive(true);
            kangi_4.SetActive(true);
            archerBlank.SetActive(false);
            archer.SetActive(true);
        }
        
        if(check6 == 1)
        {
            Level_6.SetActive(true);
            kangi_5.SetActive(true);
        }

        if(check7 == 1)
        {
            Level_7.SetActive(true);
            kangi_6.SetActive(true);
        }

        if(check8 == 1)
        {
            Level_8.SetActive(true);
            kangi_7.SetActive(true);
            unknownSoldierBlank.SetActive(false);
            unknownSoldier.SetActive(true);

        }
        if(check9 == 1)
        {
            Level_9.SetActive(true);
            kangi_8.SetActive(true);
        }

        if(check10 == 1)
        {
            Level_10.SetActive(true);
            kangi_9.SetActive(true);
        }

        if(checkGame == 1)
        {
            kangi_10.SetActive(true);
        }
    }

    public void ChangeToScene(string sceneName)    
    {        
        SceneManager.LoadScene(sceneName); 
    }

    // Update is called once per frame
    public void MouseClick(string buttonType)
    {
        // Exit Application
        if (buttonType == "exit")
        {
            Application.Quit();
        }

        // Brnig up a help menu
        if (buttonType == "options")
        {
            if (OptionsMenu.gameObject.activeInHierarchy == false)
            {
                OptionsMenu.SetActive(true);
                MainMenu.SetActive(false);
            }
        }

        if (buttonType == "options")
        {
            if (OptionsMenu.gameObject.activeInHierarchy == false)
            {
                OptionsMenu.SetActive(true);
                MainMenu.SetActive(false);
            }
        }
        if (buttonType == "character")
        {
            if (CharacterMenu.gameObject.activeInHierarchy == false)
            {
                CharacterMenu.SetActive(true);
                LevelMenu.SetActive(false);
            }
        }

        // Brnig up a level menu
        if (buttonType == "start")
        {
            if (LevelMenu.gameObject.activeInHierarchy == false)
            {
                LevelMenu.SetActive(true);
                MainMenu.SetActive(false);
            }
        }

        // Escape button for the sub-menus
        // Works like if the escape button on the keyboard was pressed
        if (buttonType == "Back")
        {
            if (LevelMenu.gameObject.activeInHierarchy == true || OptionsMenu.gameObject.activeInHierarchy == true)
            {
                OptionsMenu.SetActive(false);
                LevelMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
        }

        if (buttonType == "return")
        {
            if (CharacterMenu.gameObject.activeInHierarchy == true)
            {
                CharacterMenu.SetActive(false);
                LevelMenu.SetActive(true);
            }
        }

        if(buttonType == "level-1")
        {
            ChangeToScene("level 1");
        }
        if(buttonType == "level-2")
        {
            ChangeToScene("level 2");
        }
        if(buttonType == "level-3")
        {
            ChangeToScene("level 3");
        }
        if(buttonType == "level-4")
        {
            ChangeToScene("level 4");
        }
        if(buttonType == "level-5")
        {
            ChangeToScene("level 5");
        }
        if(buttonType == "level-6")
        {
            ChangeToScene("level 6");
        }
        if(buttonType == "level-7")
        {
            ChangeToScene("level 7");
        }
        if(buttonType == "level-8")
        {
            ChangeToScene("level 8");
        }
        if(buttonType == "level-9")
        {
            ChangeToScene("level 9");
        }
        if(buttonType == "level-10")
        {
            ChangeToScene("level 10");
        }

        if(buttonType == "wanderer")
        {
            PlayerPrefs.SetInt("wandererSelect", 1);
            PlayerPrefs.SetInt("archerSelect", 0);
            PlayerPrefs.SetInt("unknownSoldierSelect", 0);
        }
        if(buttonType == "archer")
        {
            PlayerPrefs.SetInt("archerSelect", 1);
            PlayerPrefs.SetInt("wandererSelect", 0);
            PlayerPrefs.SetInt("unknownSoldierSelect", 0);
        }
        if(buttonType == "unknownSoldier")
        {
            PlayerPrefs.SetInt("unknownSoldierSelect", 1);
            PlayerPrefs.SetInt("wandererSelect", 0);
            PlayerPrefs.SetInt("archerSelect", 0);
        }
        PlayerPrefs.Save();
    }

}
