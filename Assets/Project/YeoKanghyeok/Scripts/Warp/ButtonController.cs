using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] public Text _buttonText;
    private Transform playerPos;
    public Vector2 destinationPos;
    private void Start()
    {
        playerPos = Manager.Game.Player.transform;
    }

    public void Warp()
    {
        playerPos.transform.position = destinationPos;
    }
}
