using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Gladiator
{
    //public Rigidbody2D myRigidbody;
    private Rigidbody2D LogRigidbody;

    public Transform[] targets;
    public string[] targetsName;
    public float chaseRadius;
    public float attackRadius;

    public GameObject projectile;
    private float projectileSpeed_;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    //public Animator anim;
    private Animator LogAnim;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;
    private float pos1;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Log Start");
        moveSpeed = InitmoveSpeed.RuntimeValue;
        health = maxHealth.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        gladiatorState = GladiatorState.idle;
        LogRigidbody = GetComponent<Rigidbody2D>();
        LogAnim = GetComponent<Animator>();

        healthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
                CheckDistance(targets[i], targetsName[i]);
        }

        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public virtual void CheckDistance(Transform targetArray, string nameArray)
    {
        pos1 = Vector3.Distance(targetArray.position, transform.position);
        if (pos1 <= chaseRadius && pos1 > attackRadius)
        {
            if (gladiatorState == GladiatorState.idle || gladiatorState == GladiatorState.walk
                && gladiatorState != GladiatorState.stagger)
            {
                //transform.position = Vector3.MoveTowards(transform.position,
                //    target.position,
                //    moveSpeed * Time.deltaTime);

                Vector3 temp = Vector3.MoveTowards(transform.position,
                    targetArray.position,
                    moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(GladiatorState.walk);
            }
        }
        else if (pos1 <= chaseRadius && pos1 <= attackRadius)
        {
            if (canFire)
            {
                Vector3 tempVector = (targetArray.transform.position - transform.position).normalized;
                //if (pos1 < attackRadius / 2)
                //{
                //    tempVector = (targetArray.transform.position - transform.position).normalized;
                //}
                //else
                //{
                //    tempVector = (targetArray.transform.position - transform.position).normalized * 1.5f;
                //}
                tempVector = tempVector * (attackRadius / pos1); 
                Debug.Log("CanFile: " + tempVector + "pos1: " + pos1);
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector);
                canFire = false;
            }
        }
    }


    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        LogAnim.SetBool("attacking", true);
        yield return new WaitForSeconds(0.5f);

        gladiatorState = GladiatorState.walk;
        LogAnim.SetBool("attacking", false);
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        LogAnim.SetFloat("MoveX", setVector.x);
        LogAnim.SetFloat("MoveY", setVector.y);
        //LogAnim.SetBool("moving", true);
    }

/*    public override void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }*/

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
        // Play hurt animation

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Log Die!");
        //Die Animation
        DeathEffect();

        //Disable the enemy
        //this.gameObject.SetActive(false);
        //this.enabled = false;
        Destroy(this.gameObject);
    }

    public virtual void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }
}
