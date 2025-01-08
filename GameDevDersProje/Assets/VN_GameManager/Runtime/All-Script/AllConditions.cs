using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using static Condition;

[System.Serializable]
public class Condition
{
    [XmlElement("ConditionName")] public string conditionName;

    [XmlElement("Status")] public int status;

    [XmlElement("Type")] public ConditionType type;

    [XmlElement("CheckSign")] public string CheckSign = "=";

    public Condition()
    {
        CheckSign = "=";
    }
    public Condition(string conditionName, int status, ConditionType type = ConditionType.Switch, string CheckSign = "=")
    {
        this.conditionName = conditionName;
        this.type = type;
        this.status = status;
        this.CheckSign = CheckSign;
    }

    public Condition(XmlNode node)
    {
        conditionName = node["ConditionName"].InnerText;
        status = int.Parse(node["Status"].InnerText);
        type = (ConditionType)Enum.Parse(typeof(ConditionType), node["Type"].InnerText);
        if (node["CheckSign"] != null)
        {
            CheckSign = node["CheckSign"].InnerText;
        }
        else
        {
            CheckSign = "=";
        }
    }

    public override string ToString()
    {
        return "Name: " + conditionName + " Status: " + status + " Sign: " + CheckSign;
    }

    public enum ConditionType
    {
        [XmlEnum("Switch")] Switch,
        [XmlEnum("Variable")] Variable
    }
}

[System.Serializable]
public class CheckCondition : Condition
{
    public ConditionName enumConditionName;

    public CheckCondition()
    {
        CheckSign = "=";
    }
    public CheckCondition(ConditionName conditionName, int status, ConditionType type = ConditionType.Switch, string CheckSign = "=")
    {
        this.enumConditionName = conditionName;
        this.type = type;
        this.status = status;
        this.CheckSign = CheckSign;
    }

    public override string ToString()
    {
        return "Name: " + enumConditionName + " Status: " + status + " Sign: " + CheckSign;
    }
}
public enum ConditionName
{
    GameStart,
    ShamanTalk_1,
    ShamanTalk_2,
    ShamanTalk_3
}

[System.Serializable]

public class AllConditions
{
    public List<Condition> conditions;

    private Dictionary<ConditionName, string> conditionNameDictionary;

    public AllConditions()
    {
        conditions = new List<Condition>();
        conditionNameDictionary = new Dictionary<ConditionName, string>();

        conditionNameDictionary.Add(ConditionName.GameStart, "GameStart");
        conditionNameDictionary.Add(ConditionName.ShamanTalk_1, "ShamanTalk 1");
        conditionNameDictionary.Add(ConditionName.ShamanTalk_2, "ShamanTalk 2");
        conditionNameDictionary.Add(ConditionName.ShamanTalk_3, "ShamanTalk 3");

        conditions.Add(new Condition("GameStart", 1));
        conditions.Add(new Condition("ShamanTalk 1", 1));
        conditions.Add(new Condition("ShamanTalk 2", 0));
        conditions.Add(new Condition("ShamanTalk 3", 0));
    }


    public string GetConditionName(ConditionName conditionName)
    {
        return conditionNameDictionary.GetValueOrDefault(conditionName,"NULL");
    }

}
