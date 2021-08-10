using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : Projectile
{
    public GameObject hitEffect;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        //if (!other.isTrigger)
        {
            //for (int i = 0; i < tempTag.Length; i++)
            //{
            //    if (other.gameObject.CompareTag(tempTag[i]))
            //    {
            //        GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //        Destroy(efftct,0.2f);
            //        Destroy(this.gameObject);
            //        //Destroy(this.gameObject);
            //        //other.GetComponent<RockBreak>().RockBreakgif();
            //    }
            //}
            if (TeamSite_Projectile == A_Team)
            {
                if (other.gameObject.CompareTag("B_Team"))
                {
                    GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(efftct,0.2f);
                    Destroy(this.gameObject);
                }

                // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                // foreach (Collider2D enemy in hitEnemies)
                // {
                //     Debug.Log("projectile hit " + enemy.name);
                //     if (enemy != null)
                //     {
                //         enemy.GetComponent<Orge>().TakeDamage(baseAttack, B_Team);
                //         enemy.GetComponent<Log>().TakeDamage(baseAttack, B_Team);
                //     }
                // }

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
                    Destroy(efftct,0.2f);
                    Destroy(this.gameObject);
                }

                // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                // foreach (Collider2D enemy in hitEnemies)
                // {
                //     Debug.Log("projectile hit " + enemy.name);
                //     enemy.GetComponent<Orge>().TakeDamage(baseAttack, A_Team);
                //     enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team);
                // }
                Collider2D[] hitOrge = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Orge_MASK);
                foreach (Collider2D enemy in hitOrge)
                {
                    enemy.GetComponent<Orge>().TakeDamage_Ateam(baseAttack, A_Team);
                }

                Collider2D[] hitLog = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Log_MASK);
                foreach (Collider2D enemy in hitLog)
                {
                    enemy.GetComponent<Log>().TakeDamage(baseAttack, A_Team);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
