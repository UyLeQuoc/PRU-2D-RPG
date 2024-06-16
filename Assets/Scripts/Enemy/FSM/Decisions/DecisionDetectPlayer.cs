using UnityEngine;

public class DecisionDetectPlayer : FSMDecision
{
    [Header("Config")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerMask;

    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }

    public override bool Decide()
    {
        return DetectPlayer();
    }
    private bool DetectPlayer()
    {
        Collider2D playerCollidder = Physics2D.OverlapCircle(enemyBrain.transform.position, range, playerMask);

        if (playerCollidder != null)
        {
            enemyBrain.Player = playerCollidder.transform;
            return true;
        }
        else
        {
            enemyBrain.Player = null;
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
