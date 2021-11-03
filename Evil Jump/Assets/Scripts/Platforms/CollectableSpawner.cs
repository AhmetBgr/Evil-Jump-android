using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public bool canSpawnOnEnable = true;
    public MegaAirJump megaAirJump;
    static int megaAirJumpSpawncount = 0;
    [HideInInspector] public GameObject curCol;

    private PoolManager poolManager;
    
    void OnEnable(){
        poolManager = PoolManager.SharedInstance;
        if(transform.childCount == 0 && canSpawnOnEnable){
            PoolObjectType colType;
            
            int rand = Random.Range(0, 20);

            // Determines to what collectable will be spawned
            if(rand == 0 && megaAirJumpSpawncount < megaAirJump.maxSpawnNum){
                colType = PoolObjectType.MegaAirJump;
            }
            else{
                colType = poolManager.collectableList[Random.Range(0, poolManager.collectableList.Count)];
            }

            Spawn(transform, 0.9f, colType, false);
        }
    }

    // Spawns a collactable with a certain chance  
    public void Spawn(Transform main, float spawnChance, PoolObjectType colType, bool isUpgraded){

        if(isUpgraded){ // Changes spawn chance to %90 if replacer upgraded
            spawnChance = 0.1f;
        }

        if(Random.value > spawnChance){ 
            GameObject prefab = PoolManager.SharedInstance.GetPoolObject(colType);
            Vector3 collectiblesSpawnPos = main.transform.position + new Vector3(0f, 0.21f,-1f);
            prefab.transform.position = collectiblesSpawnPos;
            
            // Set parent
            prefab.transform.SetParent(main);
            curCol = prefab;
            prefab.SetActive(true);
            
            // Will not spawn MegaAirJump collactable more than max spawn num.
            if(colType == PoolObjectType.MegaAirJump){
                megaAirJumpSpawncount++;
                if(megaAirJumpSpawncount >= megaAirJump.maxSpawnNum){
                    poolManager.collectableList.Remove(PoolObjectType.MegaAirJump);
                }
            }
        }
    }

    void OnDestroy(){
        megaAirJumpSpawncount = 0; // Reset megaAirJumpSpawnCount
    }
}
