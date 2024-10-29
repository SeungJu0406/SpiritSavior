using UnityEngine;

public class NaturePlatform : MonoBehaviour
{
    [Space(10)]
    [SerializeField] PlayerModel.Nature _platformNature;

    int _defaultLayer;
    int _ignorePlayerLayer;

    private void Awake()
    {
        InitLayer();
    }

    private void OnEnable()
    {
        PlayerController player = Manager.Game.Player;
        player.playerModel.OnPlayerTagged += SetActiveCollider;
        SetActiveCollider(player.playerModel.curNature);
    }

    private void OnDisable()
    {
        if (Manager.Game.Player != null)
            Manager.Game.Player.playerModel.OnPlayerTagged -= SetActiveCollider;
    }
    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if (nature == _platformNature)
        {
            gameObject.layer = _defaultLayer;
        }
        else
        {
            gameObject.layer = _ignorePlayerLayer;
        }
    }

    void InitLayer()
    {
        _defaultLayer = LayerMask.NameToLayer("Default");
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
    }
}
