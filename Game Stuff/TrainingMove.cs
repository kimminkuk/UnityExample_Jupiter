using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingMove : Gladiator
{
    private Vector3 directionVector;
    public Collider2D bounds;
    private bool isMoving;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;

    // Start is called before the first frame update
    void Start()
    {
        
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        Debug.Log("TrainingMove Start()\n");
        waitTimeSeconds = Random.Range(minWaitTime, maxWaitTime);
        ChangeDirection();
    }

    // Update is called once per frame
    //public virtual void Update()
    public void Update()
    {
        if(isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds <= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
            }
            Move();
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if(waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                isMoving = true;
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            }
        }
    }

    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while(temp == directionVector && loops < 100)
        {
            Debug.Log("here");
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        //Vector3 temp = myTransform + directionVector * moveSpeed * Time.deltaTime * 3;
        Vector3 temp = transform.position + directionVector * moveSpeed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        Debug.Log("ChangeDirection()\n");
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                //Walking to the right
                directionVector = Vector2.right;
                break;
            case 1:
                //Waling Up
                directionVector = Vector2.up;
                break;
            case 2:
                //Walking left
                directionVector = Vector2.left;
                break;
            case 3:
                //Walking Down
                directionVector = Vector2.down;
                break;
            default:
                break;
        }
        changeAnim(directionVector);
        //UpdateAnimation();
    }
    //void UpdateAnimation()
    //{
    //    anim.SetFloat("MoveX", directionVector.x);
    //    anim.SetFloat("MoveY", directionVector.y);
    //}
}
