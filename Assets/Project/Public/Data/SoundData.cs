using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound")]
public partial class SoundData : ScriptableObject
{
    #region BGM
    [System.Serializable]
    public partial struct BGM
    {
        public AudioClip TitleBGM;
        public AudioClip GameBGM;
    }
    [Header("BGM")]
    [SerializeField] BGM _bgm;
    public AudioClip TitleBGM { get { return _bgm.TitleBGM; } }
    public AudioClip GameBGM { get { return _bgm.GameBGM; } }
    #endregion
    #region Player
    [System.Serializable]
    public partial struct Player
    {
        public AudioClip RunningSound;
        public AudioClip JumpSound;
        public AudioClip LandingSound;
        public AudioClip DoubleJumpSound;
        public AudioClip WallClimbSound;
        public AudioClip DashSound;
        public AudioClip HitSound;
        public AudioClip DeathSound;
        public AudioClip SpawnSound;
        public AudioClip TagSound;
    }
    [Header("플레이어 관련")]
    [SerializeField] Player _player;
    public AudioClip RunningSound { get { return _player.RunningSound; } }
    public AudioClip JumpSound { get { return _player.JumpSound; } }
    public AudioClip LandingSound { get { return _player.LandingSound; } }
    public AudioClip DoubleJumpSound { get { return _player.DoubleJumpSound; } }
    public AudioClip WallClimbSound { get { return _player.WallClimbSound; } }
    public AudioClip DashSound { get { return _player.DashSound; } }
    public AudioClip HitSound { get { return _player.HitSound; } }
    public AudioClip DeathSound { get { return _player.DeathSound; } }
    public AudioClip SpawnSound { get { return _player.SpawnSound; } }
    public AudioClip TagSound { get { return _player.TagSound; } }
    #endregion
    #region MapObject
    [System.Serializable]
    public partial struct MapObject
    {
        public AudioClip MovingPlatformSound;
        public AudioClip WarpBeforeOpenSound;
        public AudioClip WarpOpeningSound;
        public AudioClip WarpAfterOpenSound;
        public AudioClip WarpInteractionSound;
        public AudioClip WarpButtonClickSound;
        public AudioClip AbilityItemPickUpSound;
        public AudioClip SwitchInteractionSound;
        public AudioClip ClearTreeInteractionSound;
        public AudioClip ClearSound;
        public AudioClip ObjectDropSound;
        public AudioClip TrapActivationSound;
        public AudioClip EnvironmentalSound;
    }
    [Header("오브젝트 관련")]
    [SerializeField] MapObject _mapObject;
    public AudioClip MovingPlatformSound { get { return _mapObject.MovingPlatformSound; } }
    public AudioClip WarpBeforeOpenSound { get { return _mapObject.WarpBeforeOpenSound; } }
    public AudioClip WarpOpeningSound { get { return _mapObject.WarpOpeningSound; } }
    public AudioClip WarpAfterOpenSound { get { return _mapObject.WarpAfterOpenSound; } }
    public AudioClip WarpInteractionSound { get { return _mapObject.WarpInteractionSound; } }
    public AudioClip WarpButtonClickSound { get { return _mapObject.WarpButtonClickSound; } }
    public AudioClip AbilityItemPickUpSound { get { return _mapObject.AbilityItemPickUpSound; } }
    public AudioClip SwitchInteractionSound { get { return _mapObject.SwitchInteractionSound; } }
    public AudioClip ClearTreeInteractionSound { get { return _mapObject.ClearTreeInteractionSound; } }
    public AudioClip ClearSound { get { return _mapObject.ClearSound; } }
    public AudioClip ObjectDropSound { get { return _mapObject.ObjectDropSound; } }
    public AudioClip TrapActivationSound { get { return _mapObject.TrapActivationSound; } }
    public AudioClip EnvironmentalSound { get { return _mapObject.EnvironmentalSound; } }
    #endregion
    #region UI
    [System.Serializable]
    public partial struct UI
    {
        public AudioClip NotificationSound;
        public AudioClip InteractionSound;
        public AudioClip ButtonClickSound;
        public AudioClip MenuToggleSound;
        public AudioClip SliderSound;
        public AudioClip SuccessSound;
    }
    [Header("UI 관련")]
    [SerializeField] UI _uI;
    public AudioClip NotificationSound { get { return _uI.NotificationSound; } }
    public AudioClip InteractionSound { get { return _uI.InteractionSound; } }
    public AudioClip ButtonClickSound { get { return _uI.ButtonClickSound; } }
    public AudioClip MenuToggleSound { get { return _uI.MenuToggleSound; } }
    public AudioClip SliderSound { get { return _uI.SliderSound; } }
    public AudioClip SuccessSound { get { return _uI.SuccessSound; } }
    #endregion
}
