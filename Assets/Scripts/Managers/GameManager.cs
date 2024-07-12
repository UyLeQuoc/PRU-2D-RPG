using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;

    public Player Player => player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ResetPlayer();
        }
    }

    public void AddPlayerExp(float exp)
    {
        PlayerExp playerExp = player.GetComponent<PlayerExp>();
        playerExp.AddExp(exp);
    }

    public bool DamagePlayerHealth(float exp)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        return playerHealth.TakeDamageAndResult(1);
    }

    public void ResetPlayer()
    {
        player.ResetPlayer();
    }
}