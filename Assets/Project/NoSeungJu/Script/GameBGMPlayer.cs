using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGMPlayer : MonoBehaviour
{
    private void Start()
    {
        Manager.Sound.StopBGM();
        PlayGameBGM();
    }

    void PlayGameBGM()
    {
        Manager.Sound.PlayBGM(Manager.Sound.Data.GameBGM);
    }
}
