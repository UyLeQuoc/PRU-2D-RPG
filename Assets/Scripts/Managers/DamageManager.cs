using UnityEngine;

public class DamageManager : Singleton<DamageManager>
{
    [Header("Config")]
    [SerializeField] private DamageText damageTextPrefab;

    public void ShowDamageText(float damageAmount, Transform parent)
    {
        DamageText damageText = Instantiate(damageTextPrefab, parent);
        damageText.transform.position += Vector3.right * 0.5f;
        damageText.SetDamageText(damageAmount);
    }
}
