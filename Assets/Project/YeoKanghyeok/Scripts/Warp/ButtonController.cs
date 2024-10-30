using System.Collections;
using System.Collections.Generic;
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
        ActiveButton = false;
    }
    public void OnActive()
    {
        gameObject.SetActive(true);
    }

    public void OffActive()
    {
        gameObject.SetActive(false);
    }

    public void Warp()
    {
        _player.transform.position = Destinasion.position;
    }
}
