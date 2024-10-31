using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : BaseUI
{
    [SerializeField] GameObject _lifePrefab;
    List<GameObject> _lifesUI = new List<GameObject>();


    [SerializeField] bool _canTag;
    public bool CanTag { get { return _canTag; } set { _canTag = value; OnChangeCanTag.Invoke(); } }
    public event Action OnChangeCanTag;
    [SerializeField] bool _canWallJump;
    public bool CanWallJump { get { return _canWallJump; } set { _canWallJump = value; OnChangeCanWallJump.Invoke(); } }
    public event Action OnChangeCanWallJump;
    [SerializeField] bool _canDoubleJump;
    public bool CanDoubleJump { get { return _canDoubleJump; } set { _canDoubleJump = value; OnChangeCanDoubleJump.Invoke(); } }
    public event Action OnChangeCanDoubleJump;
    [SerializeField] bool _canDash;
    public bool CanDash { get { return _canDash; } set { _canDash = value; OnChangeCanDash.Invoke(); } }
    public event Action OnChangeCanDash;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        SubsCribeEvents();
        InitHp();
        InitSkillUI();
    }

    #region 캐릭터 초상화 UI
    /// <summary>
    /// 레드 캐릭터 UI 활성화
    /// </summary>
    public void ShowCharFace()
    {
        GetUI("CharFace").SetActive(true);
        GetUI("BlueFace").SetActive(false);
        GetUI("DeadFace").SetActive(false);
    }

    /// <summary>
    /// 블루 캐릭터 UI 활성화
    /// </summary>
    public void ShowBlueFace()
    {
        GetUI("CharFace").SetActive(false);
        GetUI("BlueFace").SetActive(true);
        GetUI("DeadFace").SetActive(false);
    }

    /// <summary>
    /// 캐릭터 죽음 얼굴 UI 활성화
    /// </summary>
    public void ShowDeadFace()
    {
        GetUI("CharFace").SetActive(false);
        GetUI("BlueFace").SetActive(false);
        GetUI("DeadFace").SetActive(true);
    }
    #endregion
    public void SetHp()
    {
        int hp = Manager.Game.Player.playerModel.hp;
        if (hp > _lifesUI.Count)
        {
            AddLife(hp);
        }
        for (int i = 0; i < _lifesUI.Count; i++)
        {
            if (i < hp)
            {
                _lifesUI[i].SetActive(true);
            }
            else
            {
                _lifesUI[i].SetActive(false);
            }
        }
    }

    #region 태그 능력
    /// <summary>
    /// 태그 능력 활성화 UI
    /// </summary>
    public void ShowTagSkill()
    {
        GetUI("TagOff").gameObject.SetActive(false);
        GetUI("TagOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// 태그 능력 비활성화 UI
    /// </summary>
    public void HideTagSkill()
    {
        GetUI("TagOff").gameObject.SetActive(true);
        GetUI("TagOn").gameObject.SetActive(false);
    }

    public void ToggleTagSkill()
    {
        GetUI("TagOff").gameObject.SetActive(!GetUI("TagOff").gameObject.activeSelf);
        GetUI("TagOn").gameObject.SetActive(!GetUI("TagOn").gameObject.activeSelf);
    }
    #endregion
    #region 벽 점프 능력
    /// <summary>
    /// 벽 점프 능력 활성화 UI
    /// </summary>
    public void ShowWallJumpSkill()
    {
        GetUI("WallJumpOff").gameObject.SetActive(false);
        GetUI("WallJumpOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// 벽 점프 능력 비활성화 UI
    /// </summary>
    public void HideWallJumpSkill()
    {
        GetUI("WallJumpOff").gameObject.SetActive(true);
        GetUI("WallJumpOn").gameObject.SetActive(false);
    }
    public void ToggleWallJumpSkill()
    {
        GetUI("WallJumpOff").gameObject.SetActive(!GetUI("WallJumpOff").gameObject.activeSelf);
        GetUI("WallJumpOn").gameObject.SetActive(!GetUI("WallJumpOn").gameObject.activeSelf);
    }
    #endregion
    #region 더블 점프 능력
    /// <summary>
    /// 더블 점프 능력 활성화 UI
    /// </summary>
    public void ShowDoubleJumpSkill()
    {
        GetUI("DoubleJumpOff").gameObject.SetActive(false);
        GetUI("DoubleJumpOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// 더블 점프 능력 비활성화 UI
    /// </summary>
    public void HideDoubleJumpSkill()
    {
        GetUI("DoubleJumpOff").gameObject.SetActive(true);
        GetUI("DoubleJumpOn").gameObject.SetActive(false);
    }
    public void ToggleDoubleJumpSkill()
    {
        GetUI("DoubleJumpOff").gameObject.SetActive(!GetUI("DoubleJumpOff").gameObject.activeSelf);
        GetUI("DoubleJumpOn").gameObject.SetActive(!GetUI("DoubleJumpOn").gameObject.activeSelf);
    }
    #endregion
    #region 대쉬 능력
    /// <summary>
    /// 대쉬 능력 활성화 UI
    /// </summary>
    public void ShowDashSkill()
    {
        GetUI("DashOff").gameObject.SetActive(false);
        GetUI("DashOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// 대쉬 능력 비활성화 UI
    /// </summary>
    public void HideDashSkill()
    {
        GetUI("DashOff").gameObject.SetActive(true);
        GetUI("DashOn").gameObject.SetActive(false);
    }
    public void ToggleDashSkill()
    {
        GetUI("DashOff").gameObject.SetActive(!GetUI("DashOff").gameObject.activeSelf);
        GetUI("DashOn").gameObject.SetActive(!GetUI("DashOn").gameObject.activeSelf);
    }
    #endregion


    /// <summary>
    /// 테스트 용
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleTag();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleWallJump();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleDoubleJump();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleDash();
        }
    }

    public void ToggleTag()
    {
        CanTag = !CanTag;
    }
    public void ToggleWallJump()
    {
        CanWallJump = !CanWallJump;
    }
    public void ToggleDoubleJump()
    {
        CanDoubleJump = !CanDoubleJump;
    }
    public void ToggleDash()
    {
        CanDash = !CanDash;
    }


    void AddLife(int hp)
    {
        int count = hp - _lifesUI.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject newLife = Instantiate(_lifePrefab);
            newLife.transform.SetParent(GetUI("Life").transform);
            _lifesUI.Add(newLife);
        }
    }

    void InitHp()
    {
        SetHp();
    }
    
    void InitSkillUI()
    {
        HideTagSkill();
        HideWallJumpSkill();
        HideDashSkill();
        HideDoubleJumpSkill();
    }


    void SubsCribeEvents()
    {
        Manager.Game.Player.playerModel.OnPlayerDamageTaken += SetHp;
        Manager.Game.Player.playerModel.OnPlayerHealth += SetHp;
        Manager.Game.Player.playerModel.OnPlayerSpawn += SetHp;

        OnChangeCanTag += ToggleTagSkill;
        OnChangeCanWallJump += ToggleWallJumpSkill;
        OnChangeCanDoubleJump += ToggleDoubleJumpSkill;
        OnChangeCanDash += ToggleDashSkill;
    }
}
