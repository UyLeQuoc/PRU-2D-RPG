using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponManaTMP;

    public void EquipWeapon(Weapon weapon)
    {
        weaponIcon.sprite = weapon.Icon;
        weaponIcon.SetNativeSize();
        weaponIcon.gameObject.SetActive(true);
        weaponManaTMP.text = weapon.RequireMana.ToString();
        weaponManaTMP.gameObject.SetActive(true);
        GameManager.Instance.Player.PlayerAttack.EquipWeapon(weapon);
    }
}