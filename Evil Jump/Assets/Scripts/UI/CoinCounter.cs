using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    private int _coin = 0;
    public int coin {
        get { return _coin; }
        set{
            _coin = value;
            coinText.text = _coin.ToString(); //Updates coin amount UI whenever player picks up a coin
        }
    }

    public static string goldKey = "GoldAmount";

    public TextMeshProUGUI coinText;

    void Awake(){
        GetSavedGoldAmount();
    }

    void GetSavedGoldAmount(){
        if(PlayerPrefs.HasKey(goldKey)){
            coin = PlayerPrefs.GetInt(goldKey);
        }
        else{
            coin = 0;
        }
    }

    public void SaveCoinAmount(){
        PlayerPrefs.SetInt(goldKey, coin);
        PlayerPrefs.Save();
    }

    public void ChangeCoinAmount(int amount){
        coin += amount;
        PlayerPrefs.SetInt(goldKey, coin);
        PlayerPrefs.Save();
    }
}
