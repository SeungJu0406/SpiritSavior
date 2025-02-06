using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts.States.Switch;
using UnityEngine;

public class SwitchInteractive : SwichInteractable
{
    [Header("���������")]
    [SerializeField] GameObject _interactiveObject;
    
    
    IEnumerator unActiveObject()
    {
        yield return null;
        _interactiveObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(unActiveObject());
    }
    public override void Interact()
    {
        _interactiveObject.SetActive(!_interactiveObject.activeSelf);
    }
}