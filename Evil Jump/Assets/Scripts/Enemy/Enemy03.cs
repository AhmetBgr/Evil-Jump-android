using System.Collections;
using UnityEngine;

public class Enemy03 : MonoBehaviour
{
    public float fireRate;
    public Freezer freezer;
    public GameObject bigBullet;
    private GameObject player;
    private bool freezed = false;

    void Start(){
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        freezed = false;
        
        StartCoroutine("Fire");
        freezer.FreezeEvent += Freeze;
      
    }

    void OnDisable(){
        StopAllCoroutines();
        freezer.FreezeEvent -= Freeze;
    }

    void Freeze(bool isUpgraded){
        if(this != null && isUpgraded){
            if(transform.position.y < player.transform.position.y + 15f){
                freezed = true;
            }
        }
        
    }

    IEnumerator Fire(){
        while(!freezed){
            yield return new WaitForSeconds(fireRate);

            if(!freezed){
                Vector3 spawnPos = new Vector3(transform.position.x,transform.position.y,1f);

                GameObject bigBulletClone = (GameObject)Instantiate(bigBullet,spawnPos,Quaternion.identity);
            }

        }

    }
}
