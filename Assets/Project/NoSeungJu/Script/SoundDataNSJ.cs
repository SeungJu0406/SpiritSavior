using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundData : ScriptableObject 
{
    public partial struct UI
    {
        public AudioClip MapOpenSound;
    }
    public AudioClip MapOpenSound { get {  return _uI.MapOpenSound; } }
}
