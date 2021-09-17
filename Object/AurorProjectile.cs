using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AurorProjectile : Projectile
{
    public float CalPos;
    public int DirectionSkill;
    private Vector3 TargetPos;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;

        switch (DirectionSkill)
        {
            case 1: //Left
                TargetPos.x = transform.position.x - 20f;
                TargetPos.y = transform.position.y;
                TargetPos.z = transform.position.z;
                break;
            case 2: // right
                TargetPos.x = transform.position.x + 20f;
                TargetPos.y = transform.position.y;
                TargetPos.z = transform.position.z;
                break;
            case 3: //Up
                TargetPos.x = transform.position.x;
                TargetPos.y = transform.position.y + 20f;
                TargetPos.z = transform.position.z;
                break;
            case 4: //Down
                TargetPos.x = transform.position.x;
                TargetPos.y = transform.position.y - 20f;
                TargetPos.z = transform.position.z;
                break;
            default:
                TargetPos.x = transform.position.x + 20f;
                TargetPos.y = transform.position.y + 20f;
                TargetPos.z = transform.position.z;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;

        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }

        Vector3 nextPos = Vector3.MoveTowards(transform.position, TargetPos, speed * Time.deltaTime);
        
        speed += 0.1f;
        transform.position = nextPos;
    }

    public override void InitSet(Vector3 this_target, int this_team, float this_projectileSpeed, int this_Damage)
    {
        TeamSite_Projectile = this_team;
        targetPos = this_target;
        speed = this_projectileSpeed;
        baseAttack = this_Damage;
    }

    public GameObject hitEffect;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        int inflictChance = Random.Range(0, 9);

        if (TeamSite_Projectile == A_Team)
        {
            //if (other.gameObject.CompareTag("B_Team"))
            //{
            //    GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //    Destroy(efftct, 0.2f);
            //    Destroy(this.gameObject);
            //}

            Collider2D[] hitLog = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Log_MASK);
            foreach (Collider2D enemy in hitLog)
            {
                Debug.Log("Rock Damage: " + baseAttack);
                enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team, inflictChance);
            }
        }
        else if (TeamSite_Projectile == B_Team)
        {
            //if (other.gameObject.CompareTag("A_Team"))
            //{
            //    GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //    Destroy(efftct, 0.2f);
            //    Destroy(this.gameObject);
            //}

            //Collider2D[] hitLog = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Log_MASK);
            //foreach (Collider2D enemy in hitLog)
            //{
            //    enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team, inflictChance);
            //}
            //
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Orge_MASK);
            foreach (Collider2D enemy in hitEnemy)
            {
                Debug.Log("Auror Damage: " + baseAttack);
                enemy.GetComponent<AI_Orge>().TakeDamage_Ateam(baseAttack, A_Team, inflictChance);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
