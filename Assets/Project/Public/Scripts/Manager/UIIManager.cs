using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIManager : MonoBehaviour
{
    public static UIIManager Instance;

    public GameObject WarpUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}