using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow; 
    public SceneLoader sceneLoader;
    public CoinCounter coinCounter;

    public static bool isFalling = false;
    public float lowerBorderY;
    [HideInInspector] 
    public float upperBorderY = 0;

    [HideInInspector] 
    public States state = States.Normal;

    private float time = 0f;
    private float delay = 1.3f;

    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start(){
        playerRb = objectToFollow.GetComponent<Rigidbody2D>();
        isFalling = false;
    }

    // Camera follow states
    public enum States{
        Normal, HighSpeed, Falling
    }

    void LateUpdate(){
        float targetPosY = objectToFollow.transform.position.y;
        float camPosY = transform.position.y;

        // Sets State to falling if player fals down to lower border
        if(state != States.Falling && targetPosY - camPosY <= lowerBorderY){  
            state = States.Falling;
        }
        // Sets upper border according to cam state
        else if(state == States.HighSpeed){ 
            upperBorderY = -3f;
        }
        else if(state == States.Normal){
            upperBorderY = 0f; 
        }
        else{
            upperBorderY = 3f;
        }

        // Follows the player according to camera state
        if(targetPosY - camPosY > upperBorderY){
            if(state == States.Normal){
                Follow(20f, 0f);
            }
            else if(state == States.HighSpeed){
                Follow(10f, upperBorderY);
            }
        }
        else if(state == States.Falling){
            Follow(Mathf.Pow(Mathf.Abs(playerRb.velocity.y), 2.5f)/50f, -2f);
            time += Time.deltaTime;
            if(time >= delay){ 
                // Changes to game over scene
                time = 0f;
                coinCounter.SaveCoinAmount(); 
                Score.SaveScore();               
                sceneLoader.LoadGameOverScene();
            }
        }
    }
    
    private void Follow(float speed, float border){
        Vector3 posToFollow = new Vector3(transform.position.x, objectToFollow.transform.position.y - border, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posToFollow, speed * Time.deltaTime);
    }
}
