using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Map 부분
    private PlayerController _playerController;
    [SerializeField] private GameObject _faceRed;
    [SerializeField] private GameObject _faceBlue;
    [SerializeField] private GameObject _mapCam;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask miniMapLayer;

    // Map애니메이션 부분
    [SerializeField] private RawImage _mapImage;
    public float transparencyTime = 1.0f;
    private bool _isTransparency = false;

    // 자식 이동
    [SerializeField] private GameObject _player;
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 기본 카메라로 설정
        }

        mainCamera.cullingMask &= ~miniMapLayer; // MiniMap Layer out
    }
    void Start()
    {
        _mapImage.color = new Color(_mapImage.color.r, _mapImage.color.g, _mapImage.color.b, 0); // 투명도 0
        _faceRed.SetActive(false);
        _faceBlue.SetActive(false);

        _playerController = FindObjectOfType<PlayerController>();
        ChildSettings();

        
    }
    private void ChildSettings()
    {
        // FaceRed, FaceBlue, Camera를 Player 자식으로 설정
        Transform faceRedTransform = _faceRed.transform;
        Transform faceBlueTransform = _faceBlue.transform;
        Transform cameraTransform = _mapCam.transform;

        // Player 자식으로 설정
        faceRedTransform.SetParent(_player.transform);
        faceBlueTransform.SetParent(_player.transform);
        cameraTransform.SetParent(_player.transform);

        // Player 위치로 설정
        faceRedTransform.localPosition = new Vector3(0.23f, 4.28f, 0);
        faceBlueTransform.localPosition = new Vector3(0.23f, 4.28f, 0);
        cameraTransform.localPosition = new Vector3(0.15f, 4.11f, -86.49741f);
    }
    public void FaceMap()
    {
        if (_playerController != null)
        {
            // 색상 확인
            bool isRed = _playerController.playerModel.curNature == PlayerModel.Nature.Red;
            _faceRed.SetActive(isRed);
            _faceBlue.SetActive(!isRed);
        }
    }
    private IEnumerator TransparencyImage(float start, float end)
    {
        _isTransparency = true;
        float time = 0;
        Color color = _mapImage.color;

        while (time < transparencyTime)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, time / transparencyTime);
            _mapImage.color = color;
            yield return null;
        }

        if (end == 0) // 지도가 On
        {
            _faceRed.SetActive(false);
            _faceBlue.SetActive(false);
             // Minimap layer out
        }
        else // 지도가 Off
        {
            FaceMap();
        }

        _isTransparency = false;
    }
void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !_isTransparency)
        {
            if (_mapImage.color.a > 0)
            {
                StartCoroutine(TransparencyImage(1, 0));
            }
            else
            {
                StartCoroutine(TransparencyImage(0, 1));
            }
        }
        if (_mapImage.color.a > 0)
        {
            FaceMap();
        }
    }
}