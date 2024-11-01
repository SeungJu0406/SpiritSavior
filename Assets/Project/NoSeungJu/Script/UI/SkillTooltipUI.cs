using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTooltipUI : BaseUI
{

    private void Start()
    {
        SubscribsEvents();
        Init();
    }

    void ShowUI()
    {
        Time.timeScale = 0f;
    }

    void HideUI()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }


    void Init()
    {
        ShowUI();
    }

    void SubscribsEvents()
    {
        GetUI<Button>("CancelButton").onClick.AddListener(HideUI);
    }
}
