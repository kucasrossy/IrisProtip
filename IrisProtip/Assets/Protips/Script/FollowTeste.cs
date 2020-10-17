using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTeste : MonoBehaviour
{

    public float speed;
    public float StoppingDistance;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //float distance = transform.position.x - target.position.x;
        if(Vector2.Distance(transform.position, target.position) < StoppingDistance && Vector2.Distance(transform.position, target.position) > 1.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
