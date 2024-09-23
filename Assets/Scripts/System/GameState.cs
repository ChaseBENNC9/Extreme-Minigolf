using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages the game stat variables.
/// </summary>
public enum GameState
{
    /// <summary>
    /// The game is in the main menu.
    /// </summary>
    MENU,
    /// <summary>
    /// The player is currently in a level.
    /// </summary>
    IN_GAME,
    /// <summary>
    /// Tge game is over and the player lost.
    /// </summary>
    LOSE,
    /// <summary>
    /// The game is over and the player won.
    /// </summary>
    WIN
 
}
