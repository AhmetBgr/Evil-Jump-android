using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/Mega Jump")]
public class MegaJump : Collectable
{
    public float jumpMultiplier;
    private float duration;
    public override event OnPickUpDelegate PickUpEvent;

    // Player pick up function
    public override bool PickUp()
    {
        if(PickUpEvent != null){
            Activate();
            PickUpEvent(this);
            return true;
        }
        return false;
    }

    // Does the function when player used this collectable
    public override void Activate(){
        hasThis = true;
        duration = defDuration;
        isdurationFinished = false;
    }

    public override void Duration(){
        
        duration -= Time.deltaTime;
        if(duration <= 0){
            End();
        }
    }

    public override void ResetShopFunc(){
        //hasThis = false;
        isUpgraded = false;
        getOnStart = false;
    }

    // Ends the megaJump skill
    public override void End( ){
        isdurationFinished = true;
        hasThis = false;
    }
    
}
