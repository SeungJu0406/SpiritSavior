using System.Collections.Generic;
using UnityEngine;

public class WarpPointController : MonoBehaviour
{
    [SerializeField] private GameObject _warpUI;
    [SerializeField] private Material unActiveWarp;
    [SerializeField] private Material ActiveWarp;
    [SerializeField] private bool _inWarp;
    [SerializeField] private bool _warpActive;
    [SerializeField] private bool _warpUIActive;
    private Material material;

    void Start()
    {
        _inWarp = false; // warp ���˿���
        _warpActive = false; // warp Ȱ��ȭ
        _warpUIActive = false; // warp UI Ȱ��ȭ ����
        _warpUI.SetActive(false); // warp UI ��������
    }

    private void OnTriggerEnter2D(Collider2D collision) // WarpPoint ������Ʈ�� �浹�ϴµ���
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
        if (_warpActive)
        {
            
        }
    }
}