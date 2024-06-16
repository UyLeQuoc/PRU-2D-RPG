using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform[] attackPositions;

    [Header("Melee Config")]
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;

    public Weapon CurrentWeapon { get; set; }

    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private EnemyBrain enemyTarget;
    private PlayerMana playerMana;
    private Coroutine attackCoroutine;

    private Transform currentAttackPosition;
    private float currenAttackRotation;

    private void Awake()
    {
        actions = new PlayerActions();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMana = GetComponent<PlayerMana>();
    }
    private void Start()
    {
        WeaponManager.Instance.EquipWeapon(initialWeapon);
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetFirePostion();
    }

    private void Attack()
    {
        if (enemyTarget == null) return;

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        attackCoroutine = StartCoroutine(IEAttack());
    }
    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null) yield break;
        if (CurrentWeapon.WeaponType == WeaponType.Magic)
        {
            if (playerMana.CurrentMana < CurrentWeapon.RequireMana) yield break;
            MagicAttack();
        }
        else
        {
            MeleeAttack();
        }
        playerAnimations.SetAttackingAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackingAnimation(false);
    }

    private void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();
        float currentDistanceToEnemy = Vector3.Distance(enemyTarget.transform.position, transform.position);

        if (currentDistanceToEnemy <= minDistanceMeleeAttack)
        {
            enemyTarget.GetComponent<IDamagable>().TakeDamage(GetAttackDamage());
        }
    }

    private void MagicAttack()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, currenAttackRotation);
        Projectile projectile =
            Instantiate(CurrentWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);
        projectile.Direction = Vector3.up;
        projectile.Damage = GetAttackDamage();
        playerMana.UseMana(CurrentWeapon.RequireMana);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        CurrentWeapon = newWeapon;
        playerStats.TotalDamage = playerStats.BaseDamage + CurrentWeapon.Damage;
    }

    private float GetAttackDamage()
    {
        float damage = playerStats.BaseDamage;
        damage += CurrentWeapon.Damage;

        float randomPrec = Random.Range(-0.1f, 0.1f);
        if (randomPrec <= playerStats.CriticalChance)
        {
            damage += damage * (playerStats.CriticalDamage / 100f);
        }
        return damage;
    }

    private void GetFirePostion()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;
        switch (moveDirection.x)
        {
            case > 0f:
                currentAttackPosition = attackPositions[2];
                currenAttackRotation = -90f;
                break;
            case < 0f:
                currentAttackPosition = attackPositions[0];
                currenAttackRotation = -270f;
                break;
        }
        switch (moveDirection.y)
        {
            case > 0f:
                currentAttackPosition = attackPositions[1];
                currenAttackRotation = 0f;
                break;
            case < 0f:
                currentAttackPosition = attackPositions[3];
                currenAttackRotation = 180f;
                break;
        }
    }
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }
    private void NoEnemySelectionCallback()
    {
        enemyTarget = null;
    }
    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoEnemySectionEvent += NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeathEvent += NoEnemySelectionCallback;
    }
    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoEnemySectionEvent -= NoEnemySelectionCallback;
        EnemyHealth.OnEnemyDeathEvent -= NoEnemySelectionCallback;
    }
}
