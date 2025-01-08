using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainSceneManager : MonoBehaviour
{
    public Volume volume;



    private void Start()
    {
        if(ConditionManager.instance != null)
        {
            if (ConditionManager.instance.GetConditionByName(ConditionName.Map1Complete).status == 1)
            {
                volume.profile.components[0].active = true;
                volume.profile.components[1].active = false;
            }
        }
        
    }
}
