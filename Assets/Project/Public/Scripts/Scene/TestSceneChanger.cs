using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TestSceneChanger : MonoBehaviour
{

    public static TestSceneChanger Instance;

    [Header("플레이어 전용 씬")]
    public SceneField _playerScene;
    [Header("최초 스테이지")]
    [SerializeField] SceneField _firstStage;

    [SerializeField] public SceneLoadTrigger CurSceneTrigger;
    public event UnityAction OnChangeCurSceneTrigger;

    private void Awake()
    {
        if (Instance == null && SceneChanger.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitGameScene();
    }


    /// <summary>
    /// 일회용 트랩이 사용됬는지 체크
    /// </summary>
    /// <returns></returns>
    public bool CheckKeepingTrap(string key)
    {
        if (Manager.Game.DisPosableDic.ContainsKey(key) == false)
        {
            Manager.Game.DisPosableDic.Add(key, true);
        }
        return Manager.Game.DisPosableDic[key];
    }

    /// <summary>
    /// 일회용 트랩이 사용됨을 저장
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetKeepingTrap(string key, bool value)
    {
        if (Manager.Game.DisPosableDic.ContainsKey(key))
        {
            Manager.Game.DisPosableDic[key] = value;
        }
    }

    /// <summary>
    /// 초기 게임 씬 로드
    /// </summary>
    public void InitGameScene()
    {
        AsyncOperation playerSceneOper = SceneManager.LoadSceneAsync(_playerScene);
        playerSceneOper.allowSceneActivation = true;
        AsyncOperation firstSceneOper = SceneManager.LoadSceneAsync(_firstStage, LoadSceneMode.Additive);
        firstSceneOper.allowSceneActivation = true;
        //StartCoroutine(LoadSceneRoutine());
    }

    public void SetCurSceneTrigger(SceneLoadTrigger sceneTrigger)
    {
        CurSceneTrigger = sceneTrigger;
        OnChangeCurSceneTrigger?.Invoke();
    }
}
