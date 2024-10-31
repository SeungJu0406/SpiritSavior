using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public GameObject redFace;
    public GameObject blueFace;
    public GameObject mapImage;

    private PlayerController _playerController;
    void Start()
    {
        mapImage.SetActive(false);
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapImage.SetActive(!mapImage.activeSelf);
            if (!mapImage.activeSelf)
            {
                redFace.SetActive(false);
                blueFace.SetActive(false);
            }
            FaceMap();
        }
    }
}