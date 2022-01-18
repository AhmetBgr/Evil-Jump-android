using UnityEngine;
using UnityEngine.UI;

public class ButtonSlots : MonoBehaviour
{
    public Button[] powerUpButton;
    public Image[] powerUpIcons;

    public Duration[] durationUIs;
    public Collectable[] activePUs;
    public Collectable[] passivePUs;

    public Sprite buttonDefImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < activePUs.Length; i++)
        {
            activePUs[i].PickUpEvent += AddActivePU;
            activePUs[i].hasThis = false;

            // Picks up active power ups which player bought on game over scene
            if(activePUs[i].getOnStart){ 
                activePUs[i].PickUp();
            }   
            activePUs[i].ResetShopFunc();       
        }

        for (int i = 0; i < passivePUs.Length; i++)
        {
            passivePUs[i].PickUpEvent += AddPassivePU;   
            passivePUs[i].hasThis = false;
            
            // Picks up passive power ups which player bought on game over scene
            if(passivePUs[i].getOnStart){

                passivePUs[i].PickUp();
            }
            passivePUs[i].ResetShopFunc();
        }
    }

    void OnDisable(){
        for (int i = 0; i < activePUs.Length; i++)
        {
            activePUs[i].PickUpEvent -= AddActivePU;       
        }

        for (int i = 0; i < passivePUs.Length; i++)
        {
            passivePUs[i].PickUpEvent -= AddPassivePU;   
        }
    }

    //Adds active power ups to the empty slots which player picked up
    public void AddPassivePU(Collectable powerUp){
        for (int i = 0; i < durationUIs.Length; i++){   
            if(durationUIs[i].currentCol == powerUp){
                powerUp.Activate();
                durationUIs[i].Reset();
                break;
            }
            else if(durationUIs[i].currentCol == null){
                durationUIs[i].currentCol = powerUp;
                durationUIs[i].icon.sprite = powerUp.icon;
                durationUIs[i].gameObject.SetActive(true);
                
                break;
            }
        }
    }

    //Adds active power ups to the empty slots which player picked up
    public void AddActivePU(Collectable powerUp){
        for(int i = 0; i<2; i++){   
            if(!powerUpButton[i].IsActive()){
                powerUpButton[i].enabled = true;
                powerUpIcons[i].sprite = powerUp.icon;
                powerUpIcons[i].color = Color.white;
                powerUpButton[i].onClick.AddListener(powerUp.Activate);
                if(i == 0){
                    powerUpButton[i].onClick.AddListener(() => DisableButton( 0 ));
                }
                else{
                    powerUpButton[i].onClick.AddListener(() => DisableButton( 1 ));
                }
                break;
            }
        }
    }

    void DisableButton(int index){
        powerUpButton[index].onClick.RemoveAllListeners();
        powerUpIcons[index].sprite = null;
        powerUpIcons[index].color = new Color(1,1,1,0);

        powerUpButton[index].enabled = false;
    }
}
