using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WarpButtonController : MonoBehaviour
{
    [SerializeField] GameObject _warpPoint;
    private WarpPointController _controller;
    private GameObject _playerObject;

    private void Start()
    {
        Debug.Log("start");
        gameObject.SetActive(false);
        _controller = _warpPoint.GetComponent<WarpPointController>();
        _playerObject = GameObject.FindWithTag("Player");
        StartCoroutine(UpdateRoutine());
    }
    IEnumerator UpdateRoutine()
    {
        while (true)
        {
            Debug.Log(_controller.warpActive);
            Debug.Log(_controller.warpUIActive);
            if (_controller.warpUIActive)
            {
                if (_controller.warpActive)
                {
                    gameObject.SetActive(true);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    public void Warp()
    {
        _playerObject.transform.position = _warpPoint.transform.position;
    }
}
