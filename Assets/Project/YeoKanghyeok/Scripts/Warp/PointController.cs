using System.Collections;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] Transform buttonLayout;
    [SerializeField] ParticleSystem unActiveParticle;
    [SerializeField] ParticleSystem activeParticle;
    [SerializeField] bool _pointActive; // point 활성화 여부
    [SerializeField] bool _uiActive; // ui canvas 활성화 여부
    private GameObject _buttonObject;
    public ParticleSystem ps;
    public ParticleSystem.MainModule psMain;
    public ButtonController _button;
    private Transform _transform;
    void Start()
    {
        _pointActive = false;

        psMain = ps.main;
        ps = unActiveParticle;

        buttonCanvas.SetActive(false);

        _transform = GetComponent<Transform>();


    }

    #region point 접근여부
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inputRoutine = _inputRoutine == null ? StartCoroutine(InputRoutine()) : _inputRoutine;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_inputRoutine != null)
            {
                StopCoroutine(_inputRoutine);
                _inputRoutine = null;
            }

            buttonCanvas.SetActive(false);
            _uiActive = false;
        }
    }
    #endregion

    Coroutine _inputRoutine;
    IEnumerator InputRoutine()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!_pointActive)
                {
                    ActivePoint();
                    _pointActive = true;
                }


                if (!_uiActive)
                {
                    buttonCanvas.SetActive(true);
                    _uiActive = true;
                }
                else if (_uiActive)
                {
                    buttonCanvas.SetActive(false);
                    _uiActive = false;
                }
            }
            yield return null;
        }
    }

    private void ActivePoint()
    {
        ps = activeParticle;

        _buttonObject = Instantiate(buttonPrefab, buttonLayout) as GameObject; // button 생성
        _button = _buttonObject.GetComponent<ButtonController>();
        _button.Destinasion = _transform;
        _button.Destinasion = _transform;
        _button.Destinasion.position = _transform.position; // point 위치 버튼 목적지에 대입
    }
}
