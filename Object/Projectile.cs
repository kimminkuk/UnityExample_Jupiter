using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public Vector2 directionToMove;
    public FloatValue DamageProjectile;
    public FloatValue ProjectileSpeed;
    public float projectileSpeed_;

    [Header("Life Time")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidbody;

    public string[] tempTag = { "Human", "Skeleton","Orge"};

    [Header("Collider Check")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float baseAttack;
    private float moveSpeed = 4.999f;
    // Start is called before the first frame update
    void Start()
    {
        projectileSpeed_ = ProjectileSpeed.RuntimeValue;
        baseAttack = DamageProjectile.RuntimeValue;
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
        Debug.Log("projectileSpeed_: " + projectileSpeed_);
    }

    // Update is called once per frame
    void Update()
    {
        lifetimeSeconds -= Time.deltaTime;

        if(lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel)
    {
        //myRigidbody.velocity = initialVel * moveSpeed;

        projectileSpeed_ = ProjectileSpeed.RuntimeValue;
        myRigidbody.velocity = initialVel * projectileSpeed_;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.isTrigger)
        {
            for(int i = 0; i < tempTag.Length; i++)
            {
                if(other.gameObject.CompareTag(tempTag[i]))
                {
                    Destroy(this.gameObject);
                    //other.GetComponent<RockProjectile>().RockBreak();
                }
            }
        }
    }
}
