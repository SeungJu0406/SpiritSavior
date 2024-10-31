using UnityEngine;
using UnityEngine.UI;

public class SkillSwitchInteractable : SwichInteractable
{
    public enum Skill { Tag, WallJump, DoubleJump, Dash }

    [SerializeField] SkillTooltipUI _tooltipUI;
    [SerializeField] Skill _skill;

    public override void Interact()
    {
        ShowTooltipUi();
    }

    void ShowTooltipUi()
    {
        transform.SetParent(null);

        SkillTooltipUI tooltipUI = Instantiate(_tooltipUI);
        tooltipUI.GetUI<Button>("CancelButton").onClick.AddListener(ActivePlayerSkill);
    }

    void ActivePlayerSkill()
    {
        switch (_skill)
        {
            case Skill.Tag:
                // Tag 활성화
                break;
            case Skill.WallJump:
                // WallJump 활성화
                break;
            case Skill.DoubleJump:
                // DoubleJump 활성화
                break;
            case Skill.Dash:
                // Dash 활성화
                break;
        }

        Destroy(gameObject);
    }
}
