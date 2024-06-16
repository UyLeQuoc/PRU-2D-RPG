using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Items/Letter", fileName = "ItemLetter")]
public class ItemLetter : InventoryItem
{
    public override bool UseItem()
    {
        Debug.Log("Using item: " + this.Name);
        var craftingCells = InventoryUI.Instance.CraftingCells;
        var craftingLettersCount = InventoryUI.Instance.craftingLettersCount;
        if (craftingLettersCount >= craftingCells.Count) return false;
        var i = craftingLettersCount;

        craftingCells[i].GetComponent<Image>().sprite = this.Icon;
        //setactive
        craftingCells[i].SetActive(true);

        var letter = this.ID.Substring(this.ID.Length - 1);

        InventoryUI.Instance.CraftingLetters += letter;
        Debug.Log(InventoryUI.Instance.CraftingLetters);
        InventoryUI.Instance.craftingLettersCount++;

        return true;
    }
}
