using UnityEngine;
using System;

public class Indicator : MonoBehaviour
{

    public Transform player;
    public Transform camera;
    public Physics ip;
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
        pasukimas.x = -R * (float)(Math.Sin(Math.PI * player.eulerAngles.y / 180));
        pasukimas.z = -R * (float)(Math.Cos(Math.PI * player.eulerAngles.y / 180));
        camera.position = player.position + pasukimas;
        camera.rotation = player.rotation;
        //camera.position = 
    }
}
