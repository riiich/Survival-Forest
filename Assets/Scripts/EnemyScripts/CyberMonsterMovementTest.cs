using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMonsterMovementTest : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
    }

    void enemyMove()
    {
        transform.position += Vector3.back * Time.deltaTime * speed;
    }
}
