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

    [XmlElement("CheckSign")][HideInInspector] public string CheckSign = "";

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
    [HideInInspector]public ConditionName enumConditionName;

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
    GameStart
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
    }


    public string GetConditionName(ConditionName conditionName)
    {
        return conditionNameDictionary.GetValueOrDefault(conditionName,"NULL");
    }

}
