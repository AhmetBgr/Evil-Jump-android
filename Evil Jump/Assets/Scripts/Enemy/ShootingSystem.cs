using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public Freezer freezer;
    public SpriteRenderer sight;
	public float followSpeed;
    public float returnSpeed;
    public float shootRate;
    public float shootRate2;
    public int bulletSpeed;
    static int level = 1;
    private int shootamount = 1;

	public GameObject bullet;

	public Transform center; // Center points
    public Transform bulletOrigin; // Bullet origin points

	[Range(0,6)]
	public float viewDistance;

	[Range(0,180)]
	public float viewAngle;

	private float x = 180f;

	[HideInInspector] public bool isInSight = false;
    private bool freezed = false;

	private float time = 0f;
    private float time2 = 0f;
    private int shootCount = 0;
    private float angle;
	private bool isfired  = false;
	private float idleAngle = 180;

	private Vector2 curFacingDir;
    private float time3 = 0;
    private float facingdelay = .5f;

	public LayerMask obstacleMask;

    [HideInInspector] public  GameObject player;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider2D;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
	}

    void OnEnable(){
    
        if(transform.position.x > 0){ 
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z); // Face right
            idleAngle = 0f; // default idle angle
            x = 360f;
            curFacingDir = Vector2.right; // Keep facing dir
        }
        else{ 
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); // face left
            idleAngle = 180f; // default idle angle
            x = 180f;
            curFacingDir = Vector2.left; // Keep facing dir
        }

        freezer.FreezeEvent += Freeze;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;

        // Increases enemy level every time this enemy spawned by certain chance
        if(Random.Range(0,6) == 0){
            IncreaseLevel(1);
            shootamount = level;
        }
    }

    void OnDisable(){
        freezed = false;
        freezer.FreezeEvent -= Freeze;
        sight.gameObject.SetActive(true);
    }
    
    void OnDestroy(){
        // Reset Enemy level on game over
        level = 1;
    }

	void Update()
    {
        float facingDir = player.transform.position.x > transform.position.x ? 1 : -1; // 1 Right, -1 Left

        // Faces to the player after the delay
        if(curFacingDir.x != facingDir){
            time3 += Time.deltaTime;
            if(time3 > facingdelay){
                if(player.transform.position.x > transform.position.x){
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    idleAngle = 0f;
                    x = 360f;
                    curFacingDir = Vector2.right;
                }
                else{
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    idleAngle = 180f;
                    x = 180f;
                    curFacingDir = Vector2.left;
                }
                time3 = 0;
            }
        }

        Vector2 direction =  player.transform.position - center.position;

        angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        if(!freezed){
            if(isInSight){
                Aim();
            }
            else{
                // IDLE POS
                ReturnIdlePos();
            }
        }        
    }

    void FixedUpdate(){
        CheckForPlayerInSight();
    }

    private void IncreaseLevel(int value){
        if(level <= 3){
            level++;
        }
    }

    // Lerps sight color
    private void LerpColor(){
        if(sight.color == Color.green){
            sight.color = Color.Lerp(Color.green, Color.red, Time.time);
        }
        else if(sight.color == Color.red){
            sight.color = Color.Lerp(Color.red, Color.green, Time.time);
        }
    }
    
    // Freezes when players uses upgraded freeze ability
    public void Freeze(bool isUpgraded){
        if(this != null && isUpgraded){
            if(transform.position.y < player.transform.position.y + 15f){
                freezed = true;
            }
        }
    }

    void Aim(){

        Quaternion rotation = Quaternion.AngleAxis(angle + x,Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,followSpeed*Time.deltaTime);

        if(!isfired){

            time += Time.deltaTime;

            if(time > shootRate){
                
                time2 += Time.deltaTime;
                if(time2 > shootRate2){
                    time2 = 0;
                    shootCount++;
                    Shoot();

                    if(shootCount == shootamount){
                        time = 0;
                        shootCount = 0;
                    }
                }
            }
        }
    }

    // Returns the idle state
    void ReturnIdlePos(){
        time = 0;
        time2 = 0;
        Quaternion rotation = Quaternion.AngleAxis(idleAngle + x,Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,returnSpeed*Time.deltaTime);
    }

    // Shoots a bullet
    void Shoot(){
        Vector3 bulletOP = bulletOrigin.position; // bullet origin points

        GameObject bulletClone = (GameObject)Instantiate(bullet,bulletOP,transform.rotation);

        Vector2 dir =  bulletOP - center.position;
        bulletClone.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletSpeed;

        isfired = false;
    }

    // Checks if player is in sight
    private void CheckForPlayerInSight(){
        Vector3 centerPos = center.transform.position;
		Transform target = player.transform;
		
        // Checks vertical sight
        if(target.position.y > centerPos.y - viewDistance && target.position.y < centerPos.y + viewDistance){
            Vector2 dirToTarget;
            dirToTarget = (target.position - centerPos).normalized;

            // Checks conic angle
            if (Vector2.Angle (curFacingDir, dirToTarget) < viewAngle / 2  ) { // || Vector2.Angle (Vector2.left, dirToTarget) < viewAngle / 2
                float dstToTarget = Vector2.Distance (centerPos, target.position);

                // Checks if ther is obstecle between player and enemy
                if (!Physics2D.Raycast (centerPos, dirToTarget, dstToTarget, obstacleMask)) {
                    isInSight = true;
                }
                else{
                    isInSight = false;
                }
            }
            else{
                isInSight = false;
            }
        }
        else{
            isInSight = false;
        }
    }

	public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}	
}
