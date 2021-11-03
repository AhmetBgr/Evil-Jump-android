using UnityEngine;

public class PowerUp : MonoBehaviour
{   
    public Collectable collectable;
    public Magnet magnet;

    public PoolObjectType powerType;
    private GameObject player;

    // Start is called before the first frame update
    void Start(){           
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update(){
        if(magnet != null){
            if(magnet.hasThis && collectable.type == PoolObjectType.Coin){
                magnet.GoToPlayer(player, gameObject);
            }
        }
    }

    // Pick up power up
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("player")){
            bool pickedUp = collectable.PickUp();
            if(pickedUp){
                if(collectable.type == PoolObjectType.GrayCoin){
                    gameObject.SetActive(false);
                }
                else{
                    PoolManager.SharedInstance.DisableObject(gameObject, powerType);
                }   
            }   
        }
    }

    void OnDestroy(){
        collectable.ResetShopFunc();
    }
}
