using UnityEngine;

public abstract class CUnit : MonoBehaviour
{
    [SerializeField] GameObject dropPrefab;

    //Init as gameObject Child
    private HpBarController hpBarcontroller;

    protected bool alreadyDroped = false;

    //Must have init in children classes
    protected bool IsRightSide = true;
    protected float animationMultiplier;
    protected float MaxSpeed;
    protected float LeapSpeed;
    protected float Damage;
    protected float MaxHP;
    protected float AttackSpeed;

    //Counters
    protected float BusyTime;

    //Must be used in children classes
    protected float CurrentSpeed;
    protected Vector2 MooveDirection;
    private ActionType _actionType;
    public ActionType ActionType
    {
        get { return _actionType; }
        protected set
        {
            if (!IsBusy)
                if (_actionType != ActionType.Dieing) _actionType = value;
        }
    }

    //State vars
    public float HP { get; protected set; }
    public bool IsWeaponed { get; protected set; }
    public bool IsBusy => BusyTime > 0;
    

    //Take some damage
    public void TakeDamage(float damage)
    {
        HP -= damage;
        hpBarcontroller.ChangeHP(HP / MaxHP);
        if (HP <= 0)
        {
            Die();
            return;
        }
    }

    //Main Cycle
    protected void FixedUpdate()
    {
        if (GetComponent<Animator>().GetBool("Corpse"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        switch (ActionType)
        {
            case ActionType.Moove:
                if (Moove())
                    GetComponent<Animator>().SetTrigger("Moove");
                else
                    GetComponent<Animator>().SetTrigger("Idle");
                break;
            case ActionType.Idle:
                GetComponent<Animator>().SetTrigger("Idle");
                break;
            case ActionType.Attack:
                Attack();
                GetComponent<Animator>().SetTrigger("Attack");
                break;
            case ActionType.Dieing:
                GetComponent<Animator>().SetTrigger("Die");
                break;

            case ActionType.Relax:
                Moove();
                break;
            default:
                throw new System.Exception("Need Action");
        }
    }

    //Init after Childrens
    protected virtual void Start()
    {
        HP = MaxHP;
        hpBarcontroller = transform.Find("HPBar").GetComponent<HpBarController>();
        hpBarcontroller.ChangeHP(1f);
        BusyTime = 0f; 
        GetComponent<Animator>().SetBool("Corpse", false);
    }

    //Get All Units on a map
    protected GameObject[] Targets
    {
        get
        {
            GameObject[] res = new GameObject[transform.parent.childCount];
            for (int i = 0; i < transform.parent.childCount; i++)
                res[i] = transform.parent.GetChild(i).gameObject;
            return res;
        }
    }

    //Actions
    protected bool Moove()
    {
        Vector2 dr = CurrentSpeed * MooveDirection.normalized / 16f;
        Vector2 oldPos = GetComponent<Rigidbody2D>().position;
        Vector2 newPos = GetComponent<Rigidbody2D>().position;
        newPos = oldPos + dr;
        newPos *= 16f;
        (newPos.x, newPos.y) = (Mathf.Round(newPos.x), Mathf.Round(newPos.y));
        newPos /= 16f;
        dr = newPos - oldPos;
        if (newPos.y > 15.375) newPos.y = 15.375f;
        if (newPos.y < -15.375) newPos.y = -15.375f;
        if (newPos.x > 18) newPos.x = 18f;
        if (newPos.x < -36) newPos.x = -36f;
        GetComponent<Rigidbody2D>().MovePosition(newPos);
        if (dr.magnitude > 0)
        {
            if (dr.x != 0)
                GetComponent<SpriteRenderer>().flipX = dr.x >= 0 ? !IsRightSide : IsRightSide;
            GetComponent<Animator>().SetBool("DirectionFront", dr.y <= 0);
            GetComponent<Animator>().SetFloat("Speed", animationMultiplier * dr.magnitude);
            return true;
        }
        return false;
    }
    protected abstract void Attack();
    protected void Die()
    {
        ActionType = ActionType.Dieing;
    }
    //Dead to corpse
    protected virtual void ToCorpse()
    {
        ActionType = ActionType.Corpse;
        GetComponent<Animator>().SetBool("Corpse", true);
        if (!alreadyDroped)
        {
            GameObject d = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            GameObject[] targets = Targets;
            for (int i = 0; i < targets.Length; i++)
                if ( targets[i].layer == LayerMask.NameToLayer("Player") )
                    targets[i].GetComponent<Player>().dropList.Add(d);
            alreadyDroped = true;
        }
    }
}