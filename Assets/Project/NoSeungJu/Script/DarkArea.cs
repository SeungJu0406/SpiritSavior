using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class DarkArea : Trap
{
    [Header("토글 형식?")]
    [SerializeField] bool _isToggle;

    [Space]
    [SerializeField] float _fadeTime = 0.5f;

    SpriteRenderer[] _areas;


    Coroutine _showRoutine;
    Coroutine _hideRoutine;
    private void Awake()
    {
        _areas = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in _areas)
        {
            sprite.sortingLayerName = "Player";
            sprite.sortingOrder = 10;
        }
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Active();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_isToggle)
            {
                UnActive();
            }
        }
    }

    protected override void ProcessActive()
    {
        // 하위 스프라이트 오브젝트들 비활성화
        if(_showRoutine != null)
        {
            StopCoroutine(_showRoutine);
            _showRoutine = null;
        }

         _hideRoutine = _hideRoutine== null? StartCoroutine(HideRoutine()) : _hideRoutine;
           
    }


    IEnumerator HideRoutine()
    {
        float aValue = 1;
        if (_areas.Length > 0)
            aValue = _areas[0].color.a;
        while (true)
        {
            aValue -= Time.deltaTime * (1 / _fadeTime);
            foreach (SpriteRenderer sprite in _areas)
            {
                Color color = new Color(); 
                color = Color.white;
                color.a = aValue;         
                sprite.color = color;
            }
            if (_areas.Length > 0 && _areas[0].color.a <= 0.1f)
                break;
            yield return null;
        }
        foreach (SpriteRenderer sprite in _areas)
        {
            sprite.gameObject.SetActive(false);
        }
    }


    protected override void ProcessUnActive()
    {
        // 하위 스프라이트 오브젝트들 활성화
        if (_hideRoutine != null)
        {
            StopCoroutine(_hideRoutine);
            _hideRoutine = null;
        }    
        _showRoutine = _showRoutine == null? StartCoroutine(ShowRoutine()) : _showRoutine;
    }


    IEnumerator ShowRoutine()
    {
        float aValue = 0;
        if (_areas.Length > 0)
            aValue = _areas[0].color.a;

        foreach (SpriteRenderer sprite in _areas)
        {
            sprite.gameObject.SetActive(true);
        }
        while (true)
        {
            aValue += Time.deltaTime * (1 / _fadeTime);
            foreach (SpriteRenderer sprite in _areas)
            {
                Color color = new Color();
                color = Color.white;
                color.a = aValue;
                sprite.color = color;
            }
            if (_areas.Length > 0 && _areas[0].color.a >=0.9f )
                break;
            yield return null;
        }

    }


}
