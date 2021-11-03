using UnityEngine;

[CreateAssetMenu (menuName = "Shop Item/New Shop Item")]
public class ShopItem : ScriptableObject
{
    public Collectable collectable;
    public int cost;
    public new string name;
    public bool isAvailable = true;
    public bool isInSlot = false;

    [TextArea]
    public string info;

    public ItemType itemType;

    public virtual void ShopFunc(){
        if(itemType == ItemType.Upgrade)
            collectable.UpgradeFunc();
        else
            collectable.getOnStart = true;
    }

    public virtual void ResetShopFunc(){
        collectable.ResetShopFunc();
    }
}

public enum ItemType{
    Upgrade, GetPassive, GetActive
}
