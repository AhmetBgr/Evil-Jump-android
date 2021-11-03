using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/MegaAirJump")]
public class MegaAirJump : Collectable
{
    public int speed;
    private float duration;
    public int maxSpawnNum = 1;
    public override event OnPickUpDelegate PickUpEvent;

    // Player pick up function
    public override bool PickUp(){
        if(PickUpEvent != null){
            PickUpEvent(this);
            return true;
        } 
        return false;
    }

    // Does the function when player used this collectable
    public override void Activate(){
        if(!hasThis){
            FindObjectOfType<ButtonSlots>().AddPassivePU(this);
        }
        hasThis = true;
        duration = defDuration;
        isdurationFinished = false;
    }

    // Upgrade the collectable
    public override void UpgradeFunc(){
        maxSpawnNum = 3;
        isUpgraded = true;
    }
    
    public override void ResetShopFunc(){
        maxSpawnNum = 1;
        //hasThis = false;
        isUpgraded = false;
        getOnStart = false;
    }

    public override void Duration(){
        duration -= Time.deltaTime;
        if(duration <= 0){
            isdurationFinished = true;
        }
    }

    // Ends the megaAirJump skill
    public override void End( ){
        // has wall run false
        hasThis = false;
        isdurationFinished = true;
    }
}
