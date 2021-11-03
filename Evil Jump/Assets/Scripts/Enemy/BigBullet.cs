using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public float speed;
    public Freezer freezer;
    private bool isFired = false;
    private bool fall = false;
    private Vector2 dir;

    private bool freezed = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
        isFired = true;

        if(transform.position.x > 0){
            dir = Vector2.left;
            transform.localScale = new Vector3(-0.6f, transform.localScale.y, transform.localScale.z);
        }
        else{
            dir = Vector2.right;
            transform.localScale = new Vector3(0.6f, transform.localScale.y, transform.localScale.z);
        }

        player = GameObject.FindGameObjectWithTag("player");

    }

    void OnEnable(){
        freezer.FreezeEvent += Freeze;
    }
    
    void OnDisable(){
        freezer.FreezeEvent -= Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        if(!freezed){ // movement of bullet
            if(isFired){
                transform.Translate(dir*speed*Time.deltaTime);
            }
            else if(fall){
                transform.Translate(Vector2.down*speed*2*Time.deltaTime);
            }

            if(Mathf.Abs(transform.position.x) > 3f){ // Destroy if outside of the cam view
                Destroy(gameObject);
            } 
        }
        else{ // Destroy if outside of the cam view
            if(player.transform.position.y - transform.position.y >= 4.6f){
                Destroy(gameObject);
            } 
        }
        
    }

    // Freezes when players uses upgraded freeze ability
    void Freeze(bool isUpgraded){
        if(this != null && isUpgraded){
            if(transform.position.y < player.transform.position.y + 15f){
                freezed = true;
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("player") && !freezed){ // Hits player
            Destroy(gameObject,0.2f);
            other.gameObject.GetComponent<PlayerControls>().TakeHit(dir, 4f);
        }
        else if(other.CompareTag("feet")){ // Jumps player if they landed on bullet

            PlayerAbilities playerAbilities = other.GetComponentInParent<PlayerAbilities>();
            playerAbilities.Jump(12f);
            fall = true;
            isFired = false;

        }
        else if(other.CompareTag("wall")){ // Destroys itself if touched with the wall
            Destroy(gameObject,0.2f);
        }

    }
}
