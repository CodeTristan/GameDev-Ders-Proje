using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool DebugMod;

    [SerializeField] private SahneManager sahneManager;
    [SerializeField] private XMLManager XMLmanager;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ConditionManager conditionManager;
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private BackgroundManager backgroundManager;


    public AllPlaces allPlaces;
    public AllConditions allConditions;
    public List<Mission> allMissions;
    public AllBackgroundDialogs allBackgroundDialogs;
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        allPlaces = new AllPlaces();
        allConditions = new AllConditions();
        allMissions = new List<Mission>();
        allBackgroundDialogs = new AllBackgroundDialogs();

        XMLmanager.Init();
        inputManager.Init();
        sahneManager.Init();

        backgroundManager.Init();

        dialogManager.Init();  //Dialogs, choices and mission are set in here.
        conditionManager.Init();  //Conditions from editor are set in here.
        musicManager.Init();

        saveManager.Init();


        sahneManager.LoadScene("MainMenu");


        Debug.Log("Initialize Completed!");
    }

   
}
