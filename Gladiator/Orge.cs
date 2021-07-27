using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orge : Gladiator
{
    public Rigidbody2D myRigidbody;
    public Transform[] targets;
    public string[] targetsName;
    public float chaseRadius;
    public float attackRadius;

    public Animator anim;

    public Transform[] attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth.initialValue;
        gladiatorState = GladiatorState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //for (int i = 0; i < targets.Length; i++)
        //{
        //    //if (targets[i] != null)
        //    targets[i] = GameObject.FindWithTag(targetsName[i]).transform;
        //}
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CheckDistance();

        for (int i = 0; i < targets.Length; i++)
        {
            if(targets[i] != null)
                CheckDistance(targets[i], targetsName[i]);
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
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
        anim.SetBool("moving", true);
    }

    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        anim.SetBool("attacking", true);
        yield return new WaitForSeconds(0.5f);

        gladiatorState = GladiatorState.idle;
        anim.SetBool("attacking", false);
    }

    public void changeAnim(Vector2 direction)
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
    }

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

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        // Play hurt animation

        if (health <= 0)
        {
            Die();
        }
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
