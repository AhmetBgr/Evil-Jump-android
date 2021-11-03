using UnityEngine;

public class Collectable : ScriptableObject
{
    public PoolObjectType type;
    public Sprite icon;
    public float defDuration; 
    public bool getOnStart; // player gets the collectable on start
    public bool hasThis; // does player current has this power up

    [HideInInspector] public bool isdurationFinished = false;
    public bool isUpgraded = false;

    public delegate void OnPickUpDelegate(Collectable powerUp);
    public virtual event OnPickUpDelegate PickUpEvent;

    // Player pick up function
    public virtual bool PickUp(){
        if(PickUpEvent != null){
            PickUpEvent(this);
            return true;
        }
        return false;
    }

    // Does the function when player used this collectable
    public virtual void Activate(){}
    
    // Upgrade the collectable
    public virtual void UpgradeFunc(){}
    
    public virtual void ResetShopFunc(){
        getOnStart = false;
        isUpgraded = false;
    }

    // End the skill if it is passive power
    public virtual void End(){}

    public virtual void Duration(){}

    // Collectable type
    public enum Type{
        Coin, GrayCoin, AirJump, Freezer, Replacer, MegaAirJump, WallRun, MegaJump, Magnet
    }

    void OnDisable(){
        hasThis = false;
    }
}
