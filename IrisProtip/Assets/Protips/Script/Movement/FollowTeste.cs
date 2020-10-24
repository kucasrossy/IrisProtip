using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTeste : MonoBehaviour
{

    public float speed;
    public float StoppingDistance;
    public Transform target;
    public Transform groundCheck;
    private bool isGrounded;
    public float groundRadius;

    public LayerMask whatisGround;

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatisGround);

        if (isGrounded)
        {
            if (Vector2.Distance(transform.position, target.position) < StoppingDistance && Vector2.Distance(transform.position, target.position) > 1.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
