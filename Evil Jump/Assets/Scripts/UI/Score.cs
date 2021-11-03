using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Score UI element
    public TextMeshProUGUI highScoreText; 

    public GameObject player;
    private Rigidbody2D playerRB;

    public int score = 0;
    public int speed;

    public bool onGameScene = true;

    public static float highScore = 0;
    public static float lastScore = 0;

    public static string lastScoreKey = "LastScore";
    public static string highScoreKey = "HighScore";

    // Start is called before the first frame update
    void Start(){
        if(onGameScene){
            playerRB = player.GetComponent<Rigidbody2D>();
            StartCoroutine("GetScoreWithDelay", 0.06f); // Increase delay if you want a little more performance
        }
        score = (int)PlayerPrefs.GetFloat(lastScoreKey);
        GetSavedHighScore();
        UpdateScoreTexts();
    }
    
    IEnumerator GetScoreWithDelay(float delay){
        while(true){
            yield return new WaitForSeconds(delay);
            GetScore();
        }
    }

    // Gets scores depending player's height
    void GetScore(){
        if(playerRB.velocity.y > 0 && player.transform.position.y >= transform.position.y){
            transform.position = new Vector3(0,player.transform.position.y); 
            score = (int)transform.position.y;
            scoreText.text = score.ToString("0.##");
            lastScore = score;
        }
    }

    void GetSavedHighScore(){
        if(PlayerPrefs.HasKey(highScoreKey)){
            highScore = PlayerPrefs.GetFloat(highScoreKey);
        }
    }

    private void UpdateScoreTexts(){
        scoreText.text = score.ToString("0.##");
        if(highScoreText != null){
            highScoreText.text = highScore.ToString("0.##");
        }
    }

    // Saves last score and high score 
    public static void SaveScore(){
        PlayerPrefs.SetFloat(lastScoreKey,lastScore);

        if(lastScore > highScore){
            PlayerPrefs.SetFloat(highScoreKey,lastScore);
        }
        else{
            PlayerPrefs.SetFloat(highScoreKey,highScore);
        }

        PlayerPrefs.Save();
    
    }
}
