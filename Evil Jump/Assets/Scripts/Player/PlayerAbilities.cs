using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public AirJump airJump;
    public MegaAirJump megaAirJump;
    public WallRun1 wallRun;
    public MegaJump megaJump;

    public Animator animator;
    public PlayerControls playerControls;
    public CameraFollow cameraFollow;

    public bool hasInvincibility = false;

    private Rigidbody2D rb;

    private string airJumpKey = "AirJumped";
    private string megaAirJumpKey = "hasMegaAirJump";
    private string wallRunKey = "hasWallRun";
    private string wallslideKey = "wallSliding";

    private string wallTag = "wall";

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable(){
        airJump.AirJumpEvent += AirJump;
    }

    void OnDisable(){
        airJump.AirJumpEvent -= AirJump;
    }

    void FixedUpdate(){
        if(megaAirJump.hasThis){
            MegaAirJump();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag(wallTag)){ 
            //
            // Prevents player from passing through the wall.
            // Don't use 'is trigger = false' because that causes an issue that 
            // allows player to stop when turning the phone 90 degrees 
            // while touching the wall
            // 

            cameraFollow.state = CameraFollow.States.Normal;
            
            Vector2 velocity = rb.velocity;
            if(velocity.x < 0f){
                playerControls.canMoveLeft = false;
                rb.velocity = new Vector2(0,velocity.y);
            }
            else if(rb.velocity.x > 0f ){
                playerControls.canMoveRight = false;
                rb.velocity = new Vector2(0,velocity.y);
            }
        }
        else if(other.CompareTag("wallBottom")){ // Ends MegaAirjump skill
            megaAirJump.End();
            animator.SetBool(megaAirJumpKey, false);
        }
    }
    
    void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag(wallTag)){ // Lets the player run through walls if they have the wallRun ability
            
            if(!megaAirJump.hasThis && cameraFollow.state != CameraFollow.States.Falling){
                cameraFollow.state = CameraFollow.States.Normal;
            }
            
            if(wallRun.hasThis && !PlayerControls.isDashing && !megaAirJump.hasThis){ 
                // Wall run
                rb.velocity = new Vector2(0, 7f);
                animator.SetBool(wallRunKey, true);
                animator.SetBool(wallslideKey, false);
            }
            else{ 
                animator.SetBool(wallRunKey, false);
                if(rb.velocity.y < 0f && !PlayerControls.isDashing){ // Wall slide
                    
                    rb.velocity = new Vector2(0, rb.velocity.y + 0.009f);
                    
                    animator.SetBool(wallslideKey, true);
                }
                else{
                    animator.SetBool(wallslideKey, false);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag(wallTag)){ 
            // Removes any movement restrictions
            playerControls.canMoveLeft = true;
            playerControls.canMoveRight = true;
            animator.SetBool(wallRunKey, false);
            animator.SetBool(wallslideKey, false);
        }
    }

    // Jumps the player when they are on the platform
    public void Jump(float jumpForce){
        cameraFollow.state = CameraFollow.States.Normal;
        
        if(PlayerControls.isDashing){
            playerControls.EndDash();
        }
        
        jumpForce = megaJump.hasThis ? jumpForce * megaJump.jumpMultiplier : jumpForce;

        rb.velocity = new Vector2(0f, jumpForce);
        playerControls.movementSpeed = 12f;
    }

    // Makes player invincible when they are shot by an enemy
    public void BecomeInvincible(float duration){
        StartCoroutine(Invinciblity(duration));
    }

    private IEnumerator Invinciblity(float duration){
        hasInvincibility =  true;
        yield return new WaitForSeconds(duration);
        hasInvincibility = false;
    }

    // Jumps the player in air when they uses the ability
    private void AirJump(){
        cameraFollow.state = CameraFollow.States.Normal;
        playerControls.movementSpeed = 12f;
        rb.velocity = new Vector2(0f, airJump.jumpForce);
        //wallRun.End();
        animator.SetBool(airJumpKey,true);
        animator.SetBool(wallRunKey,false);
    }

    // Lets player fly for a certian duration when they pick up MegaAirJump power up 
    private void MegaAirJump(){
        cameraFollow.state = CameraFollow.States.HighSpeed;
        playerControls.movementSpeed = 12f;
        megaAirJump.Duration();
        rb.velocity = new Vector2(rb.velocity.x, megaAirJump.speed);
        animator.SetBool(megaAirJumpKey, true);

        if(megaAirJump.isdurationFinished){
            cameraFollow.state = CameraFollow.States.Normal;
            animator.SetBool(megaAirJumpKey, false);
            megaAirJump.End();
        }
    }

    public void EndAirJumpAnim(){
        animator.SetBool(airJumpKey,false);
    }
}
