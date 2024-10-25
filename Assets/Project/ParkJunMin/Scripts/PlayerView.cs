using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerModel _playerModel;
    private PlayerController _player;
    private Animator _animator;
    //[SerializeField] AudioClip[] clips;

    private int[] _animationHash = new int[]
    {
        Animator.StringToHash("Idle_Red"),
        Animator.StringToHash("Run_Red"),
        Animator.StringToHash("Jump_Red"),
        Animator.StringToHash("Fall_Red"),

        Animator.StringToHash("Idle_Blue"),
        Animator.StringToHash("Run_Blue"),
        Animator.StringToHash("Jump_Blue"),
        Animator.StringToHash("Fall_Blue")
    };

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _playerModel = _player.playerModel;
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(int animationIndex)
    {
        if(animationIndex >= 0 &&  animationIndex < _animationHash.Length)
        {
            _animator.Play(_animationHash[animationIndex],0,0);
        }
        else
        {
            Debug.LogError("애니메이션 인덱스 에러");
        }
    }

}
