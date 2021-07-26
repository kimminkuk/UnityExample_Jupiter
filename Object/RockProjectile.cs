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
            for (int i = 0; i < tempTag.Length; i++)
            {
                if (other.gameObject.CompareTag(tempTag[i]))
                {
                    GameObject efftct = Instantiate(hitEffect, transform.position, Quaternion.identity);
                    Destroy(efftct,0.2f);
                    Destroy(this.gameObject);
                    //Destroy(this.gameObject);
                    //other.GetComponent<RockBreak>().RockBreakgif();
                }
            }
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("projectile hit " + enemy.name);
            enemy.GetComponent<Orge>().TakeDamage(baseAttack);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
