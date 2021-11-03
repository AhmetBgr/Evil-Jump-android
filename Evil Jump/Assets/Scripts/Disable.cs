using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour
{
    private float distance;
    
    public float height;
    public bool isMain = true;
    public GameObject[] childsToEnable;
    public PoolObjectType type;

    private GameObject cam;
    private GameObject parent;
    private Collider2D collider2d;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        //objectInfo = PoolManager.SharedInstance.objects[0];
        
        
        distance = 5f + height;

        if(transform.parent != null){
            parent = transform.parent.gameObject;
        }

        if(gameObject.name == "Platform06(Clone)"){
            collider2d = GetComponentInChildren<Collider2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        else{
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
    }

    void OnEnable(){
        if(childsToEnable.Length != 0){
            foreach (var child in childsToEnable)
            {
                child.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update(){
        
        if(cam.transform.position.y - transform.position.y > distance){
            DisableObject();
        }
    }

    public void DisableObject(){

        if(isMain){
            PoolManager.SharedInstance.DisableObject(gameObject, type);
        }
        else{
            gameObject.SetActive(false);
        }
    }
}
