using UnityEngine;

public class PlatformDef : MonoBehaviour
{
    public bool ignoreReplacer;
    public bool isDestroyable;
    public Renderer platformRenderer;
    public Replacer replacer;
    public CollectableSpawner collectableSpawner;
    public GameObject particleObject;

    [HideInInspector] public PlayerAbilities playerAbilities;

    [HideInInspector] public Rigidbody2D playerRB;
    
    public float jumpForce;

    void OnEnable(){
        replacer.ReplacerEvent += Replace;

        if(transform.childCount != 0){ 
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnDisable(){
        replacer.ReplacerEvent -= Replace;
    }

    // Transforms this platform to broken platform if it is visible
    public void Replace(bool isUpgraded){
        if(platformRenderer != null && !ignoreReplacer){
            if(platformRenderer.isVisible){
                DisableCollectable();
                GameObject clone = PoolManager.SharedInstance.GetPoolObject(PoolObjectType.Platform02);
                clone.transform.position = transform.position;

                // Spawns a coin with a certain chance
                collectableSpawner.Spawn(clone.transform, 0.7f, PoolObjectType.Coin, isUpgraded);
                
                clone.SetActive(true);
                gameObject.SetActive(false);
            }
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("feet")){ // Jumps the player
            playerRB = other.GetComponentInParent<Rigidbody2D>();
         
            if(playerRB.velocity.y < 0 ){ 
                if(playerAbilities == null){ 
                    playerAbilities = other.GetComponentInParent<PlayerAbilities>();
                }
                
                PlayerControls.isDashing = false;
                playerAbilities.Jump(jumpForce);
                
                if(isDestroyable){ // Disables broken platform afther jumping

                    // Play breaking effect
                    GameObject pObject = Instantiate(particleObject, transform.position, Quaternion.identity); 
                    pObject.GetComponent<ParticleSystem>().Play();

                    // Deataches the child collectabe on this platform if has any
                    int childCount = transform.childCount;
                    if(childCount > 0){
                        for (int i = 0; i < childCount; i++){
                            Transform child = transform.GetChild(i);
                            if( child.CompareTag("collectable") ){
                                child.SetParent(null);
                            }
                        }
                    }

                    gameObject.SetActive(false);
                }
            }
        }   
    }

    // Disables any collectale on this platform
    public void DisableCollectable(){
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; ++i){
            Transform child = null;
            if(gameObject.transform.childCount > 0){
                child = transform.GetChild(i);

                if(child.tag == "collectable"){
                    PoolObjectType type = child.GetComponent<PoolObject>().type;
                    PoolManager.SharedInstance.DisableObject(child.gameObject, type);
                }
            }
        }
    }
}
