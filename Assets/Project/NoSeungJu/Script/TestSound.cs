using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    
    public void PlaySFX(AudioClip clip)
    {
        Manager.Sound.PlaySFX(clip);
    }
    
    public void SetVolume1SFX(float volume)
    {
        Manager.Sound.SetVolumeSFX(volume);
    }
}
