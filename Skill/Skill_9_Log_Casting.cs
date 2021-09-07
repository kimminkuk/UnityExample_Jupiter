using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_9_Log_Casting : MonoBehaviour
{
    private Animator Ani;
    // Start is called before the first frame update

    private float EndTime;
    private bool OnOff;
    private float waitTime;
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
        yield return new WaitForSeconds(waitTime);

        Ani.SetBool("Casting", false);
        Destroy(this.gameObject);
        Debug.Log("Casting Destroy()\n");
    }
}
