using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    int leftOrRight; // 0 = left, 1 = Right.
    public Freezer freezer;
    public bool switchup;
    static float speed = 0.8f;
    private float maxSpeed = 5f;
    float maxX;
    float minX;

    public bool freezed = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start(){

        player = GameObject.FindGameObjectWithTag("player");
    }

    void OnEnable(){
        leftOrRight = Random.Range(1,3);
        
        switchup = (leftOrRight == 1) | false ;

        float scale = transform.localScale.x/2;
        maxX = 2.80f - scale; 
        minX = -maxX;
        freezer.FreezeEvent += Freeze;
        
        // Increases platform speed with %33 chance every time this platform spawned
        if(Random.Range(0,3) == 0){
            IncreaseSpeed(0.08f);
        }
    }

    void OnDisable(){
        freezed = false;
        freezer.FreezeEvent -= Freeze;
    }

    void OnDestroy(){
        // reset speed
        speed = 0.8f;
    }

    // Update is called once per frame
    void Update(){   
        if(!freezed){
            Move();
        }    
    }

    private void IncreaseSpeed(float value){
        if(speed <= maxSpeed - value){
            speed += value;
        }
    }

    private void Freeze(bool isUpgraded){
        if(this != null){
            if(transform.position.y < player.transform.position.y + 15f){
                freezed = true;
            }
        }
    }

    void Move(){
        float posX = transform.position.x;
        if(posX >= maxX && switchup){
            switchup = false;
            leftOrRight = 0;
        }
        else if(posX <= minX && !switchup){
            switchup = true;
            leftOrRight = 1;
        }
        else{
            if(leftOrRight == 1){
                transform.Translate(new Vector3(speed,0,0)*Time.deltaTime);
            }
            else{
                transform.Translate(new Vector3(-speed,0,0)*Time.deltaTime);
            }   
        }
    }
}


