
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
}
