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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            HideUI();
        }
    }

    void ShowUI()
    {
        Time.timeScale = 0f;
    }

    void HideUI()
    {
        Time.timeScale = 1f;
        Manager.UI.IsPopUp = false;
        Destroy(gameObject);
    }

    void Init()
    {
        Manager.UI.IsPopUp = true;
        ShowUI();
    }

    void SubscribsEvents()
    {
        GetUI<Button>("CancelButton").onClick.AddListener(HideUI);
    }
}
