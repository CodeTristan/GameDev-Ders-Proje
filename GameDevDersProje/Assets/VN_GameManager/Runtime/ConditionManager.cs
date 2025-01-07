using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    public static ConditionManager instance;
    [Header("Conditions")]
    public List<Condition> conditions;

    public void Init()
    {
        instance = this;
        conditions = GameManager.instance.allConditions.conditions;

        List<Condition> conditionfromXml = DialogManager.instance.File.Conditions;
        Debug.Log("Retrieved Condition data: " + conditionfromXml.Count);
        foreach (Condition condition in conditionfromXml)
        {
            Condition existedCon = GetConditionByName(condition.conditionName,false);
            if (existedCon == null)
            {
                conditions.Add(condition);
            }
            else
            {
                Debug.Log("Existed Condition: " + existedCon.conditionName + " - Status changed " + existedCon.status + " -> " + condition.status);
                existedCon.conditionName = condition.conditionName;
                existedCon.status = condition.status;
                existedCon.type = condition.type;
                existedCon.CheckSign = condition.CheckSign;
            }
        }
        
    }
    

    public bool CheckConditions(List<Condition> conditions, bool isRepeatable = false, bool isTriggered = false)
    {
        //It is not repeatable dialog and it is triggered once
        if (isRepeatable == false && isTriggered == true)
            return false;

        if(conditions == null)
            return true;
        if(conditions.Count == 0) 
            return true;
        foreach (Condition c in conditions)
        {
            

            Condition con = null;
            if (c.GetType() == typeof(CheckCondition))
            {
                CheckCondition cc = (CheckCondition)c;
                con = GetConditionByName(cc.enumConditionName);
            }
            else if (c.GetType() == typeof(Condition))
            {
                con = GetConditionByName(c.conditionName);
            }
            


            if (con == null)
            {
                Debug.LogError("Returned False in CheckDialogConditions in ConditionManager because condition is null! --> " + c.conditionName);
                return false;
            }
            

            switch (c.CheckSign)
            {
                case "=":
                    {
                        if (c.status != con.status)
                        {
                            return false;
                        }
                        break;
                    }
                case "!=":
                    {
                        if (c.status == con.status)
                        {
                            return false;
                        }
                        break;
                    }
                case "<":
                    {
                        if (con.status >= c.status)
                        {
                            return false;
                        }
                        break;
                    }
                case "<=":
                    {
                        if (con.status > c.status)
                        {
                            return false;
                        }
                        break;
                    }
                case ">":
                    {
                        if (con.status <= c.status)
                        {
                            return false;
                        }
                        break;
                    }
                case ">=":
                    {
                        if (con.status < c.status)
                        {
                            return false;
                        }
                        break;
                    }
                default:
                    Debug.LogError("Sing error in CheckConditions --> " + con.CheckSign);
                    break;
            }
        }

        return true;
    }

    public void AdjustCondition(ConditionInstruction conditionInstruction)
    {
        Condition condition = GetConditionByName(conditionInstruction.condition.conditionName);
        if(condition == null)
        {
            Debug.LogError("Condition not found! --> " + conditionInstruction.condition.conditionName);
            return;
        }

        int value = 0;
        string text = "Variable adjusted: " + condition.conditionName + " , Operation: " + conditionInstruction.operation +  " , Old Value : " + condition.status + " New Value : ";
        switch (conditionInstruction.operand)
        {
            case ConditionInstruction.ConditionOperand.Constant:
                {
                    value = conditionInstruction.condition.status;
                    break;
                }
            case ConditionInstruction.ConditionOperand.Variable:
                {
                    value = GetConditionByName(conditionInstruction.operand_VariableName).status;
                    break;
                }
            case ConditionInstruction.ConditionOperand.Random:
                {
                    value = Random.Range(conditionInstruction.randomValueStart, conditionInstruction.randomValueEnd + 1);
                    break;
                }

        }

        string change = "";

        switch (conditionInstruction.operation)
        {
            case ConditionInstruction.ConditionOperation.Set:
                {
                    condition.status = value;
                    break;
                }
            case ConditionInstruction.ConditionOperation.Add:
                {
                    condition.status += value;
                    change = "+";
                    break;
                }
            case ConditionInstruction.ConditionOperation.Sub:
                {
                    condition.status -= value;
                    change = "-";
                    break;
                }
            case ConditionInstruction.ConditionOperation.Mul:
                {
                    condition.status *= value;
                    break;
                }
            case ConditionInstruction.ConditionOperation.Div:
                {
                    condition.status /= value;
                    break;
                }
            case ConditionInstruction.ConditionOperation.Mod:
                {
                    condition.status = condition.status % value;
                    break;
                }
        }

        text += condition.status;
    }

    public void ChangeCondition(ConditionName conditionName,int status)
    {
        Condition condition = GetConditionByName(conditionName);
        if(condition != null)
        {
            condition.status = status;
        }
    }

    public void ChangeCondition(string conditionName, int status)
    {
        Condition condition = GetConditionByName(conditionName);
        if (condition != null)
        {
            condition.status = status;
        }
    }

    public void IncreaseCondition(ConditionName conditionName, int status)
    {
        Condition condition = GetConditionByName(conditionName);
        if (condition != null)
        {
            condition.status += status;
        }
    }
    public Condition GetConditionByName(ConditionName name,bool showError = true)
    {
        string ConName = GameManager.instance.allConditions.GetConditionName(name);
        if(ConName == "NULL")
        {
            Debug.LogError("Condition couldn't found in ConditionManager --> " + name);
            return null;
        }

        foreach (Condition condition in conditions)
        {
            if(condition.conditionName == ConName)
                return condition;
        }

        if(showError)
            Debug.LogError("Condition couldn't found in ConditionManager --> " + name);
        return null;
    }

    public Condition GetConditionByName(string name, bool showError = true)
    {
        foreach (Condition condition in conditions)
        {
            if (condition.conditionName == name)
                return condition;
        }

        if (showError)
            Debug.LogError("Condition not found in ConditionManager --> " + name);
        return null;
    }

}
