using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void _StartGame()
    {
        StartCoroutine(startGame());
    }

    private IEnumerator startGame()
    {
        StartCoroutine(BackgroundManager.instance.Fadeout());
        yield return new WaitForSeconds(1);
        SahneManager.instance.LoadScene("MainScene");
    }

    public void _Exit()
    {
        Application.Quit();
    }
}
