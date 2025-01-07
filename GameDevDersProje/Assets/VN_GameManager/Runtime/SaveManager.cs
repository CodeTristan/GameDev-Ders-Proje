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

        //Setting StartData
        SetGameData(StartGameData);
        saveSlotFile = new SaveSlotFile();
        LoadSaveSlots();

        _ChangeSavePage(1);
        _CloseSaveCanvas();
    }

    private void LoadSaveSlots()
    {
        if (File.Exists(savePath + "/SaveSlot.sav") == false)
        {
            Debug.Log("SaveSlot file not found");
            return;
        }

        var binaryFormatter = new BinaryFormatter();
        var fileStream = File.Open(savePath + "/SaveSlot.sav", FileMode.Open);
        saveSlotFile = (SaveSlotFile)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();

    }

    public void _ChangeSavePage(int page)
    {
        SavePage = page;
        int index = (page - 1) * 6;
        for (int i = 0; i < saveSlots.Length; i++)
        {
            saveSlots[i].OuterText.text = "Save " + (index +i+ 1);
            saveSlots[i].DayText.text = saveSlotFile.SaveSlots[index + i].DayText;
            saveSlots[i].GoldText.text = saveSlotFile.SaveSlots[index + i].GoldText;

            if (saveSlotFile.SaveSlots[index + i].isEmpty)
            {
                Sprite sprite = Sprite.Create(DefaultSaveImage, new Rect(0, 0, DefaultSaveImage.width, DefaultSaveImage.height), new Vector2(0.5f, 0.5f));
                saveSlots[i].image.sprite = sprite;
                saveSlots[i].image.color = new Color32(113,113,113,255);
            }
            else
            {
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(saveSlotFile.SaveSlots[index + i].ImageData);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                saveSlots[i].image.sprite = sprite;
                saveSlots[i].image.color = new Color32(255, 255, 255, 255);
            }

        }
    }
    public void ToggleSaveCanvas(InputAction.CallbackContext context)
    {
        if(SaveCanvas.enabled)
            _CloseSaveCanvas();
    }
    public void _OpenSaveCanvas()
    {
        SaveCanvas.enabled = true;
        Header.text = "SAVE";
        for (int i = 0; i < saveSlots.Length; i++)
        {
            int index = i;
            saveSlots[i].button.onClick.RemoveAllListeners();
            saveSlots[i].button.onClick.AddListener(() => _SaveGame(index));
        }
    }

    public void _OpenLoadCanvas()
    {
        SaveCanvas.enabled = true;
        Header.text = "LOAD";
        for (int i = 0; i < saveSlots.Length; i++)
        {
            int index = i;
            saveSlots[i].button.onClick.RemoveAllListeners();
            saveSlots[i].button.onClick.AddListener(() => _Load(index));
        }
    }

    public void _CloseSaveCanvas() 
    {
        SaveCanvas.enabled = false; 
    }

    public void _SaveGame(int SlotIndex)
    {
        int FileIndex = (SavePage - 1) * 6;
        FileIndex += SlotIndex;

        if (saveSlotFile.SaveSlots[FileIndex].isEmpty)
        {
            SetGameData(CurrentGameData);
            SetSlotData(CurrentGameData,SlotIndex, FileIndex);
            StartCoroutine(WriteToFile(CurrentGameData,FileIndex));
        }
        else
        {
            AreYouSureObject.SetActive(true);
            AreYouSureText.text = "Are you sure you want to override this save file?";
            AreYouSureYesButton.onClick.RemoveAllListeners();
            AreYouSureYesButton.onClick.AddListener(() => SaveGameOverride(SlotIndex,FileIndex));
        }
    }
    public void _Load(int SlotIndex)
    {
        int FileIndex = (SavePage - 1) * 6;
        FileIndex += SlotIndex;

        if (saveSlotFile.SaveSlots[FileIndex].isEmpty)
            return;

        AreYouSureObject.SetActive(true);
        AreYouSureText.text = "Do you want to load\nSave " + (FileIndex + 1) + "?";
        AreYouSureYesButton.onClick.RemoveAllListeners();
        AreYouSureYesButton.onClick.AddListener(() => Load(FileIndex));

        AreYouSureYesButton.onClick.AddListener(() => SahneManager.instance.LoadScene("MainScene",true));
        AreYouSureYesButton.onClick.AddListener(() => BackgroundManager.instance.ToggleGeneralCanvas(true));

    }

    private void SaveGameOverride(int SlotIndex,int FileIndex)
    {
        AreYouSureObject.SetActive(false);
        SetGameData(CurrentGameData);
        SetSlotData(CurrentGameData, SlotIndex, FileIndex);
        StartCoroutine(WriteToFile(CurrentGameData, FileIndex));
    }
    public void Load(int FileIndex)
    {
        if(File.Exists(savePath + "/Save" + FileIndex + ".sav") == false)
        {
            Debug.Log("Save File not found");
            return;
        }

        var binaryFormatter = new BinaryFormatter();
        var fileStream = File.Open(savePath + "/Save" + FileIndex + ".sav", FileMode.Open);
        CurrentGameData = (GameData)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();

        SetLoadData(CurrentGameData);
    }

    public void StartNewGame(GameData gameData)
    {
        CurrentGameData = gameData.Clone();
        CurrentGameData.DataName = "New Data";
        SetLoadData(CurrentGameData);
    }

    private void SetLoadData(GameData gameData)
    {

        GameManager.instance.allBackgroundDialogs.backgroundDialogs = gameData.AllBackgroundDialogs.ToList();
        GameManager.instance.allConditions.conditions = gameData.AllConditions.ToList();
        GameManager.instance.allMissions = gameData.AllMissions.ToList();
        GameManager.instance.allPlaces.places = gameData.AllPlaces.ToList();

        BackgroundManager.instance.SetBackgroundDialogs();
        ConditionManager.instance.conditions = GameManager.instance.allConditions.conditions;

        BackgroundManager.instance.currentPlace = gameData.currentPlace;

        DialogManager.instance.PlayerName = gameData.PlayerName;
        //DialogManager.instance.File = gameData.DialogFile;

        BackgroundManager.instance.ChangeBackground(BackgroundManager.instance.currentPlace, false);
        Debug.Log(CurrentGameData.DayCount);

        AreYouSureObject.SetActive(false);
        _CloseSaveCanvas();
    }
    private IEnumerator WriteToFile(GameData gameData,int FileIndex)
    {
        var binaryFormatter = new BinaryFormatter();
        var fileStream = File.Create(savePath + "/Save" + FileIndex + ".sav");

        _CloseSaveCanvas();
        yield return new WaitForEndOfFrame();
        Texture2D texture = ScreenCapture.CaptureScreenshotAsTexture();
        _OpenSaveCanvas();

        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();

        //SaveSlot file formatter
        fileStream = File.Create(savePath + "/SaveSlot.sav");
        saveSlotFile.SaveSlots[FileIndex].ImageData = texture.EncodeToPNG();
        saveSlotFile.SaveSlots[FileIndex].isEmpty = false;
        binaryFormatter.Serialize(fileStream, saveSlotFile);
        fileStream.Close();

        _ChangeSavePage(SavePage);
        Destroy(texture);
        Debug.Log("Saved to: " + Application.persistentDataPath);
    }

    public void SetGameData(GameData gameData)
    {
        gameData.AllBackgroundDialogs = (BackgroundDialog[])GameManager.instance.allBackgroundDialogs.backgroundDialogs.ToArray().Clone();
        gameData.AllConditions = (Condition[])GameManager.instance.allConditions.conditions.ToArray().Clone();
        gameData.AllPlaces = (Place[])GameManager.instance.allPlaces.places.ToArray().Clone();
        gameData.AllMissions = (Mission[])GameManager.instance.allMissions.ToArray().Clone();

        if(gameData != StartGameData)
        {
            gameData.currentPlace = BackgroundManager.instance.currentPlace;
            gameData.DataName = "Current Data";
        }
        else
        {
            gameData.DayCount = 1;
            gameData.DayIndex = 0;
            gameData.GossipIndex = 0;
            gameData.DataName = "Start Data";

        }

        gameData.PlayerName = DialogManager.instance.PlayerName;
        //gameData.DialogFile = DialogManager.instance.File.Clone();

    }

    private void SetSlotData(GameData gameData,int slotIndex,int fileIndex)
    {
        saveSlotFile.SaveSlots[fileIndex].isEmpty = false;
        saveSlotFile.SaveSlots[fileIndex].DayText = "";

        //image data is set on WriteToFile function
    }
}
