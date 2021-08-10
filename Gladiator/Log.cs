using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Gladiator
{
    //public Rigidbody2D myRigidbody;
    private Rigidbody2D LogRigidbody;

    public Transform[] targets;
    private Transform testTarget;
    private int Team_State;

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

    [Header("Damage Popup")]
    public GameObject hudDamageText;
    public Transform hudPos;
    public GameObject FloatingTextPrefab;

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

        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    private void TransformFunc()
    {
        if (TeamSite_IntValue.RuntimeValue == A_Team)
        {
            testTarget = GameObject.FindGameObjectWithTag("B_Team").GetComponent<Transform>();
        }
        else if (TeamSite_IntValue.RuntimeValue == B_Team)
        {
            testTarget = GameObject.FindGameObjectWithTag("A_Team").GetComponent<Transform>();
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
                tempVector = tempVector * (attackRadius / pos1); 
                Debug.Log("CanFile: " + tempVector + "pos1: " + pos1);
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector, this.Team_State);
                canFire = false;
            }
        }
    }

    public virtual void CheckDistance(Transform targetArray)
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
                tempVector = tempVector * (attackRadius / pos1);
                Debug.Log("CanFile: " + tempVector + "pos1: " + pos1);
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector, this.Team_State);
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
        DamagePopupOpen(damage);
        // Play hurt animation

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(int damage, int this_team)
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
        //GameObject hudText = Instantiate(hudDamageText);
        //hudText.transform.position = hudPos.position;
        //hudText.GetComponent<DmgLogText>().damage = damage;
        //Vector3 temp = transform.position;
        //temp.y += 1f;
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
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
