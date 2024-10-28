using UnityEngine;

public class WarpPointController : MonoBehaviour
{
    [SerializeField] private GameObject _warpUI;
    private bool _warpActivate;
    private bool _warpUIActive;

    void Start()
    {
        _warpActivate = false; // warp 충돌여부
        _warpUIActive = false; // warp UI 활성화 여부
        _warpUI.SetActive(false); // warp UI 생성여부
    }

    private void OnTriggerEnter2D(Collider2D collision) // WarpPoint 오브젝트와 충돌하는동안
    {
        if (collision.gameObject.tag == "Player")
        {
            _warpActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _warpActivate = false;
        }
    }

    private void Update()
    {
        if (_warpActivate)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (_warpUIActive)
                {
                    _warpUI.SetActive(false);
                    _warpUIActive = false;
                }
                else
                {
                    _warpUI.SetActive(true);
                    _warpUIActive = true;
                }
            }
        }
        else
        {
            _warpUI.SetActive(false);
            _warpActivate = false;
        }
    }
}
