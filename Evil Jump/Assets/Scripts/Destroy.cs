using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private GameObject cam;
    public float distance;

    public bool UseManuelHeightInput;
    public float height;
    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        if(UseManuelHeightInput){
            distance = 5f + height/2;
        }
        else{
            distance = 5f + transform.localScale.y/2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(cam.transform.position.y - transform.position.y > distance){
            //Debug.Log("destroyed"+ gameObject.name);
            //if(gameObject.tag == "platform")

            Destroy(gameObject);
            
        }
    }

    /*void OnBecameInvisible()
    {
        Debug.Log("invisible " + gameObject.name);
        if(gameObject != null){
            
            if(cam.transform.position.y > transform.position.y ){
                Destroy(gameObject);
            }
        }
        
    }*/
}
