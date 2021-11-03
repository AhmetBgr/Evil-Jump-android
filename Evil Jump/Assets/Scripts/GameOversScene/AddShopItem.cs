using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddShopItem : MonoBehaviour
{
    public Button buyButton;

    public ShopItem item;

    public Sprite blankImage;

    private Button button;
    private RemoveShopItem targetScript;
    private Button targetButton;
    private Sprite icon;

    // Start is called before the first frame update
    void Start(){
        GetComponentInChildren<TextMeshProUGUI>().text = item.cost.ToString();
        button = GetComponent<Button>();
        icon = GetComponent<Image>().sprite;
        item.isAvailable = true;
        item.isInSlot = false;
        item.ResetShopFunc();
        
        if(item.itemType == ItemType.GetActive){
            ShopManager.instance.GetItemDisableEvent += Disable;
            ShopManager.instance.GetItemEnableEvent += Enable;
        }        
    }
    
    public void OnItemClick(){
        ShopManager shopManager = ShopManager.instance;
        shopManager.UpdateInfo(item);
        targetScript = shopManager.FindEmptySlot();
        if(item.isAvailable && targetScript != null){
            
            // Add icon to the slot pass item
            targetScript.icon.sprite = icon;
            targetScript.icon.color = Color.white;
            targetScript.item = item;
            targetButton = targetScript.button;

            // Adds enable func to slot button to enable back related item when player clicks it in the slot
            targetButton.onClick.AddListener(Enable); 
            
            shopManager.AddToBuy(item);
            shopManager.UpdateBuyButtonUI(item.cost);
            
            if(item.itemType == ItemType.GetActive){
                shopManager.CheckForDisable();
            }
            
            Disable();
            item.isInSlot = true;
        } 
    }

    // Disables item to prevent adding to the slot 
    void Disable(){
        button.image.color = new Color32(255,255,255,120);
        item.isAvailable = false;
    }

    // Enables back item to allow adding to the slots
    public void Enable(){
        if(!item.isInSlot){
            button.image.color = new Color32(255,255,255,255);
            item.isAvailable = true;        
        }
    }
}
