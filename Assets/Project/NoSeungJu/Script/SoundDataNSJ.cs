using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundData : ScriptableObject 
{
    public partial struct BGM
    {
        public AudioClip TestBGM;
    }
    public AudioClip TestBGM { get { return _bgm.TestBGM; } }
}
