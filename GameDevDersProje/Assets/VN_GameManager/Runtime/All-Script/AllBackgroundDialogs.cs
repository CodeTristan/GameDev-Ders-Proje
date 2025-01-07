using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BackgroundDialog
{
    public BackgroundName backgroundName;
    public BackgroundCharacterName characterName;
    public List<DialogTrigger> dialogTriggers;
    public List<CheckCondition> conditions;
    
    public BackgroundDialog(BackgroundName name,BackgroundCharacterName characterName,List<DialogTrigger> dialogTriggers,List<CheckCondition> conditions)
    {
        backgroundName = name;
        this.characterName = characterName;
        this.dialogTriggers = dialogTriggers;
        this.conditions = conditions;
    }

    public bool CheckIfAnyDialogToTrigger()
    {
        foreach (var trigger in dialogTriggers)
        {
            List<Condition> conditions = new List<Condition>();
            conditions.AddRange(trigger.RequiredConditions);
            if (ConditionManager.instance.CheckConditions(conditions, trigger.isRepeatable, trigger.isTriggered))
            {
                return true;
            }
        }

        //There is nothing to trigger
        return false;
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
public class AllBackgroundDialogs
{
    public List<BackgroundDialog> backgroundDialogs;

    public BackgroundDialog GetBackgroundDialogByName(BackgroundName backgroundName,BackgroundCharacterName characterName)
    {
        foreach (BackgroundDialog item in backgroundDialogs)
        {
            if (item.backgroundName == backgroundName && item.characterName == characterName)
                return item;
        }

        Debug.LogError("BackgroundDialog returned null in AllBackgroundDialogs:GetTriggerByName --> " + backgroundName + " - " + characterName);
        return null;
    }

    public AllBackgroundDialogs()
    {
        backgroundDialogs = new List<BackgroundDialog>();
    }


}
