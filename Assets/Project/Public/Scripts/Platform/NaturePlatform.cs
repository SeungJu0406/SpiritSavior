using System.Collections.Generic;
using UnityEngine;

public class NaturePlatform : MonoBehaviour
{
    public enum Type{ Platform, Wall}

    public struct NaturePlatformStruct
    {
        public GameObject Platform;
        public int Layer;
    }

    [Space(10)]
    [SerializeField] PlayerModel.Nature _platformNature;
    [Space(10)]
    [SerializeField] Type _type;

    List<NaturePlatformStruct> objectList = new List<NaturePlatformStruct>();
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
                for (int i = 0; i< objectList.Count; i++ )
                {
                    SetDefaultLayer(objectList[i]);
                }
            }
            else
            {
                for (int i = 0; i < objectList.Count; i++)
                {
                    SetIgnorePlayerLayer(objectList[i]);
                }
            }
        }
        else if(_type == Type.Wall)
        {
            if (nature == _platformNature)
            {
                for (int i = 0; i < objectList.Count; i++)
                {
                    SetIgnorePlayerLayer(objectList[i]);
                }
            }
            else
            {
                for (int i = 0; i < objectList.Count; i++)
                {
                    SetDefaultLayer(objectList[i]);
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
        objectList.Add(CreateNaturePlatformStruct(gameObject, gameObject.layer));
        Transform[] childObjects = GetComponentsInChildren<Transform>();
        foreach (Transform obj in childObjects)
        {
            objectList.Add( CreateNaturePlatformStruct(obj.gameObject, obj.gameObject.layer));
        }
    }

    NaturePlatformStruct CreateNaturePlatformStruct(GameObject platform, int layer)
    {
        NaturePlatformStruct newPlatform = new NaturePlatformStruct();
        newPlatform.Platform = platform;
        newPlatform.Layer = layer;
        return newPlatform;
    }

    void SetDefaultLayer(NaturePlatformStruct naturePlatform)
    {
        naturePlatform.Platform.layer = naturePlatform.Layer;
    }
    void  SetIgnorePlayerLayer(NaturePlatformStruct naturePlatform)
    {
        naturePlatform.Platform.layer = _ignorePlayerLayer;
    }
}
