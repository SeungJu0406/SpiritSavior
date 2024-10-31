using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleButton : BaseUI, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] GameObject HighlightObject;

    void Start()
    {
        HighlightObject = GetUI("Highlight");
        HighlightObject.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject); // OnSelect¿¡ Á¢±Ù
    }

    public void OnSelect(BaseEventData eventData)
    {
        HighlightObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        HighlightObject?.SetActive(false);
    }
}
