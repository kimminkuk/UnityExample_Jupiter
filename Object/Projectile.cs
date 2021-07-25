using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;

    [Header("Life Time")]
    public float lifetime;
    private float lifetimeSeconds;
    public Rigidbody2D myRigidbody;

    public string[] tempTag = { "Human", "Skeleton","Orge"};

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
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
        myRigidbody.velocity = initialVel * moveSpeed;
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
