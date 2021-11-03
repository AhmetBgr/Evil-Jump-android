using UnityEngine;
using UnityEngine.UI;

public class GameplayButtons : MonoBehaviour
{

    public GameObject gameplayButtons;

    public GameObject startingScreen;

    private bool isFaded = false;

    private Image blackImage;

    // Start is called before the first frame update
    void Awake(){
        blackImage = startingScreen.GetComponent<Image>();
        blackImage.enabled = blackImage.enabled ? true : true;
    }

    // Update is called once per frame
    void Update(){
        if(PauseMenu.isPaused){
            gameplayButtons.SetActive(false);
        }
        else{
            gameplayButtons.SetActive(true);
        }

        //Debug.Log(blackImage.fillOrigin);

        if(!isFaded){
            Transition();
        }
    }


    void Transition(){

        if(blackImage.fillOrigin == 0){
            blackImage.fillAmount +=  1.6f * Time.deltaTime;
            //gameplayButtons.SetActive(false);
            if(blackImage.fillAmount >= 0.9f){
                blackImage.fillOrigin = 1;
            }
        }
        else{
            blackImage.fillAmount -=  1.2f * Time.deltaTime;
            //gameplayButtons.SetActive(false);
            if(blackImage.fillAmount <= 0.1f){
                PlayerControls.hasStarted = true;
                startingScreen.SetActive(false);
                //gameplayButtons.SetActive(true);
                isFaded = true;
            }
        }
    }

}
