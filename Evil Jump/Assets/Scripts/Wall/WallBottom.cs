using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBottom : MonoBehaviour
{
    private Rigidbody2D playerRB;

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.CompareTag("player")){
            if(playerRB == null){
                playerRB = other.GetComponent<Rigidbody2D>();
            }
            if(playerRB.velocity.y > 0 ){
                playerRB.velocity = Vector2.zero;
            }
        }
    }
}
