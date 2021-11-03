using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int rng = Random.Range(1,3);

        if(rng == 1){

            for(int i =0;i<transform.childCount; i++){

                Transform child = transform.GetChild(i);

                if(child.CompareTag("enemy")){
                    Vector3 enemyPos = child.transform.position;
                    child.transform.position = new Vector3(-enemyPos.x, enemyPos.y, enemyPos.z);
                }else{
                    child.localScale = new Vector3(-child.localScale.x,child.localScale.y,child.localScale.z);
                }
                
                //Debug.Log("flip " + child.name);
            }
            //transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);

        }
        
    }


}
