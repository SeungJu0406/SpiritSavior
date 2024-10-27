using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerUI _playerUI;
    [SerializeField] float _invincibility = 1;

    bool _canTakeDamage = true;
    bool _isDead;

    WaitForSeconds _dieDelay = new WaitForSeconds(0.767f);

    private void OnEnable()
    {
        if (_isDead)
        {
            InitRespawnPlayer();
        }
    }

    /// <summary>
    /// 플레이어의 데미지 피격
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (_canTakeDamage == false) return;

        playerModel.hp -= damage;
        _playerUI.SetHp(playerModel.hp);
        StartCoroutine(InvincibilityRoutine());
        if (playerModel.hp <= 0)
        {
            playerModel.hp = 0;
            Die();
        }
    }

    /// <summary>
    /// 플레이어가 힐을 받음
    /// </summary>
    /// <param name="healAmount"></param>
    public void TakeHeal(int healAmount)
    {
        playerModel.hp += healAmount;
        _playerUI.SetHp(playerModel.hp);
        if (playerModel.hp > playerModel.MaxHP)
        {
            playerModel.hp = playerModel.MaxHP;
        }
    }

    /// <summary>
    /// 캐릭터 죽음 메서드
    /// </summary>
    void Die()
    {
        _playerUI.ShowDeadFace();
        StartCoroutine(DieRoutine());
    }

    /// <summary>
    /// 캐릭터 죽음 애니메이션 딜레이 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator DieRoutine()
    {
        _isDead = true;
        ChangeState(State.Dead);
        yield return _dieDelay;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 피격무적시간 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator InvincibilityRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(_invincibility);
        _canTakeDamage = false;
        yield return delay;
        _canTakeDamage = true;
    }

    /// <summary>
    /// 리스폰시에 캐릭터 설정 초기화 
    /// </summary>
    void InitRespawnPlayer()
    {
        _isDead = false;
        transform.position = Manager.Game.RespawnPoint;
        playerModel.curNature = PlayerModel.Nature.Red;
        ChangeState(State.Idle);
        playerModel.hp = playerModel.MaxHP;
        _playerUI.SetHp(playerModel.hp);
    }
}
