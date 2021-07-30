using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    SpriteRenderer sprite;
    Vector2 direction;
    public float moveSpeed;
    public float minSize;
    public float maxSize;
    public float sizeSpeed;
    public float colorSpeed;
    public Color[] colors;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);
        sprite.color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        transform.Translate(direction * moveSpeed);
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
