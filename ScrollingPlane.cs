using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPlane : MonoBehaviour
{
    Vector3 pos;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.y -= Time.deltaTime * speed;
        transform.position = pos;

        if (pos.y < -24f)
            pos.y = 0.1f;
    }
}
