using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Place
{
    public PlaceName placeName;
    public BackgroundName backgroundName;
    public bool HasBeenVisited;
    public string MusicName;
    public bool isLocked = true;
    public List<DialogTrigger> dialogTriggers;
    public Place(PlaceName placeName,BackgroundName backgroundName,string MusicName,List<DialogTrigger> dialogTriggers,bool isLocked = true)
    {
        this.placeName = placeName;
        this.backgroundName = backgroundName;
        this.MusicName = MusicName;
        this.isLocked = isLocked;
        this.dialogTriggers = dialogTriggers;
        HasBeenVisited = false;
    }

    public void CheckDialogToTrigger()
    {
        foreach (var trigger in dialogTriggers)
        {
            List<Condition> conditions = new List<Condition>();
            conditions.AddRange(trigger.RequiredConditions);
            if (ConditionManager.instance.CheckConditions(conditions, trigger.isRepeatable, trigger.isTriggered))
            {
                trigger.TriggerDialog();
                break;
            }
        }
    }
   
}

[System.Serializable]
public enum PlaceName
{
    Null
}

[System.Serializable]
public class PlaceUI
{
    [SerializeField] private string InspectorName;
    public PlaceName placeName;
    public Button placeButton;
    public ButtonHover placeButtonHover;
    public TextMeshProUGUI placeText;
    public GameObject NewText;
}
[System.Serializable]
public class DialogTrigger
{
    public string branchName;
    public string fileName;
    public bool isRepeatable;
    public bool isTriggered;
    public bool isRandom;
    public List<CheckCondition> RequiredConditions;
    public List<CheckCondition> AffectedConditions;

    public Dialog randomDialog = null;
    public bool RandomDialogSelected = false;
    public DialogTrigger(string branchName, string fileName, bool isRepeatable, List<CheckCondition> requiredConditions, List<CheckCondition> affectedConditions, bool isRandom = false)
    {
        this.branchName = branchName;
        this.fileName = fileName;
        this.isRepeatable = isRepeatable;
        RequiredConditions = requiredConditions;
        AffectedConditions = affectedConditions;
        this.isRandom = isRandom;
    }

    public void TriggerDialog()
    {
        isTriggered = true;
        if(isRandom && RandomDialogSelected == false)
        {
            randomDialog = DialogManager.instance.GetRandomDialog(fileName, branchName);
            RandomDialogSelected = true;
            DialogBranch branch = new DialogBranch();
            branch.instructions.Add(randomDialog);
            DialogManager.instance.StartBranch(branch);
        }
        else if (isRandom && RandomDialogSelected == true)
        {
            DialogBranch branch = new DialogBranch();
            branch.instructions.Add(randomDialog);
            DialogManager.instance.StartBranch(branch);
        }
        else
        {
            DialogManager.instance.StartBranch(fileName, branchName);
        }

        //After dialog editing new conditions
        for (int i = 0; i < AffectedConditions.Count; i++)
        {
            CheckCondition con = AffectedConditions[i];
            ConditionManager.instance.GetConditionByName(con.enumConditionName).status = con.status;
        }
    }

}
[System.Serializable]

public class AllPlaces
{

    public List<Place> places;
    private Dictionary<string,PlaceName> placeNameDict;

    public PlaceName GetPlaceNameEnum(string name)
    {
        PlaceName outValue;

        if(placeNameDict.TryGetValue(name, out outValue))
            return outValue;
        else
        {
            Debug.LogError("Place not found!! --> " +  name);
            return PlaceName.Null;
        }
    }

    public Place GetPlaceByName(PlaceName placeName)
    {
        if(placeName != PlaceName.Null)
        {
            foreach (Place place in places)
            {
                if (place.placeName == placeName)
                    return place;
            }
        }

        Debug.LogError("Place error! No place in MapManager called --> " + placeName);
        return null;
    }

    public AllPlaces()
    {
        places = new List<Place>();
        placeNameDict = new Dictionary<string, PlaceName>();

    }

}
