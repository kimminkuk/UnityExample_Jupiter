using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Gladiator
{
    public Rigidbody2D myRigidbody;
    public Transform[] targets;
    public string[] targetsName;
    public float chaseRadius;
    public float attackRadius;

    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        gladiatorState = GladiatorState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = GameObject.FindWithTag(targetsName[i]).transform;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CheckDistance();

        for (int i = 0; i < targets.Length; i++)
        {
            CheckDistance(targets[i], targetsName[i]);
        }

        fireDelaySeconds -= Time.deltaTime;
        if (fireDelaySeconds <= 0)
        {
            canFire = true;
            fireDelaySeconds = fireDelay;
        }
    }

    public virtual void CheckDistance(Transform targetArray, string nameArray)
    {
        if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
            && Vector3.Distance(targetArray.position, transform.position) > attackRadius)
        {
            if (gladiatorState == GladiatorState.idle || gladiatorState == GladiatorState.walk
                && gladiatorState != GladiatorState.stagger)
            {
                //transform.position = Vector3.MoveTowards(transform.position,
                //    target.position,
                //    moveSpeed * Time.deltaTime);

                Vector3 temp = Vector3.MoveTowards(transform.position,
                    targetArray.position,
                    moveSpeed * Time.deltaTime);

                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);

                ChangeState(GladiatorState.walk);
            }
        }
        else if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
            && Vector3.Distance(targetArray.position, transform.position) <= attackRadius)
        {

            if (canFire)
            {
                Vector3 tempVector = targetArray.transform.position - transform.position;
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector);
                canFire = false;
                //ChangeState(GladiatorState.attack);
                //anim.SetBool("attacking", true);
            }
            //if (gladiatorState == GladiatorState.walk || gladiatorState == GladiatorState.idle)
            //{
            //    if()
            //    //StartCoroutine(AttackCo());
            //}
        }
    }


    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        anim.SetBool("attacking", true);
        yield return new WaitForSeconds(0.5f);

        gladiatorState = GladiatorState.walk;
        anim.SetBool("attacking", false);
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
        //anim.SetBool("moving", true);
    }

    public void changeAnim(Vector2 direction)
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
    }

    public void ChangeState(GladiatorState newState)
    {
        if (gladiatorState != newState)
        {
            gladiatorState = newState;
        }
    }
}
