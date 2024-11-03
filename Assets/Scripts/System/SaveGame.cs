using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
  

[System.Serializable]
public class LevelData
{
    public int level;
    public int score;
    public bool unlocked;
}
/// <summary>
/// SaveGame class is used to save and load the game data
/// </summary>
public class SaveGame
{

    /// <summary>
    /// Save the game data
    /// </summary>
    /// <param name="levels">The Array of Level Data to save to the file</param>
    public static void Save(LevelData[] levels)
    {

        string path = Application.persistentDataPath + "/saveData.txt";
        Debug.Log("path: " + path);

            string json = JsonHelper.ToJson(levels, true);
            File.WriteAllText(path, json);    
    
 
        
 
        Debug.Log("Saved data: " + File.ReadAllText(path));
   
        
    }


/// <summary>
/// Load the game data from the file
/// </summary>
/// <returns>The Array of Level Data that was loaded from the file</returns>
    public static LevelData[] Load()
    {
        string path = Application.persistentDataPath + "/saveData.txt";
        Debug.Log("path: " + path);
        if (File.Exists(path))
        {
            if (File.ReadAllText(path) == "" || File.ReadAllText(path) == null || File.ReadAllText(path) == "{}")
            {
                Debug.Log(" a No data found");
                return null;
            }
            string json = File.ReadAllText(path);
        Debug.Log("b load data " + json);


            return JsonHelper.FromJson<LevelData>(json);
        }
        else
        {
            Debug.Log("c No file exists");
            return null;
        }
    }

/// <summary>
/// Update the score of a specific level and save the data
/// </summary>
/// <param name="level">The Level to Overwrite</param>
/// <param name="score">the new score value</param>
    public static void UpdateLevelScore(int level, int score)
    {
        Debug.Log("Updating level: " + level + " with score: " + score);
        foreach (LevelData levelData in GameManager.levels)
        {
            if (levelData.level == level)
            {
                levelData.score = score;
            }
        }
        Save(GameManager.levels);
    }

    /// <summary>
    /// Unlock a specific level and save the data
    /// </summary>
    /// <param name="level"></param>
    public static void UnlockLevel(int level)
    {
        Debug.Log("Unlocking level: " + level);
        foreach (LevelData levelData in GameManager.levels) //iterate through levels until the level is found
        {
            if (levelData.level == level)
            {
                levelData.unlocked = true;
            }
        }
        Save(GameManager.levels);
    }
    /// <summary>
    /// iterate through levels and return last unlocked level
    /// </summary>
    public static int GetCurrentLevel()
    {
        int lastUnlocked = 0;
        foreach (LevelData levelData in GameManager.levels)
        {
            if (levelData.unlocked)
            {
                lastUnlocked = levelData.level;
            }
        }
        return lastUnlocked;
    }

/// <summary>
/// Get the score for a specific level
/// </summary>
/// <param name="level"></param>
/// <returns></returns>
    public static int GetScoreForLevel(int level)
    {
        foreach (LevelData levelData in GameManager.levels)
        {
            if (levelData.level == level)
            {
                return levelData.score;
            }
        }
        return -1;
    }


}
