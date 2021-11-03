using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float distanceY = 0f;
    public float smoothingY = 1f;

    private float speed;
    private Transform cam;

    Vector3 previousCamPos;
    // Start is called before the first frame update
    void Start()
    {   
        speed = Random.Range(0.2f,1f);
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*Time.deltaTime * speed);
        ParallaxEffect();

        if(cam.position.x - 2.81f > transform.position.x + transform.localScale.x/2 || cam.position.y - 5f > transform.position.y + transform.localScale.y/2){
            Debug.Log("invisible " + gameObject.name);
            gameObject.SetActive(false);
        }
    }

    /*void OnBecameInvisible()
    {
        
        if(cam.position.x > transform.position.x){
            Debug.Log("invisible " + gameObject.name);
            gameObject.SetActive(false);
        }
        
    }*/

    void ParallaxEffect(){

		if (distanceY != 0f) {
			float parallaxY = (previousCamPos.y - cam.position.y) * distanceY;
			Vector3 backgroundTargetPosY = new Vector3(transform.position.x, 
			                                           transform.position.y + parallaxY, 
			                                           transform.position.z);
			
			transform.position = Vector3.Lerp(transform.position, backgroundTargetPosY, smoothingY * Time.deltaTime);
		}

		previousCamPos = cam.position;	
	    
    }
}
