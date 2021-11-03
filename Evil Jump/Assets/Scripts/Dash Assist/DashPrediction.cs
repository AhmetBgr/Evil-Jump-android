using UnityEngine;


public class DashPrediction : MonoBehaviour
{
    public Rigidbody2D dpRigidBody;
    private GameObject dpGameObject;

    private Rigidbody2D playerRB;
    private PlayerControls playerControls;

    [HideInInspector] public Vector2 facingDir;

    [HideInInspector] public bool startDashPrediction = false;
    [HideInInspector] public bool resetAssistPos = true;
    public float dpImageSpeed;
    public float distanceCovered = 0f;

    void Start(){
        playerRB = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerControls>();
        dpGameObject = dpRigidBody.gameObject;
    }

    void FixedUpdate(){
        if(PauseMenu.isDashAssistOn == 1){
            if(startDashPrediction){
                PredictDash();   
            }
            else{
                dpGameObject.SetActive(false);
            }
        }
    }

    // Dynamically predicts (while player holding dash button) where player would dash after completing dashing
    private void PredictDash(){

        if(resetAssistPos){
            dpGameObject.transform.position = transform.position;
            resetAssistPos = false;
        }
        
        dpGameObject.SetActive(true);

        float dashTime2 = (Time.time - playerControls.dashBeginningTime) *5f;
        if(dashTime2 < 0.2f){
            dpRigidBody.velocity = new Vector2(playerControls.dashFacingDir.x * dpImageSpeed, playerRB.velocity.y);
        }
        else{
            dashTime2 = 0.2f;
            dpRigidBody.velocity = playerRB.velocity;
        }
        distanceCovered = dashTime2 * dpImageSpeed/5f;    
    }
}
