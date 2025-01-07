using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public Vector2 scale;
    public Button button;
    public GameObject border;
    public GameObject nameTag;

    float timer;

    private void Start()
    {
        scale = new Vector2(1.34f, 1.34f);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }
    public void OnMouseEnter()
    {
        if (button.interactable && timer < 0)
        {
            timer = 0.2f;
            transform.localScale = scale;
            if (border != null)
            {
                border.transform.localScale = new Vector3(scale.x + 0.15f, scale.y + 0.15f);
                border.SetActive(true);

            }
            if (nameTag != null)
            {
                nameTag.transform.localScale = new Vector3(scale.x + 0.15f, scale.y + 0.15f);
                nameTag.SetActive(true);
            }
        }
    }

    private void OnMouseOver()
    {
        OnMouseEnter();
    }

    public void OnMouseExit()
    {
        if(button.interactable)
        {
            transform.localScale = new Vector2(1f, 1f);
            if (border != null)
            {
                border.transform.localScale = new Vector2(1.15f, 1.15f);
                border.SetActive(false);
            }
            if (nameTag != null)
            {
                nameTag.transform.localScale = new Vector2(1.15f, 1.15f);
                nameTag.SetActive(false);
            }
        }

    }
}
