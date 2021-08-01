using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime; //1f
    public Vector3 Offset = new Vector3(0.5f, 1f, 0);
    //public Vector3 RandomizeInensity = new Vector3(0.5f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, DestroyTime);
        transform.localPosition += Offset;
    //    transform.localPosition += new Vector3(Random.Range(-RandomizeInensity.x, RandomizeInensity.x),
    //        Random.Range(-RandomizeInensity.y, RandomizeInensity.y),
    //        Random.Range(-RandomizeInensity.z, RandomizeInensity.z));
    }
}
