using System.Collections.Generic;
using Project.ParkJunMin.Scripts;
using UnityEngine;

public class PlayerUI : BaseUI
{
    [SerializeField] GameObject _lifePrefab;
    List<GameObject> _lifesUI = new List<GameObject>();
    List<GameObject> _faceList = new List<GameObject>();


    [SerializeField] bool _canTag;
    [SerializeField] bool _canWallJump;
    [SerializeField] bool _canDoubleJump;
    [SerializeField] bool _canDash;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void Start()
    {
        SubsCribeEvents();
        Init();
    }

    #region ĳ���� �ʻ�ȭ UI

    public void UpdateFace()
    {
        PlayerModel.Nature nature = Manager.Game.Player.playerModel.curNature;
        if (nature == PlayerModel.Nature.Red)
        {
            ShowRedFace();
        }
        else if (nature == PlayerModel.Nature.Blue)
        {
            ShowBlueFace();
        }
    }
    public void UpdateFace(PlayerModel.Nature nature)
    {
        nature = Manager.Game.Player.playerModel.curNature;
        if (nature == PlayerModel.Nature.Red)
        {
            ShowRedFace();
        }
        else if (nature == PlayerModel.Nature.Blue)
        {
            ShowBlueFace();
        }
    }

    /// <summary>
    /// ���� ĳ���� UI Ȱ��ȭ
    /// </summary>
    public void ShowRedFace()
    {
        foreach (GameObject face in _faceList)
        {
            if (face.name == GetUI("RedFace").name)
            {
                face.SetActive(true);
            }
            else
            {
                face.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ���� ĳ���� UI Ȱ��ȭ
    /// </summary>
    public void ShowBlueFace()
    {
        foreach (GameObject face in _faceList)
        {
            if (face.name == GetUI("BlueFace").name)
            {
                face.SetActive(true);
            }
            else
            {
                face.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ĳ���� ���� �� UI Ȱ��ȭ
    /// </summary>
    public void ShowDeadFace()
    {
        foreach (GameObject face in _faceList)
        {
            if (face.name == GetUI("DeadFace").name)
            {
                face.SetActive(true);
            }
            else
            {
                face.SetActive(false);
            }
        }
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

    #region �±� �ɷ�
    /// <summary>
    /// �±� �ɷ� Ȱ��ȭ UI
    /// </summary>
    public void ShowTagSkill()
    {
        GetUI("TagOff").gameObject.SetActive(false);
        GetUI("TagOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// �±� �ɷ� ��Ȱ��ȭ UI
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
    #region �� ���� �ɷ�
    /// <summary>
    /// �� ���� �ɷ� Ȱ��ȭ UI
    /// </summary>
    public void ShowWallJumpSkill()
    {
        GetUI("WallJumpOff").gameObject.SetActive(false);
        GetUI("WallJumpOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// �� ���� �ɷ� ��Ȱ��ȭ UI
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
    #region ���� ���� �ɷ�
    /// <summary>
    /// ���� ���� �ɷ� Ȱ��ȭ UI
    /// </summary>
    public void ShowDoubleJumpSkill()
    {
        GetUI("DoubleJumpOff").gameObject.SetActive(false);
        GetUI("DoubleJumpOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// ���� ���� �ɷ� ��Ȱ��ȭ UI
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
    #region �뽬 �ɷ�
    /// <summary>
    /// �뽬 �ɷ� Ȱ��ȭ UI
    /// </summary>
    public void ShowDashSkill()
    {
        GetUI("DashOff").gameObject.SetActive(false);
        GetUI("DashOn").gameObject.SetActive(true);
    }
    /// <summary>
    /// �뽬 �ɷ� ��Ȱ��ȭ UI
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

    private void Init()
    {
        InitFace();
        InitHp();
        InitSkillUI();
    }

    void InitFace()
    {
        _faceList.Add(GetUI("RedFace"));
        _faceList.Add(GetUI("BlueFace"));
        _faceList.Add(GetUI("DeadFace"));
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
        Manager.Game.Player.playerModel.OnPlayerSpawn += UpdateFace;
        Manager.Game.Player.playerModel.OnPlayerDied += ShowDeadFace;
        Manager.Game.Player.playerModel.OnPlayerTagged += UpdateFace;


        Manager.Game.Player.playerModel.OnAbilityUnlocked += UpdateSkillUI;
    }
    void UpdateSkillUI(PlayerModel.Ability ability)
    {
        switch (ability)
        {
            case PlayerModel.Ability.Tag:
                ShowTagSkill();
                break;
            case PlayerModel.Ability.WallJump:
                ShowWallJumpSkill();
                break;
            case PlayerModel.Ability.DoubleJump:
                ShowDoubleJumpSkill();
                break;
            case PlayerModel.Ability.Dash:
                ShowDashSkill();
                break;
            default:
                break;
        }
    }
}
