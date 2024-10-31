using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] public bool ActiveButton;
    [SerializeField] public Transform Destinasion;
    [SerializeField] PlayerController _player;
    private void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        _player = playerObject.GetComponent<PlayerController>();
        
    }

    public void Warp()
    {
        _player.transform.position = Destinasion.position;
    }
}
