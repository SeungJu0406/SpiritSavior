using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Trap
{
    [SerializeField] SwichInteractable _swichInteractable;

    Coroutine _enterTriggerRoutine;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        _enterTriggerRoutine = _enterTriggerRoutine == null ? StartCoroutine(EnterTriggerRoutine()) : _enterTriggerRoutine;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_enterTriggerRoutine != null)
        {
            StopCoroutine(_enterTriggerRoutine);
            _enterTriggerRoutine = null;
        }
    }

    void Interact()
    {
        Debug.Log("인터렉트");
        _swichInteractable.Interact();
    }

    IEnumerator EnterTriggerRoutine()
    {
        while (true)
        {
            Debug.Log("루프");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("입력");
                Interact();
                UnActiveTrap();
                gameObject.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
}
