using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableDialog : MonoBehaviour
{
    public DialogTrigger trigger;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            List<Condition> conditions = new List<Condition>();
            conditions.AddRange(trigger.RequiredConditions);
            if (ConditionManager.instance.CheckConditions(conditions, trigger.isRepeatable, trigger.isTriggered))
            {
                trigger.TriggerDialog();
            }

        }
    }
}
