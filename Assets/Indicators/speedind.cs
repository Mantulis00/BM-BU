using UnityEngine;
using System;
//using  System.Collections;

public class speedind : MonoBehaviour
{
    public GameObject CP;
   // public Transform player;
    public Transform block;
    public float R;
    Vector3 pasukimas;

    // Use this for initialization
    void Start()
    {
        R = 7;
    }

    // Update is called once per frame
    void Update()
    {



        pasukimas.x = R * (float)(Math.Sin(CP.GetComponent<Physics>().aSpeed));
        pasukimas.z = R * (float)(Math.Cos(CP.GetComponent<Physics>().aSpeed));
        block.position = CP.transform.position + pasukimas;
        block.rotation = CP.transform.rotation;
        //camera.position = 
    }
}
