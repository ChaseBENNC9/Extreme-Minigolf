using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static MenuManager i;
    void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        if(SaveGame.Load() != null)
        {
            Debug.Log("Save file found");
           GameManager.levels = SaveGame.Load();
           Debug.Log("existing data found: " + GameManager.levels[0].level + " " + GameManager.levels[0].score);
        }
        else
        {
            Debug.Log("No save file found");
            SaveGame.Save(GameManager.levels);
            Debug.Log("new data created: " + GameManager.levels[0].level + " " + GameManager.levels[0].score);
        }

    }
    private void ReadScore()
    {
        
    }

    public void  DisableEvents(EventSystem eventSystem)
    {
       eventSystem.GetComponent<InputSystemUIInputModule>().DeactivateModule ();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    

/// <summary>
/// Start new game
/// </summary>
    public void StartGame()
    {
        GameManager.gameState = GameState.IN_GAME;
        StartLevel(SaveGame.GetCurrentLevel());
    }


    public void LevelSelect(int level)
    {
        StartLevel(level);
    }
    public void NextLevel()
    {
        StartLevel(SaveGame.GetCurrentLevel() + 1); //The method should catch an invalid level and return to menu
    }
 

    public void RestartLevel()
    {
        StartLevel(GameManager.currentLevel);
    }



    public void MainMenu()
    {
        GameManager.gameState = GameState.MENU;
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }

    public void Scores()
    {
        SceneManager.LoadScene("Scores");
    }

    private void StartLevel(int level)
    {
        GameManager.currentLevel = level;
        GameManager.gameState = GameState.IN_GAME;
       switch(level)
       {
            case 1:
                try
                {
                    SceneManager.LoadScene("Level 1");
                }
                catch
                {
                    Debug.Log("Level not found");
                    SceneManager.LoadScene("MainMenu");
                }   
                break;
            case 2:
                try
                {
                    SceneManager.LoadScene("Level 2");
                    SaveGame.UnlockLevel(2);
                }
                catch
                {
                    Debug.Log("Level not found");
                    SceneManager.LoadScene("MainMenu");
                }   
                break;
            case 3:
                try
                {
                    SceneManager.LoadScene("Level 3");
                    SaveGame.UnlockLevel(3);
                }
                catch
                {
                    Debug.Log("Level not found");
                    SceneManager.LoadScene("MainMenu");
                }   
                break;
            default:
                SceneManager.LoadScene("MainMenu");
                break;
       }
    }



}
