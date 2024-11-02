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
        StartCoroutine(WarpRoutine());       
    }
   
    IEnumerator WarpRoutine()
    {
        SceneLoadTrigger instance = Instantiate(_loadTrigger, destinationPos, Quaternion.identity);
        instance.AddSceneToLoad(SceneToLoad);
        yield return null;
        playerPos.transform.position = destinationPos;  
        instance.Delete();

    }
}
