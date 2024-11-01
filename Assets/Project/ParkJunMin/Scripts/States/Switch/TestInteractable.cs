using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : SwichInteractable
{
    [SerializeField] GameObject gameObject;
    private GameObject interactiveObject;
    public override void Interact()
    {
        interactiveObject = Instantiate(gameObject);
        interactiveObject.SetActive(true);
    }
}