using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_9_Log_Casting : MonoBehaviour
{
    private Animator Ani;
    // Start is called before the first frame update

    private float EndTime;
    private bool OnOff;
    private bool OnOff2 = true;
    private float waitTime;
    public GameObject projectile_townt;

    private Vector3 pos;
    private int runtimeValue;
    private float v1;
    int v2;
    void Start()
    {
        Ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnOff)
        {
            StartCoroutine(E_CastingSkill(waitTime));
        }
    }

    public void Casting(float time)
    {
        Debug.Log("Casting Call()\n");
        OnOff = true;
        waitTime = time;
        EndTime = waitTime * 2;
    }

    public IEnumerator E_CastingSkill(float waitTime)
    {
        Ani.SetBool("Casting", true);
        yield return new WaitForSeconds(0.35f);

        Ani.SetBool("Casting", false);
        
        if (OnOff2)
        {
            OnOff2 = false;
            Vector3 temp = transform.position;
            temp.y -= 1f;
            GameObject townt = Instantiate(projectile_townt, temp, Quaternion.identity);
            townt.GetComponent<Townt_Projectile>().InitSet(pos, runtimeValue, v1, v2);
        }
        Destroy(this.gameObject);
        Debug.Log("Casting Destroy()\n");
    }

    internal void InitSet(Vector3 position, int runtimeValue, float v1, int v2)
    {
        this.pos = position;
        this.runtimeValue = runtimeValue;
        this.v1 = v1;
        this.v2 = v2;

        Debug.Log("Casting InitSet Call()\n");
        OnOff = true;
        waitTime = 1f;
        EndTime = waitTime * 2;
    }
}
