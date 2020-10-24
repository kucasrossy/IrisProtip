using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControll : MonoBehaviour
{
    [Header("Mundos")]
    public GameObject AcsaWorld;
    public GameObject ArielWorld;
    private bool teste = false;

    private void Update()
    {
        if (teste)
        {
            if (!AcsaWorld.GetComponent<World>().isActive && ArielWorld.GetComponent<World>().isActive)
            {
                AcsaWorld.SetActive(true);
                AcsaWorld.GetComponent<World>().isActive = true;
                ArielWorld.SetActive(false);
                ArielWorld.GetComponent<World>().isActive = false;
            }
            else if (!ArielWorld.GetComponent<World>().isActive && AcsaWorld.GetComponent<World>().isActive)
            {
                AcsaWorld.SetActive(false);
                AcsaWorld.GetComponent<World>().isActive = false;
                ArielWorld.SetActive(true);
                ArielWorld.GetComponent<World>().isActive = true;
            }

            teste = false;
        }
    }

    public void SetTime()
    {
        teste = true;
    }
}
