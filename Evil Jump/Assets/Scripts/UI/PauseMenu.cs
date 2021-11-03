using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static int isDashAssistOn = 1; 
    public GameObject pauseMenuUI;
    public TextMeshProUGUI dashAssistText;

    public static string dashAssistKey= "DashAssist";

    // Start is called before the first frame update
    void Start(){
        UpdateDashAssistOpt();
        SaveDashAssistOpt();
        UpdateDashAssistText(isDashAssistOn);        
    }

    // Update is called once per frame
    void Update(){
        if(Swipe.swipedUp){
            if(!isPaused){
                Pause();
            }
        }
    }

    void OnApplicationFocus(bool pauseStatus){
        if(!pauseStatus){
            Pause();
        }    
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void UpdateDashAssistText(int i){
        dashAssistText.text = i == 1 ? "Dash Assist On" : "Dash Assist Off" ;
    }

    public void ChangeDashAssist(){
        if(isDashAssistOn == 1){
            isDashAssistOn = 0;
            dashAssistText.text = "Dash Assist Off";
        }
        else{
            isDashAssistOn = 1;
            dashAssistText.text = "Dash Assist On";
        }
        SaveDashAssistOpt();
    }

    private void UpdateDashAssistOpt(){
        isDashAssistOn = PlayerPrefs.HasKey(dashAssistKey) ? PlayerPrefs.GetInt(dashAssistKey) : 1;
    }

    public static void SaveDashAssistOpt(){
        PlayerPrefs.SetInt(dashAssistKey, isDashAssistOn);
        PlayerPrefs.Save();
    }
}
