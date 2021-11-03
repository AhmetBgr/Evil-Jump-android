using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/Freezer")]
public class Freezer : Collectable
{
    public delegate void OnFreezeDelegate(bool isUpgraded);
    public event OnFreezeDelegate FreezeEvent;
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
        if(FreezeEvent != null){
            FreezeEvent(isUpgraded);
        }
    }

    // Upgrade the collectable
    public override void UpgradeFunc(){
        isUpgraded = true;
    }
    
    public override void ResetShopFunc(){
        //hasThis = false;
        isUpgraded = false;
        getOnStart = false;
    }
 


}
