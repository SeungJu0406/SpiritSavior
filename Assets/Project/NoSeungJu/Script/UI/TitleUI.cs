using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : BaseUI
{
    List<GameObject> _highlightList = new List<GameObject>(5);

    List<GameObject> optionBoxs = new List<GameObject>(2);

    int _menuButtonInHash;
    int _menuButtonOutHash;

    private void Start()
    {
        Init();
        SubsCribeEvents();
        Time.timeScale = 1.0f;
    }


    private void Update()
    {

    }

    /// <summary>
    /// UI 이벤트 구독
    /// </summary>
    void SubsCribeEvents()
    {
        // 새 게임 시작 이벤트 구독
        GetUI<Button>("NewGameButton").onClick.AddListener(StartNewGame);

        // 크레딧 시작 이벤트 구독
        GetUI<Button>("CreditButton").onClick.AddListener(ShowCredit);

        // 크레딧 종료 이벤트 구독
        GetUI<Button>("CreditExitButton").onClick.AddListener(HideCredit);

        // 게임 종료 이벤트 구독
        GetUI<Button>("GameOffButton").onClick.AddListener(ExitGame);

        // 설정창 On/Off 이벤트 구독
        GetUI<Button>("BackButton").onClick.AddListener(ToggleOptionUI);
        GetUI<Button>("OptionButton").onClick.AddListener(ToggleOptionUI);

        // 조작키 설정 이벤트 구독
        GetUI<Button>("KeyButton").onClick.AddListener(ToggleKeyOption);

        // 오디오 설정버튼 이벤트 구독
        GetUI<Button>("AudioButton").onClick.AddListener(ToggleAudioOption);

        // Audio 슬라이더 이벤트 구독
        GetUI<Slider>("MasterVolume").onValueChanged.AddListener(SetVolumeMaster);
        GetUI<Slider>("BGMVolume").onValueChanged.AddListener(SetVolumeBGM);
        GetUI<Slider>("SFXVolume").onValueChanged.AddListener(SetVolumeSFX);
    }


    void StartNewGame()
    {
        SceneChanger.Instance.InitGameScene();
    }

    void ShowCredit()
    {
        GetUI("BlackFillter").gameObject.SetActive(false);
        GetUI("CreditFillter").gameObject.SetActive(true);
        CloseHighlight();
    }

    void HideCredit()
    {
        GetUI("BlackFillter").gameObject.SetActive(true);
        GetUI("CreditFillter").gameObject.SetActive(false);      
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #region 옵션
    /// <summary>
    /// 게임 옵션 UI On/Off
    /// </summary>
    private void ToggleOptionUI()
    {
        if (GetUI("OptionUI").activeSelf)
        {
            GetUI("TitleUI").SetActive(true);
            GetUI("OptionUI").SetActive(false);
            CloseHighlight();
        }
        else
        {
            GetUI("TitleUI").SetActive(false);
            GetUI("OptionUI").SetActive(true);
            CloseOptionBox();
        }
    }


    /// <summary>
    /// 조작법 ON/OFF
    /// </summary>
    private void ToggleKeyOption()
    {
        GameObject keyOption = GetUI("KeyOption");
        for (int i = 0; i < optionBoxs.Count; i++)
        {
            if (optionBoxs[i] == keyOption)
                continue;
            optionBoxs[i].SetActive(false);
        }
        keyOption.SetActive(!keyOption.activeSelf);
    }

    /// <summary>
    /// 오디오 설정 ON/OFF
    /// </summary>
    private void ToggleAudioOption()
    {
        GameObject audioOption = GetUI("AudioOption");
        for (int i = 0; i < optionBoxs.Count; i++)
        {
            if (optionBoxs[i] == audioOption)
                continue;
            optionBoxs[i].SetActive(false);
        }
        audioOption.SetActive(!audioOption.activeSelf);
    }

    private void SetVolumeMaster(float volume)
    {
        if (Manager.Sound.GetVolumeBGM() > volume)
        {
            SetVolumeBGM(volume);
            GetUI<Slider>("BGMVolume").value = Manager.Sound.GetVolumeBGM();
        }
        if (Manager.Sound.GetVolumeSFX() > volume)
        {
            SetVolumeSFX(volume);
            GetUI<Slider>("SFXVolume").value = Manager.Sound.GetVolumeSFX();
        }
    }
    private void SetVolumeBGM(float volume)
    {
        if (GetUI<Slider>("BGMVolume").value > GetUI<Slider>("MasterVolume").value)
        {
            volume = GetUI<Slider>("MasterVolume").value;
            GetUI<Slider>("BGMVolume").value = volume;
        }
        Manager.Sound.SetVolumeBGM(volume);
    }

    private void SetVolumeSFX(float volume)
    {
        if (GetUI<Slider>("SFXVolume").value > GetUI<Slider>("MasterVolume").value)
        {
            volume = GetUI<Slider>("MasterVolume").value;
            GetUI<Slider>("SFXVolume").value = volume;
        }
        Manager.Sound.SetVolumeSFX(volume);
    }

    /// <summary>
    /// 설정 초기세팅
    /// </summary>
    private void Init()
    {
        InitVolume();
        InitButton();
        InitOptionBox();
    }

    /// <summary>
    /// 볼륨값 초기세팅
    /// </summary>
    private void InitVolume()
    {
        float masterVolume = GetUI<Slider>("MasterVolume").value;
        if (Manager.Sound.GetVolumeBGM() > masterVolume)
        {
            SetVolumeBGM(masterVolume);
        }
        GetUI<Slider>("BGMVolume").value = Manager.Sound.GetVolumeBGM();

        if (Manager.Sound.GetVolumeSFX() > masterVolume)
        {
            SetVolumeSFX(masterVolume);
        }
        GetUI<Slider>("SFXVolume").value = Manager.Sound.GetVolumeSFX();
    }

    private void InitButton()
    {
        if (GetUI("OptionUI").activeSelf) // 옵션UI 가 On일때는 꺼두기
            ToggleOptionUI();
    }

    private void InitOptionBox()
    {
        optionBoxs.Add(GetUI("KeyOption"));
        optionBoxs.Add(GetUI("AudioOption"));

        CloseOptionBox();
    }
    private void CloseOptionBox()
    {
        foreach (GameObject optionBox in optionBoxs)
        {
            optionBox.SetActive(false);
        }
    }

    void CloseHighlight()
    {
        foreach (GameObject highlight in _highlightList)
        {
            highlight.SetActive(false);
        }
    }
    #endregion




    /// <summary>
    /// 하이라이트 리스트에 추가
    /// 하이라이트들에 대해 관리하게 편하게하기 위함
    /// </summary>
    /// <param name="highlight"></param>
    public void AddHighlightList(GameObject highlight)
    {
        _highlightList.Add(highlight);
    }
}
