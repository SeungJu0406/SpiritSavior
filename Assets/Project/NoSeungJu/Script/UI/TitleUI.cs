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
        GetUI("OptionUI").SetActive(true);
        GetUI("TitleUI").SetActive(false);
    }

    private void Init()
    {
        GetUI("OptionUI").SetActive(false);
    }

    void SubsCribeEvents()
    {
        GetUI<Button>("OptionButton").onClick.AddListener(ShowOption);
    }
}
