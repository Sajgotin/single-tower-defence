using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    public static Points Instance { get; private set; }
    public int points;

    private void OnEnable()
    {
        if (MainMenu.saveExist) Load();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Save()
    {
        SaveSystem.SaveData(nameof(points), points);
    }

    void Load()
    {
        points = SaveSystem.LoadData(nameof(points), points);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
