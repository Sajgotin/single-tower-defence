using UnityEngine;

public static class SaveSystem
{
    private static string BOOL = "BOOL";

    public static void SaveData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static void SaveData(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public static void SaveData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static void SaveData(string key, bool value)
    {
        if (value)
        {
            SaveData(key + BOOL, 1);
        }
        else
        {
            SaveData(key + BOOL, 0);
        }
    }

    /// <summary>
    /// Load data from memory.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="variable">Pass variable to recognize data type and return default value if no key found.</param>
    /// <returns></returns>
    public static dynamic LoadData(string key, dynamic variable) 
    {
        if(PlayerPrefs.HasKey(key + BOOL))
        {
            if (PlayerPrefs.GetInt(key + BOOL) == 1) return true;
            else return false;
        }

        if (PlayerPrefs.HasKey(key))
        {
            if(variable is int) return PlayerPrefs.GetInt(key);
            if(variable is float) return PlayerPrefs.GetFloat(key);
            if(variable is string) return PlayerPrefs.GetString(key);
        }

        Debug.LogWarning("No data found for key: " + key + ". Ignore if the game started for the first time.");
        return variable;
    }
}
