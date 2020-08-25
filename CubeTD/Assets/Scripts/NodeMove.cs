using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMove : MonoBehaviour
{
    public float speed = 1;
    public float offset;

    private float x;

    public float a = 0;
    public float b = 0.3f;

    private void Start()
    {
        x += offset;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        x += Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, ((Mathf.Sin(x) + a) * b), transform.position.z);
        if (x >= Mathf.PI * 2)
            x = 0;
    }
}
