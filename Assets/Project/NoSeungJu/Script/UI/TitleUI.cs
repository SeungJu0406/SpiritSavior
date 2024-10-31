using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    private void Start()
    {
        Init();
        SubsCribeEvents();
    }

    void ShowOption()
    {
        Debug.Log("1");
        GetUI("OptionUI").SetActive(true);
        GetUI("TitleUI").SetActive(false);
    }

    private void Init()
    {
        
    }

    void SubsCribeEvents()
    {
        GetUI<Button>("OptionButton").onClick.AddListener(ShowOption);
    }
}
