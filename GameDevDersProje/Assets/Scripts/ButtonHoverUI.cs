using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 Scale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Scale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f,1f);
    }

}
