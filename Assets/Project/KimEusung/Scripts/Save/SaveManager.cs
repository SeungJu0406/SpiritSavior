using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath; // 저장할 파일 경로

    void Start()
    {
        // 저장 파일 경로
        saveFilePath = Path.Combine(Application.persistentDataPath, "savedata.json");
    }

    // 게임 저장
    public void SaveGame(int playerLives, Vector3 playerPosition /*, List<string> items, List<bool> traps */)
    {
        GameData gameData = new GameData
        {
            playerHp = playerLives,
            playerPosition = playerPosition,
            // items = items, // 아이템 목록 (추후 구현)
            // traps = traps // 함정 상태 (추후 구현)
        };

        // JSON으로 변환하여 파일에 저장
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("저장 완료"); // 저장 완료 로그
    }

    // 게임 불러오기
    public GameData LoadGame()
    {
        // 저장 파일 존재 확인
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath); // 파일 읽기
            GameData gameData = JsonUtility.FromJson<GameData>(json); // JSON을 객체로 변환
            Debug.Log("불러오기를 성공했습니다.");
            return gameData; // 데이터 반환
        }
        // 파일이 없으면
        else
        {
            Debug.LogWarning("파일을 찾을 수 없습니다.");
            return null;
        }
    }
}