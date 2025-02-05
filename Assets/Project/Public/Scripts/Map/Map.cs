using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Map �κ�
    private PlayerController _playerController;
    [SerializeField] private GameObject _faceRed;
    [SerializeField] private GameObject _faceBlue;
    [SerializeField] private GameObject _mapCam;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask miniMapLayer;

    // Map�ִϸ��̼� �κ�
    [SerializeField] private RawImage _mapImage;
    public float transparencyTime = 1.0f;
    private bool _isTransparency = false;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // �⺻ ī�޶�� ����
        }

        mainCamera.cullingMask &= ~miniMapLayer; // MiniMap Layer out
    }
    void Start()
    {
        _playerController = Manager.Game.Player.GetComponent<PlayerController>();
        _mapImage.color = new Color(_mapImage.color.r, _mapImage.color.g, _mapImage.color.b, 0); // ������ 0
        _faceRed.SetActive(false);
        _faceBlue.SetActive(false);
        ChildSettings();

    }
    private void ChildSettings()
    {
        // FaceRed, FaceBlue, Camera�� Player �ڽ����� ����
        Transform faceRedTransform = _faceRed.transform;
        Transform faceBlueTransform = _faceBlue.transform;
        Transform cameraTransform = _mapCam.transform;

        // Player �ڽ����� ����
        faceRedTransform.SetParent(Manager.Game.Player.transform);
        faceBlueTransform.SetParent(Manager.Game.Player.transform);
        cameraTransform.SetParent(Manager.Game.Player.transform);

        // Player ��ġ�� ����
        faceRedTransform.localPosition = new Vector3(0.23f, 4.28f, 0);
        faceBlueTransform.localPosition = new Vector3(0.23f, 4.28f, 0);
        cameraTransform.localPosition = new Vector3(0.15f, 4.11f, -86.49741f);
    }
    public void FaceMap()
    {
        if (_playerController != null)
        {
            // ���� Ȯ��
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

        if (end == 0) // ������ On
        {
            _faceRed.SetActive(false);
            _faceBlue.SetActive(false);
             // Minimap layer out
        }
        else // ������ Off
        {
            FaceMap();
        }

        _isTransparency = false;
    }
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isTransparency)
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