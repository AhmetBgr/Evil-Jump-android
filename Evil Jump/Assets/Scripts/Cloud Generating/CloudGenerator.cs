using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public float period;

    private float time = 0f;
    private Vector3 spawnPos;
    private Vector3 localScale;

    private float posY;
    private float camPosY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > period){
            time = 0f;
            Generate();
        }
    }

    void Generate(){
        GameObject cloud = CloudPooler.SharedInstance.GetPooledObject(); 
        if (cloud != null) {
            camPosY = transform.position.y;
            spawnPos = new Vector3(5f, Random.Range(camPosY, camPosY +10f), 10f);
            localScale = new Vector3(Random.Range(2f,4f),Random.Range(0.4f,2f),1f);
            cloud.transform.position = spawnPos;
            cloud.transform.localScale = localScale;
            //cloud.transform.rotation = turret.transform.rotation;
            cloud.SetActive(true);
            
        }
    }
}
