using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V1 : MonoBehaviour
{
    public GameObject pos;

    private void Update()
    {
        if(pos != null)
        {
            transform.position = pos.transform.position;
        }
    }

    public void GetPosToFollow(GameObject newPos)
    {
        pos = newPos;
    }
}
