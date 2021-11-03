using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/Replacer")]
public class Replacer : Collectable
{
    public delegate void OnReplacerDelegate(bool isUpgraded);
    public event OnReplacerDelegate ReplacerEvent;
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
        if(ReplacerEvent != null){
            ReplacerEvent(isUpgraded);
        }
    }

    // Upgrade the collectable
    public override void UpgradeFunc(){
        isUpgraded = true;
    }
    
    public override void ResetShopFunc(){
        isUpgraded = false;
        getOnStart = false;
    }
 


}
