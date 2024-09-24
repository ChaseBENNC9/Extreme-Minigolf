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
public class SaveGame
{


    public static void Save(LevelData[] levels)
    {

        string path = Application.persistentDataPath + "/saveData.txt";
        Debug.Log("path: " + path);

            string json = JsonHelper.ToJson(levels, true);
            File.WriteAllText(path, json);    
    
 
        
 
        Debug.Log("Saved data: " + File.ReadAllText(path));
   
        
    }


//return list of levels
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

    public static void UnlockLevel(int level)
    {
        Debug.Log("Unlocking level: " + level);
        foreach (LevelData levelData in GameManager.levels)
        {
            if (levelData.level == level)
            {
                levelData.unlocked = true;
            }
        }
        Save(GameManager.levels);
    }

    //iterate through levels and return last unlocked level
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


    public static void PrintLevels()
    {
        foreach (LevelData level in GameManager.levels)
        {
            Debug.Log("Level: " + level.level + " Score: " + level.score);
        }
    }
}
