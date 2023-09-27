using UnityEngine;

public class Bullet: Item
{
    private static Sprite pistol;

    public WeaponType Type;
    public Weapon Weapon
    {
        get
        {
            switch (Type)
            {
                case WeaponType.Pistol:
                    return Weapon.Pistol;
                default:
                    throw new System.Exception();
            }
        }
    }

    public static Bullet PistolBullet
    {
        get
        {
            return new Bullet(WeaponType.Pistol, pistol, "Pistol Bullet");
        }
    }

    public Bullet(WeaponType type, Sprite sprite, string name) : base(sprite, name)
    {
        Type = type;
    }

    static Bullet()
    {
        pistol = Resources.Load<Sprite>("PistolBullet");
    }
}