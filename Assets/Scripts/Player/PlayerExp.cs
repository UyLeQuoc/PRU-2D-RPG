using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats playerStats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddExp(300);
        }
    }

    public void AddExp(float amount)
    {
        playerStats.TotalExp += amount;
        playerStats.CurrentExp += amount;

        while (playerStats.CurrentExp >= playerStats.NextLevelExp)
        {
            playerStats.CurrentExp -= playerStats.NextLevelExp;
            NextLevel();
        }
    }

    private void NextLevel()
    {
        playerStats.Level++;
        playerStats.AttributePoints++;
        float currentExpRequired = playerStats.NextLevelExp;
        float newNextLevelExp = Mathf.Round(currentExpRequired + playerStats.NextLevelExp * (playerStats.ExpMultiplier / 100f));

        //update next level exp
        playerStats.NextLevelExp = newNextLevelExp;
    }
}