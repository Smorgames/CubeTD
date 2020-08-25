using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartToScale : MonoBehaviour
{
    public float speed;
    private float x = 0;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        x += Time.deltaTime * speed;
        transform.localScale = new Vector3(((Mathf.Sin(x) + 1) * 0.3f) + 1.2f, transform.localScale.y, ((Mathf.Sin(x) + 1) * 0.3f) + 1.2f);
        if (x >= Mathf.PI * 2)
            x = 0;
    }
}
