using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController Player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
