using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapImage;

    void Start()
    {
        mapImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapImage.SetActive(!mapImage.activeSelf);
        }
    }
}