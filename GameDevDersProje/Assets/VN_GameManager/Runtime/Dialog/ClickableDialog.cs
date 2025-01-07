using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableDialog : MonoBehaviour
{
    public Dialog[] dialogs;

    public string fileName;
    public string branchName;
    public string ExamineText = "Examine";


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            _StartBranchWithFile();
            Destroy(this);
        }
    }
    public void _StartBranchWithFile()
    {
        DialogManager.instance.StartBranch(fileName, branchName);
    }
    public void _StartDialog()
    {
        DialogBranch branch = new DialogBranch();
        foreach (Dialog dialog in dialogs)
        {
            branch.instructions.Add(dialog);
        }
        DialogManager.instance.StartBranch(branch);
    }
}
