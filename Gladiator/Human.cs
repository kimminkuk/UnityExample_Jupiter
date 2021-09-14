using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Human : Gladiator
{
    [Tooltip("Default Setting")]
    private Rigidbody2D HumanRigidbody;
    public float chaseRadius;
    public float attackRadius;
    private Animator HumanAnim;
    private Vector3 GetMoveTowards;
    private float AttackDelaySeconds;
    private float AttackWait;
    private bool canAttack = true;
    public bool inflictDamage;
    private int Team_State;
    public Transform[] attackPoint;
    public float attackRange = 0.5f;
    

    [Header("Damage Popup")]
    public GameObject FloatingTextPrefab;

    [Header("Pathfinding")]
    private Path path;
    private Seeker seeker;
    private Transform Ai_targets;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    public float nextWayPointDistance = 3f;

    [Header("Death Effects")]
    public GameObject deathEffect;
    protected float deathEffectDelay = 1f;

    [Header("Human Active SkillList")]
    public BoolValue[] ActiveSkillList;
    private bool Skill_1_OnOff = true;
    private float Skill_1_Range;
    private Vector3 Skill_1_CurPos;

    // Start is called before the first frame update
    void Start()
    {
        gladiatorState = GladiatorState.idle;
        seeker = GetComponent<Seeker>();
        HumanRigidbody = GetComponent<Rigidbody2D>();
        HumanAnim = GetComponent<Animator>();

        HumanAnim.SetFloat("moveX", 0);
        HumanAnim.SetFloat("moveY", -1);

        moveSpeed = InitmoveSpeed.RuntimeValue;
        health = maxHealth.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        AttackSpeed = WeaponSpeed.RuntimeValue;
        healthBar.SetMaxHealth(health);
        Alive_BoolValue.RuntimeValue = true;
        AttackDelaySeconds = AttackSpeed;
        AttackWait = AttackSpeed;

        if (TeamSite_IntValue.RuntimeValue == A_Team)
        {
            this.gameObject.tag = "A_Team";
            this.Team_State = A_Team;

        }
        else if (TeamSite_IntValue.RuntimeValue == B_Team)
        {
            this.gameObject.tag = "B_Team";
            this.Team_State = B_Team;
        }

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (Ai_targets == null) return;

        if (seeker.IsDone())
        {
            seeker.StartPath(HumanRigidbody.position, Ai_targets.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (TeamSite_IntValue.RuntimeValue == A_Team)
        {
            if (GameObject.FindGameObjectWithTag("B_Team"))
            {
                Ai_targets = GameObject.FindGameObjectWithTag("B_Team").GetComponent<Transform>();
            }
        }
        else if (TeamSite_IntValue.RuntimeValue == B_Team)
        {
            if (GameObject.FindGameObjectWithTag("A_Team"))
            {
                Ai_targets = GameObject.FindGameObjectWithTag("A_Team").GetComponent<Transform>();
            }
        }

        if (path == null || Ai_targets == null)
        {
            return;
        }

        CheckDistance(Ai_targets);

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        float distance = Vector2.Distance(HumanRigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }

    public virtual void CheckDistance(Transform targetPos)
    {
        var DisPos = Vector3.Distance(targetPos.position, transform.position);
        GetMoveTowards = Vector3.MoveTowards(transform.position, Ai_targets.position, moveSpeed * Time.deltaTime);
        
        if(Skill_1_OnOff)
        {
            Skill_1_Range = attackRadius * 2f;
        }
        else
        {
            Skill_1_Range = attackRadius;
        }
        if (DisPos <= chaseRadius && DisPos > Skill_1_Range)
        {
            if (gladiatorState == GladiatorState.idle || gladiatorState == GladiatorState.walk)
            {
                changeAnim(GetMoveTowards - transform.position);
                HumanRigidbody.MovePosition(GetMoveTowards);
                ChangeState(GladiatorState.walk);
            }
        }
        else if (DisPos <= chaseRadius && DisPos <= Skill_1_Range)
        {
            if (gladiatorState == GladiatorState.walk || gladiatorState == GladiatorState.idle)
            {
                if (AttackDelaySeconds <= 0)
                {
                    canAttack = true;
                    AttackDelaySeconds = AttackWait;
                }
                if (!canAttack)
                {
                    AttackDelaySeconds -= Time.deltaTime;
                }
                else
                {
                    if (Skill_1_OnOff && ActiveSkillList[0].RuntimeValue)
                    {
                        baseAttack = DamageIntValue.RuntimeValue * 2;
                        StartCoroutine(Skill_1_Sting());
                    }
                    else
                    {
                        StartCoroutine(AttackCo());
                    }

/*                    if (inflictDamage)
                    {
                        inflictDamage = false;
                        changeAnimAttackDirection(GetMoveTowards - transform.position);
                    }*/
                }
            }
        }
    }

    private void ChangeState(GladiatorState newState)
    {
        if (gladiatorState != newState)
        {
            gladiatorState = newState;
        }
    }

    private void changeAnimAttackDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                Debug.Log("Human Attack Right");
                HumanDamageLayer(attackPoint[0].position);
            }
            else
            {
                HumanDamageLayer(attackPoint[1].position);
                Debug.Log("Human Attack Left");
            }
        }
        else
        {
            if (direction.y > 0)
            {
                HumanDamageLayer(attackPoint[2].position);
            }
            else
            {
                HumanDamageLayer(attackPoint[3].position);
            }
        }
    }

    private void HumanDamageLayer(Vector2 this_AttackPoint)
    {
        int inflictChance = Random.Range(0, 9);
        if (Team_State == A_Team)
        {
            Collider2D[] hitOrge = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitOrge)
            {
                enemy.GetComponent<NewGladiator>().TakeDamage_Bteam(baseAttack, B_Team, inflictChance);
            }

            Collider2D[] hitLog = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Log_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team, inflictChance);
            }
            Collider2D[] hitLog_A = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Log_A_MASK);
            foreach (Collider2D enemy in hitLog_A)
            {
                enemy.GetComponent<EnemyAI>().TakeDamage(baseAttack, B_Team, inflictChance);
            }
        }
        else if (Team_State == B_Team)
        {
            Collider2D[] hitLog = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                enemy.GetComponent<NewGladiator>().TakeDamage_Ateam(baseAttack, A_Team, inflictChance);
            }
        }
    }

    public virtual void TakeDamage(int damage, int this_team, int dodge)
    {
        if (this_team == this.Team_State)
        {
            if (DodgeChance <= dodge)
            {
                health -= damage;
                healthBar.SetHealth(health);
                DamagePopupOpen(damage);

                // Play hurt animation and KnockBack
                StartCoroutine(TakeKnock());

                if (health <= 0)
                {
                    Die();
                }
            }
            else
            {
                DodgePopupOpen();
            }
        }
    }

    private IEnumerator Skill_1_Sting()
    {
        Skill_1_CurPos = GetMoveTowards;
        gladiatorState = GladiatorState.attack;
        HumanAnim.SetBool("Skill_1_Sting", true);
        canAttack = false;

        yield return new WaitForSeconds(AttackWait * 0.5f);
        changeAnimAttackDirection(Skill_1_CurPos - transform.position);

        yield return new WaitForSeconds(AttackWait * 0.17f);

        Skill_1_OnOff = false;

        baseAttack = DamageIntValue.RuntimeValue;
        gladiatorState = GladiatorState.idle;
        HumanAnim.SetBool("Skill_1_Sting", false);
        yield return new WaitForSeconds(AttackWait * 0.67f);
    }
    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        HumanAnim.SetBool("attacking", true);
        canAttack = false;

        yield return new WaitForSeconds(AttackWait * 0.5f);

        changeAnimAttackDirection(GetMoveTowards - transform.position);

        gladiatorState = GladiatorState.idle;
        HumanAnim.SetBool("attacking", false);
        //HumanAnim.SetBool("moving", true);
        yield return new WaitForSeconds(AttackWait * 0.67f);
    }

    private IEnumerator TakeKnock()
    {
        // Play hurt animation
        if (gladiatorState != GladiatorState.attack)
        {
            HumanAnim.SetBool("hurting", true);
            gladiatorState = GladiatorState.stagger;
            yield return new WaitForSeconds(0.17f);

            // Play hurt animation
            HumanAnim.SetBool("hurting", false);
            gladiatorState = GladiatorState.idle;
        }
    }


    public virtual void DodgePopupOpen()
    {

        return;
    }

    public virtual void Die()
    {
        Debug.Log("Human Die!");

        //Die Animation
        //DeathEffect();
        DieAnimation();

        //Disable the enemy
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    private void DieAnimation()
    {
        HumanAnim.SetTrigger("Die");
        return;
    }

    private void DamagePopupOpen(int damage)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
    }

    public virtual void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        for (int i = 0; i < attackPoint.Length; i++)
        {
            Gizmos.DrawWireSphere(attackPoint[i].position, attackRange);
        }
    }
}