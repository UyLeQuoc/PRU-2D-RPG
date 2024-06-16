using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public static event Action OnEnemyDeathEvent;

    [Header("Config")]
    [SerializeField] private float health;

    public float CurrentHealth { get; private set; }

    private Animator animator;
    private Rigidbody2D rb2D;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    private EnemyLoot enemyLoot;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
        enemyLoot = GetComponent<EnemyLoot>();
    }

    private void Start()
    {
        CurrentHealth = health;
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            DisableEnemy();
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }

    private void DisableEnemy()
    {
        animator.SetTrigger("Dead");
        enemyBrain.enabled = false;
        enemySelector.NoEnemySelectionCallback();
        rb2D.bodyType = RigidbodyType2D.Static;
        OnEnemyDeathEvent?.Invoke();
        GameManager.Instance.AddPlayerExp(enemyLoot.ExpDrop);
    }
}
