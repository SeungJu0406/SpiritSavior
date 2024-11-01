using System.Collections;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [Header("사용자지정")]
    [SerializeField] string pointName;
    [SerializeField] ButtonController buttonPrefab;
    [SerializeField] SceneField _pointScene;

    [Space(10f)]
    [SerializeField] GameObject buttonCanvas;
    [SerializeField] Transform buttonLayout;
    [SerializeField] ParticleSystem unActiveParticle;
    [SerializeField] ParticleSystem activeParticle;
    private bool _pointActive; // point 활성화 여부
    private bool _uiActive; // ui canvas 활성화 여부
    private ButtonController _button;

    void Start()
    {   
        _pointActive = false;

        unActiveParticle.gameObject.SetActive(true);
        activeParticle.gameObject.SetActive(false);

        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        yield return null;
        buttonCanvas = Manager.UI.WarpUI;
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

        _button = Instantiate(buttonPrefab, buttonCanvas.transform);
        _button._buttonText.text = pointName;
        _button.destinationPos = transform.position;
        _button.PointScene = _pointScene;
    }
}
