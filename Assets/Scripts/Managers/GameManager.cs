using UnityEngine;

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
}
