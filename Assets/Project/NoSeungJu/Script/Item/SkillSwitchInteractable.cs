using UnityEngine;
using UnityEngine.UI;

public class SkillSwitchInteractable : SwichInteractable
{ 

    [SerializeField] SkillTooltipUI _tooltipUI;
    [SerializeField] PlayerModel.Ability _ability;

    public override void Interact()
    {
        ActivePlayerSkill();
    }

    void ActivePlayerSkill()
    {
        transform.SetParent(null);

        Instantiate(_tooltipUI);
        Manager.Game.Player.UnlockAbility(_ability);
        //Manager.Game.Player.playerModel.UnlockAbilityEvent(_ability);
        Destroy(gameObject);
    }
}
