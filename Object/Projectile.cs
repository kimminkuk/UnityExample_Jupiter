using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public Vector2 directionToMove;
    public IntValue DamageProjectile;
    public FloatValue ProjectileSpeed;
    public float projectileSpeed_;
    protected int TeamSite_Projectile;
    protected int A_Team = 1;
    protected int B_Team = 2;
    protected int A_Team_Layer = 11;
    protected int B_Team_Layer = 12;
    protected LayerMask Orge_MASK = 1 << 8;
    protected LayerMask Log_MASK = 1 << 10;

    [Header("Life Time")]
    public float lifetime;
    protected float lifetimeSeconds;
    public Rigidbody2D myRigidbody;

    //public string[] tempTag = { "Human", "Skeleton","Orge"};

    [Header("Collider Check")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int baseAttack;
    private float moveSpeed = 4.999f;

    public Vector3 targetPos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
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

        Vector3 nextPos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.position = nextPos;
    }

    public virtual void InitSet(Vector3 this_target, int this_team, float this_projectileSpeed, int this_Damage)
    {
        TeamSite_Projectile = this_team;
        targetPos = this_target;
        speed = this_projectileSpeed;
        baseAttack = this_Damage;
    }

    public void Launch(Vector2 initialVel, int this_Team, float this_projectileSpeed)
    {
        //projectileSpeed_ = ProjectileSpeed.RuntimeValue * 100f;
        projectileSpeed_ = this_projectileSpeed * 100f;
        baseAttack = DamageProjectile.RuntimeValue;
        myRigidbody.velocity = initialVel * projectileSpeed_ * Time.deltaTime;

        TeamSite_Projectile = this_Team;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.isTrigger)
        {
            if (TeamSite_Projectile == A_Team)
            {
                if(other.gameObject.CompareTag("B_Team"))
                {
                    Destroy(this.gameObject);
                }
            }
            else if (TeamSite_Projectile == B_Team)
            {
                if (other.gameObject.CompareTag("A_Team"))
                {
                    Destroy(this.gameObject);
                }
            }

            // for(int i = 0; i < tempTag.Length; i++)
            // {
            //     if(other.gameObject.CompareTag(tempTag[i]))
            //     {
            //         Destroy(this.gameObject);
            //     }
            // }
        }
    }
}
