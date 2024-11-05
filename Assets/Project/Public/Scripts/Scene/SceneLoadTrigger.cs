using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _sceneToLoad;

    SceneField _DontUnLoadScene;
    BoxCollider2D _boxCollider2D;
    bool _canUnload = true;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (SceneChanger.Instance != null) 
        {
            _DontUnLoadScene= SceneChanger.Instance._playerScene;
            SceneChanger.Instance.OnChangeCurSceneTrigger += CheckCurSceneTrigger;
        }
        else if(TestSceneChanger.Instance != null)
        {
            _DontUnLoadScene = TestSceneChanger.Instance._playerScene;
            TestSceneChanger.Instance.OnChangeCurSceneTrigger += CheckCurSceneTrigger;
        }
      StartCoroutine(StartRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Manager.Game.Player.gameObject.tag)
        {
            LoadScene();
            UnloadScene();
            SetCurSceneTrigger();
        }
    }

    public void AddSceneToLoad(SceneField[] sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
        _canUnload = false;
    }

    public void Delete()
    {
        StartCoroutine(DeleteRoutine());
    }

    WaitForSeconds SceneTriggerDeleteDelay = new WaitForSeconds(1f);
    IEnumerator DeleteRoutine()
    {
        yield return SceneTriggerDeleteDelay;
        Destroy(gameObject);
    }

    void LoadScene()
    {
        for (int i = 0; i < _sceneToLoad.Length; i++) 
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++) 
            {
                Scene loadedScneen = SceneManager.GetSceneAt(j);
                if(loadedScneen.name == _sceneToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }
            if (!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_sceneToLoad[i], LoadSceneMode.Additive);
            }
        }
    }

    void UnloadScene()
    {
        if (_canUnload == false) return;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            bool isLoadScene = false;
            Scene loadedScene = SceneManager.GetSceneAt(i);
            // 씬이 절대 언로드되선 안되는 씬이랑 같음?
            if (loadedScene.name == _DontUnLoadScene.SceneName)
            {
                isLoadScene = true;
            }
            else
            {
                // 씬이 로드해야하는 씬이랑 같음?
                for (int j = 0; j < _sceneToLoad.Length; j++)
                {
                    if (loadedScene.name == _sceneToLoad[j].SceneName)
                    {
                        isLoadScene = true;
                    }
                }
            }

            // 로드해야할 씬이 아니면 언로드
            if(isLoadScene == false)
            {
                SceneManager.UnloadSceneAsync(loadedScene);
            }
        }
    }

    IEnumerator StartRoutine()
    {
        yield return null;
        CheckCurSceneTrigger();
    }

    void CheckCurSceneTrigger()
    {
        if (_boxCollider2D == null) return;
        if (SceneChanger.Instance != null)
        {
            if (SceneChanger.Instance.CurSceneTrigger == this)
                _boxCollider2D.enabled = false;
            else
                _boxCollider2D.enabled = true;
        }
        else if (TestSceneChanger.Instance != null)
        {
            if (TestSceneChanger.Instance.CurSceneTrigger == this)
                _boxCollider2D.enabled = false;
            else
                _boxCollider2D.enabled = true;

        }
    }
    void SetCurSceneTrigger()
    {
        if (SceneChanger.Instance != null)
        {
            SceneChanger.Instance.SetCurSceneTrigger(this);
        }
        else if (TestSceneChanger.Instance != null)
        {
            TestSceneChanger.Instance.SetCurSceneTrigger(this);
        }
    }

}
