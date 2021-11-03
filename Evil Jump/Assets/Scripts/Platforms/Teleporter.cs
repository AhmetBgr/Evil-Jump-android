using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Sprite firstSprite;
    public Sprite secondSprite;
    public bool freezed = false;
    public float period;
    public Freezer freezer;

    private float time = 0f;

    private SpriteRenderer currentSprite;

    private GameObject player;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("player");

        currentSprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable(){
        freezed = false;
        freezer.FreezeEvent += Freeze;
    }
    
    void OnDisable(){
        freezer.FreezeEvent -= Freeze;
    }

    // Update is called once per frame
    void Update()
    {
        if(!freezed){
            time += Time.deltaTime;

            if(time > period){
                time = 0f;
                Teleport();
            }
        }
        
    }

    private void Freeze(bool isUpgraded){
        if(this != null){
            if(transform.position.y < player.transform.position.y + 15f){
                freezed = true;
            }
        }
    }

    // Teleports platform to the other side 
    private void Teleport(){
        Vector3 parentPos = transform.parent.position;
        if(parentPos.x > 0){
            currentSprite.sprite = firstSprite;
            Vector3 leftSidePos = new Vector3(-parentPos.x, transform.position.y, transform.position.z);
            transform.parent.position = leftSidePos;
        }
        else{
            currentSprite.sprite = secondSprite;
            Vector3 rightSidePos = new Vector3(-parentPos.x, transform.position.y, transform.position.z);
            transform.parent.position = rightSidePos;
        }
    }
}
