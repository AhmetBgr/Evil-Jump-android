using System.Collections;
using UnityEngine;

public class Enemy01 : MonoBehaviour
{
    private float scaleX = 1f;
    private GameObject player;
    private Rigidbody2D playerRB;
    private PlayerControls playerControls;
    private PlayerAbilities playerAbilities;

    public EnemyType enemyType;
    public Freezer freezer;
    private Animator animator;
    private Vector3 defPos;
    private new ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider2D;
    private Transform parent;
    private Vector3 defLocalPos;

    public enum EnemyType{
        Harmless, Shooter
    };

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerControls = player.GetComponent<PlayerControls>();
        playerAbilities = player.GetComponent<PlayerAbilities>();
        defPos = transform.localPosition;
        if(enemyType == EnemyType.Harmless){
            animator = GetComponent<Animator>();
        }
        particleSystem = GetComponent<ParticleSystem>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defLocalPos = transform.localPosition;
    }

    void OnEnable(){
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        transform.localPosition = defPos;

        if(enemyType == EnemyType.Harmless){
            animator.enabled = true;
            freezer.FreezeEvent += Freeze;
        }
        else{
            GetComponent<ShootingSystem>().enabled = true;
        }
    }

    void OnDisable(){
        freezer.FreezeEvent -= Freeze;
    }

    // Freezes enemy when player uses upgraded freeze ability
    void Freeze(bool isUpgraded){
        if(this != null && isUpgraded){
            if(transform.position.y < player.transform.position.y + 15f && enemyType == EnemyType.Harmless){
                animator.enabled = false;
            }
        }        
    }

    // Faces to player. shooter enemy has this ability
    IEnumerator FaceTargetWithDelay(float delay){
        while(true){
            yield return new WaitForSeconds(delay);
            if(transform.position.x > player.transform.position.x){
                transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
            }
            else{
                transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
            }
        }
    }
    
    // Disables enemy with delay becuase destroy particle efect last at least 1 sec
    private IEnumerator Disable(){
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        if(enemyType == EnemyType.Harmless){
            animator.enabled = false;
        }
        else{
            GetComponent<ShootingSystem>().enabled = false;
            transform.GetChild(2).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1.5f);
        transform.SetParent(parent);
        transform.localPosition = defLocalPos;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        gameObject.SetActive(false);    
    }
    
    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("player") || other.gameObject.CompareTag("feet")){
            if(PlayerControls.isDashing){ // Player destroyed the enemy
                playerAbilities.BecomeInvincible(1);
                playerControls.EndDash();
         
                playerRB.velocity = new Vector2(0,0);
                transform.localScale = Vector3.one;
                parent = transform.parent;
                transform.SetParent(null);
                particleSystem.Play();
                StartCoroutine(Disable());
                
            }
            // Player jumps on enemy
            else if(other.gameObject.CompareTag("feet") && playerControls.canMoveLeft && playerControls.canMoveRight){ 
                if(playerRB.velocity.y < 0f){
                    playerRB.velocity = new Vector2(0,13f);
                }
            }
            else{ // enemy hits player
                Vector2 dir;
                if(other.transform.position.x - transform.position.x <= 0f){
                    dir = Vector2.left;
                }
                else{
                    dir = Vector2.right;
                }
                if(!playerAbilities.hasInvincibility){
                    playerControls.TakeHit(dir, 3.7f);
                }   
            }
        }
    }
}
