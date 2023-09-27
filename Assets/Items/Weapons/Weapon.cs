using UnityEngine;

public class Weapon : Item
{
    public readonly static Sprite PistolSprite;
    public readonly static Sprite ShotgunSprite;
    public readonly static Sprite PistolBulletSprite;
    public readonly static Sprite ShotgunBulletSprite;

    public static Weapon Pistol => new Weapon(WeaponType.Pistol, Bullet.PistolBullet, 2f, 25f, 10000f, PistolSprite, "Pistol");
    public static Weapon Shotgun => new Weapon(WeaponType.Shotgun, null, 2f, 5f, 1.5f, ShotgunSprite, "Shotgun");//Fixnull


    public float Shot(float distance)
    {
        switch (Type)
        {
            case WeaponType.Shotgun:
                float s = 0;
                for (int i = 0; i < 10; i++)
                    if (Random.value < Accuracy / distance)
                        s += Damage;
                return s;
            case WeaponType.Pistol:
                if (Random.value < Accuracy / distance)
                    return Damage;
                return 0;
            default:
                throw new System.Exception("Unknown weapon type");
        }
    }


    public WeaponType Type;
    public float AttackSpeed;
    public float Damage;
    public float Accuracy;
    public Bullet Bullet;


    public Weapon(WeaponType type, Bullet bullet, float attackSpeed, float damage, float accuracy, Sprite sprite, string name) : base(sprite, name)
    {
        Type = type;
        Bullet = bullet;
        AttackSpeed = attackSpeed;
        Damage = damage;
        Accuracy = accuracy;
    }


    static Weapon()
    {
        ShotgunSprite = Resources.Load<Sprite>("Shotgun");
        PistolSprite = Resources.Load<Sprite>("Pistol");
        ShotgunBulletSprite = Resources.Load<Sprite>("ShotgunBullet");
        PistolBulletSprite = Resources.Load<Sprite>("PistolBullet");
    }
}