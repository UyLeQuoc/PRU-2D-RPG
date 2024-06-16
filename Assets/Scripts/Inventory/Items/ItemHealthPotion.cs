using UnityEngine;

[CreateAssetMenu(menuName = "Items/Health Potion", fileName = "ItemHealthPotion")]
public class ItemHealthPotion : InventoryItem
{
    [Header("Config")]
    public float HealthValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.PlayerHealth.CanRestoreHealth())
        {
            GameManager.Instance.Player.PlayerHealth.RestoreHealth(HealthValue);
            return true;
        }
        return false;
    }
}