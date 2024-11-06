using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleButton : BaseUI, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    TitleUI _titleUI;
    GameObject _highlightObject;

    void Start()
    {
        _highlightObject = GetUI("Highlight");
        _highlightObject.SetActive(false);
        _titleUI = GetComponentInParent<TitleUI>();
        _titleUI.AddHighlightList(_highlightObject);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject); // OnSelect¿¡ Á¢±Ù
    }

    public void OnSelect(BaseEventData eventData)
    {
        _highlightObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _highlightObject?.SetActive(false);
    }
}
