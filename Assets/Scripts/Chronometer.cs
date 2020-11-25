using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    public float cpt = 0f;
    public float minCpt = 10f;

    void Update()
    {
        cpt += Time.deltaTime;

        if(cpt >= minCpt)
        {
            Debug.Log("pouet");
        }
    }
}
