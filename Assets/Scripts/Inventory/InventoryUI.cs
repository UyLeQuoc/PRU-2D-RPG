using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    [Header("Config")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private TextMeshProUGUI itemDescriptionTMP;

    [Header("Crafting Panel")]
    [SerializeField] private List<GameObject> craftingCells;

    public List<GameObject> CraftingCells => craftingCells;

    public string CraftingLetters { get; set; }
    public int craftingLettersCount = 0;

    public InventorySlot CurrentSlot { get; set; }

    private List<InventorySlot> slotList = new List<InventorySlot>();

    protected override void Awake()
    {
        base.Awake();
        InitInventory();
    }

    public void AddCraftingCell()
    {
        List<string> fourLetterWords = new List<string>
        {
            "Segg", "Able", "Acid", "Aged", "Bake", "Sack", "Away", "Baby", "Back", "Bank", "Been", "Ball", "Base", "Busy", "Bend", "Bell", "Bird", "Came", "Calm", "Card", "Coat", "City", "Chat", "Cash", "Crow", "Cook", "Cool", "Dark", "Each", "Evil", "Even", "Ever", "Face", "Fact", "Four", "Five", "Fair", "Feel", "Fell", "Fire", "Fine", "Fish", "Game", "Gone", "Gold", "Girl", "Have", "Hair", "Here", "Hear", "Into", "Iron", "Jump", "Kick", "Kill", "Life", "Like", "Love", "Main", "Move", "Meet", "More", "Nose", "Near", "Open", "Only", "Push", "Pull", "Sell", "Sale", "Good", "Work", "Year", "Time", "Day", "Food", "Man", "Word", "Long", "High", "Some", "Look", "Ask", "Full", "Hand", "Help", "Year", "Hour", "Mind", "Side", "Make", "Part", "Seem", "Come", "Show", "Form", "Book", "Home", "Take", "Idea", "Kind", "Room", "Line", "Turn", "Head", "Live", "Walk", "Note", "Body", "Last", "Link", "Call", "Land", "Want", "Left", "Pass", "View", "Held", "Late", "Door", "Week", "John", "Send", "Soul", "Case", "List", "Fact", "News", "Term", "Ones", "Wife", "Area", "Mark", "Song", "Knew", "Spot", "Self", "Past", "Lord", "Word", "Road", "Rule", "Talk", "Star", "Wall", "East", "Name", "Tell", "Plan", "Test", "Paid", "Deep", "Ship", "Idea", "Hold", "House", "Stay", "Kept", "Wish", "Free", "Told", "Away", "Army", "Mean", "King", "Pick", "Feet", "Stop", "Cold", "Unit", "Drop", "Face", "Town", "Read", "Lead", "Rest", "Path", "Gave", "Deal", "Huge", "Hard", "Must", "Plant", "Pair", "Loan", "Hear", "Fear", "Fell", "Half", "Your", "Wide", "Shot", "Find", "Self", "Four", "Tone", "Lack", "Lack", "Nice", "Lady", "Else", "File", "Soft", "Told", "Born", "Able", "Onto", "Copy", "True", "Air", "Duty", "Pity", "Ring", "Pool", "Club", "Boat", "Desk", "Bed", "Thus", "Safe", "Male", "Keep", "Wait"
        };
        foreach (var word in fourLetterWords)
        {
            if (CraftingLetters.ToLower().Contains(word.ToLower()))
            {
                GameManager.Instance.AddPlayerExp(300);
                ClearCraftingCell();
                return;
            }
        }
    }
    public void ClearCraftingCell()
    {
        CraftingLetters = "";
        craftingLettersCount = 0;
        foreach (var cell in craftingCells)
        {
            cell.GetComponent<Image>().sprite = null;
            //setactive
            cell.SetActive(false);
        }
    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.Index = i;
            slotList.Add(slot);
        }
    }

    public void UseItem()
    {
        Inventory.Instance.UseItem(CurrentSlot.Index);
    }

    public void RemoveItem()
    {
        if (CurrentSlot == null) return;

        Inventory.Instance.RemoveItem(CurrentSlot.Index);
    }

    public void EquipItem()
    {
        if (CurrentSlot == null) return;
        Inventory.Instance.EquipItem(CurrentSlot.Index);
    }

    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        if (item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;
        descriptionPanel.SetActive(true);

        itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
        //itemIcon.SetNativeSize();
        itemNameTMP.text = Inventory.Instance.InventoryItems[index].Name;
        itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description;
    }

    public void OpenCloseInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf == false)
        {
            descriptionPanel.SetActive(false);
            CurrentSlot = null;
        }
    }

    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
        ShowItemDescription(slotIndex);
    }

    private void OnEnable()
    {
        InventorySlot.OnSlotSellectedEvent += SlotSelectedCallback;
    }
    private void OnDisable()
    {
        InventorySlot.OnSlotSellectedEvent -= SlotSelectedCallback;
    }
}
