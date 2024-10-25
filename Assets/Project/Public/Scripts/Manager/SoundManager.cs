using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사운드 매니저
/// Manager.Sound 를 통해서 사용
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _bgm;
    [SerializeField] private AudioSource _sfx;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// BGM 실행
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBGM(AudioClip clip)
    {
        _bgm.clip = clip;
        _bgm.Play();
    }

    /// <summary>
    /// BGM 정지
    /// </summary>
    public void StopBGM() 
    {
        if (_bgm.isPlaying)
        {
            _bgm.Stop();
        }
    }

    /// <summary>
    /// BGM 일시정지
    /// </summary>
    public void PauseBGM()
    {
        if (_bgm.isPlaying)
        {
            _bgm.Pause();
        }
    }

    /// <summary>
    /// BGM 볼륨 조절
    /// </summary>
    /// <param name="volume"></param>

    public void SetVolumeBGM(float volume)
    {
        _bgm.volume = volume;
    }

    /// <summary>
    /// SFX 실행
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySFX(AudioClip clip) 
    {
        _sfx.PlayOneShot(clip);
    }

    /// <summary>
    /// SFX 볼륨 조절
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolumeSFX(float volume)
    {
        _sfx.volume = volume;
    }
}
