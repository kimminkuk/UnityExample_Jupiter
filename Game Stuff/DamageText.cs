using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destoryTime;
    TextMeshPro text;
    Color alpha;
    public int damage;
    public string miss_text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        //text.text = damage.ToString();
        //alpha = text.color;
        Invoke("DestroyObject", destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    public void SelectTextType(int ThisType, int Thisdamage, string ThisText )
    {
        text = GetComponent<TextMeshPro>();
        switch (ThisType)
        {
            case 0:
                text.text = Thisdamage.ToString();
                alpha = text.color;
                break;
            case 1:
                //text.text = ThisText.ToString();
                //text = GetComponent<TextMeshPro>();
                text.text = ThisText;
                alpha = text.color;
                break;
            default:
                break;
        }
        return;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
