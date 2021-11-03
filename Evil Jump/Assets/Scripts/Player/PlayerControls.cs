using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Animator animator;
    public DashPrediction dashPrediction;
    public DashAssistImage dashAssistImage;
    
    private Rigidbody2D playerRB;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider;

    public static bool isDashing = false;
    public static bool hasStarted = false;

    public bool canMoveRight = true;
    public bool canMoveLeft = true;  

    public int dashSpeed;
    public float movementSpeed = 12f;
    public float dashBeginningTime;
    public Vector3 dashFacingDir;

    private bool facedLeft = false;
    private bool facedRight = false;
    private float dashHoldTimer = 0f;
    private float inputAx;

    public delegate void OnChangingDirDelegate(Vector3 dir);
    public event OnChangingDirDelegate OnChangingDir;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRB = GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();

        movementSpeed = 12f;
        Time.timeScale = 1;
    }
    
    void Update(){
        inputAx = Input.acceleration.x; // Gets gyro input
    }

    void FixedUpdate(){   
        
        if (hasStarted){      
            MovePlayer(); 
        }

        // Setting Speed var for animator to set player jump and fall anmation
        animator.SetFloat("Speed",playerRB.velocity.y);  

        if(Input.GetKeyDown(KeyCode.Space)){
            DashButtonDown();

        }
        if(Input.GetKeyUp(KeyCode.Space)){
            DashButtonUp();
        }        
    }

    // Flips player's sprite to left when movement dir left
    private void FaceLeft(){
        if(!facedLeft){
            spriteRenderer.flipX = false;
            dashFacingDir = Vector3.left;
            OnChangingDirEvent();

            facedLeft = true;
            facedRight = false;
        }  
    }

    // Flips player's sprite to right when movement dir right
    private void FaceRight(){
        if(!facedRight){
            spriteRenderer.flipX = true;
            dashFacingDir = Vector3.right;
            OnChangingDirEvent();
            facedRight = true;
            facedLeft = false;
        }
    }

    void OnChangingDirEvent(){
        if(OnChangingDir != null){
            OnChangingDir(dashFacingDir);
        }
    }

    // Moves player depending on input
    private void MovePlayer(){
        if( (inputAx >= 0.02f && canMoveRight && !isDashing) ||  Input.GetKey(KeyCode.RightArrow) ){
            inputAx = Input.GetKey(KeyCode.RightArrow) ? 0.3f : inputAx;
            Vector2 position = new Vector2(inputAx*movementSpeed,playerRB.velocity.y);
            playerRB.velocity = position;
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            FaceRight();
        }
        else if( (inputAx < -0.02f && canMoveLeft && !isDashing) || Input.GetKey(KeyCode.LeftArrow) ){
            inputAx = Input.GetKey(KeyCode.LeftArrow) ? -0.3f : inputAx;
            Vector2 position = new Vector2(inputAx*movementSpeed,playerRB.velocity.y);
            playerRB.velocity = position;
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            FaceLeft();
        }  

        transform.position = new Vector3(transform.position.x, transform.position.y, -5f);  
    }

    // if player hit enemy or bullet this will restrict his movement and causes to fall. 
    public void TakeHit(Vector2 direction, float magnitude){
        playerRB.velocity = Vector2.zero;        
        animator.Play("Player_Hurt", -1, 0f);
        StartCoroutine(Impact(0.35f, direction, magnitude));
    }

    // Pushes player to given direction and restricts the movement.
    IEnumerator Impact(float duration, Vector2 direction, float magnitude){
        
        playerRB.velocity = direction * magnitude; // Impact force    
        canMoveLeft = false;
        canMoveRight = false;
        collider.enabled = false;
        yield return new WaitForSeconds(duration);

        collider.enabled = true;
        canMoveLeft = true;
        canMoveRight = true;
        movementSpeed = 4f;
    }

    // Will be triggered at the end of hurt animation. do not delete
    public void EndHurtAnim(){
        animator.SetBool("isHitTaken", false);
    }

    // Gets dash begining time and facing dir to setup dash
    public void DashButtonDown(){
        // Slows time
        Time.timeScale = 0.05f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        
        dashBeginningTime = Time.time;
        if(!canMoveLeft || !canMoveRight){
            dashFacingDir = canMoveLeft ? Vector2.left : Vector2.right;
        }

        dashPrediction.startDashPrediction = true;
    }

    // Starts dashing depends on how long player hold dash button
    public void DashButtonUp(){
        // Ends dash prediction
        dashPrediction.startDashPrediction = false;
        dashPrediction.resetAssistPos = true;

        float dashTimeLimit = 0.2f;
        float dashLengthSpeed = 5f;
        canMoveLeft = false;
        canMoveRight = false;

        dashHoldTimer = (Time.time - dashBeginningTime) * dashLengthSpeed; 

        if(dashHoldTimer >= dashTimeLimit){ // This will limit how long player can dash 
            dashHoldTimer = dashTimeLimit;
        }

        // Back to normal time speed
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.deltaTime;
       
        isDashing = true;
        playerRB.velocity = dashFacingDir*dashSpeed; // Dash
        animator.SetBool("isDashing", true);
  
        Invoke("EndDash", dashHoldTimer);
    }

    public void EndDash(){
        canMoveLeft = true;
        canMoveRight = true;        
        playerRB.velocity = new Vector2(0,playerRB.velocity.y);
        animator.SetBool("isDashing", false);
        isDashing = false;
    }
    

}
