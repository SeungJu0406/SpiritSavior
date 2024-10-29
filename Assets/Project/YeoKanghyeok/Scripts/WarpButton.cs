using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpButton : MonoBehaviour
{
    [SerializeField] GameObject goalObject;
    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    public void Warp()
    {
        playerObject.transform.position = goalObject.transform.position;
    }
}
