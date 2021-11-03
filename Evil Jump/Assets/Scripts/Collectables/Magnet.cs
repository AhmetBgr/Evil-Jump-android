using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/Magnet")]
public class Magnet : Collectable{
    public float duration;
    public override event OnPickUpDelegate PickUpEvent;

    // Player pick up function
    public override bool PickUp(){
        if(PickUpEvent != null){
            Activate();
            PickUpEvent(this);
            return true;
        } 
        return false;
    }
    
    public void GoToPlayer(GameObject player, GameObject attachedGO){
        float dis = Vector2.Distance (player.transform.position, attachedGO.transform.position);
        
        if (dis < 4f){
            float speed = 25-dis;
            speed = speed *Time.deltaTime*.5f;
            attachedGO.transform.position = Vector2.MoveTowards (attachedGO.transform.position, player.transform.position, speed);
        }
    }

    // Does the function when player used this collectable
    public override void Activate(){
        hasThis = true;
        duration = defDuration;
        isdurationFinished = false;
    }
    
    public override void ResetShopFunc(){
        //hasThis = false;
        isUpgraded = false;
        getOnStart = false;
    }

    public override void Duration(){
        duration -= Time.deltaTime;
        if(duration <= 0){            
            End();
        }
    }

    // Ends the magnet skill
    public override void End( ){
        isdurationFinished = true;
        hasThis = false;
    }

}

