using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionUI : BaseUI
{
    int _menuButtonInHash;
    int _menuButtonOutHash;

    protected override void Awake()
    {
        base.Awake();

        _menuButtonInHash = Animator.StringToHash("In");
        _menuButtonInHash = Animator.StringToHash("Out");
    }

    private void Start()
    {
        SubscribeEvent();
        InitVolume();
    }

    /// <summary>
    /// 오디오 설정 ON/OFF
    /// </summary>
    private void ToggleAudioOption()
    {
        if (GetUI("AudioOption").activeSelf)
        {
            GetUI("AudioOption").SetActive(false);
        }
        else
        {
            GetUI("AudioOption").SetActive(true);
        }
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
    /// 볼륨값 초기화
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


    /// <summary>
    /// 게임 옵션 UI On/Off
    /// </summary>
    private void ToggleGameOptionUI()
    {
        if (GetUI("GameOptionUI").activeSelf)
        {
            GetUI("GameOptionUI").SetActive(false);
            GetUI<Animator>("MenuButton").Play("Out");
        }
        else
        {
            GetUI("GameOptionUI").SetActive(true);
            GetUI<Animator>("MenuButton").Play("In");

            GetUI("AudioOption").SetActive(false);
        }
       
    }


    /// <summary>
    /// UI 이벤트 구독
    /// </summary>
    private void SubscribeEvent()
    {
        // 오디오 설정버튼 이벤트 구독
        GetUI<Button>("AudioButton").onClick.AddListener(ToggleAudioOption);

        // Audio 슬라이더 이벤트 구독
        GetUI<Slider>("MasterVolume").onValueChanged.AddListener(SetVolumeMaster);
        GetUI<Slider>("BGMVolume").onValueChanged.AddListener(SetVolumeBGM);
        GetUI<Slider>("SFXVolume").onValueChanged.AddListener(SetVolumeSFX);

        // 메뉴 On/Off 이벤트 구독
        GetUI<Button>("BackButton").onClick.AddListener(ToggleGameOptionUI);
        GetUI<Button>("MenuButton").onClick.AddListener(ToggleGameOptionUI);
    }


}
