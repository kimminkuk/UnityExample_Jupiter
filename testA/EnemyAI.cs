using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : Log
{
    private Transform Ai_targets;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator Loganim;

    [Header("Projectile")]
    private int Team_State;
    private float pos1;
    private float AttackWait = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        gladiatorState = GladiatorState.idle;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Loganim = GetComponent<Animator>();

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
        moveSpeed = InitmoveSpeed.RuntimeValue;
        health = maxHealth.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        AttackSpeed = WeaponSpeed.RuntimeValue;
        healthBar.SetMaxHealth(health);
        Alive_BoolValue.RuntimeValue = true;
        fireDelaySeconds = AttackSpeed;
        fireDelay = AttackSpeed;

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
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

        if (Ai_targets == null) return;
        pos1 = Vector3.Distance(Ai_targets.position, transform.position);
        if (pos1 <= attackRadius)
        {
            if (canFire)
            {
                StartCoroutine(AttackCo());

                Vector3 tempVector = (Ai_targets.transform.position - transform.position).normalized;
                tempVector = tempVector * (attackRadius / pos1);
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector, this.Team_State, ProjectileSpeed_base);
                canFire = false;
            }
        }

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, Ai_targets.position, OnPathComplete);
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
        Debug.Log("Enemy AIFixedUpdate()");

        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;

        }
        if (path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        //changeAnim(direction);
        //Vector2 force = direction * moveSpeed * Time.deltaTime * 200;
        //rb.AddForce(force);


        Vector3 temp = Vector3.MoveTowards(transform.position, Ai_targets.position, moveSpeed * Time.deltaTime);
        changeAnim(temp - transform.position);
        myRigidbody.MovePosition(temp);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }

    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        Loganim.SetBool("attacking", true);
        yield return new WaitForSeconds(this.AttackWait);

        gladiatorState = GladiatorState.idle;
        Loganim.SetBool("attacking", false);
    }

    public override void TakeDamage(int damage)
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

    public override void TakeDamage(int damage, int this_team)
    {
        if (this_team == this.Team_State)
        {
            health -= damage;
            healthBar.SetHealth(health);
            DamagePopupOpen(damage);

            // Play hurt animation
            StartCoroutine(TakeKnock());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator TakeKnock()
    {
        // Play hurt animation
        if (gladiatorState != GladiatorState.attack)
        {
            Loganim.SetBool("hurting", true);
            gladiatorState = GladiatorState.stagger;
            yield return new WaitForSeconds(0.1f);

            // Play hurt animation
            Loganim.SetBool("hurting", false);
            gladiatorState = GladiatorState.idle;
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
        Debug.Log("Log DamagePopupOpen: " + damage);
    }

    public override void Die()
    {
        Debug.Log("Log Die!");
        //Die Animation
        DeathEffect();

        //Disable the enemy
        //this.gameObject.SetActive(false);
        //this.enabled = false;
        Alive_BoolValue.RuntimeValue = false;
        Destroy(this.gameObject);
    }

    public override void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
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
}
