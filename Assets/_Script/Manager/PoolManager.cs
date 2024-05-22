using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [Header("Prefab")]
    [SerializeField] private TextPopUp textPopUpPrefab;

    [Header("Holder")]
    [SerializeField] private GameObject ObjectPoolHolder;

    [Header("List Obj")]
    public Dictionary<PoolType, List<Component>> objListDic = new Dictionary<PoolType, List<Component>>();
    public Dictionary<PoolType, GameObject> objHolderDic = new Dictionary<PoolType, GameObject>();
    private void Start()
    {
        SetUpObjEmpties();
    }
    public List<Component> GetListObjByPoolType(PoolType poolType)
    {
        if (poolType == PoolType.None) return null;

        if (objListDic.ContainsKey(poolType))
            return objListDic[poolType];
        return null;
    }
    public GameObject GetObjHolderByPoolType(PoolType poolType)
    {
        if (poolType == PoolType.None) return null;

        if (objHolderDic.ContainsKey(poolType))
            return objHolderDic[poolType];
        return null;

    }
    void SetUpObjEmpties()
    {
        ObjectPoolHolder = new GameObject("ObjectPoolHolder");
        ObjectPoolHolder.transform.SetParent(transform);

        foreach (PoolType poolType in Enum.GetValues(typeof(PoolType)))
        {
            if (poolType == PoolType.None) continue;

            GameObject objHolder = new GameObject(poolType.ToString() + " Holder");
            objHolder.transform.SetParent(ObjectPoolHolder.transform);
            objHolderDic.Add(poolType, objHolder);
        }
    }

    public T SpawnObj<T>(T component, Vector2 spawnPos, PoolType poolType ) where T : Component
    {
        if (!objListDic.TryGetValue(poolType, out var objList))
        {
            objList = new List<Component>();
            objListDic.Add(poolType, objList);
        }

        T inactiveObj = objList.Find(obj => !obj.gameObject.activeSelf) as T; //Find inactive obj

        if (inactiveObj == null) //Inactive obj not found
        {
            T spawnableObj = Instantiate(component, spawnPos, Quaternion.identity);

            objList.Add(spawnableObj);

            GameObject holder = GetObjHolderByPoolType(poolType); //Get obj holder
            if (holder != null) spawnableObj.transform.SetParent(holder.transform);

            return spawnableObj;
        }
        else
        {
            inactiveObj.gameObject.transform.position = spawnPos;
            inactiveObj.gameObject.SetActive(true);

            return inactiveObj;
        }
    }

    public TextPopUp GetTextPopUp()
    {
        return textPopUpPrefab;
    }
}
