using System.Collections;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [Header("사용자지정")]
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] Transform buttonLayout;
    [SerializeField] string pointName;
    [SerializeField] GameObject buttonPrefab;

    [Space (10f)]
    [SerializeField] ParticleSystem unActiveParticle;
    [SerializeField] ParticleSystem activeParticle;
    private bool _pointActive; // point 활성화 여부
    private bool _uiActive; // ui canvas 활성화 여부
    private GameObject _buttonObject;
    private ButtonController _button;
    void Start()
    {
        _pointActive = false;

        unActiveParticle.gameObject.SetActive(true);
        activeParticle.gameObject.SetActive(false);

        buttonCanvas.SetActive(false);
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
        unActiveParticle.gameObject.SetActive(false);
        activeParticle.gameObject.SetActive(true);

        _buttonObject = Instantiate(buttonPrefab, buttonLayout) as GameObject; // button 생성
        _button = _buttonObject.GetComponent<ButtonController>();
        _button._buttonText.text = pointName;
        _button.destinationPos = transform.position;
    }
}
