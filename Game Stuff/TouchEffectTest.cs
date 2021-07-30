using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectTest : MonoBehaviour
{
    SpriteRenderer sprite;
    public float x_size;
    public float y_size;
    public float sizeSpeed;
    public float colorSpeed;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(x_size, y_size);
        sprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);
        Color color = sprite.color;
        color.a = Mathf.Lerp(sprite.color.a, 0, Time.deltaTime * colorSpeed);
        sprite.color = color;
        if(sprite.color.a <= 0.01f)
        {
            Destroy(this.gameObject);
        }
    }
}
