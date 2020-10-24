using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPlayerV2 : MonoBehaviour
{

    public GameObject Acsa;
    public GameObject Ariel;
    public Color colorShadow;
    public float gravityScale;
    public bool isAcsa;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isAcsa)
            {
                EnablePlayerTurn(Ariel);
                DisablePlayerTurn(Acsa);
                FindObjectOfType<WorldControll>().SetTime();
                isAcsa = false;
            }
            else
            {
                EnablePlayerTurn(Acsa);
                DisablePlayerTurn(Ariel);
                FindObjectOfType<WorldControll>().SetTime();
                isAcsa = true;
            }
        }
    }
    

    private void EnablePlayerTurn(GameObject player)
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        //player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void DisablePlayerTurn(GameObject player)
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //player.GetComponent<SpriteRenderer>().color = colorShadow;
    }
}
