using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{   
    public static ShopManager instance;

    public Button buyButton;
    public TextMeshProUGUI infoArea;
    public TextMeshProUGUI nameInfoArea;
    public RemoveShopItem[] slots;

    private RemoveShopItem targetScript;

    public delegate void OnGetItemDisabled();
    public event OnGetItemDisabled GetItemDisableEvent;

    public delegate void OnGetItemEnabled();
    public event OnGetItemEnabled GetItemEnableEvent;

    void Awake(){
        instance = this;
    }

    // Finds and returns the first empty slot
    public RemoveShopItem FindEmptySlot(){
        for(int i = 0; i < slots.Length; i++){
            targetScript = slots[i];
            if(targetScript.item == null){
                return targetScript;
            }
        }

        return null;        
    }

    // Disables slots which contains a skill
    public void DisableFullSlots(){
        for(int i = 0; i < slots.Length; i++){
            targetScript = slots[i];
            if(targetScript.item != null){
                targetScript.button.enabled = false;
            }
        }
    }

    // Disables rest of active power ups to prevent from adding to the slots
    // if there are 2 of them in the slots already
    public void CheckForDisable(){
        int activeGetItemCount = 0;
        foreach (var slot in slots){
            if(slot.item != null && slot.item.itemType == ItemType.GetActive){
                activeGetItemCount++;
                if(activeGetItemCount >= 2 && GetItemDisableEvent != null){
                    
                    // Disable active get items
                    GetItemDisableEvent();
                    break;
                }    
            }
        }

    }

    // Enables active power ups which are not in any slot to 
    // allow to add add to the slot if there are less than 2 power up in the slots
    public void CheckForEnable(){
        foreach (var slot in slots){
            if(slot.item != null && slot.item.itemType == ItemType.GetActive && GetItemEnableEvent != null){
                
                // Enable Get Items which are not in slot
                GetItemEnableEvent();
                break;
            }
        }
    }

    public void UpdateBuyButtonUI(int cost){
        buyButton.gameObject.GetComponent<Buy>().UpdateButtonColorAndText(cost);
    }

    // Adds item's shop function to buy button listener. 
    // This way player will be have the power up or upgrade them
    public void AddToBuy(ShopItem item){
        buyButton.onClick.AddListener(item.ShopFunc);
    }

    // Removes item's shop function from buy button listener
    public void RemoveFromBuy(ShopItem item){
        buyButton.onClick.RemoveListener(item.ShopFunc);
    }

    // Updates info ares with item's description
    public void UpdateInfo(ShopItem item){
        infoArea.text = item.info;
        nameInfoArea.text = item.name;
    }
}
