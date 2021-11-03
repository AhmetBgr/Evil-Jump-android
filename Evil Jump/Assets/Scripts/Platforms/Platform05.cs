using UnityEngine;
using DG.Tweening;

public class Platform05 : PlatformDef
{
    private bool rigidbodyTaken = false;

    private int jumpCount = 0;
 
    void OnEnable(){
        replacer.ReplacerEvent += Replace;
    }

    void OnDisable(){
        replacer.ReplacerEvent -= Replace;
        jumpCount = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("feet")){
            
            if(!rigidbodyTaken){ // Get player rigidbody
                playerRB = other.GetComponentInParent<Rigidbody2D>();  
                rigidbodyTaken = true; 
            }
            if(playerAbilities == null){ // Get playerAbility
                playerAbilities = other.GetComponentInParent<PlayerAbilities>();
            }
            if(playerRB.velocity.y < 0f){ // Jump player
                jumpCount++;
                if(jumpCount == 1){
                    jumpForce = 11f; 
                }
                else{
                    jumpForce = 11f;
                }
                transform.DOMove(transform.position - new Vector3(0f, 0.4f, 0f), 0.6f);
                playerAbilities.Jump(jumpForce);    
            }
        }
    }
}
