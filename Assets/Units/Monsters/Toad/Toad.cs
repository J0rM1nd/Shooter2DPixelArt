using Unity.VisualScripting;
using UnityEngine;

public class Toad : CUnit
{
    protected float AgreDistance;
    protected float AttackDistance;

    protected override void Attack()
    {
        Moove();
    }

    public void Hit()
    {
        GameObject[] targets = Targets;
        for (int i = 0; i < targets.Length; i++)
            if (Vector3.Distance(transform.position, targets[i].transform.position) < AttackDistance)
                if ( targets[i] != gameObject && targets[i].GetComponent<CUnit>().ActionType != ActionType.Dieing )
                    targets[i].GetComponent<CUnit>().TakeDamage(Damage);
    }

    protected GameObject FindTarget()
    {
        GameObject[] targets = Targets;
        GameObject player = null;
        for (int i = 0; i < targets.Length; i++)
            if ( targets[i].layer == LayerMask.NameToLayer("Player") && targets[i].GetComponent<CUnit>().ActionType != ActionType.Dieing)
            {
                player = targets[i];
                break;
            }
        return player;
    }
    
    protected void Update()
    {
        if (HP <= 0)
            return;
        GameObject target = FindTarget();
        if ( target == null && !target.IsDestroyed() )
        {
            ActionType = ActionType.Idle;
            CurrentSpeed = 0;
            MooveDirection = Vector2.zero;
            return;
        }
        MooveDirection = target.transform.position - transform.position;
        if (MooveDirection.magnitude < AgreDistance)
        {
            if (MooveDirection.magnitude < AttackDistance)
            {
                CurrentSpeed = LeapSpeed;
                ActionType = ActionType.Attack;
            }
            else
            {
                CurrentSpeed = MaxSpeed;
                ActionType = ActionType.Moove;
            }
        }
        else
        {
            ActionType = ActionType.Idle;
            CurrentSpeed = 0;
            MooveDirection = Vector2.zero;
        }
    }

    protected void Awake()
    {
        AgreDistance = 12f;
        AttackDistance = 3f;
        animationMultiplier = 1f;
        LeapSpeed = 3.5f;
        MaxSpeed = 0.5f*LeapSpeed;
        Damage = 10f;
        MaxHP = 100f;
        AttackSpeed = 2f;
    }
}