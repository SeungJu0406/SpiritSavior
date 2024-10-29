using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpButton : MonoBehaviour
{
    public GameObject warpObject;
    private GameObject _playerObject;

    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
    }
    public void Warp()
    {
        _playerObject.transform.position = warpObject.transform.position;
    }
}
