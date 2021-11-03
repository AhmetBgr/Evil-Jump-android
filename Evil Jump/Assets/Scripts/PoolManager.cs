using System;
using System.Collections.Generic;
using UnityEngine;

// All pool objects
public enum PoolObjectType{
    Platform01, Platform02, Platform03, Platform05, Platform06, TeleporterPlatform,
    Golds01, Golds02, Golds03, Golds04, Golds05, Enemy1, Enemy2, Enemy3, Enemy4, Enemy5,
    Wall01, Wall02, Wall03, Wall04,
    Coin, AirJump, Freezer, MegaJump, Magnet, Replacer, MegaAirJump, GrayCoin, WallRun
};

[Serializable]
public class PooledObjectInfo{

    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector] public List<GameObject> pool = new List<GameObject>();
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] List<PooledObjectInfo> objects;

    public static PoolManager SharedInstance;

    public List<PoolObjectType> collectableList = new List<PoolObjectType>{PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin,
    PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.Coin,
    PoolObjectType.Coin, PoolObjectType.Coin, PoolObjectType.AirJump, PoolObjectType.AirJump, PoolObjectType.AirJump, PoolObjectType.Freezer,
    PoolObjectType.Replacer, PoolObjectType.MegaJump, PoolObjectType.Magnet};


    // Start is called before the first frame update
    void Awake()
    {
        SharedInstance = this;
        for (int i = 0; i < objects.Count; i++)
        {
            FillPool(objects[i]);
        }
    }

    // Instantiates all objects
    void FillPool(PooledObjectInfo info){
        for (int i = 0; i < info.amount; i++)
        {
            GameObject clone = Instantiate(info.prefab, info.container.transform);
            clone.SetActive(false);
            info.pool.Add(clone);
        }   
    }

    // Gets a pool object by type
    public GameObject GetPoolObject(PoolObjectType type){
        PooledObjectInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;
        
        GameObject objIntance = null;
        if(pool.Count > 0){ // Gets fom object pool 
            objIntance = pool[pool.Count - 1];
            pool.Remove(objIntance);
        }
        else{ // Gets by instaniating new object if object pool is empty
            objIntance = Instantiate(selected.prefab, selected.container.transform);
            //selected.pool.Add(objIntance);
        }
        
        return objIntance;

    }

    // disables object and adds to the related container
    public void DisableObject(GameObject obj, PoolObjectType type){

        if(obj.GetComponent<PowerUp>() != null){
            AddToContainer(obj, type);
        }

        obj.SetActive(false);
        
        PooledObjectInfo selected = GetPoolByType(type);

        List<GameObject> pool = null;
        pool = selected.pool;

        if(!pool.Contains(obj)){
            pool.Add(obj);
        }
        
    }


    private PooledObjectInfo GetPoolByType(PoolObjectType type){

        for (int i = 0; i < objects.Count; i++)
        {   
            if(type == objects[i].type){
                return objects[i];
            }
        }
        return null;
    }

    // Adds object to the related container
    public void AddToContainer(GameObject obj, PoolObjectType type){
        if(obj != null){
            obj.transform.SetParent(GetPoolByType(type).container.transform);
        }
    }
}
