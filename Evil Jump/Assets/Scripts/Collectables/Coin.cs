using UnityEngine;

[CreateAssetMenu (menuName = "Collectable/Coin")]
public class Coin : Collectable
{
    public bool isGray = false;
    public Magnet magnet;

    public override event OnPickUpDelegate PickUpEvent;

    // Player pick up function
    public override bool PickUp(){
        if(isGray){
            if(PlayerControls.isDashing){
                Activate();
                return true;
            }
            return false;
        }
        else{
            Activate();
            return true;
        }
    }

    // Does the function when player used this collectable
    public override void Activate(){
        FindObjectOfType<CoinCounter>().coin++;
    }


}
