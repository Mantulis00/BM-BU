using UnityEngine;



public class ControlManager : MonoBehaviour {


   public GameObject CP;



	// Use this for initialization
	void Start () {
        CP.AddComponent<Physics>();
        CP.GetComponent<Physics>().player = transform;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A))
            CP.GetComponent<Physics>().beta = -20;
        else if (Input.GetKey(KeyCode.D))
            CP.GetComponent<Physics>().beta = 20;
        else
        {
            // beta = -(aSpace - aSpeed)*180/Math.PI;
            CP.GetComponent<Physics>().beta = 0;
        }


        if (Input.GetKey(KeyCode.W))
        {
            CP.GetComponent<Physics>().force = 20;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            CP.GetComponent<Physics>().brakeX = -100;
            CP.GetComponent<Physics>().brakeY = -100;

            // if (Math.Cos(aSpeed - aSpace + beta) < 0) brakeY *= -1;
            // if (Math.Sin(aSpeed - aSpace + beta) > 0) brakeX *= -1;
      
           // CP.GetComponent<Physics>().forceY = CP.GetComponent<Physics>().brakeY * Math.Cos(CP.GetComponent<Physics>().beta); // jegos vieno rato !
            //forceX += brakeX * Math.Sin(beta);

        }
        else
        {
            CP.GetComponent<Physics>().force = 0;
            CP.GetComponent<Physics>().brakeY = 0;
            CP.GetComponent<Physics>().brakeX = 0;
        }
        if (Input.GetKey(KeyCode.E))
        {
            CP.GetComponent<Physics>().angleSpeed = 0;
            CP.GetComponent<Physics>().aSpeed = 0;
            CP.GetComponent<Physics>().SpeedIn = 0;
            CP.GetComponent<Physics>().speeds.x = 0;
            CP.GetComponent<Physics>().speeds.z = 0;
        }

        if (Input.GetKey(KeyCode.T))
            CP.GetComponent<Physics>().force = 1000;
    }
}
