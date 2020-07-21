using UnityEngine;
using System;
//using  System.Collections;

public class forceind : MonoBehaviour
{

    public GameObject player;
    public Transform block;
    public float R;
    Vector3 pasukimas;
    public double kampas;

    // Use this for initialization
    void Start()
    {
        R = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Physics>().forceX != 0 && player.GetComponent<Physics>().forceY != 0)
            kampas = 
                Math.Acos(player.GetComponent<Physics>().forceY / 
                (Math.Sqrt(player.GetComponent<Physics>().forceX * player.GetComponent<Physics>().forceX + 
                player.GetComponent<Physics>().forceY * player.GetComponent<Physics>().forceY)));
        else
            kampas = 0;

        if (player.GetComponent<Physics>().forceX < 0)
            kampas = 2 * Math.PI - kampas ;

        pasukimas.x = R * (float)(Math.Sin(kampas+ player.GetComponent<Physics>().aSpace));
        pasukimas.z = R * (float)(Math.Cos(kampas + player.GetComponent<Physics>().aSpace));
        block.position = player.transform.position + pasukimas;
        block.rotation = player.transform.rotation;
        //camera.position = 
    }
}
