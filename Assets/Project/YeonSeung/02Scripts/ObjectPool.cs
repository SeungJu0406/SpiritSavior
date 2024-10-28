using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static List<PooledObject> ObjectPools = new List<PooledObject>();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectsEmpty;


    // Hierarchy 유지
    public enum PoolType
    {
        ParticleSystem, GameObject, None
    }

    public static PoolType PoolingType;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemsEmpty = new GameObject("Particle Effects");
        _particleSystemsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }




    public static GameObject SpawnObject(GameObject objToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObject pool = ObjectPools.Find(p => p.checkString == objToSpawn.name);

        // 존재하지 않는 pool이면 만들기
        if (pool == null)
        {
            pool = new PooledObject() { checkString = objToSpawn.name };
            ObjectPools.Add(pool);
        }

        // pool 에 비활성 object 있나 확인
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
        
        
        // 없으면, 만들기
        if (spawnableObj == null)
        {
            // EmptyObject 부모 찾기
            GameObject parentObject = SetParentObject(poolType);

            // 비활성화된 object 없으면 만들기
            spawnableObj = Instantiate(objToSpawn, spawnPosition, spawnRotation);

            // 부모object있으면 SetParent
            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }

        // 있으면, 활성화
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public static void ReturnObjectPool(GameObject obj)
    {
        string objName = obj.name.Substring(0, obj.name.Length - 7);
        // 비교할때 (clone) <-- 이거 7글자 지워서 비교해서 지우기
        // instantiate전에 되다보니 클론이 안되고 나와서 그거랑 비교하기위해

        PooledObject pool = ObjectPools.Find(p => p.checkString == objName);
        if (pool == null)
        {
            // Debug.LogWarning($"pool 안된 object, {obj.name}");
            }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
            // transform 같은거 필요하면 거기서 리셋해주거나 해야됨
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;
            case PoolType.GameObject:
                return _gameObjectsEmpty;
            case PoolType.None:
                return null;

            default:
                return null; 
        }
    }


    public class PooledObject
    {
        public string checkString;
        public List<GameObject> InactiveObjects = new List<GameObject>();
    }

}
