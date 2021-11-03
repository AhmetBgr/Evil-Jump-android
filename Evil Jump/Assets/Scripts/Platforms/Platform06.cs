using UnityEngine;

public class Platform06 : MonoBehaviour
{
    public Freezer freezer;
    public Transform center;
    public Sprite firstFaze;
    public Sprite secondFaze;
    public GameObject particleObject;
    public bool freezed = false;
    public bool reverseAngle;

    private float rotationSpeed = 1f;
    private float radius = 1.2f;

    private int touchCount = 0;
    private SpriteRenderer platformSprite;
    private Rigidbody2D playerRB;
 
    private Vector2 _center;
    private float _angle;
    
    private GameObject player;
 
    private void Awake(){
        platformSprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void OnEnable(){
        touchCount = 0;
        transform.position = transform.parent.position;
        platformSprite.sprite = firstFaze;
        _center = center.position;
        freezer.FreezeEvent += Freeze;
    }

    private void OnDisable(){
        freezer.FreezeEvent -= Freeze;
        freezed = false;
        transform.position = transform.parent.position;
    }
 
    private void Update(){
        if(!freezed){ // Rotate platform
            _angle += rotationSpeed * Time.deltaTime;

            float x = Mathf.Sin(_angle);
            float y = Mathf.Cos(_angle);

            var offset = new Vector2(x, y) * radius;

            if(reverseAngle){
                offset *= -1;
            }

            transform.position = _center + offset;
        }
    }

    // Freezes when players uses freeze ability
    private void Freeze(bool isUpgraded){
        if(this != null){
            if(transform.position.y < player.transform.position.y + 15f){
                freezed = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("feet")){
            if(playerRB.velocity.y <= 0){

                touchCount++;
                if(touchCount == 1){ // Transform to broken platform
                    platformSprite.sprite = secondFaze;
                }
                else{ // Brake the platform
                
                    // Play breaking effect
                    GameObject pObject = Instantiate(particleObject, transform.position, Quaternion.identity); 
                    pObject.GetComponent<ParticleSystem>().Play();
                    
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
