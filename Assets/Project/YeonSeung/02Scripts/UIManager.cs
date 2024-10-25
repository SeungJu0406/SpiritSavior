using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    //죽음상태
    private bool _isDead;
    // 죽었을때 Sprite (색없는 얼굴)
    [SerializeField] GameObject deadFace;

    // 얼음속성
    private bool _isBlue;
    // 파란얼굴
    [SerializeField] GameObject BlueFace;

    // 불속성
    private bool _isRed;
    // 빨간얼굴
    [SerializeField] GameObject RedFace;



    /* 나중에 플레이어에서 연결 ?
     * 플레이어에서 아무래도 hp?관리할거같으니까 / 라이프도? 
    [SerializeField] private Slider HPBar;
    */
    [Header ("HP Bar")]
    [SerializeField] float curHp;
    [SerializeField] float maxHp;
    [SerializeField] private Slider _hpBar;


    [Header("Live Count")]
    [SerializeField] GameObject[] lives;
    [SerializeField] public int life;
    [SerializeField] public int maxLife;

    /// <summary>
    /// 타입변경 (불 <--> 얼음)
    /// 'Tab'키로 변경
    /// </summary>
    public void ChangeType()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_isBlue == false)
            {
                _isBlue = true;
                _isRed = false;
                Debug.Log($"{_isBlue},    {_isRed}");
            }
            else if(_isBlue == true)
            {
                _isBlue = false;
                _isRed = true;
                Debug.Log($"{_isBlue},    {_isRed}");
            }
        }
    }

    /// <summary>
    /// 얼굴인터페이스변경
    /// </summary>
    public void FaceToggleSwtich()
    {
        if (_isBlue)
        {
            BlueFace.SetActive(true);
            RedFace.SetActive(false);
        }
        else if (!_isBlue)
        {
            BlueFace.SetActive(false);
            RedFace.SetActive(true);
        }
    }


    /// <summary>
    /// 죽을때 얼굴UI 활성화
    /// </summary>
    public void DeadFaceOn()
    {
        if (curHp <= 0)
        {
            _isDead = true;
            deadFace.SetActive(true);
        }
        // 봐서 밑에 active만 쓰거나 위처럼 그냥 다쓰거나
        // deadFace.SetActive(true);
    }

    /// <summary>
    /// 데미지 받는함수
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (life >= 1)
        {
            life -= damage;

            //Destroy(lives[life].gameObject);
            lives[life].SetActive(false);
            if (life < 1)
            {
                _isDead = true;
                // 하트 다 닳으면 죽은 얼굴 활성화
                deadFace.SetActive(true); 
            }
        }
    }

    /// <summary>
    /// Life 추가 (에너지 더해주는 아이템을 먹는다던가
    /// </summary>
    public void GiveLife()
    {
        // HP BAR
        // 이거는 실사용시 지워도 됨 밑에 Update에서 플레이어 연결만 하면됨
        #region 테스트용HP감소
        if (curHp >= 100)
        {
            curHp = 100;
        }
        else
        {
            curHp += 33;

        }
        #endregion

        // LIFE 
        if (life >= maxLife)
        {
            // 최대체력 못넘어가기.
            life = maxLife;
            lives[life].SetActive(true);
        }
        else
        {

            lives[life].SetActive(true);
            life += 1;
        }
    }



    void Start()
    {
        maxLife = lives.Length;
        life = lives.Length;
        curHp = maxHp;
    }

    void Update()
    {
        _hpBar.value = curHp / maxHp;
        ChangeType();
        FaceToggleSwtich();

    }





    // 확인하고 삭제
    #region 테스트용 필요없을수도
   


    /// <summary>
    /// HP감소 함수
    /// 인데 나중에 함정같은거에서 데미지 구현하면 거기 콜리전으로 데미지받아올거같아서 딲히 필요없을수도
    /// 지금 그냥 테스트용
    /// </summary>
    public void GiveDamage()
    {
        curHp -= 33;
        // 체력 다 닳으면 죽은얼굴 활성화
        DeadFaceOn();
    }
    #endregion


}



/* 위에 목숨 UI만
public void LifeCount()
{
    // 데미지 받는거말고 그냥 하트수만 하면
    if (life < 1)
    {
        Destroy(lives[0].gameObject);
    }
    else if (life < 2)
    {
        Destroy(lives[1].gameObject);
    }
    else if (life < 3)
    {
        Destroy(lives[2].gameObject);
    } 

}
*/