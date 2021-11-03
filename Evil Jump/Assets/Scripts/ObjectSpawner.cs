using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform goldSpawnPoint;
    public Transform enemySpawnPoint;
    public Transform platformSpawnPoint;

    public GameObject enemy;
    public bool spawnWallRun; 

    static int _wallRunSpawnChance = 80;
    private int wallRunSpawnChance{
        get{ return _wallRunSpawnChance; }
        set{
            if(value > 10){
                _wallRunSpawnChance = value;
            }
        }
    }

    private Vector3 coinSpawnPos;
    private Vector3 enemySpawnPos;
    private Vector3 platformSpawnPos;

    public int coinAmount;
    public float maxPlatformPosY;
    public float platformRangeX;

    public GameObject[] childsToFlip;
    public PoolObjectType[] platformTypes;

    private Vector3[] defaultChildPos;


    void OnEnable(){
        Flip();
        SpawnCoins();
        SpawnPlatforms();
        if(spawnWallRun){
            if(Random.value * 100 < wallRunSpawnChance){
                SpawnWallRun();
            }
        }    
    }

    void OnDestroy(){
        wallRunSpawnChance = 80;
    }

    // Flips objects vertically
    private void Flip(){
        int rand = Random.Range(0,2);

        // Set spawn positions
        if(rand == 0){
            coinSpawnPos.Set(-goldSpawnPoint.position.x, goldSpawnPoint.position.y, goldSpawnPoint.position.z);
            
            platformSpawnPos.Set(-platformSpawnPoint.position.x, platformSpawnPoint.position.y, platformSpawnPoint.position.z);

            if(enemySpawnPoint != null){
                enemySpawnPos.Set(-enemySpawnPoint.position.x, enemySpawnPoint.position.y, enemySpawnPoint.position.z);
                enemy.transform.position = enemySpawnPos;
                enemy.SetActive(true);
            }
            else{
                enemy.SetActive(false);
            }   
        }
        else{
            coinSpawnPos = goldSpawnPoint.position;

            platformSpawnPos = platformSpawnPoint.position;  

            if(enemySpawnPoint != null){
                enemySpawnPos = enemySpawnPoint.position;
                enemy.transform.position = enemySpawnPos;
                enemy.SetActive(true);
            }
        }
        
        // Flip 
        for (int i = 0; i < childsToFlip.Length; i++){   
            Vector3 childPos = childsToFlip[i].transform.position;
            if(childPos.x * coinSpawnPos.x < 0f){
                childsToFlip[i].transform.position = new Vector3(-childPos.x, childPos.y, childPos.z);
            }
                
        }
    }

    private void SpawnCoins(){
        float range = 1.5f;
        Vector3 spawnPos = coinSpawnPos;
        for (int i = 0; i < coinAmount; i++)
        {
            // Spawn Coins 
            GameObject coin = PoolManager.SharedInstance.GetPoolObject(PoolObjectType.Coin);
            coin.transform.position = spawnPos;
            coin.SetActive(true);
            spawnPos += new Vector3(0f, range, 0f);
        }
    }

    private void SpawnEnemy(){

        GameObject enemy = PoolManager.SharedInstance.GetPoolObject(PoolObjectType.Enemy4);
        enemy.transform.position = enemySpawnPos;
        enemy.SetActive(true);

    }

    private void SpawnPlatforms(){

        
        Vector3 spawnPos = platformSpawnPos;
        float spawnPosX =spawnPos.x;
        
        while(spawnPos.y - platformSpawnPoint.position.y < maxPlatformPosY){

            spawnPos =  new Vector3(Random.Range(spawnPos.x - platformRangeX/2,spawnPos.x + platformRangeX/2),spawnPos.y,spawnPos.z);
            GameObject clone = PoolManager.SharedInstance.GetPoolObject(platformTypes[Random.Range(0,platformTypes.Length)]);
            clone.transform.position = spawnPos;
            spawnPos = new Vector3(spawnPosX,spawnPos.y + Random.Range(0.6f,2f), 0f);
            clone.SetActive(true);
        }

        if(spawnPos.y > maxPlatformPosY && maxPlatformPosY - spawnPos.y >= 0.6f){
            spawnPos = new Vector3(spawnPos.x, maxPlatformPosY, spawnPos.z);
            GameObject clone = PoolManager.SharedInstance.GetPoolObject(platformTypes[Random.Range(0,platformTypes.Length)]);
            clone.transform.position = spawnPos;
            clone.SetActive(true);
        }
    }

    private void SpawnWallRun(){
        Vector3 spawnPos =  coinSpawnPos + new Vector3(0, -0.75f, 0f);
        
        GameObject clone = PoolManager.SharedInstance.GetPoolObject(PoolObjectType.WallRun);
        clone.transform.position = spawnPos;
        clone.SetActive(true);
        wallRunSpawnChance -= 5;
        
    }
}
