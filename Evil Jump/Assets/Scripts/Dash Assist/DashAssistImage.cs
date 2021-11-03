using UnityEngine;

public class DashAssistImage : MonoBehaviour
{
    public DashPrediction dashPrediction;
    public SpriteRenderer dpSprite;
    public Rigidbody2D dashAssistRB;
    private GameObject dpGameObject;

    private Color defColor;

    public GameObject player;
    public Rigidbody2D playerRB;
    public PlayerControls playerControls;

    [HideInInspector] public Vector2 facingDir;

    void Start(){
        defColor = dpSprite.color;
        dpGameObject = dpSprite.gameObject;
        playerControls.OnChangingDir += FixDashAssistPos;
    }

    void OnTriggerEnter2D(Collider2D other){

        // Stops dash assists image if it is collide with a platform to tell player 
        // they can jump on the platform if they complete dashing
        if(other.CompareTag("platform") || other.CompareTag("enemy")){ 
            facingDir = dashAssistRB.velocity.x < 0 ? Vector2.left : Vector2.right;
            dashAssistRB.constraints = RigidbodyConstraints2D.FreezePositionX;
            dpSprite.color = new Color(255, 236, 0, 130);
        }                                            
    }

    void OnTriggerExit2D(Collider2D other){
        // Fixes dash assist image's position after player passes
        // the platform while they continues holding dash button
        if(other.CompareTag("platform") || other.CompareTag("enemy")){
            dashAssistRB.constraints = RigidbodyConstraints2D.None;

            FixDashAssistPos(playerControls.dashFacingDir);

            dpSprite.color = defColor;
        }   
    }

    // Fixes dash assist image's position. can be used where dash assist image stoped
    public void FixDashAssistPos(Vector3 dashFacingDir){
        Vector3 dashPos = transform.position;
        dashAssistRB.constraints = RigidbodyConstraints2D.None;
        
        transform.position = new Vector3(player.transform.position.x + dashPrediction.distanceCovered * dashFacingDir.x,
        dashPos.y, dashPos.z);
        
    }
 
}
