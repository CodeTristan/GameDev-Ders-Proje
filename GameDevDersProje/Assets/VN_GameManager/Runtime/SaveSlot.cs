using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public TextMeshProUGUI OuterText;
    public TextMeshProUGUI DayText;
    public TextMeshProUGUI GoldText;
    public Image image;
    public Button button;
}

[System.Serializable]
public class SaveSlotSerializeable
{
    public bool isEmpty;
    public string DayText;
    public string GoldText;
    public byte[] ImageData;

    public SaveSlotSerializeable()
    {
        isEmpty = true;
        DayText = "No Data";
        GoldText = "";
    }
}

[System.Serializable]

public class SaveSlotFile
{
    public SaveSlotSerializeable[] SaveSlots;

    public SaveSlotFile()
    {
        SaveSlots = new SaveSlotSerializeable[48];
        for (int i = 0; i < SaveSlots.Length; i++)
        {
            SaveSlots[i] = new SaveSlotSerializeable();
        }
    }
}