using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[System.Serializable]
public class Background
{
    [SerializeField] private string TitleName; //For unity inspector

    public BackgroundName name;
    public GameObject backgroundObject;
    public Image backgroundImage;
    public Sound[] soundEffects;
    public Sound[] loopingSounds;
    public BGCharacter[] characterButtons;
    public BackgroundDialog[] dialogs;

    public delegate void BackgroundButtonAction();

    [System.Serializable]
    public class BGCharacter
    {
        public BackgroundCharacterName name;
        public GameObject characterGameObject;
        public Button characterButton;
        [HideInInspector]public List<CheckCondition> conditions;
        public List<BackgroundButtonAction> buttonActions;
        public bool HasDialogs = true;

        public BGCharacter()
        {
            conditions = new List<CheckCondition>();
            buttonActions = new List<BackgroundButtonAction>();
        }
    }

    public void CheckCharacterConditions()
    {
        foreach (var character in characterButtons)
        {
            List<Condition> conditions = new List<Condition>();
            conditions.AddRange(character.conditions);
            if(ConditionManager.instance.CheckConditions(conditions))
            {
                character.characterButton.gameObject.SetActive(true);
                character.characterGameObject.gameObject.SetActive(true);
            }
            else
            {
                character.characterButton.gameObject.SetActive(false);
                character.characterGameObject.gameObject.SetActive(false);
            }
        }
    }

    public void AddButtonAction(BackgroundCharacterName characterName, BackgroundButtonAction action)
    {
        foreach(var character in characterButtons)
        {
            if(character.name == characterName)
            {
                character.buttonActions.Add(action);
            }
        }
    }
    public void AssignButtonActions()
    {
        foreach(var character in characterButtons)
        {
            character.characterButton.onClick.RemoveAllListeners();
            foreach (var action in character.buttonActions)
            {
                character.characterButton.onClick.AddListener(() => {  action(); });
            }
        }
    }
}

public enum BackgroundName
{
    Null,
    Your_House,
    Tavern,
    Ceremonial_Ground,
    Ceremonial_Ground_Kp1,
    Ceremonial_Ground_Kp2,
    Lyceum_Empty,
    Lyceum_Apathe,
    Great_Library_Empty,
    Great_Library_Apathe,
    Temple_Of_Zeus,
    Temple_Of_Zeus_Ze1,
    Temple_Of_Zeus_Ze2,
    Temple_Of_Zeus_Ze3,
    Temple_Of_Zeus_Ze4,
    Temple_Of_Athena_Empty,
    Temple_Of_Athena,
    Zeus_Forest,
    Zeus_Forest_Naked,
    Zeus_Forest_Hi1,
    Zeus_Forest_Hi2_Naked,
    Zeus_Forest_Hi2,
    Athenas_Maze,
    Athenas_Maze_Inside,
    Temple_Of_Hera,
    Temple_Of_Artemis,
    Temple_Of_Aphrodite,
    Artemis_Orman
}

public enum BackgroundCharacterName
{
    Tavernkeep,
    Keeper,
    Bullshiticus,
    Thesauros,
    Zeus,
    Hercules,
    Hippolyta,
    Athena,
    Hera,
    Artemis,
    Aphrodite
}
public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;

    public static event BackgroundChangeDelegate OnBackgroundChange;
    public delegate void BackgroundChangeDelegate(Background background);

    //[SerializeField] private PopUpManager popUpManager;
    [Header("UI")]
    [SerializeField] private Canvas generalCanvas;
    [SerializeField] private Canvas backgroundCanvas;
    [SerializeField] private Animator generalCanvasAnimator;
    [SerializeField] private Button MapButton;
    [SerializeField] private Button MenuButton;

    public TextMeshProUGUI Action_Point_Text;

    [Header("Background Sprites")]
    public Background currentBackground;
    [SerializeField] private Background[] backgrounds;

    public Place currentPlace;
    public List<SoundEffect> soundEffects;
    private bool isChangingBG;
    private bool startNextInstruction;
    private Dictionary<string, BackgroundName> backgroundNameDict;


    public void Init()
    {
        instance = this;
        soundEffects = new List<SoundEffect>();
        currentBackground = GetBackgroundByName(BackgroundName.Your_House);
        ToggleGeneralCanvas(true);
        CloseAllBackgrounds();

        //popUpManager.Init();
        //MapButton.onClick.AddListener(() => { MapButtonOnClick(); });
        //MenuButton.onClick.AddListener(() => { MenuButtonOnClick(); });

        SetBackgroundDialogs();
    }

    public void SetBackgroundDialogs()
    {
        foreach (Background bg in backgrounds)
        {
            bg.dialogs = new BackgroundDialog[bg.characterButtons.Length];
            for (int i = 0; i < bg.dialogs.Length; i++)
            {
                bg.dialogs[i] = GameManager.instance.allBackgroundDialogs.GetBackgroundDialogByName(bg.name, bg.characterButtons[i].name);
                bg.characterButtons[i].conditions = bg.dialogs[i].conditions;
            }
        }
    }
    public void ToggleBackgroundCanvas(bool toggle)
    {
        backgroundCanvas.enabled = toggle;
    }
    public void ToggleGeneralCanvas(bool enabled)
    {
        //if (VideoManager.instance.inVideo == true)
        //    return;
        if(SahneManager.instance.currentSceneName != "MainScene")
        {
            enabled = false;
        }

        generalCanvas.enabled = enabled;
    }
    public void TintBackground(Color32 color)
    {
        currentBackground.backgroundImage.color = color;
    }
    public void ChangeBackground(string name)
    {
        name = name.Trim();
        //For fade away
        if (name == "FADEAWAY")
        {
            DarkenScreen(1.5f,true);
            return;
        }
        else if(name == "RESETSPRITEUI")
        {
            SpriteManager.instance.ResetAllUI();
            DialogManager.instance.StartNextInstruction();
            return;
        }
        else if (name == "FADEOUT")
        {
            StartCoroutine(Fadeout());
            return;
        }
        else if (name == "FADEIN")
        {
            StartCoroutine(FadeIn());
            return;
        }
        else if (name == "NONE")
        {
            CloseAllBackgrounds();
            DialogManager.instance.StartNextInstruction();
            return;
        }


        Background bg = GetBackgroundByName(GetBackgroundNameEnum(name));
        if (bg == null)
        {
            Debug.LogError("Returned in ChangeBackground, because background is null!! --> " + name);
            return;
        }

        if(bg != currentBackground)
            CheckForSounds(bg);
        CloseAllBackgrounds();
        currentBackground = bg;
        bg.backgroundObject.SetActive(true);
        bg.CheckCharacterConditions();

        //bg.CheckCharacterConditions();
        //CheckBackgroundCharacters(bg);
        //currentPlace = MapManager.instance.GetPlaceByName(name);
        DialogManager.instance.StartNextInstruction();
    }
    public void ChangeBackground(Place place,bool WithAnim,bool checkDialogs = true,bool startNextInstruction = false)
    {
        this.startNextInstruction = startNextInstruction;
        if(place == null) return;
        Background bg = GetBackgroundByName(place.backgroundName);
        currentPlace = place;
        if (bg == null)
        {
            Debug.LogError("Returned in ChangeBackground, because background is null!! --> " + place.placeName);
            return;
        }
        if (isChangingBG)
            return;

        if(bg != currentBackground)
            CheckForSounds(bg);
        MusicManager.instance.PlayMusic(place.MusicName, true);
        if(WithAnim)
        {
            isChangingBG = true;
            StartCoroutine(StartChangePlaceAnim(checkDialogs));
        }
        else
        {
            ChangePlaceAnimation();
            ChangePlaceAnimationEnd(checkDialogs);
        }
        
    }

    private void CheckBackgroundCharacters(Background bg)
    {
        for (int i = 0; i < bg.characterButtons.Length; i++)
        {
            Background.BGCharacter character = bg.characterButtons[i];
            if (character == null && character.characterButton == null)
                continue;

            for (int j = 0; j < bg.dialogs.Length; j++)
            {
                if (bg.dialogs[j].characterName != character.name || character.HasDialogs == false)
                    continue;

                character.characterButton.onClick.RemoveAllListeners();
                BackgroundDialog dialog = bg.dialogs[j];
                character.characterButton.onClick.AddListener(() => dialog.CheckDialogToTrigger());
                //Debug.Log("Listener added : " + character.name + "  " + dialog.dialogTriggers.Count);
                break;
            }
        }
        bg.CheckCharacterConditions();
    }
    private IEnumerator StartChangePlaceAnim(bool checkDialogs)
    {
        generalCanvasAnimator.SetTrigger("FadeAway");
        yield return new WaitForSeconds(0.5f);
        ChangePlaceAnimation();
        yield return new WaitForSeconds(0.5f);
        ChangePlaceAnimationEnd(checkDialogs);
    }
    private void CheckForSounds(Background bg)
    {
        foreach (var item in soundEffects.ToArray())
        {
            if(item.source.loop)
            {
                item.kill();
                soundEffects.Remove(item);
            }
        }
        if (bg.soundEffects.Length > 0 && currentBackground != bg)
        {
            int r = UnityEngine.Random.Range(0, bg.soundEffects.Length);
            //Debug.Log(r + " / " + bg.soundEffects.Length);
            MusicManager.instance.PlaySound(bg.soundEffects[r]);
        }

        foreach (var item in bg.loopingSounds)
        {
            MusicManager.instance.PlaySound(item);
            soundEffects.Add(MusicManager.instance.SoundEffects[MusicManager.instance.SoundEffects.Count - 1]);
        }
    }
    public void ToggleDarkness(bool darkness)
    {
        if (darkness)
        {
            generalCanvasAnimator.SetTrigger("Darken");
        }
        else
        {
            generalCanvasAnimator.SetTrigger("Lighten");
        }
    }
    public void DarkenScreen(float DarkTime,bool startNextInstruction = false)
    {
        StartCoroutine(DarkenScreenNumerator(DarkTime,startNextInstruction));
    }
    public IEnumerator DarkenScreenNumerator(float timer, bool startNextInstruction)
    {
        generalCanvasAnimator.SetTrigger("Darken");

        yield return new WaitForSeconds(timer);

        generalCanvasAnimator.SetTrigger("Lighten");
        yield return new WaitForSeconds(0.2f);

        if(startNextInstruction)
        {
            DialogManager.instance.StartNextInstruction();
        }
    }

    public IEnumerator Fadeout()
    {
        generalCanvasAnimator.SetTrigger("Darken");

        yield return new WaitForSeconds(generalCanvasAnimator.GetCurrentAnimatorStateInfo(0).length);

        DialogManager.instance.StartNextInstruction();
    }

    public IEnumerator FadeIn()
    {
        generalCanvasAnimator.SetTrigger("Lighten");

        yield return new WaitForSeconds(.4f);

        DialogManager.instance.StartNextInstruction();
    }
    public void ChangePlaceAnimation()
    {
        Background bg = GetBackgroundByName(currentPlace.backgroundName);
        CloseAllBackgrounds();
        bg.backgroundObject.SetActive(true);
        CheckBackgroundCharacters(bg);

        //if(MapManager.instance != null)
        //    MapManager.instance.CloseMap();

    }

    public void ChangePlaceAnimationEnd(bool checkDialogs)
    {
        Background bg = GetBackgroundByName(currentPlace.backgroundName);
        currentBackground = bg;
        isChangingBG = false;
        ToggleGeneralCanvas(true);
        if (startNextInstruction)
            DialogManager.instance.StartNextInstruction();
        if(checkDialogs)
            currentPlace.CheckDialogToTrigger();

        OnBackgroundChange?.Invoke(bg);

    }
    public Background GetBackgroundByName(BackgroundName name)
    {
        foreach (Background bg in backgrounds)
        {
            if (bg.name == name)
            {
                return bg;
            }
        }

        Debug.LogError("Background is not found in BackgroundManager! --> '" + name +"'");
        return null;
    }

    public BackgroundName GetBackgroundNameEnum(string name)
    {
        BackgroundName outValue;

        if (backgroundNameDict.TryGetValue(name, out outValue))
            return outValue;
        else
        {
            Debug.LogError("Background not found!! --> " + name);
            return BackgroundName.Null;
        }
    }

    public void CloseAllBackgrounds()
    {
        for(int i = 1; i < backgrounds.Length; i++)
        {
            backgrounds[i].backgroundObject.SetActive(false);
        }
    }

    public void FindCamera()
    {
        backgroundCanvas.worldCamera = Camera.main;
    }


    ////For Background Objects
    //private void MapButtonOnClick()
    //{
    //    if(MapManager.instance != null)
    //    {
    //        MapManager.instance.OpenMap();
    //    }
    //}

    //private void MenuButtonOnClick()
    //{
    //    if (MenuManager.instance != null)
    //    {
    //        MenuManager.instance._OpenMenuCanvas();
    //        StatMenuController.instance._OpenStatCanvas();
    //    }
    //}

    //public void _BedSleep()
    //{
    //    DayManager.instance._Sleep();
    //}

    //public void _ChangePlace(string placeName)
    //{
    //    if(MapManager.instance != null)
    //    {
    //        MapManager.instance._ChangePlace(GameManager.instance.allPlaces.GetPlaceNameEnum(placeName));
    //    }
    //}

    //public void _OpenMap()
    //{
    //    if(MapManager.instance != null)
    //    {
    //        MapManager.instance.OpenMap();
    //    }
    //}
}
