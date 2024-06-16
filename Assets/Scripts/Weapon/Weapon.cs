using UnityEngine;

public enum WeaponType
{
    Melee,
    Magic
}

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Config")]
    public Sprite Icon;
    public WeaponType WeaponType;
    public float Damage;

    public Projectile ProjectilePrefab;
    public float RequireMana;
}
