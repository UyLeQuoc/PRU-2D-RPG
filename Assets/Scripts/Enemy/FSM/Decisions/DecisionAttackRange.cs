using UnityEngine;

public class DecisionAttackRange : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return PlayerInAttackRange();
    }
    private bool PlayerInAttackRange()
    {
        if (enemyBrain.Player == null) return false;

        Collider2D playerCollidder = Physics2D.OverlapCircle(enemyBrain.transform.position, attackRange, playerMask);

        if (playerCollidder != null)
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
