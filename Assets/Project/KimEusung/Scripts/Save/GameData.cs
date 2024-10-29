using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int playerHp;             // 플레이어의 생명갯수
    public Vector3 playerPosition; // 플레이어의 위치를 저장함 (상황에맞게 사용 Vector3, Vector2)
    //public Vector2 playerPosition;
    public List<string> items;       // 플레이어 아이템 목록 (나중에 구현필요)
    public List<bool> traps;         // 맵 함정들의 사용 유무 (나중에 구현 필요)

    /*====================================추후 저장할것이 있으면 추가===========================================*/
}
    
