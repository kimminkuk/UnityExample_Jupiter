using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class AI_Orge : NewGladiator
{
    private Transform Ai_targets;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private Animator Orgeanim;
    private float pos1;

    [Header("Enemy List")]
    public BoolValue[] AliveList;
    private void Awake()
    {
        Debug.Log("AI_Orge Awake() Call\n");
        var objs = FindObjectsOfType<ClickCharacter>();

        if (objs.Length <= 2)
        {
            if (this.Alive_BoolValue.RuntimeValue)
            {
                Debug.Log("AI_Orge this.Alive_BoolValue.RuntimeValue Call\n");
                DontDestroyOnLoad(this.gameObject);
                if (Check_ASite_Scene_Gladiators.RuntimeValue != 0) // another Place
                {

                }
                else //Training Room
                {
                    Vector3 temp;

                    switch (RePosition.RuntimeValue)
                    {
                        case 0:
                            temp.x = 10f;
                            temp.y = -4f;
                            temp.z = 0;
                            this.transform.position = temp;
                            break;
                        case 1:
                            temp.x = 8f;
                            temp.y = 0f;
                            temp.z = 0;
                            this.transform.position = temp;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("AI_Orge Destroy Call\n");
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);

        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Orgeanim = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (Check_ASite_Scene_Gladiators.RuntimeValue != 0)
        {
            if (TeamSite_IntValue.RuntimeValue == A_Team)
            {
                if (GameObject.FindGameObjectWithTag("B_Team"))
                {
                    Ai_targets = GameObject.FindGameObjectWithTag("B_Team").GetComponent<Transform>();
                }
            }
            else if (TeamSite_IntValue.RuntimeValue == B_Team)
            {
                if (GameObject.FindGameObjectWithTag("A_Team"))
                {
                    Ai_targets = GameObject.FindGameObjectWithTag("A_Team").GetComponent<Transform>();
                }
            }

            if (Ai_targets == null)
            {
                for (int i = 0; i < AliveList.Length; i++)
                {
                    if (AliveList[i].RuntimeValue)
                    {
                        return;
                    }
                }
                Orgeanim.SetBool("Win", true);
                return;
            }
            this.CheckDistance(Ai_targets);

            if (path == null)
            {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWayPointDistance)
            {
                currentWaypoint++;
            }
        }
    }

    void UpdatePath()
    {

        if (Ai_targets == null) return;

        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, Ai_targets.position, OnPathComplete);
        }
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public override void CheckDistance(Transform targetArray)
    {
        base.CheckDistance(targetArray);
    }

    public override void TakeDamage_Ateam(int damage, int this_team)
    {
        base.TakeDamage_Ateam(damage, this_team);
    }

    public override void TakeDamage_Bteam(int damage, int this_team)
    {
        base.TakeDamage_Bteam(damage, this_team);
    }

    public override void DamagePopupOpen(int damage)
    {
        base.DamagePopupOpen(damage);
    }
}
