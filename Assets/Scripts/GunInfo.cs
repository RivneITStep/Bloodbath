using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponClass
{
    Pistol,
    Submachine,
    Shotgun,
    AssaultRiffle,
    SniperRiffle,
    Machinegun,
    Special
}

public class GunInfo : MonoBehaviour
{
    // Start is called before the first frame update

    public string item_id;

    public string DisplayName;

    public Rarity Rarity;

    public WeaponClass weaponClass;

    public override bool Equals(object obj)
    {
        return obj is GunInfo info &&
               item_id == info.item_id &&
               DisplayName == info.DisplayName &&
               Rarity == info.Rarity &&
               weaponClass == info.weaponClass;
    }

    public override int GetHashCode()
    {
        int hashCode = -305631161;
        hashCode = hashCode * -1521134295 + base.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(item_id);
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DisplayName);
        hashCode = hashCode * -1521134295 + Rarity.GetHashCode();
        hashCode = hashCode * -1521134295 + weaponClass.GetHashCode();
        return hashCode;
    }
}
