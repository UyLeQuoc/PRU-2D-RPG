using System;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static Action<EnemyBrain> OnEnemySelectedEvent;
    public static Action OnNoEnemySectionEvent;

    [Header("Config")]
    [SerializeField] private LayerMask enemyMask;

    private Camera mainCamera;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void SelectEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit =
                Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, Mathf.Infinity, enemyMask);

            if (hit.collider != null)
            {
                EnemyBrain enemyBrain = hit.collider.GetComponent<EnemyBrain>();

                if (enemyBrain == null) return;
                EnemyHealth enemyHealth = enemyBrain.GetComponent<EnemyHealth>();
                if (enemyHealth.CurrentHealth <= 0)
                {
                    EnemyLoot enemyLoot = enemyBrain.GetComponent<EnemyLoot>();
                    LootManager.Instance.ShowLoot(enemyLoot);
                }
                else
                {
                    OnEnemySelectedEvent?.Invoke(enemyBrain);
                }
            }
            else
            {
                OnNoEnemySectionEvent.Invoke();
            }
        }

    }

    private void Update()
    {
        SelectEnemy();
    }
}
