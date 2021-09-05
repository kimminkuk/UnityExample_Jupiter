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
        lifetimeSeconds = lifetime*2;
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

        transform.position = this.targetPos2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifetimeSeconds -= Time.deltaTime;

        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
        // // 1) Only MoveTowards
        // // transform.position = this.targetPos2;
        // Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime *1.5f);
        // transform.position = nextPos;

        // 2) MoveTowards + Lerp
        // transform.position = this.targetPos2;
        // Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        // float baseY = Mathf.Lerp(transform.position.y, targetPos.y, 0.001f); //7*0.1 -> 0.7
        // nextPos = new Vector3(nextPos.x, nextPos.y - baseY, transform.position.z);
        // transform.position = nextPos;

        // 3) AddForce
        //myRigidbody.AddForce(new Vector3(0, -4f, 0), ForceMode2D.Impulse);
        myRigidbody.AddForce(new Vector3(0, -4f, 0), ForceMode2D.Force);
        
        // 
        // 4) velocity?
        // myRigidbody.velocity = new Vector3(0, -1, 0) * Time.deltaTime;
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
            Debug.Log("OnTriggerEnter2D Townt Call: " + baseAttack);
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
                Debug.Log("Townt Damage: " + baseAttack);
                enemy.GetComponent<NewGladiator>().TakeDamage_Ateam(baseAttack, A_Team);
            }
        }
    }
}
