using System.Collections.Generic;
using UnityEngine;

public class NaturePlatform : MonoBehaviour
{
    public enum Type{ Platform, Wall}

    [Space(10)]
    [SerializeField] PlayerModel.Nature _platformNature;
    [Space(10)]
    [SerializeField] Type _type;

    List<GameObject> objectList = new List<GameObject>();
    PlayerController _player;
    int _defaultLayer;
    int _ignorePlayerLayer;

    private void Awake()
    {
        InitLayer();
        InitObjectList();
    }

    private void Start()
    {
        if (_player != null) return;
        _player = Manager.Game.Player;
        _player.playerModel.OnPlayerTagged += SetActiveCollider;
        SetActiveCollider(_player.playerModel.curNature);
    }

    private void OnEnable()
    {
        if (Manager.Game == null) return;
        _player = Manager.Game.Player;
        _player.playerModel.OnPlayerTagged += SetActiveCollider;
        SetActiveCollider(_player.playerModel.curNature);
    }

    private void OnDisable()
    {
        if (Manager.Game.Player != null)
            Manager.Game.Player.playerModel.OnPlayerTagged -= SetActiveCollider;
    }
    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if(_type== Type.Platform)
        {
            if (nature == _platformNature)
            {
                foreach (GameObject obj in objectList)
                {
                    obj.layer = _defaultLayer;
                }

            }
            else
            {
                foreach (GameObject obj in objectList)
                {
                    obj.layer = _ignorePlayerLayer;
                }
            }
        }
        else if(_type == Type.Wall)
        {
            if (nature == _platformNature)
            {
                foreach (GameObject obj in objectList)
                {                  
                    obj.layer = _ignorePlayerLayer;
                }

            }
            else
            {
                foreach (GameObject obj in objectList)
                {
                    obj.layer = _defaultLayer;
                }
            }
        }

    }

    void InitLayer()
    {
        _defaultLayer = gameObject.layer;
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
    }

    void InitObjectList()
    {
        objectList.Add(gameObject);
        Transform[] childObjects = GetComponentsInChildren<Transform>();
        foreach (Transform obj in childObjects)
        {
            objectList.Add(obj.gameObject);
        }
    }
}
