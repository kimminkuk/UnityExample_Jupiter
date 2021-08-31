using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBreak : MonoBehaviour
{
    private Animator Stonebreak_anim;

    // Start is called before the first frame update
    void Start()
    {
        Stonebreak_anim = GetComponent<Animator>();
    }

    public void RockBreakgif()
    {
        Stonebreak_anim.SetBool("Break", true);
        StartCoroutine(RockBreakCo());
    }

    private IEnumerator RockBreakCo()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
    }
}
