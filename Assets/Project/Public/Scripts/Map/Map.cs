using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    // Map 부분
    private PlayerController _playerController;
    public GameObject redFace;
    public GameObject blueFace;
    public GameObject mapImage;

    // Map애니메이션 부분
    public RawImage map;
    public float transparencyTime = 1.0f;
    private bool _isTransparency = false;

    void Start()
    {
        map.color = new Color(map.color.r, map.color.g, map.color.b, 0); // 투명도 0
        redFace.SetActive(false);
        blueFace.SetActive(false);

        _playerController = FindObjectOfType<PlayerController>();
    }
    public void FaceMap()
    {
        if (_playerController != null)
        {
            // 색상 확인
            if (_playerController.playerModel.curNature == PlayerModel.Nature.Red)
            {
                redFace.SetActive(true);
                blueFace.SetActive(false);
            }
            else if (_playerController.playerModel.curNature == PlayerModel.Nature.Blue)
            {
                redFace.SetActive(false);
                blueFace.SetActive(true);
            }

        }
    }

    private IEnumerator TransparencyImage(float start, float end)
    {
        _isTransparency = true;
        float time = 0;
        Color color = map.color;

        while (time < transparencyTime)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(start, end, time / transparencyTime);
            map.color = color;
            yield return null;
        }

        _isTransparency = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !_isTransparency)
        {
            if (map.color.a > 0)
            {
                StartCoroutine(TransparencyImage(1, 0));
                redFace.SetActive(false);
                blueFace.SetActive(false);
            }
            else
            {
                StartCoroutine(TransparencyImage(0, 1));
                FaceMap();
            }
        }
        if (map.color.a > 0)
        {
            FaceMap();
        }
    }
}