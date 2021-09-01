using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicRock : Projectile
{
    [Tooltip("Position we want to hit")]
    //public Vector3 targetPos;
    public GameObject hitEffect;
    public int team;
    // 
    // [Tooltip("Horizontal seppd, in units/sec")]
    //public float speed = 1;
    private Vector3 startPos;
    // 
    // [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Parabolic_Start\n");
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
        startPos = transform.position;
        //TeamSite_Projectile = team;
    }
    public override void InitSet(Vector3 this_target, int this_team, float this_projectileSpeed, int this_Damage)
    {
        TeamSite_Projectile = this_team;
        targetPos = this_target;
        speed = this_projectileSpeed;
        baseAttack = this_Damage;
    }
    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }


        Debug.Log("Parabolic_Update : " + targetPos);
         // Compute the next position -- straight flight
         Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
         
         // Compute the next position, with arc added in
         float x0 = startPos.x;
         float x1 = targetPos.x;
         float dist = x1 - x0;
         float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
         float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
         float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
         
         //result-> -0.25f * ( dist )
         
         nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
         
         // Rotate to face the next position, and then move there
         //transform.rotation = LookAt2D(nextPos - transform.position);
         transform.position = nextPos;
         
         // // Do something when we reach the target
         // if (nextPos == targetPos) Arrived();
    }

    public void Parabolic_Launch(Vector3 this_target, int this_Team, float this_projectileSpeed)
    {
        projectileSpeed_ = this_projectileSpeed * 100f;
        baseAttack = DamageProjectile.RuntimeValue;
        myRigidbody.velocity = this_target * projectileSpeed_ * Time.deltaTime;

        TeamSite_Projectile = this_Team;
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
    void Arrived()
    {
        Destroy(gameObject);

    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}
