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
        GameManger.gameState = GameState.IN_GAME;
        SceneManager.LoadScene("Game");
    }
    public void MainMenu()
    {
        GameManger.gameState = GameState.MENU;
        SceneManager.LoadScene("Menu");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("End");
    }



}
