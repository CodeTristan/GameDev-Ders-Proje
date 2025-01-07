using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.InputSystem;

[System.Serializable]
public class GameData
{
    public string DataName;
    //public ExportableFile DialogFile;
    public BackgroundDialog[] AllBackgroundDialogs;
    public Place[] AllPlaces;
    public Condition[] AllConditions;
    public Mission[] AllMissions;

    public int DayCount;
    public int DayIndex;
    public int GossipIndex;

    public Place currentPlace;

    public string PlayerName;

    public GameData()
    { }
    public GameData(GameData gameData)
    {
        PlayerName = gameData.PlayerName;
        currentPlace = gameData.currentPlace;
        GossipIndex = gameData.GossipIndex;
        DayIndex = gameData.DayIndex;
        DayCount = gameData.DayCount;

        AllBackgroundDialogs = (BackgroundDialog[])gameData.AllBackgroundDialogs.ToArray().Clone();
        AllPlaces = (Place[])gameData.AllPlaces.ToArray().Clone();
        AllConditions = (Condition[])gameData.AllConditions.ToArray().Clone();
        AllMissions = (Mission[])gameData.AllMissions.ToArray().Clone();

    }

    public GameData Clone()
    {
        string jsonString = JsonUtility.ToJson(this);
        GameData gameData = JsonUtility.FromJson<GameData>(jsonString);
        return gameData;

    }
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Header("UI")]
    [SerializeField] private Canvas SaveCanvas;
    [SerializeField] private TextMeshProUGUI Header;
    [SerializeField] private GameObject AreYouSureObject;
    [SerializeField] private TextMeshProUGUI AreYouSureText;
    [SerializeField] private Button AreYouSureYesButton;
    [SerializeField] private SaveSlot[] saveSlots;
    

    public int SavePage = 1;
    private SaveSlotFile saveSlotFile;

    [Header("Game Data")]
    [SerializeField] private Texture2D DefaultSaveImage;
    public GameData CurrentGameData;
    public GameData StartGameData;

    private string savePath;    
    public void Init()
    {
        instance = this;
        savePath = Application.persistentDataPath;

    }

    
}
