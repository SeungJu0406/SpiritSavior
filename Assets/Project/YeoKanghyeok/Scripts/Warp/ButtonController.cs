using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _buttonText;
    [SerializeField] public SceneField[] SceneToLoad;
    [SerializeField] SceneLoadTrigger _loadTrigger;

    private Transform playerPos;
    public Vector2 destinationPos;
    private void Start()
    {
        playerPos = Manager.Game.Player.transform;
    }

    public void Warp()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.WarpButtonClickSound); // 2.5 워프리스트에서 워프 버튼 클릭 시 소리
        StartCoroutine(WarpRoutine());       
    }
   
    IEnumerator WarpRoutine()
    {
        SceneLoadTrigger instance = Instantiate(_loadTrigger, destinationPos, Quaternion.identity);
        instance.AddSceneToLoad(SceneToLoad);
        instance.IsInstance = true;
        yield return null;
        playerPos.transform.position = destinationPos;
        instance.Delete();
    }
}
