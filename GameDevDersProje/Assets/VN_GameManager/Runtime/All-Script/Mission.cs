using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using UnityEngine;

[System.Serializable]
public class Mission
{
    [XmlElement("Name")] public string name;
    [XmlElement("DisplayName")] public string displayName;
    [XmlElement("Type")] public MissionType type;

    [XmlIgnore] public bool isCompleted;
    [XmlIgnore] public bool isActive;

    [XmlIgnore] public int objectiveIndex;
    [XmlIgnore] public List<Objective> activeObjectives;
    [XmlElement("Objective")] public List<Objective> objectives;

    public Mission()
    {
        objectives = new List<Objective>();
        activeObjectives = new List<Objective>();
    }
    public Mission(string name, MissionType type, List<Objective> objectives) : this()
    {
        this.name = name;
        this.type = type;
        this.objectives = objectives;
    }

    public Mission(XmlNode node) : this()
    {
        name = node["Name"].InnerText;
        displayName = node["DisplayName"].InnerText;
        type = (MissionType)Enum.Parse(typeof(MissionType), node["Type"].InnerText);

        XmlNodeList objectiveNodes = node.SelectNodes("Objective");
        foreach (XmlNode objNode in objectiveNodes)
        {
            Objective objective = new Objective(objNode);
            objectives.Add(objective);
        }
    }

    public void StartMission()
    {
        objectiveIndex = 0;
        isActive = true;
        StartNextObjective();
    }

    public void StartObjective(Objective objective)
    {
        bool isCorrectObjective = false;
        int index = 0;
        foreach (var item in objectives)
        {
            if(objective == item)
            {
                isCorrectObjective = true;
                break;
            }
            index++;
        }
        if(isCorrectObjective == false)
        {
            Debug.LogError("Objective is not in this mission!! Mission: " + name + " --> Objective: " + objective.name);
            return;
        }

        objective.isVisible = true;
        objectiveIndex = index + 1;
        activeObjectives.Add(objective);

    }

    public void StartNextObjective()
    {
        if(objectiveIndex == objectives.Count)
        {
            Debug.Log("All Objectives have started already!! --> " + name);
            return;
        }

        StartObjective(objectives[objectiveIndex]);
    }

    public void CompleteObjective(Objective objective)
    {
        bool isCorrectObjective = false;
        foreach (var item in objectives)
        {
            if (objective == item)
            {
                isCorrectObjective = true;
                break;
            }
        }
        if (isCorrectObjective == false)
        {
            Debug.LogError("Objective is not in this mission!! Mission: " + name + " --> Objective: " + objective.name);
            return;
        }

        activeObjectives.Remove(objective);
        objective.CompleteObjective();

        //Check if all missions are completed
        bool missionCompleted = true;
        foreach (var item in objectives)
        {
            if (item.isCompleted == false)
            {
                missionCompleted = false;
                break;
            }
        }

        if (missionCompleted) CompleteMission();
    }
    public void CompleteMission()
    {
        isCompleted = true;
        isActive = false;
    }
}
[System.Serializable]
public class Objective
{
    [XmlElement("Name")] public string name;
    [XmlElement("Description")] public string description;
    [XmlElement("HelpText")] public string helpText;
    public bool isCompleted;
    public bool isVisible;

    public Objective()
    {

    }
    public Objective(string name, string description, string helpText, bool isVisible)
    {
        this.name = name;
        this.description = description;
        this.helpText = helpText;
        this.isVisible = isVisible;
    }

    public Objective(XmlNode node)
    {
        name = node["Name"].InnerText;
        description = node["Description"].InnerText;
        helpText = node["HelpText"].InnerText;
    }

    public void CompleteObjective()
    {
        isCompleted = true;
    }
}
[System.Serializable]
public enum MissionType
{
    [XmlEnum("Main")] Main,
    [XmlEnum("Side")] Side
}

