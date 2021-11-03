using UnityEngine;
using DG.Tweening;

public class PanelFlipper : MonoBehaviour
{
    public GameObject panels;

    private Animator flipAnimator;


    // Start is called before the first frame update
    void Start(){
        flipAnimator = panels.GetComponent<Animator>();

        //Invoke("OpenShopPanel", 1f);     
        MovePanel(-1625f, 1f, 1.2f);   
    }

    void Update(){
        if(Swipe.swipedLeft || Input.GetKey(KeyCode.RightArrow)){
            panels.transform.DOLocalMoveX(-1625f, 0.5f);
            MovePanel(-1625f, 0.5f, 0f);
        }
        else if(Swipe.swipedRight || Input.GetKey(KeyCode.LeftArrow)){
            MovePanel(0f, 0.5f, 0f);
        }
    }

    private void MovePanel(float toX, float duration, float delay){
        panels.transform.DOLocalMoveX(toX, duration).SetDelay(delay);
    }

    // Currently not used
    // Flips to shop panel
    void OpenShopPanel(){
        flipAnimator.SetBool("openShop", true);
    }

    // Currently not used
    // Flip to other panel
    public void Flip(){
        if(flipAnimator.GetBool("openShop")){
            flipAnimator.SetBool("openScore", true);
            flipAnimator.SetBool("openShop", false);

        }
        else if(flipAnimator.GetBool("openScore")){
            flipAnimator.SetBool("openShop", true);
            flipAnimator.SetBool("openScore", false);
        }
    }
}
