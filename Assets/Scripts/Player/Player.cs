using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Test")]
    public ItemHealthPotion HealthPotion;
    public ItemManaPotion ManaPotion;

    public PlayerStats PlayerStats => playerStats;
    public PlayerMana PlayerMana { get; private set; }
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }

    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerMana = GetComponent<PlayerMana>();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (HealthPotion.UseItem())
            {
                Debug.Log("Using Health Potion");
            }
            else
            {
                Debug.Log("Health is full");
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (ManaPotion.UseItem())
            {
                Debug.Log("Using Mana Potion");
            }
            else
            {
                Debug.Log("Mana is full");
            }
        }

    }

    public void ResetPlayer()
    {
        playerStats.ResetPlayerStats();
        playerAnimations.ResetPlayerAnimation();
        PlayerMana.ResetMana();
    }
}