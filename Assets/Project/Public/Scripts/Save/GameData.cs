using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int playerHp; // 플레이어의 HP
    public Vector3 playerPosition; // 플레이어의 위치
    public int currentStage; // 현재 스테이지
    public int unlockedAbilities; // 능력 어빌리티
    public bool[] trapStates; // 함정 상태 배열
}
    
