using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPlayer : MonoBehaviour
{
	public float timeStart = 60;
    private float culdowBtn;
    public float culdowBtnSet = 5f;
	public Text textBox;
    public GameObject Acsa;
    public GameObject Ariel;
    [SerializeField]
    private bool isAcsaFirst;
    private GameObject isTurnThis;

    private void Start()
    {
		textBox.text = timeStart.ToString();
        culdowBtn = culdowBtnSet;
        if (isAcsaFirst)
        {
            isTurnThis = Acsa;
        }
        else
        {
            isTurnThis = Ariel;
        }
    }

    private void Update()
    {
        SetTime();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            TurnOut();
            culdowBtn = -1;
        }

        if(Input.GetKeyDown(KeyCode.X) && timeStart > 0 && culdowBtn >= culdowBtnSet)
        {
            SwitchPos();
        }

        if(culdowBtn < culdowBtnSet)
        {
            culdowBtn += Time.deltaTime;
        }
    }

    private void SetTime()
    {
        timeStart -= Time.deltaTime;
        textBox.text = Mathf.Round(timeStart).ToString();
        if (timeStart <= 0)
        {
            TurnOut();
        }
    }

    private void TurnOut()
    {
        //Trocando o controle do player
        if (isAcsaFirst)
        {
            Acsa.GetComponent<PlayerController>().enabled = false;
            Ariel.GetComponent<PlayerController>().enabled = true;
            FindObjectOfType<V1>().GetPosToFollow(Ariel.gameObject);
            isTurnThis = Ariel;
            isAcsaFirst = false;
            //Trocando os mundos e crianto um fantasma da Acsa
            FindObjectOfType<WorldControll>().SetTime();
        }
        else
        {
            Ariel.GetComponent<PlayerController>().enabled = false;
            Acsa.GetComponent<PlayerController>().enabled = true;
            FindObjectOfType<V1>().GetPosToFollow(Acsa.gameObject);
            isTurnThis = Acsa;
            isAcsaFirst = true;
            //Trocando os mundos e criando um fantasma do Ariel
            FindObjectOfType<WorldControll>().SetTime();
        }

        timeStart = 10f;
    }

    private void SwitchPos()
    {
        Vector3 pos1 = Acsa.transform.position;
        Vector3 pos2 = Ariel.transform.position;


        Ariel.transform.position = pos1;
        Acsa.transform.position = pos2;
    }

    public GameObject GetTurnToPlayer()
    {
        return isTurnThis;
    }
}

