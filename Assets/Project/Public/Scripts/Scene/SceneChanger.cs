using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    [Header("�÷��̾� ���� ��")]
    public SceneField _playerScene;
    [Header("���� ��������")]
    [SerializeField] SceneField _firstStage;

    [HideInInspector] public SceneLoadTrigger CurSceneTrigger;
    public event UnityAction OnChangeCurSceneTrigger;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// ��ȸ�� Ʈ���� ������� üũ
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
    /// ��ȸ�� Ʈ���� ������ ����
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
    /// �ʱ� ���� �� �ε�
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
    //IEnumerator LoadSceneRoutine()
    //{ 

    //}
}