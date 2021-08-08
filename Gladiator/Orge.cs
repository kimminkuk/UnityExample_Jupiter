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
    public GameObject[] tests;
    public string[] targetsName;
    public float chaseRadius;
    public float attackRadius;

    private Transform testTarget;

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

        //Test Target Tracking
        //if (testTarget != null)
        {
            testTarget = GameObject.FindGameObjectWithTag("Log_G").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("testTarget: " +  testTarget.position);
        // for (int i = 0; i < targets.Length; i++)
        // {
        // 
        //     if (targets[i] != null)
        //     {
        //         //targets[0] = check_.On_BSite_Gladiators.t
        //         CheckDistance(targets[i], targetsName[i]);
        //     }
        // }
        if (testTarget != null)
        {
            CheckDistance(testTarget);
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
                changeAnimAttackDirection(temp - transform.position);
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
        yield return new WaitForSeconds(0.5f);

        gladiatorState = GladiatorState.idle;
        OrgeAnim.SetBool("attacking", false);
    }

    //public override void changeAnim(Vector2 direction)
    //{
    //    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    //    {
    //        if (direction.x > 0)
    //        {
    //            SetAnimFloat(Vector2.right);
    //        }
    //        else
    //        {
    //            SetAnimFloat(Vector2.left);
    //        }
    //    }
    //    else
    //    {
    //        if (direction.y > 0)
    //        {
    //            SetAnimFloat(Vector2.up);
    //        }
    //        else
    //        {
    //            SetAnimFloat(Vector2.down);
    //        }
    //    }
    //}

    public void changeAnimAttackDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint[0].position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                    enemy.GetComponent<Log>().TakeDamage(baseAttack);
                }
            }
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint[1].position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                    enemy.GetComponent<Log>().TakeDamage(baseAttack);
                }
            }
        }
        else
        {
            if (direction.y > 0)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint[2].position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                    enemy.GetComponent<Log>().TakeDamage(baseAttack);
                }
            }
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint[3].position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("We hit " + enemy.name);
                    enemy.GetComponent<Log>().TakeDamage(baseAttack);
                }
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
