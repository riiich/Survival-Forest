using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float amplitude;
    public float frequency;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        this.initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, Mathf.Sin(Time.time * amplitude) * frequency, 0);
    }
}
