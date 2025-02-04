using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;
using System.Xml;
using static UnityEngine.InputSystem.DefaultInputActions;
using UnityEngine.VFX;

[System.Serializable]
public class DialogManagerVariables
{
    public string currentFileName;
    public List<Instruction> instructions;
    public DialogBranch currentBranch;
    public string sentence;
    public int dialogIndex;
    public int instructionIndex;
    public bool isLooping;
    public int beforeLoopChoiceIndex;
}
public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [HideInInspector] public DialogBranch FirstBranch;
    [SerializeField] private ChoiceManager choiceManager;
    [SerializeField] private SpriteManager spriteManager;
    [SerializeField] private DialogLogManager dialogLogManager;

    [Header("UI")]
    [SerializeField] private Canvas DialogCanvas;
    [SerializeField] private GameObject clickBlockerPanel;
    [SerializeField] public TextMeshProUGUI nameText;
    [SerializeField] public TextMeshProUGUI dialogText;
    [SerializeField] private GameObject nextSentenceButton;
    [SerializeField] private Image AutoImage;
    [SerializeField] private float typeDelay;


    [Header("Manager")]
    public string PlayerName;
    public Color32 playerTextColor;
    public Color32 AutoColor;
    public ExportableFile File;
    public float AutoTimer;

    [Header("Debug")]
    public bool inDialog;
    [SerializeField] private DialogManagerVariables[] debugManagerVariablesStack;
    [SerializeField] private DialogBranch[] debugLoopingBranchStack;


    private Stack<DialogManagerVariables> ManagerVariables;
    private Stack<DialogBranch> LoopingBranchStack;

    public DialogManagerVariables currentManagerVariables;

    private Queue<string> sentences;
    private string sentence;
    private int DialogTextMaxVisibleCharacter = 0;
    private int RichTagCharacter = 0;
    public bool isAuto;
    public bool inLog;
    public bool inMenu;
    private bool isSentenceFinished;
    private float currentAutoTimer;
    public void Init()
    {
        instance = this;
        DialogCanvas.enabled = false;
        ManagerVariables = new Stack<DialogManagerVariables>();
        LoopingBranchStack = new Stack<DialogBranch>();
        dialogText.text = "";
        nameText.text = "";

        sentences = new Queue<string>();

        choiceManager.Init();
        spriteManager.Init();
        dialogLogManager.Init();

        //This is to override current Dialog file with the file in the Debug directory.
        string DialogPath = Directory.GetCurrentDirectory() + "/Debug";
        string xmlAsset = "";
        if (Directory.GetFiles(DialogPath, "Dialogs.xml").Length == 0)
        {
            Debug.Log("Debug Dialogs.xml file not found.");
            File = XMLManager.instance.GetInstructions();
        }
        else
        {
            StreamReader inp_stm = new StreamReader(DialogPath + "/Dialogs.xml");
            xmlAsset = inp_stm.ReadToEnd();
            TextAsset textAsset = new TextAsset(xmlAsset);
            File = XMLManager.instance.GetInstructions(textAsset);
        }

        foreach (Mission item in File.Missions)
        {
            GameManager.instance.allMissions.Add(item);
        }


        //INPUTS
        InputManager.instance.playerActions.DialogControls.NextSentence.performed += InputNextSentence;
        InputManager.instance.playerActions.DialogControls.ToggleDialogCanvas.performed += ToggleDialogCanvas;

        //InputManager.instance.playerActions.DialogControls.DebugSkipBranch.performed += _DebugSkipBranch;
        //InputManager.instance.playerActions.DialogControls.DebugSkipBranch.performed += DialogManager.instance.InputNextSentence;
    }

    private void Update()
    {
        currentAutoTimer -= Time.deltaTime;
        if (inMenu || inLog || choiceManager.inChoice)
            currentAutoTimer = AutoTimer;
        if(isAuto && isSentenceFinished && currentAutoTimer <= 0)
        {
            isSentenceFinished = false;
            _StartNextSentence(false);
        }    
    }

    public void StartBranch(string fileName, string branchName, bool isLoop = false)
    {
        if(FindObjectOfType<PlayerMovement>() != null)
        {
            FindObjectOfType<PlayerMovement>().LockMovement = true;
            FindObjectOfType<PlayerCam>().LockMovement = true;
        }
        

        FileXML file = GetFileByName(fileName);

        DialogManagerVariables var = new DialogManagerVariables();
        ManagerVariables.Push(var);
        currentManagerVariables = ManagerVariables.Peek();
        SetManagerVariables(fileName, branchName, isLoop);

        BackgroundManager.instance.ToggleGeneralCanvas(false);
        inDialog = true;
        StartNextInstruction();
    }

    //overflow
    public void StartBranch(DialogBranch branch,bool isLoop = false)
    {
        if (FindObjectOfType<PlayerMovement>() != null)
        {
            FindObjectOfType<PlayerMovement>().LockMovement = true;
            FindObjectOfType<PlayerCam>().LockMovement = true;
        }
      

        inDialog = true;

        DialogManagerVariables var = new DialogManagerVariables();
        ManagerVariables.Push(var);
        currentManagerVariables = ManagerVariables.Peek();

        BackgroundManager.instance.ToggleGeneralCanvas(false);
        SetManagerVariables(branch,isLoop);
        StartNextInstruction();
    }

    public void StartRandomDialog(DialogBranch branch)
    {
        Dialog[] dialogs = branch.dialogs.ToArray();

        int random = Random.Range(0, dialogs.Length);

        DialogBranch newBr = new DialogBranch();
        newBr.instructions.Add(dialogs[random]);
        StartBranch(newBr);
    }
    public void StartRandomDialog(string fileName, string branchName)
    {
        FileXML file = GetFileByName(fileName);
        DialogBranch branch = GetBranchByName(file, branchName);

        Dialog[] dialogs = branch.dialogs.ToArray();

        int random = Random.Range(0, dialogs.Length);

        DialogBranch newBr = new DialogBranch();
        newBr.instructions.Add(dialogs[random]);
        StartBranch(newBr);
    }

    public Dialog GetRandomDialog(string fileName, string branchName)
    {
        FileXML file = GetFileByName(fileName);
        DialogBranch branch = GetBranchByName(file, branchName);

        Dialog[] dialogs = branch.dialogs.ToArray();

        int random = Random.Range(0, dialogs.Length);

        Dialog dialog = dialogs[random];
        return dialog;
    }

    public void ResetDialogManager()
    {
        choiceManager.CloseChoiceCanvas();
        ManagerVariables.Clear();
        currentManagerVariables = null;
        sentence = "";
        LoopingBranchStack.Clear();
        StopAllCoroutines();
        BackgroundManager.instance.TintBackground(new Color32(255,255,255,255));
        spriteManager.ResetAllUI();
        InputManager.instance.DisableAllControls();
    }
    public void _DebugSkipBranch(InputAction.CallbackContext context)
    {

        //if (currentManagerVariables.instructions[currentManagerVariables.instructions.Count-1].type == InstructionType.JumpBranchInstruction)
        //{
        //    currentManagerVariables.instructionIndex = currentManagerVariables.instructions.Count - 1;
        //    StartNextInstruction();
        //}
        //else
        //    EndBranch();


        for (int i = currentManagerVariables.instructionIndex - 1; i < currentManagerVariables.instructions.Count; i++)
        {
            if(ManagerVariables.Count > 0)
                StartNextInstruction();
        }
        //EndBranch();

        if (ManagerVariables.Count == 0)
        {
            sentences.Clear();
            sentence = "";
            CloseDialogCanvas(true);
            StopAllCoroutines();
            choiceManager.CloseChoiceCanvas();
        }
        else
        {
            _DebugSkipBranch(context);
        }
    }

    public void CloseNextDialogButton()
    {
        nextSentenceButton.SetActive(false);
    }
    public void OpenDialogCanvas(bool isBlockerPanelEnabled)
    {
        if (dialogText.text == "")
            return;
        DialogCanvas.enabled = true;
        clickBlockerPanel.SetActive(isBlockerPanelEnabled);
    }

    public void CloseDialogCanvas(bool isBlockerPanelEnabled)
    {
        DialogCanvas.enabled = false;
        clickBlockerPanel.SetActive(isBlockerPanelEnabled);
    }

    public void ToggleDialogCanvas(InputAction.CallbackContext context)
    {
        if (DialogCanvas.enabled)
            CloseDialogCanvas(true);
        else
            OpenDialogCanvas(true);
    }

    private void SetManagerVariables(string fileName, string branchName, bool isLoop = false)
    {
        FileXML file = GetFileByName(fileName);
        DialogBranch branch = GetBranchByName(file, branchName);

        if (isLoop)
        {
            LoopingBranchStack.Push(branch);
        }

        currentManagerVariables.currentFileName = fileName;
        currentManagerVariables.currentBranch = branch;
        currentManagerVariables.instructions = GetBranchInstructions(branch);
        currentManagerVariables.instructionIndex = currentManagerVariables.beforeLoopChoiceIndex;
        currentManagerVariables.dialogIndex = 0;
        currentManagerVariables.isLooping = isLoop;
    }

    //overflow
    private void SetManagerVariables(DialogBranch branch, bool isLoop = false)
    {
        currentManagerVariables.currentBranch = branch;
        currentManagerVariables.instructions = GetBranchInstructions(branch);
        currentManagerVariables.instructionIndex = currentManagerVariables.beforeLoopChoiceIndex;
        currentManagerVariables.dialogIndex = 0;
        currentManagerVariables.isLooping = isLoop;
    }
    public void StartNextInstruction()
    {
        List<Instruction> instructions = currentManagerVariables.currentBranch.instructions;
        int instructionIndex = currentManagerVariables.instructionIndex;
        if (instructionIndex >= instructions.Count)
        {
            EndBranch();
            return;
        }

        InstructionType type = instructions[instructionIndex].type;
        switch (type)
        {
            case InstructionType.DialogBranch:
                {
                    StartBranch(currentManagerVariables.currentFileName, currentManagerVariables.currentBranch.name);
                    break;
                }
            case InstructionType.ChoiceBody:
                {
                    choiceManager.StartChoice((ChoiceBody)instructions[instructionIndex]);
                    InputManager.instance.DisableAllControls();
                    InputManager.instance.EnableChoiceControls();
                    break;
                }
            case InstructionType.Background:
                {
                    currentManagerVariables.instructionIndex++;
                    BackgroundInstruction ins = (BackgroundInstruction)instructions[instructionIndex];
                    if (ins.checkDialogs)
                        BackgroundManager.instance.ChangeBackground(GameManager.instance.allPlaces.GetPlaceByName(GameManager.instance.allPlaces.GetPlaceNameEnum(ins.name)), true, true);
                    else
                        BackgroundManager.instance.ChangeBackground(ins.name);
                    return;  //Change Bg
                }
            case InstructionType.Music:
                {
                    MusicManager.instance.PlayMusic(instructions[instructionIndex].name);
                    currentManagerVariables.instructionIndex++;
                    StartNextInstruction();
                    return;
                }//Change Music
            case InstructionType.Minigame:
                {
                    InputManager.instance.DisableAllControls();
                    LoadMinigame(instructions[instructionIndex].name);
                    break;
                } //Change scene to minigame
            case InstructionType.Dialog:
                {
                    dialogLogManager.AddLog(instructions[instructionIndex]);
                    StartDialog((Dialog)instructions[instructionIndex]);
                    InputManager.instance.DisableAllControls();
                    InputManager.instance.EnableDialogControls();
                    break;
                }
            case InstructionType.ConditionVariableInstruction:
                {
                    ConditionManager.instance.AdjustCondition((ConditionInstruction)instructions[instructionIndex]);
                    currentManagerVariables.instructionIndex++;
                    StartNextInstruction();
                    return;
                }
            case InstructionType.ConditionSwitchInstruction:
                {
                    ConditionInstruction conditionInstruction = (ConditionInstruction)instructions[instructionIndex];
                    ConditionManager.instance.ChangeCondition(conditionInstruction.condition.conditionName, conditionInstruction.condition.status);
                    currentManagerVariables.instructionIndex++;
                    StartNextInstruction();
                    break;
                }
            case InstructionType.ConditionalBranchInstruction:
                {
                    ConditionalBranchInstruction conditionalBranchInstruction = (ConditionalBranchInstruction)instructions[instructionIndex];
                    CheckConditionalBranch(conditionalBranchInstruction);
                    return;
                }
            case InstructionType.WaitInstruction:
                {
                    WaitInstruction waitIns = (WaitInstruction)instructions[instructionIndex];
                    StartCoroutine(ExecuteWaitInstruction(waitIns));
                    return;
                }
            case InstructionType.JumpBranchInstruction:
                {
                    JumpBranchInstruction jBIns = (JumpBranchInstruction)instructions[instructionIndex];
                    if(jBIns.Return)
                    {
                        currentManagerVariables.instructionIndex++;
                        StartBranch(jBIns.FileName,jBIns.BranchName);
                    }
                    else
                    {
                        ManagerVariables.Clear();
                        StartBranch(jBIns.FileName, jBIns.BranchName);
                        currentManagerVariables.instructionIndex = 1;
                    }
                    return;
                }
            case InstructionType.MissionInstruction:
                {
                    MissionInstruction missionInstruction = (MissionInstruction)instructions[instructionIndex];
                    currentManagerVariables.instructionIndex++;
                    StartNextInstruction();
                    return;
                }
            default: Debug.LogError("Wrong type in StartNextInstruction"); break;
        }

        currentManagerVariables.instructionIndex++;
    }

    private void EndBranch()
    {
        if(FindAnyObjectByType<PlayerMovement>() != null)
        {
            FindObjectOfType<PlayerMovement>().LockMovement = false;
            FindObjectOfType<PlayerCam>().LockMovement = false;
        }
   


        //Debug.Log(ManagerVariables.Count);
        DialogManagerVariables popped = ManagerVariables.Pop();


        //End
        if (ManagerVariables.Count <= 0)
        {
            CheckBranch(popped.currentBranch.name);
            dialogText.text = "";
            spriteManager.ResetAllUI();
            InputManager.instance.DisableAllControls();
            InputManager.instance.EnableMainSceneControl();
            BackgroundManager.instance.TintBackground(new Color32(255, 255, 255, 255));
            BackgroundManager.instance.ToggleGeneralCanvas(true);
            inDialog = false;
            BackgroundManager.instance.ChangeBackground(BackgroundManager.instance.currentPlace, false,false);
            return;
        }

        if (ManagerVariables.Peek().isLooping)
        {
            currentManagerVariables = ManagerVariables.Peek();
            currentManagerVariables.instructionIndex = currentManagerVariables.beforeLoopChoiceIndex;
            currentManagerVariables.isLooping = !choiceManager.IsLoopFinished((ChoiceBody)currentManagerVariables.instructions[currentManagerVariables.instructionIndex]);

            if (!currentManagerVariables.isLooping)
                currentManagerVariables.instructionIndex++;

            StartNextInstruction();
            return;
        }

        //Returning to parent branch
        currentManagerVariables = ManagerVariables.Peek();
        StartNextInstruction();

    }

    public void StartDialog(Dialog dialog)
    {
        DialogCanvas.enabled = true;
        //Camera.main.cullingMask = 0b1111101111111111;

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        if(dialog.voiceOverName != "")
        {
            if(dialog.voiceOverName != "NONE")
                MusicManager.instance.PlaySound(dialog.voiceOverName);
        }

        
        if (dialog.name == "MC")
        {
            nameText.text = PlayerName;
            //if(dialog.Expression != "NoTint")
                //BackgroundManager.instance.TintBackground(new Color32(100,100, 100, 255));
        }
        else if(dialog.name == "NONE")
        {
            nameText.text = "";
        }
        else if(dialog.Expression == "NoSprite")
        {
            nameText.text = dialog.name;
            //BackgroundManager.instance.TintBackground(new Color32(100, 100, 100, 255));
        }
        else if (dialog.Expression == "NoTint")
        {
            nameText.text = dialog.name;
        }
        else
        {
            nameText.text = dialog.name;
            BackgroundManager.instance.TintBackground(new Color32(100,100, 100, 255));
            spriteManager.ChangeSprite(dialog.name, dialog.Expression);
        }
        dialogText.text = "";

        if (dialog.name == "MC")
            dialogText.color = playerTextColor;
        else
            dialogText.color = Color.black;

        sentence = "";
        _StartNextSentence(true);
    }

    public void InputNextSentence(InputAction.CallbackContext context)
    {
        _StartNextSentence(false);
    }
    public void _StartNextSentence(bool firstTime)
    {
        if (dialogText.maxVisibleCharacters != sentence.Length && !firstTime)
        {
            StopAllCoroutines();
            nextSentenceButton.SetActive(true);
            dialogText.maxVisibleCharacters = sentence.Length;
            currentAutoTimer = AutoTimer;
            isSentenceFinished = true;
            return;
        }
        if (sentences.Count <= 0)
        {
            EndDialog();
            return;
        }

        sentence = sentences.Dequeue();
        Dialog d = (Dialog)dialogLogManager.LogInstructions[dialogLogManager.LogInstructions.Count - 1];
        d.sentences.Add(sentence);

        if (d.name == "�aman")
        {
            int r = Random.Range(1, 4);
            MusicManager.instance.PlaySound("talk" + r);
        }
        StartCoroutine(TypeSentence());
    }

    private void EndDialog()
    {
        DialogCanvas.enabled = false;
        currentManagerVariables.dialogIndex++;
        InputManager.instance.DisableDialogControls();
        StartNextInstruction();
    }

    
    private void LoadMinigame(string MinigameName)
    {
        Debug.Log("Loading Minigame: " + MinigameName);
        LoopingBranchStack.Clear();
        ManagerVariables.Clear();
        MusicManager.instance.KillAllSounds();
        SahneManager.instance.LoadScene(MinigameName);
    }
    private IEnumerator ExecuteWaitInstruction(WaitInstruction waitIns)
    {
        yield return new WaitForSeconds(waitIns.waitTime);
        currentManagerVariables.instructionIndex++;
        StartNextInstruction();
    }

    private void CheckConditionalBranch(ConditionalBranchInstruction conditionalBranchInstruction)
    {
        bool conditionPassed = ConditionManager.instance.CheckConditions(conditionalBranchInstruction.conditions);
        currentManagerVariables.instructionIndex++;
        if (conditionPassed)
        {
            StartBranch(conditionalBranchInstruction.branch);
        }
        else if(conditionPassed == false && conditionalBranchInstruction.ElseBranchCreated)
        {
            StartBranch(conditionalBranchInstruction.elseBranch);
        }
        else
        {
            StartNextInstruction();
        }
    }
    private List<Instruction> GetBranchInstructions(DialogBranch branch)
    {
        List<Instruction> instructions = new List<Instruction>();

        foreach (Instruction ins in branch.instructions)
        {
            instructions.Add(ins);
        }

        return instructions;
    }
    public FileXML GetFileByName(string fileName)
    {
        foreach (var item in File.FileClusters)
        {
            foreach (var file in item.Files)
            {
                if(file.FileName == fileName)
                    return file;
            }
        }

        Debug.LogError("File Name Not Found!! --> " + fileName);
        return null;
    }
    public DialogBranch GetBranchByName(FileXML file, string branchName)
    {
        foreach (var item in file.BranchClusters)
        {
            foreach (var branch in item.Branches)
            {
                if(branch.name == branchName)
                    return branch;
            }
        }

        Debug.LogError("Error: Branch couldn't found!! --> " + branchName);
        return null;
    }

    public void _ToggleAuto()
    {
        isAuto = !isAuto;
        currentAutoTimer = AutoTimer;
        if (isAuto)
        {
            AutoImage.color = AutoColor;
        }
        else
        {
            AutoImage.color = new Color32(255,255, 255, 255);
        }
    }

    private void RedesignSentence()
    {
        //Renaming MC to Player Name
        while (sentence.IndexOf("#MC") != -1)
        {
            string after = sentence.Substring(sentence.IndexOf("#MC") + 3);
            string before = sentence.Substring(0, sentence.IndexOf("#MC"));
            before += PlayerName;
            sentence = before + after;
        }
        
    }

    private IEnumerator TypeSentence()
    {
        dialogText.text = "";
        isSentenceFinished = false;
        nextSentenceButton.SetActive(false);
        bool isRichTextTag = false;

        RedesignSentence();

        //Typing Sentence
        char[] array = sentence.ToCharArray();
        
        DialogTextMaxVisibleCharacter = 0;
        RichTagCharacter = 0;
        dialogText.text = sentence;


        for (int i = 0; i < array.Length; i++)
        {
            DialogTextMaxVisibleCharacter++;
            dialogText.maxVisibleCharacters = DialogTextMaxVisibleCharacter;
            char letter = array[i];
            if (letter == '<' || isRichTextTag)
            {
                isRichTextTag = true;
                //dialogText.text += letter;
                if (letter == '>')
                    isRichTextTag = false;
                RichTagCharacter++;
                DialogTextMaxVisibleCharacter--;
                
            }
            else
            {
                yield return new WaitForSeconds(typeDelay);
            }

        }
        currentAutoTimer = AutoTimer;
        isSentenceFinished = true;
        nextSentenceButton.SetActive(true);
    }

    private void CheckBranch(string branchName)
    {
        if(branchName == "Shaman Talk 1")
        {
            GameObject obj = GameObject.FindWithTag("Portal1");
            obj.GetComponent<VisualEffect>().enabled = true;
            obj.GetComponent<Collider>().enabled = true;
        }
        else if (branchName == "Shaman Talk 2")
        {
            GameObject obj = GameObject.FindWithTag("Portal2");
            obj.GetComponent<VisualEffect>().enabled = true;
            obj.GetComponent<Collider>().enabled = true;
        }
        if (branchName == "Shaman Talk 3")
        {
            SahneManager.instance.LoadScene("MainMenu");
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(ManagerVariables != null)
            debugManagerVariablesStack = ManagerVariables.ToArray();

        if(LoopingBranchStack != null)
            debugLoopingBranchStack = LoopingBranchStack.ToArray();
    }
}
