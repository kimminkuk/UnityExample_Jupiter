using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgLogText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destoryTime;
    TextMeshPro text;
    Color alpha;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destoryTime);
    }

    // Update is called once per frame
    void Update()
    {
        alpha.r = 130;
        alpha.g = 130;
        alpha.b = 130;
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
