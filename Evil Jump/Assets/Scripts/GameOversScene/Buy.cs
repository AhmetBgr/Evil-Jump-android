using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Buy : MonoBehaviour
{
    public static int totalCost = 0;
    public TextMeshProUGUI totalCostText;
    public CoinCounter coinCounter;
    private Image buttonImage;
    private Button button;

    // Start is called before the first frame update
    void Start(){
        totalCost = 0;
        totalCostText = GetComponentInChildren<TextMeshProUGUI>();
        buttonImage = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    
    // Buy items which is in the slots
    public void BuyItems(){
        coinCounter.ChangeCoinAmount(-totalCost);
        totalCost = 0;
        ShopManager.instance.DisableFullSlots();
        
        button.enabled = false;
        totalCostText.text = "Done";
        buttonImage.color = new Color32(255, 255, 255, 255);
    }

    // Changes clickable status depends on total cost of shop items in the slots
    public void UpdateButtonColorAndText(int cost){
        totalCost += cost;
        totalCostText.text = totalCost.ToString();
        if(totalCost > coinCounter.coin){ // Can't buy
            buttonImage.color = new Color32(210, 84, 84, 255);
            button.enabled = false;
        }
        else{ // Can buy
            buttonImage.color = new Color32(255, 255, 255, 255);
            button.enabled = true;
        }
    }
}
