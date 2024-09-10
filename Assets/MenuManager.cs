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
    


    public void StartGame()
    {
        GameManager.gameState = GameState.IN_GAME;
        SceneManager.LoadScene("Level");
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



}
