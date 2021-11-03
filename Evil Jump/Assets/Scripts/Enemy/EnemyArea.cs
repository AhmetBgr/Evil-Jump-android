using UnityEngine;

public class EnemyArea : MonoBehaviour
{   
    public GameObject enemy;
    public GameObject[] platformSpawnPoints;
    public PoolObjectType[] pTypes;
    public bool dontPutMiddle;

    void OnEnable(){
        int enemyIndex;
        if(dontPutMiddle){
            enemyIndex = Random.Range(0,2) == 0 ? 0 : 2;
            
        }
        else{
            enemyIndex = Random.Range(0,3);
        }

        for (int i = 0; i < platformSpawnPoints.Length; i++)
        {
            if(i != enemyIndex){

                // SPAWN PLATFORM
                int pTypeIndex = Random.Range(0,pTypes.Length);
                GameObject platform = PoolManager.SharedInstance.GetPoolObject(pTypes[pTypeIndex]);
                platform.transform.position = platformSpawnPoints[i].transform.position;
    
                platform.SetActive(true);
            }
            else{
                // SPAWN ENEMY
                enemy.transform.position = platformSpawnPoints[i].transform.position + new Vector3(0f, 2.57f, 0f);
            }
        }
    }
}
