using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BackScene : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public Text PassText;
    private int GladiatorLevel;

    [Header("Gladiator Stats")]
    public IntValue maxHealth;
    public FloatValue InitmoveSpeed;
    public IntValue DamageIntValue;
    public FloatValue ProjectileSpeed;
    public FloatValue WeaponSpeed;
    public IntValue Level_IntValue;
    public IntValue UpgradeLevel_IntValue;

    private void Start()
    {
        Debug.Log("BackScene Up\n");
        GladiatorLevel = UpgradeLevel_IntValue.RuntimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LoadResult(SceneManager.GetActiveScene().buildIndex - 2));
        }
    }


    IEnumerator LoadResult(int levelIndex)
    {
        PassText.text = "+" + GladiatorLevel.ToString();
        //play animation
        transition.SetTrigger("Start_Result");
        //wait
        yield return new WaitForSeconds(transitionTime);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
