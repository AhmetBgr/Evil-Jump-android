using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyablePlatform : PlatformDef
{
    void OnEnable(){
        replacer.ReplacerEvent += Replace;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable(){
        replacer.ReplacerEvent -= Replace;
    }

    /*protected override void Replace(bool isUpgraded){
        base.Replace(isUpgraded);
    }*/

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        

        if(other.CompareTag("feet")){
            playerRB = other.GetComponentInParent<Rigidbody2D>();
         
            if(playerRB.velocity.y < 0 ){
                //other.GetComponentInParent<PlayerControls>().EndDash();
                //colorCycler.Cycle();

                /*if(Stats.hasMegaJump){
                    jumpForce = Mathf.Sqrt(jumpForce*jumpForce*3/2);
                }
                else{
                    jumpForce = 10f;
                }*/
                
                PlayerControls.isDashing = false;

                if(playerAbilities == null){
                    playerAbilities = other.GetComponentInParent<PlayerAbilities>();
                }

                //playerRB.velocity =new Vector2(0,0);
                
                //ballRigidbody.AddForce(Vector2.up*160f);
                //playerRB.velocity = new Vector2(0, jumpForce);
                
                playerAbilities.Jump(jumpForce);
                //transform.DOMoveY(transform.position.y - 0.5f, 0.2f);
                Debug.Log("afterJump");
                
                
                //Invoke("Disable", 0.15f);
                
            }
        }
    }

    void Disable(){
        //PoolManager.SharedInstance.DisableObject(gameObject, type);
    }
}
