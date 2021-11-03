using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/AirJump")]
public class AirJump : Collectable
{
    public int jumpForce;

    public delegate void OnAirJumpDelegate();
    public event OnAirJumpDelegate AirJumpEvent;

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
        if(AirJumpEvent != null){
            AirJumpEvent();
        }
    }

    public override void ResetShopFunc(){
        getOnStart = false;
    }


}
