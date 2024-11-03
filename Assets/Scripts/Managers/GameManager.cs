
using System.Collections.Generic;

/// <summary>
/// Manages the global game variables.
/// </summary>
public static class GameManager
{

    /// <summary>
    /// Current state of the game.
    /// </summary>
    public static GameState gameState = GameState.MENU;

    /// <summary>
    /// The last score the player got.
    /// </summary>
    public static int lastScore = 0;

/// <summary>
/// The current level the player is on.
/// </summary>
    public static int currentLevel = 1;

/// <summary>
/// The initial data for each level. This will be overwritten when the game is saved or loaded.
/// </summary>
    public static LevelData[] levels = new LevelData[]
    {
        new LevelData(){level = 1, score = 0, unlocked = true},
        new LevelData(){level = 2, score = 0, unlocked = false},
        new LevelData(){level = 3, score = 0, unlocked = false},
    };
}
