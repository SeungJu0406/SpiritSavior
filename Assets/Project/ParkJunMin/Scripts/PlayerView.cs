using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerModel _playerModel;
    private PlayerController _player;
    public Animator animator;
    public SpriteRenderer renderer;
    public Sprite[] sprites;
    private int _curSpriteIndex = 0;
    public int animationIntervalNumber = 4;
    public AnimatorStateInfo stateInfo;

    //[SerializeField] AudioClip[] clips;

    private int[] _animationHash = new int[]
    {
        //Red
        Animator.StringToHash("Idle_Red"),
        Animator.StringToHash("Run_Red"),
        Animator.StringToHash("Dash_Red"),
        Animator.StringToHash("Jump_Red"),
        Animator.StringToHash("DoubleJump_Red"),
        Animator.StringToHash("Fall_Red"),
        Animator.StringToHash("Land_Red"),
        Animator.StringToHash("WallGrabStart_Red"),
        Animator.StringToHash("WallSliding_Red"),
        Animator.StringToHash("WallJump_Red"),
        Animator.StringToHash("DamageStart_Red"),
        Animator.StringToHash("WakeUp_Red"),
        Animator.StringToHash("Die_Red"),
        Animator.StringToHash("Resurrection_Red"),

        //Blue
        Animator.StringToHash("Idle_Blue"),
        Animator.StringToHash("Run_Blue"),
        Animator.StringToHash("Dash_Blue"),
        Animator.StringToHash("Jump_Blue"),
        Animator.StringToHash("DoubleJump_Blue"),
        Animator.StringToHash("Fall_Blue"),
        Animator.StringToHash("Land_Blue"),
        Animator.StringToHash("WallGrabStart_Blue"),
        Animator.StringToHash("WallSliding_Blue"),
        Animator.StringToHash("WallJump_Blue"),
        Animator.StringToHash("DamageStart_Blue"),
        Animator.StringToHash("WakeUp_Blue"),
        Animator.StringToHash("Die_Blue"),
        Animator.StringToHash("Resurrection_Blue")
    };

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
        renderer = GetComponent<SpriteRenderer>();
        if(sprites.Length > 0 )
            renderer.sprite = sprites[_curSpriteIndex];
        _playerModel = _player.playerModel;
        animator = GetComponent<Animator>();
    }



    public void FlipRender(float _moveDirection)
    {
        if (_moveDirection < 0)
        {
            renderer.flipX = true;
            _player.isPlayerRight = -1;
        }

        if (_moveDirection > 0)
        {
            renderer.flipX = false;
            _player.isPlayerRight = 1;
        }
    }

    public void PlayAnimation(int animationIndex)
    {
        if (animationIndex >= 0 && animationIndex < _animationHash.Length)
        {
            if (_playerModel.curNature == PlayerModel.Nature.Red)
            {
                animator.Play(_animationHash[animationIndex],0,0);
            }
            else if (_playerModel.curNature == PlayerModel.Nature.Blue)
            {
                animator.Play(_animationHash[animationIndex + (int)PlayerController.State.Size],0,0);
            }
        }
        else
        {
            Debug.LogError("애니메이션 인덱스 에러");
        }
    }

    public void PlayAnimation(int animationIndex, float normalizedTime)
    {
        if (animationIndex >= 0 && animationIndex < _animationHash.Length)
        {
            if (_playerModel.curNature == PlayerModel.Nature.Red)
            {
                animator.Play(_animationHash[animationIndex], 0, normalizedTime);
            }
            else if (_playerModel.curNature == PlayerModel.Nature.Blue)
            {
                animator.Play(_animationHash[animationIndex + (int)PlayerController.State.Size], 0, normalizedTime);
            }
        }
        else
        {
            Debug.LogError("애니메이션 인덱스 에러");
        }
    }

    public bool IsAnimationFinished()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.normalizedTime >= 1.0f)
        {
            return true;
        }
        else
            return false;
    }
    public void ChangeSprite()
    {
        //renderer.sprite = sprites[(_curSpriteIndex++)%sprites.Length];
    }
}
