using UnityEngine;
using System;


// reikia patobulint kad vietoj nesisuktu, angle speed duoda greiti x asim
// reik ta sutaisyti

public class Physics : MonoBehaviour {

    public Transform player;
    public double SpeedIn;
    public double AngleIN;
    public double beta;
    public double force, aSpeed, aSpace, aRel, aFriction, RepRep;
    double accelX, accelY;
      public double forceX, forceY, forceZ;
    public double brakeX, brakeY;
    double fsping;


     double  baseR, tiresDisR, massCentR, 
        inertiaK, kof,
        g, mass, tiresR;
    public double frictionK;



    public float aAccel, angleSpeed;

     public Vector3 speeds ;

    public double fFrictionXF, fFrictionYF;
    public double rFrictionXF, rFrictionYF;
    double Ffriction ;



    // Use this for initialization
    void Start () {
        beta = 0;
        baseR = 4;
        tiresDisR = 1;
        massCentR = 2.8;
        frictionK = 2;
        inertiaK = 0.1;
        g = 10;
        mass = 1;
        tiresR = 1;
        speeds.x = 0;
        speeds.z = 0;
        kof = 0.2;
        Ffriction = mass * g * frictionK;
    }
	
	// Update is called once per frame
	void Update () {
        //if (FWD)
        //else if (RWD)
        //if (WR)
        //else if (AWD)

        forceY = 0;
        forceX = 0;
        fFrictionXF = 0; fFrictionYF=0;
        rFrictionXF = 0; rFrictionYF=0;
    ///////////////////////////////////////////////
    /* 
            if (Input.GetKey(KeyCode.A))
                beta = -30;
            else if (Input.GetKey(KeyCode.D))
                beta = 30;
            else
            {
               // beta = -(aSpace - aSpeed)*180/Math.PI;
                beta = 0;
            }


            if (Input.GetKey(KeyCode.W))
            {
                force = 20;
            }

            else if (Input.GetKey(KeyCode.S))
            {
                brakeX = -100;
                brakeY = -100;

               // if (Math.Cos(aSpeed - aSpace + beta) < 0) brakeY *= -1;
               // if (Math.Sin(aSpeed - aSpace + beta) > 0) brakeX *= -1;

                forceY += brakeY * Math.Cos(beta); // jegos vieno rato !
                //forceX += brakeX * Math.Sin(beta);

            }
            else
            {
                force = 0;
                brakeY = 0;
                brakeY = 0;
            }
            if (Input.GetKey(KeyCode.E))
            { angleSpeed = 0;
                speeds.x = 0;
                speeds.z = 0;
            }

            if (Input.GetKey(KeyCode.T))
                force = 1000;
                */
    /////////////////////////////////////////////////////////

    forceY += brakeY * Math.Cos(beta);



        aSpace = Math.PI*player.rotation.eulerAngles.y/180;

        if (speeds.x != 0 && speeds.z != 0)
        {
            aSpeed = Math.Acos(speeds.z / (Math.Sqrt((speeds.x-baseR*angleSpeed/180*Math.PI) * (speeds.x - baseR * angleSpeed / 180 * Math.PI) + speeds.z * speeds.z)));
        }

        else
        { aSpeed = 0; }

        beta *= Math.PI / 180;
        if (speeds.x<0)
        {
            aSpeed =  2*Math.PI-aSpeed;
        }

         aRel = (aSpace - aSpeed)+beta;




        forceY +=   force * Math.Cos(beta); // jegos vieno rato !
        forceX +=   force * Math.Sin(beta);




        


      //  forceY = force * Math.Cos(beta); // jegos vieno rato !
     //   forceX = force * Math.Sin(beta);

        




        
        if ( (Math.Cos(aSpeed-aSpace) > Math.Sqrt(2)/2 || Math.Cos(aSpeed - aSpace) < -Math.Sqrt(2)/2) && SpeedIn > 3)
        {
            fFrictionXF = mass * g * frictionK *Math.Sin(2 * aRel)/2;
            fFrictionYF = -mass * g * frictionK * Math.Sin(aRel) * Math.Sin(aRel);

            rFrictionXF = mass * g * frictionK * Math.Sin(2 * (aRel-beta)) / 2;
            rFrictionYF = -mass * g * frictionK * Math.Sin(aRel-beta) * Math.Sin(aRel-beta);

            if (Math.Cos(aSpeed - aSpace)<0)
            {
                rFrictionYF *= -1;
                rFrictionXF *= -1;

            }

            if (Math.Cos(aSpeed - aSpace+beta) < 0)
            {
                
                fFrictionYF *= -1;
                fFrictionXF *= -1;
            }


            // fFrictionYF = 0;
            // fFrictionXF = 0;
            // rFrictionXF = 0;
           // rFrictionYF = 0;

        }



        else if ((Math.Cos(aSpeed - aSpace) < Math.Sqrt(2)/2 && Math.Cos(aSpeed - aSpace) > -Math.Sqrt(2)/ 2) && SpeedIn > 3)
        {
             fFrictionYF = -mass * g * frictionK * Math.Sin(2 * aRel) / 2;
            fFrictionXF = -mass * g * frictionK * Math.Sin(aRel) * Math.Sin(aRel);

              rFrictionYF = -mass * g * frictionK * Math.Sin(2 * (aRel-beta)) / 2;
             rFrictionXF = -mass * g * frictionK * Math.Sin(aRel-beta) * Math.Sin(aRel-beta);

            if (Math.Sin(aSpeed - aSpace) < 0)
            {
                rFrictionXF *= -1;
            }
            else
            {
                rFrictionYF *= -1;
            }

            if (Math.Sin(aSpeed - aSpace + beta) < 0)
            {
                fFrictionXF *= -1;
            }
            else
            {
                fFrictionYF *= -1;
            }


            // fFrictionYF = 0;
            // fFrictionXF = 0;
            // rFrictionXF = 0;
            // rFrictionYF = 0;


        }


        



        forceX += fFrictionXF * (massCentR / baseR) + rFrictionXF * (1 - (massCentR / baseR));
        forceY += fFrictionYF * (massCentR / baseR) + rFrictionYF * (1 - (massCentR / baseR));



        aAccel = (float) (baseR*(fFrictionXF-rFrictionXF) - 0.3*massCentR*(fFrictionXF-rFrictionXF)) / (float)(inertiaK * mass * massCentR*massCentR);



        /////////////////////////////////////////////////////////////////////////
        ///Jei juda labai letai letinam iki nuliaus

        if (force < 1 && SpeedIn < 1)
        {
            forceX *= 0.1;
            forceY *= 0.1;
            angleSpeed *= 0.1f;
            speeds.x *= 0.1f;
            speeds.z *= 0.1f;
            aSpeed *= 0.1f;
            aAccel = 0;
        }
        if (force < 1 && SpeedIn < 1)
        {
            forceX = 0;
            forceY = 0;
            angleSpeed = 0;
            speeds.x = 0;
            speeds.z = 0;
            aAccel = 0;
            aSpeed = 0;
        }
        //////////////////////////////////////////////////////////////////////////





        angleSpeed += aAccel * Time.deltaTime;
        angleSpeed *= ((1-Time.deltaTime/3));

        if (angleSpeed > 0)
        {
           // angleSpeed -= (float)(Math.Abs(Math.Sin(aRel)) * (massCentR / baseR) *50 +0.1)*Time.deltaTime;
        }

        else
        {
           //angleSpeed += (float)(Math.Abs(Math.Sin(aRel)) * (massCentR / baseR) *50 + 0.1) * Time.deltaTime;
        }




        accelX = (Math.Sin(aSpace) * (forceY) + Math.Cos(aSpace) * (forceX))/mass;
        accelY = (Math.Cos(aSpace) * (forceY) - Math.Sin(aSpace) * (forceX))/mass;


        speeds.x += (float)accelX * Time.deltaTime;
        speeds.z += (float)accelY * Time.deltaTime;

        SpeedIn = Math.Sqrt(Math.Pow(speeds.x, 2) + Math.Pow(speeds.z, 2));

        player.position += speeds*Time.deltaTime;
         player.Rotate(Vector3.up, angleSpeed * Time.deltaTime);


        AngleIN = aRel*180 / Math.PI;

    }
}
