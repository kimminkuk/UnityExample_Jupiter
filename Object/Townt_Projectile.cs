using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Townt_Projectile : Projectile
{
    [Tooltip("Position we want to hit")]
    //public Vector3 targetPos;
    public GameObject hitEffect;
    public int team;
    private Vector3 startPos;
    private Vector3 targetPos2;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
        startPos = transform.position; 
    }
    public override void InitSet(Vector3 this_target, int this_team, float this_projectileSpeed, int this_Damage)
    {
        TeamSite_Projectile = this_team;
        targetPos = this_target;
        speed = this_projectileSpeed;
        baseAttack = this_Damage;
        this.targetPos2 = this_target;
        this.targetPos2.y += + 7f;
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;

        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
        Vector3 nextPos = Vector3.MoveTowards(targetPos2, targetPos, speed * Time.deltaTime);

        // float nextY = Mathf.MoveTowards(targetPos2.y, targetPos.y, speed * Time.deltaTime);
        // float dist = targetPos2.y - targetPos.y;
        float baseY = Mathf.Lerp(targetPos2.y, targetPos.y, 0.3f);
        // //
        nextPos = new Vector3(targetPos.x, targetPos2.y-baseY, targetPos.z);
        transform.position = nextPos;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (TeamSite_Projectile == A_Team)
        {
            if (other.gameObject.CompareTag("B_Team"))
            {
                GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(efftct, 0.2f);
                Destroy(this.gameObject);
            }

            Collider2D[] hitOrge = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitOrge)
            {
                enemy.GetComponent<Orge>().TakeDamage_Bteam(baseAttack, B_Team);
            }

            Collider2D[] hitLog = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Log_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team);
            }
        }
        else if (TeamSite_Projectile == B_Team)
        {
            if (other.gameObject.CompareTag("A_Team"))
            {
                GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(efftct, 0.2f);
                Destroy(this.gameObject);
            }

            Collider2D[] hitLog = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Log_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team);
            }

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitEnemy)
            {
                Debug.Log("Stone Damage: " + baseAttack);
                enemy.GetComponent<NewGladiator>().TakeDamage_Ateam(baseAttack, A_Team);
            }
        }
    }
}
