using UnityEngine;

public class PointController : Warp
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonCanvas;
    [SerializeField] Material unActiveMaterial;
    [SerializeField] Material activeMaterial;
    [SerializeField] bool _inPoint; // point 접근여부
    [SerializeField] bool _pointActive; // point 활성화 여부
    private GameObject _buttonObject;
    private SpriteRenderer _spriteRenderer;
    public ButtonController _button;
    private Transform _transform;
    void Start()
    {
        _inPoint = false;
        _pointActive = false;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = unActiveMaterial;

        _transform = GetComponent<Transform>();
    }

    #region point 접근여부
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _inPoint = false;
        }
    }
    #endregion


    void Update()
    {
        if (_inPoint)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!_pointActive) // point활성화 및 button 생성
                {
                    ActivePoint();
                    _pointActive = true;
                }

                if (!uiActive)
                {
                    uiActive = true;
                    OnUI();
                }
                else if (uiActive)
                {
                    uiActive = false;
                    OffUI();
                }
            }
        }
        else
        {
            uiActive = false;
            OffUI() ;
        }
    }

    private void ActivePoint()
    {
        _buttonObject = Instantiate(buttonPrefab,buttonCanvas) as GameObject; // button 생성
        _button = _buttonObject.GetComponent<ButtonController>();
        _button.Destinasion.position = _transform.position; // point 위치 버튼 목적지에 대입
        _button.ActiveButton = true;
    }
}
