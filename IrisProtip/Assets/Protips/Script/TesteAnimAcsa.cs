using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteAnimAcsa : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("hVel", horizontal);

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("atck");
        }
    }
}
