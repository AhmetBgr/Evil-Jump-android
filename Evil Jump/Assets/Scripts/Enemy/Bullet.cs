using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Freezer freezer;
    private bool isFreezed = false;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable(){
        freezer.FreezeEvent += Freeze;
    }
    void OnDisable(){
        freezer.FreezeEvent -= Freeze;
    }

    // Freezes when players uses upgraded freeze ability
    private void Freeze(bool isUpgraded){   
        if(this != null && isUpgraded){
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isFreezed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("player") || other.CompareTag("feet")){
            if(!isFreezed){
                if(!PlayerControls.isDashing){
                    
                    Vector2 dir = rb.velocity.x >= 0 ? Vector2.right : Vector2.left;

                    if(other.CompareTag("player")){ // Hit player
                        other.gameObject.GetComponent<PlayerControls>().TakeHit(dir, 3.5f);
                    }
                    else{
                        other.transform.parent.gameObject.GetComponent<PlayerControls>().TakeHit(dir, 3.5f);
                    }
                    Destroy(gameObject);
                }else{
                    Destroy(gameObject);
                }
            }
            else{
                rb.constraints = RigidbodyConstraints2D.None;
                rb.gravityScale = 1;
            }
        }
        else if(other.CompareTag("wall")){
            Destroy(gameObject);
        }
    }
}
