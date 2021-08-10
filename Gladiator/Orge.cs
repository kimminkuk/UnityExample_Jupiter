using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orge : Gladiator
{
    //public Rigidbody2D myRigidbody;
    private Rigidbody2D OrgeRigidbody;
    public Transform[] targets;
    public string[] targetsName;
    public float chaseRadius;
    public float attackRadius;

    private Transform testTarget;
    private int Team_State;
    private GameObject testEnemyTarget;
    private float AttackWait = 0.5f;
    private bool tookDamage;

    //public Animator anim;
    private Animator OrgeAnim;

    public Transform[] attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

    [Header("Damage Popup")]
    public GameObject hudDamageText;
    public Transform hudPos;

    [Header("Test of Prefab Tracking")]
    private CheckGladiator check_;



    // Start is called before the first frame update
    private void Awake()
    {
        //targets[0] = GameObject.Find("Log_G").transform;
    }
    void Start()
    {
        Debug.Log("Orge Start");
        moveSpeed = InitmoveSpeed.RuntimeValue;
        health = maxHealth.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        gladiatorState = GladiatorState.idle;

        OrgeRigidbody = GetComponent<Rigidbody2D>();
        OrgeAnim = GetComponent<Animator>();

        OrgeAnim.SetFloat("moveX", 0);
        OrgeAnim.SetFloat("moveY", -1);
        healthBar.SetMaxHealth(health);
        A_Team_Layer = LayerMask.NameToLayer("A_TEAM_LAYER");
        B_Team_Layer = LayerMask.NameToLayer("B_TEAM_LAYER");

        if (TeamSite_IntValue.RuntimeValue == A_Team)
        {
            this.gameObject.tag = "A_Team";
            //this.gameObject.layer = A_Team_Layer;
            this.Team_State = A_Team;
        }
        else if (TeamSite_IntValue.RuntimeValue == B_Team)
        {
            this.gameObject.tag = "B_Team";
            //this.gameObject.layer = B_Team_Layer;
            this.Team_State = B_Team;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         TransformFunc();
         if (testTarget != null)
         {
            
            CheckDistance(testTarget);
         }
    }
    private void TransformFunc()
    {
        if (TeamSite_IntValue.RuntimeValue == A_Team)
        {
            testTarget = GameObject.FindGameObjectWithTag("B_Team").GetComponent<Transform>();
            testEnemyTarget = GameObject.FindGameObjectWithTag("B_Team").GetComponent<GameObject>();
        }
        else if (TeamSite_IntValue.RuntimeValue == B_Team)
        {
            testTarget = GameObject.FindGameObjectWithTag("A_Team").GetComponent<Transform>();
            testEnemyTarget = GameObject.FindGameObjectWithTag("A_Team").GetComponent<GameObject>();
        }
    }
    public virtual void CheckDistance(Transform targetArray, string nameArray)
    {
        if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
            && Vector3.Distance(targetArray.position, transform.position) > attackRadius)
        {
            if (gladiatorState == GladiatorState.idle || gladiatorState == GladiatorState.walk
                && gladiatorState != GladiatorState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    targetArray.position,
                    moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(GladiatorState.walk);
            }
        }
        else if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
                 && Vector3.Distance(targetArray.position, transform.position) <= attackRadius)
        {
            if (gladiatorState == GladiatorState.walk || gladiatorState == GladiatorState.idle)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                               targetArray.position,
                               moveSpeed * Time.deltaTime);

                StartCoroutine(AttackCo());
                changeAnimAttackDirection(temp - transform.position);
            }
        }
    }

    public virtual void CheckDistance(Transform targetArray)
    {
        if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
            && Vector3.Distance(targetArray.position, transform.position) > attackRadius)
        {
            if (gladiatorState == GladiatorState.idle || gladiatorState == GladiatorState.walk
                && gladiatorState != GladiatorState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                    targetArray.position,
                    moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(GladiatorState.walk);
            }
        }
        else if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
                 && Vector3.Distance(targetArray.position, transform.position) <= attackRadius)
        {
            if (gladiatorState == GladiatorState.walk || gladiatorState == GladiatorState.idle)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position,
                               targetArray.position,
                               moveSpeed * Time.deltaTime);

                StartCoroutine(AttackCo());
                if (tookDamage)
                {
                    changeAnimAttackDirection(temp - transform.position);
                }
                tookDamage = false;
            }
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
    private void SetAnimFloat(Vector2 setVector)
    {
        OrgeAnim.SetFloat("MoveX", setVector.x);
        OrgeAnim.SetFloat("MoveY", setVector.y);
        OrgeAnim.SetBool("moving", true);
    }

    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        OrgeAnim.SetBool("attacking", true);
        tookDamage = true;
        yield return new WaitForSeconds(AttackWait);

        gladiatorState = GladiatorState.idle;
        OrgeAnim.SetBool("attacking", false);
    }

    public void changeAnimAttackDirection(Vector2 direction)
    {
        int tm = enemyLayers;
        Debug.Log("changeAnimAttackDirection: " + tm);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                OrgeDamageLayer(attackPoint[0].position);
            }
            else
            {
                OrgeDamageLayer(attackPoint[1].position);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                OrgeDamageLayer(attackPoint[2].position);
            }
            else
            {
                OrgeDamageLayer(attackPoint[3].position);
            }
        }
    }

    private void OrgeDamageLayer(Vector2 this_AttackPoint)
    {
        // if (Team_State == A_Team)
        // {
        //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, enemyLayers);
        //     foreach (Collider2D enemy in hitEnemies)
        //     {
        //         enemy.GetComponent<Orge>().TakeDamage(baseAttack, B_Team);
        //         //enemy.GetComponent<Orge>().TakeDamage(baseAttack, B_Team);
        //     }
        // 
        //     Collider2D[] hitEnemies1 = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, enemyLayers);
        //     foreach (Collider2D enemy in hitEnemies1)
        //     {
        //         enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team);
        //         //enemy.GetComponent<Orge>().TakeDamage(baseAttack, A_Team);
        //     }
        // 
        // }
        // else if (Team_State == B_Team)
        // {
        //     Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, enemyLayers);
        //     foreach (Collider2D enemy in hitEnemies)
        //     {
        //         enemy.GetComponent<Orge>().TakeDamage(baseAttack, A_Team);
        //         //enemy.GetComponent<Orge>().TakeDamage(baseAttack, A_Team);
        //     }
        // 
        //     Collider2D[] hitEnemies1 = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, enemyLayers);
        //     foreach (Collider2D enemy in hitEnemies1)
        //     {
        //         enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team);
        //         //enemy.GetComponent<Orge>().TakeDamage(baseAttack, A_Team);
        //     }
        // }

        if (Team_State == A_Team)
        {
            Collider2D[] hitOrge = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitOrge)
            {
                Debug.Log("enemy.GetComponent<Orge>().TakeDamage(baseAttack, B_Team)");
                enemy.GetComponent<Orge>().TakeDamage_Bteam(baseAttack, B_Team);
            }

            Collider2D[] hitLog = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Log_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                Debug.Log("enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team)");
                enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team);
            }
        }
        else if (Team_State == B_Team)
        {
            Collider2D[] hitOrge = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitOrge)
            {
                Debug.Log("enemy.GetComponent<Orge>().TakeDamage(baseAttack, A_Team)");
                enemy.GetComponent<Orge>().TakeDamage_Ateam(baseAttack, A_Team);
            }

            Collider2D[] hitLog = Physics2D.OverlapCircleAll(this_AttackPoint, attackRange, Log_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                Debug.Log("enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team)");
                enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team);
            }
        }
    }

    public void ChangeState(GladiatorState newState)
    {
        if (gladiatorState != newState)
        {
            gladiatorState = newState;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        DamagePopupOpen(damage);
        // Play hurt animation

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage_Ateam(int damage, int this_team)
    {
        if (this_team == this.Team_State)
        {
            health -= damage;
            healthBar.SetHealth(health);
            DamagePopupOpen(damage);
            // Play hurt animation
            if (health <= 0)
            {
                Die();
            }
        }
    }
    public virtual void TakeDamage_Bteam(int damage, int this_team)
    {
        if (this_team == this.Team_State)
        {
            health -= damage;
            healthBar.SetHealth(health);
            DamagePopupOpen(damage);
            // Play hurt animation
            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void DamagePopupOpen(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;
        hudText.GetComponent<DamageText>().damage = damage;
    }

    public virtual void Die()
    {
        Debug.Log("Orge Die!");
        //Die Animation
        DeathEffect();
        //Disable the enemy
        //this.gameObject.SetActive(false);
        //this.enabled = false;
        Destroy(this.gameObject);
    }

    public virtual void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }
}
