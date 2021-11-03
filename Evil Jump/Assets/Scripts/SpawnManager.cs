using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int pRange = 4;
    private float minGap = 0.9f;
    private float maxGap = 1f;
    public PoolObjectType[] platformTypes;
    public PoolObjectType[] wallTypes;
    public PoolObjectType[] varietyTypes;
    private PoolObjectType[] curPTypes = new PoolObjectType[2];
    public Vector3 lastPlatformPos;

    public GameObject cam;
    public ColorCycler colorCycler;
    private bool isDifChange = false;
    private bool isWallSpawned = false;
    private bool isVarietySpawned = false;
    
    private int x = 1;
    private string lastPname;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector2(0,-14.5f);
        StartCoroutine("IncreaseDiffuculty");

        // Sets starting platform types
        curPTypes[0] = platformTypes[0];
        curPTypes[1] = platformTypes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(lastPlatformPos.y - cam.transform.position.y < 20f){ 
            int rand = Random.Range(0,38);
            GameObject prefab = null;
            if(rand <= 1 && lastPlatformPos.y >= 25f && !isWallSpawned){ // Spawn wall area           
                int rand3 = Random.Range(0,4);
                prefab = PoolManager.SharedInstance.GetPoolObject(wallTypes[rand3]);
                SetWallPos(prefab);
                isVarietySpawned = false;
            }
            else if((rand == 2 || rand == 3) && !isVarietySpawned){ // Spawn variety area
                int rand4 = Random.Range(0,varietyTypes.Length);
                prefab = PoolManager.SharedInstance.GetPoolObject(varietyTypes[rand4]);
                SetVarietyPos(prefab);
                isWallSpawned = false;
            }
            else{  // Spawn platform
                int rand2 = Random.Range(0,2);
                prefab = PoolManager.SharedInstance.GetPoolObject(curPTypes[rand2]);
                SetPlatformPos(prefab);
                isWallSpawned = false;
                isVarietySpawned = false;
            }
        }
    }
    
    // Increases difficulty every 20 units
    IEnumerator IncreaseDiffuculty(){
        while(true){
            yield return new WaitForSeconds(0.3f);

            float height = cam.transform.position.y;
            if(height >= x * 20f && !isDifChange) { 
                if(x%2 == 0)
                    colorCycler.Cycle();

                x++;
                IncreasePRange();
                IncreaseGap();
                SelectCurPlatforms();
                
                isDifChange = true;
            }
            if((height >= x * 20f) && (isDifChange)){
                isDifChange = false;
            }
        }

    }
    
    private void SetVarietyPos(GameObject prefab){
        Vector3 spawnPos = new Vector3(0, Random.Range(lastPlatformPos.y + 2f, lastPlatformPos.y + 2.4f), 0f);
        prefab.transform.position = spawnPos;
        prefab.SetActive(true);
        lastPlatformPos = new Vector3(0, spawnPos.y + prefab.GetComponent<PoolObject>().height, 0);
        lastPname = prefab.name;
        isVarietySpawned = true;
    }

    private void SetWallPos(GameObject prefab){
        Vector3 spawnPos = new Vector3(0, Random.Range(lastPlatformPos.y + 0.5f, lastPlatformPos.y + 1f), 0f);
        prefab.transform.position = spawnPos;
        prefab.SetActive(true);
        lastPlatformPos = new Vector3(0, spawnPos.y + prefab.GetComponent<PoolObject>().height, 0);
        lastPname = prefab.name;
        isWallSpawned = true;
    }

    // Changes current platform types
    private void SelectCurPlatforms(){
        for(int i=0; i<=1; i++){
            curPTypes[i] = platformTypes[Random.Range(0,pRange)];
        }
    }

    // Increases platform range to get a chance to spawn difficult platform types
    private void IncreasePRange(){
        if(pRange < platformTypes.Length){
            pRange++;
        }
    }

    // Increases gap betwwen two platforms
    private void IncreaseGap(){
        if(maxGap < 3.1f){
            maxGap += 0.04f;
        }
        else{
            if(minGap <= 3f){
                minGap += 0.01f;
            }   
        }
    }
    

    void SetPlatformPos(GameObject prefab){       
        Vector3 pSpawnPos = new Vector3(0, Random.Range(lastPlatformPos.y + minGap, lastPlatformPos.y + maxGap), 0f);

        if(lastPname == "platform_teleporter_main(Clone)"){ 
            
            pSpawnPos = new Vector3(0, Random.Range(lastPlatformPos.y + 1.3f, lastPlatformPos.y + 2.9f), 0f);
        }
 
        prefab.transform.position = pSpawnPos;

        if(prefab.name == "platform06_main(Clone)"){ 
            prefab.transform.position += new Vector3(Random.Range(-0.97f,0.97f), 0f, 0f);
        }
        else if(prefab.name == "platform_teleporter_main(Clone)"){
            float x = Random.Range(0,2) == 0 ? 1.3f : -1.3f;
            prefab.transform.position += new Vector3(x, 0f, 0f);
        }
        else{
            prefab.transform.position += new Vector3(Random.Range(-2.2f,2.2f), 0f, 0f); 
        }
        prefab.SetActive(true);
        lastPname = prefab.name;
        lastPlatformPos = prefab.transform.position;
        if(prefab.name == "platform06_main(Clone)"){
            lastPlatformPos += new Vector3(0f, prefab.GetComponent<PoolObject>().height);
        }
    }
}
