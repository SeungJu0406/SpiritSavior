using UnityEngine;
using UnityEngine.UI;

public class SkillSwitchInteractable : SwichInteractable
{ 

    [SerializeField] SkillTooltipUI _tooltipUI;
    [SerializeField] PlayerModel.Ability _ability;

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
        Manager.Game.Player.UnlockAbility(_ability);
        Manager.Game.Player.playerModel.UnlockAbilityEvent(_ability);

        Destroy(gameObject);
    }
}
