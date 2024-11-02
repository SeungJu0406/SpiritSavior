using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StageUI : BaseUI
{
    int _curStage;
    private void Start()
    {
        SubscribesEvents();
        Init();
     
     }

    private void UpdateStage(int stage)
    {
        GameObject unClearIcon = GetUI($"{stage}StageUnClear");
        GameObject clearIcon = GetUI($"{stage}StageClear");
        bool isClear = Manager.Game.GetIsClearStageDIc(stage);
        if (isClear)
        {
            unClearIcon.SetActive(false);
            clearIcon.SetActive(true);
        }
        else
        {
            unClearIcon.SetActive(true);
            clearIcon.SetActive(false);
        }

        UpdateClearParticle(stage);
    }

    void UpdateClearParticle(int stage)
    {

        if (stage > _curStage)
        {
            _curStage = stage;
            Color color = GetUI<ParticleSystem>("StageParticle").startColor;
            color.a = (0.8f / Manager.Game.MaxStage) * _curStage;
            GetUI<ParticleSystem>("StageParticle").startColor = color;
        }
    }

    private void Init()
    {
        GetUI("1StageUnClear").SetActive(true);
        GetUI("1StageClear").SetActive(false);

        GetUI("2StageUnClear").SetActive(true);
        GetUI("2StageClear").SetActive(false);

        GetUI("3StageUnClear").SetActive(true);
        GetUI("3StageClear").SetActive(false);

        GetUI("4StageUnClear").SetActive(true);
        GetUI("4StageClear").SetActive(false);

        GetUI("StageParticle").SetActive(true);
    }

    private void SubscribesEvents()
    {
        Manager.Game.OnChangeIsClearStage += UpdateStage;
    }

}
