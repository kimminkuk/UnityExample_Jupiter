using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Gladiator
{
    //public Rigidbody2D myRigidbody;
    private Rigidbody2D HumanRigidbody;

    public Transform[] targets;
    public string[] targetsName;
    public float chaseRadius;
    public float attackRadius;

    //public Animator anim;
    private Animator HumanAnim;

    // Start is called before the first frame update
    void Start()
    {
        gladiatorState = GladiatorState.idle;
        HumanRigidbody = GetComponent<Rigidbody2D>();
        HumanAnim = GetComponent<Animator>();

        HumanAnim.SetFloat("moveX", 0);
        HumanAnim.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CheckDistance();

        for (int i = 0; i < targets.Length; i++)
        {
            CheckDistance(targets[i], targetsName[i]);
        }
    }

    public virtual void CheckDistance(Transform targetArray, string nameArray)
    {
        if (Vector3.Distance(targetArray.position, transform.position) <= chaseRadius
            && Vector3.Distance(targetArray.position, transform.position) > attackRadius)
        {
            //ChangeState(GladiatorState.walk);
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
            if(gladiatorState == GladiatorState.walk || gladiatorState == GladiatorState.idle)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        HumanAnim.SetFloat("MoveX", setVector.x);
        HumanAnim.SetFloat("MoveY", setVector.y);
        HumanAnim.SetBool("moving", true);
    }

    private IEnumerator AttackCo()
    {
        gladiatorState = GladiatorState.attack;
        HumanAnim.SetBool("attacking", true);
        yield return new WaitForSeconds(0.5f);

        gladiatorState = GladiatorState.walk;
        HumanAnim.SetBool("attacking", false);
    }
/*
    public override void changeAnim(Vector2 direction)
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
    }*/

    public void ChangeState(GladiatorState newState)
    {
        if (gladiatorState != newState)
        {
            gladiatorState = newState;
        }
    }
}
