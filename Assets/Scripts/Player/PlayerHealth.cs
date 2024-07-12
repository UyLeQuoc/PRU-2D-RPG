using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField] private PlayerStats playerStats;

    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        if (playerStats.Health <= 0f)
        {
            PlayerDead();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1f);
        }
    }

    public void TakeDamage(float amount)
    {
        if (playerStats.Health <= 0f) return;
        playerStats.Health = Mathf.Max(playerStats.Health - amount, 0f);
        DamageManager.Instance.ShowDamageText(amount, transform);
        if (playerStats.Health <= 0)
        {
            PlayerDead();
        }
    }

    public bool TakeDamageAndResult(float amount)
    {
        if (playerStats.Health <= 0f) throw new Exception("wtf player");
        playerStats.Health = Mathf.Max(playerStats.Health - amount, 0f);
       // DamageManager.Instance.ShowDamageText(amount, transform);
        if (playerStats.Health <= 0)
        {
            PlayerDead();
            return false;
        }
        return true;
    }

    public void RestoreHealth(float amount)
    {
        playerStats.Health = Mathf.Min(playerStats.Health + amount, playerStats.MaxHealth);
    }

    public bool CanRestoreHealth()
    {
        return playerStats.Health > 0 && playerStats.Health < playerStats.MaxHealth;
    }

    private void PlayerDead()
    {
        playerAnimations.SetDeadAnimation();
    }
}