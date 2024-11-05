using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGMPlayer : MonoBehaviour
{
    private void Start()
    {
        Manager.Sound.StopBGM();
        PlayTitleBGM();
    }

    void PlayTitleBGM()
    {
        Manager.Sound.PlayBGM(Manager.Sound.Data.TitleBGM);
    }
}

