using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats playerStats;

    [Header("Bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI expText;

    [Header("Stats Panel")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI statLevelTMP;
    [SerializeField] private TextMeshProUGUI statDamageTMP;
    [SerializeField] private TextMeshProUGUI statCChanceTMP;
    [SerializeField] private TextMeshProUGUI statCDamageTMP;
    [SerializeField] private TextMeshProUGUI statTotalExpTMP;
    [SerializeField] private TextMeshProUGUI statCurrentExpTMP;
    [SerializeField] private TextMeshProUGUI statRequireExpTMP;

    [SerializeField] private TextMeshProUGUI attributePointsTMP;
    [SerializeField] private TextMeshProUGUI strengthTMP;
    [SerializeField] private TextMeshProUGUI dexterityTMP;
    [SerializeField] private TextMeshProUGUI intelligenceTMP;

    private void Update()
    {
        UpdatePlayerUI();
    }
    public void OpenCloseStatsPabel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if (statsPanel.activeSelf)
        {
            UpdateStatsPanel();
        }
    }

    private void UpdatePlayerUI()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerStats.Health / playerStats.MaxHealth, 10f * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, playerStats.Mana / playerStats.MaxMana, 10f * Time.deltaTime);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, playerStats.CurrentExp / playerStats.NextLevelExp, 10f * Time.deltaTime);

        levelText.text = $"Level: {playerStats.Level}";
        healthText.text = $"{playerStats.Health} / {playerStats.MaxHealth}";
        manaText.text = $"{playerStats.Mana} / {playerStats.MaxMana}";
        expText.text = $"{playerStats.CurrentExp} / {playerStats.NextLevelExp}";
    }

    private void UpdateStatsPanel()
    {
        statLevelTMP.text = playerStats.Level.ToString();
        statDamageTMP.text = playerStats.BaseDamage.ToString();
        statCChanceTMP.text = playerStats.CriticalChance.ToString();
        statCDamageTMP.text = playerStats.CriticalDamage.ToString();
        statTotalExpTMP.text = playerStats.TotalExp.ToString();
        statCurrentExpTMP.text = playerStats.CurrentExp.ToString();
        statRequireExpTMP.text = playerStats.NextLevelExp.ToString();

        attributePointsTMP.text = $"Points: {playerStats.AttributePoints.ToString()}";
        strengthTMP.text = playerStats.Strength.ToString();
        dexterityTMP.text = playerStats.Dexterity.ToString();
        intelligenceTMP.text = playerStats.Intelligence.ToString();
    }

    private void UpgradeCallBack()
    {
        UpdateStatsPanel();
    }

    private void OnEnable()
    {
        PlayerUpgrade.OnPlayerUpgradeEvent += UpgradeCallBack;
    }
    private void OnDisable()
    {
        PlayerUpgrade.OnPlayerUpgradeEvent -= UpgradeCallBack;
    }

}
