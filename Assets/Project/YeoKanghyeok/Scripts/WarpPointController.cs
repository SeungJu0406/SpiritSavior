using System.Collections.Generic;
using UnityEngine;

public class WarpPointController : MonoBehaviour
{
    [SerializeField] private GameObject _warpUI;
    private bool _inWarp;
    private bool _warpActive;
    private bool _warpUIActive;

    void Start()
    {
        _inWarp = false; // warp 접촉여부
        _warpActive = false; // warp 활성화
        _warpUIActive = false; // warp UI 활성화 여부
        _warpUI.SetActive(false); // warp UI 생성여부
    }

    private void OnTriggerEnter2D(Collider2D collision) // WarpPoint 오브젝트와 충돌하는동안
    {
        if (collision.gameObject.tag == "Player")
        {
            _inWarp = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inWarp = false;
        }
    }

    private void Update()
    {
        if (_inWarp)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (!_warpActive) { _warpActive = true; }
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
            _inWarp = false;
        }
    }
}
