using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftSideWrapping : MonoBehaviour
{
    public GameObject cam;

    // Update is called once per frame
    void Update()       
    {
        float camPosY = cam.transform.position.y;

        transform.position = new Vector3(-3.6f,camPosY,0f);
    }

    // Teleports player to right side of the screen
    void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "player"){
            other.transform.position = new Vector2(2.82f,other.transform.position.y);
        }
        else if(other.CompareTag("dashPrediction")){
            other.transform.position = new Vector2( ( 2.813f * 2 - Mathf.Abs(other.transform.position.x) ), other.transform.position.y);
        }

    }
}
