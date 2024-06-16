using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats playerStats;

    private void Start()
    {
        ResetMana();
    }

    public float CurrentMana { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            UseMana(3f);
        }
    }

    public void UseMana(float amount)
    {
        playerStats.Mana = Mathf.Max(playerStats.Mana - amount, 0f);
        CurrentMana = playerStats.Mana;
    }
    public void ResetMana()
    {
        CurrentMana = playerStats.MaxMana;
    }


    public void RecoverMana(float amount)
    {
        playerStats.Mana += amount;
        playerStats.Mana = Mathf.Min(playerStats.Mana, playerStats.MaxMana);
    }
    public bool CanRecoverMana()
    {
        return playerStats.Mana >= 0 && playerStats.Mana < playerStats.MaxMana;
    }
}