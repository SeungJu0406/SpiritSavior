using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    protected void Start()
    {
        GetUI<Button>("RespawnButton").onClick.AddListener(RespawnPlayer); // 리스폰 버튼에 RespawnPlayer 메서드 구독
        Manager.Game.Player.playerModel.OnPlayerDied += ShowGameOverUI; // 플레이어가 죽으면 게임오버 UI가 나타남

        HideGameOverUI();
    }
    /// <summary>
    /// RespawnPlayer 버튼을 누르면 캐릭터가 부활
    /// </summary>
    public void RespawnPlayer()
    {
        Manager.Game.Player.gameObject.SetActive(true);
        HideGameOverUI();
    }

    /// <summary>
    /// 게임오버 UI 나타내기
    /// </summary>
    public void ShowGameOverUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 게임오버 UI 숨기기
    /// </summary>
    public void HideGameOverUI()
    {
        gameObject.SetActive(false);
    }
}
