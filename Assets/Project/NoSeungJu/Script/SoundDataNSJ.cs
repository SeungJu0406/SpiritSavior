using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundData : ScriptableObject 
{
    [Header("BGM")]
    public AudioClip TitleBGM;
    public AudioClip GameBGM;

    [Header("UI ฐüทร")]
    public AudioClip HighlightSound;
    public AudioClip ClickSound;
    public AudioClip GameStartSound;
}
