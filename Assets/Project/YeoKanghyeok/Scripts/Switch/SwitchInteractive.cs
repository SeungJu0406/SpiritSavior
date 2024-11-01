using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteractive : SwichInteractable
{
    [Header("사용자지정")]
    [SerializeField] GameObject gameObject;
    [SerializeField] Transform apperPos;
    private GameObject interactiveObject;
    public override void Interact()
    {
        interactiveObject = Instantiate(gameObject,apperPos);
        interactiveObject.SetActive(true);
    }
}