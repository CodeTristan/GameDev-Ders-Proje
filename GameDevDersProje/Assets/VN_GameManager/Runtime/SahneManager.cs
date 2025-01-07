using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneManager : MonoBehaviour
{
    public static SahneManager instance;

    public Scene currentScene;
    public string currentSceneName;
    public void Init()
    {
        instance = this;
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string name,bool isLoadingData = false)
    {
        if(currentSceneName == "MainScene")
        {
        }
        currentScene = SceneManager.GetSceneByName(name);
        currentSceneName = name;
        SceneManager.LoadScene(name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BackgroundManager.instance.FindCamera();

        if (scene.name == "MainScene")
        {   

            InputManager.instance.EnableLogControls();

            List<Condition> conditions = new List<Condition> { new CheckCondition(ConditionName.GameStart, 1) };
            if (ConditionManager.instance.CheckConditions(conditions, false, false))
            {
                DialogManager.instance.StartBranch(DialogManager.instance.FirstBranch);
                ConditionManager.instance.GetConditionByName(ConditionName.GameStart).status = 0;
            }
        }
        else
        {
            InputManager.instance.DisableLogControls();
            InputManager.instance.DisableMainSceneControl();
            Debug.Log(scene.name);
        }
    }
}
