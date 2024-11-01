using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] public Text _buttonText;
    [SerializeField] public SceneField PointScene;
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
        instance.AddSceneToLoad(PointScene);
        yield return null;
        playerPos.transform.position = destinationPos;
        yield return null;
        Destroy(instance.gameObject);
    }
}
