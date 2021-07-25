using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBreak : MonoBehaviour
{
    private Animator rockbreak_anim;

    private void Start()
    {
        rockbreak_anim = GetComponent<Animator>();
    }

    public void RockBreakgif()
    {
        rockbreak_anim.SetBool("Break", true);
        StartCoroutine(RockBreakCo());
    }

    private IEnumerator RockBreakCo()
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
    }
}
