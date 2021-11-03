using UnityEngine;
using UnityEngine.UI;

public class RemoveShopItem : MonoBehaviour
{
    [HideInInspector] public ShopItem item;
    [HideInInspector] public Image image;

    public Image icon;
    public static int activeItemCount = 0;
    public Button button;

    private Color emptyIconCol = new Color(1, 1, 1, 0);

    // Start is called before the first frame update
    void Start(){
        button = GetComponent<Button>();
        item = null;
        icon.color = emptyIconCol;
    }

    // Remove item from slot
    public void Remove(){
        if(item != null){ 
            icon.sprite = null;
            item.isAvailable = true;
            item.isInSlot = false;
            icon.color = emptyIconCol;

            ShopManager.instance.RemoveFromBuy(item);
            ShopManager.instance.UpdateBuyButtonUI(-item.cost);
         
            item = null;
            ShopManager.instance.CheckForEnable();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Remove);
        }
    }
}
