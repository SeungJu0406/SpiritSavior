using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager
{
    public static GameManager Game { get { return GameManager.Instance; }}
    public static SoundManager Sound { get {  return SoundManager.Instance; } }
}
