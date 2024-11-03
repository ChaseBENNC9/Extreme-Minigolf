using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
/// <summary>
/// MenuManager class is used to manage the menu
/// </summary>
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
        if(SaveGame.Load() != null) //Check if save file exists
        {
            Debug.Log("Save file found");
           GameManager.levels = SaveGame.Load(); //Load save file
           Debug.Log("existing data found: " + GameManager.levels[0].level + " " + GameManager.levels[0].score);
        }
        else //The Save file does not exist or was corrupted
        {
            Debug.Log("No save file found");
            SaveGame.Save(GameManager.levels); //Create new save file with default data
            Debug.Log("new data created: " + GameManager.levels[0].level + " " + GameManager.levels[0].score);
        }

    }

    /// <summary>
    /// Disables the input system so UI does not interfere with the game
    /// </summary>
    /// <param name="eventSystem"></param>
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
/// <summary>
/// Select a level to play
/// </summary>
/// <param name="level"></param>

    public void LevelSelect(int level)
    {
        StartLevel(level);
    }
/// <summary>
/// load the next level
/// </summary>
    public void NextLevel()
    {
        StartLevel(SaveGame.GetCurrentLevel() + 1); //The method should catch an invalid level and return to menu
    }
 
/// <summary>
///    Restart the current level
/// </summary>
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
/// <summary>
/// Validate the level name to ensure it exists
/// </summary>
/// <param name="levelName"></param>
/// <returns></returns>

    private bool ValidateLevel(string levelName)
    {
       return SceneUtility.GetBuildIndexByScenePath(levelName) != -1;
    }

/// <summary>
/// Start the level
/// </summary>
/// <param name="level"></param>
    private void StartLevel(int level)
    {
        GameManager.gameState = GameState.IN_GAME;
       if(ValidateLevel("Level " + level)) //Check if the level exists
        {
            SceneManager.LoadScene("Level " + level);
            SaveGame.UnlockLevel(level);
        }
        else //If the level does not exist, return to the main menu
        {
            Debug.LogError("Level not found");
            MainMenu();
        }
    }



}
