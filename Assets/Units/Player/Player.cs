using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : CUnit
{
    [SerializeField] private GameObject backpackButton;
    [SerializeField] private GameObject joystickGO;

    private bl_Joystick joystick;

    private Vector2 lastDirection = Vector2.right;
    private bool IsShooting = false;
    private bool WhantToShoot = false;

    public Inventory Inventory { get; protected set; }

    public List<GameObject> dropList;

    public bool ShootTrigger = false;

    public void Relax() => IsShooting = false;
    public void EndShoot()
    {
        GetComponent<Animator>().SetBool("FirstShoot", false);
        if (WhantToShoot)
            ActionType = ActionType.Attack;
        else
        {
            ActionType = ActionType.Relax;
            GetComponent<Animator>().SetTrigger("Relax");
        }
    }
    public void Hit()
    {
        Inventory.DeleteBackpackItem(Inventory.EquippedWeapon.Bullet, 1);
        GameObject[] targets = Targets;
        GameObject target = null;
        float dist = 0;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].layer == LayerMask.NameToLayer("Player") 
                || targets[i].GetComponent<CUnit>().ActionType == ActionType.Dieing 
                || targets[i].GetComponent<CUnit>().ActionType == ActionType.Corpse)
                continue;
            Vector2 dif = targets[i].transform.position - transform.position;
            float angle = Vector2.Angle(lastDirection, dif);
            if (angle < 30)
                if (target == null)
                {
                    dist = dif.magnitude;
                    target = targets[i];
                }
                else if (dif.magnitude < dist)
                {
                    dist = dif.magnitude;
                    target = targets[i];
                }
        }
        if (target != null)
        {
            float damage = Inventory.EquippedWeapon.Shot(dist);
            if (damage > 0)
                target.GetComponent<CUnit>().TakeDamage(damage);
        }
    }

    protected override void Attack()
    {
        CurrentSpeed = LeapSpeed;
        Moove();
    }

    protected override void ToCorpse()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void Update()
    {
        for (int i = 0; i < dropList.Count; i++)
            if (Vector3.Distance(dropList[i].transform.position, transform.position) < 1f)
            {
                Destroy(dropList[i]);
                dropList.RemoveAt(i);
                Inventory.AddBackpackItem(Bullet.PistolBullet, 5);
                break;
            }
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            direction += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            direction += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            direction += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector2.right;
        //MooveDirection = new Vector2(joystick.Horizontal, joystick.Vertical) * 3f /joystick.Radio;
        MooveDirection = direction;
        if (direction.magnitude > 0) lastDirection = direction;
        WhantToShoot = ( Input.GetKey(KeyCode.Z) || ShootTrigger ) && Inventory.Count(Inventory.EquippedWeapon.Bullet) > 0;
        ShootTrigger = false;
        if (IsShooting)
        {
            CurrentSpeed = MooveDirection == Vector2.zero ? 0 : LeapSpeed;
        }
        else if (WhantToShoot)
        {
            CurrentSpeed = MooveDirection == Vector2.zero ? 0 : LeapSpeed;
            ActionType = ActionType.Attack;
            IsShooting = true;
            GetComponent<Animator>().SetBool("FirstShoot", true);
            GetComponent<Animator>().SetInteger("WeaponType", (int)Inventory.EquippedWeapon.Type);
            GetComponent<Animator>().SetFloat("AttackSpeed", Inventory.EquippedWeapon.AttackSpeed);
        }
        else
        {
            CurrentSpeed = MooveDirection == Vector2.zero ? 0 : MaxSpeed;
            ActionType = ActionType.Moove;
        }
    }

    protected override void Start()
    {
        base.Start();

        Inventory = GetComponent<Inventory>();
        Inventory.AddInhandsItem(Weapon.Pistol, 1);
        Inventory.EquippedWeapon = Weapon.Pistol;
        Inventory.AddBackpackItem(Bullet.PistolBullet, 30);

        backpackButton.GetComponent<ItemButton>().OnClick();

        dropList = new List<GameObject>();
    }

    private void Awake()
    {
        joystick = joystickGO.GetComponent<bl_Joystick>();

        animationMultiplier = 1f;
        MaxSpeed = 4f;
        LeapSpeed = MaxSpeed / 10f;
        Damage = 0f;
        MaxHP = 100f;
    }
}