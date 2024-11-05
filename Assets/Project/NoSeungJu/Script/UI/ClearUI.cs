using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearUI : BaseUI
{
    PlayableDirector _timelineDirector;

    protected override void Awake()
    {
        base.Awake();
        _timelineDirector = GetComponent<PlayableDirector>();
    }
    private void OnEnable()
    {
        _timelineDirector.Play();
    }

    private void Start()
    {
        Init();
        SubscribesEvents();
    }

    void BackTitleScene()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.ButtonClickSound);

        SceneManager.LoadSceneAsync("TitleScene");
    }

    private void SubscribesEvents()
    {
        GetUI<Button>("MainButton").onClick.AddListener(BackTitleScene);
    }

    private void Init()
    {
        
    }
}
