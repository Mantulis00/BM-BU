using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TO DO
/// - Reik listo botu
/// - Reik kad pries posuki pastabdytu botas
/// - Reik juostu
/// </summary>
/// 


public class AImanager : MonoBehaviour {

    public GameObject BotModel;
   public byte  rsection = 0;
    int section = 0;
    byte AIcount  ;
    GameObject AI;
    private List<GameObject> SpawnedAI;
    public float angle, dAngle;
    TileManager.Kintamieji varList;

    public float AImaxSpeed = 20;

    public Vector3 PosToReach;
    public Vector3 MyPos;

    byte listsSizes;
    float sinusas, kosinusas;
    byte tileWith;
    bool rightLeft, upDown;
    public float engels, myEngels;
    public  float myrot;
    public float deltax, deltay, distance;




    public float debugS, debugC;
    /// <summary>
    /// sukuriam gameobject objekta su nustatytom koordinatem ir pasukimu
    /// atspawninam objekta taske varList[0].startPos
    /// tam objektui priskiriam physics rigidbody ir boxcolider scriptus
    /// 
    /// </summary>

     // reikalingi ( startPos ir endPos)
   

    float pi = (float)3.14159265359;
    /*
    public struct Kintamieji // VarList struct ( nusako visus sekcijos parametrus)
    {
        public Vector3 startPos;
        public Vector3 endPos;
        public byte size;
        public float angle;
        public byte width, eismoSK;
        public int juostuSK;

    };
    */

    /*
     go = Instantiate(tilePrefabS[0]) as GameObject;
    go.transform.SetParent(transform);
    scaleVec.Set(sankryzosParametrai * 2, 1f, sankryzosParametrai * 2);
    go.transform.localScale = scaleVec;
    go.transform.rotation = Quaternion.Euler(0, varList[listsSize - 1].angle / pi * 180, 0);
    go.transform.position = offsetVec;
    objektai.Add(go);
     */


    GameObject TileModel;


  IEnumerator Delay ()
    {
        yield return new WaitForSeconds(1);
        AI.transform.position = TileModel.GetComponent<TileManager>().varList[0].startPos;
        AI.transform.rotation = Quaternion.Euler(0, TileModel.GetComponent<TileManager>().varList[0].angle/pi*180, 0);
    }

    // Use this for initialization
    void Start () {

       
        StartCoroutine(Delay());
       // listsSizes = TileModel.GetComponent<TileManager>().listsSize;

        //section = 0;



        AI = Instantiate(BotModel) as GameObject;
        AI.transform.SetParent(transform);
        AI.transform.rotation = Quaternion.Euler(0, 0, 0); // AI.transform.rotation = Quaternion.Euler(0, varList[0].angle / pi * 180, 0);
       // AI.transform.position = TileModel.GetComponent<TileManager>().varList[0].startPos;
        AI.AddComponent<Physics>();
        AI.GetComponent<Physics>().player = transform;


        SpawnedAI = new List<GameObject>();
        SpawnedAI.Add(AI);


        //  AI.AddComponent<TileManager>();
        // TileManager.Kintamieji[] varList = new TileManager.Kintamieji[3];
        //StartCoroutine(opa(10));


        // varList = new TileManager.Kintamieji[GetComponent<TileManager>().listsSize];
        //varList = GetComponent<TileManager>().varList;

        TileModel = GameObject.Find("TileManager");


        
    }



    // Update is called once per frame
    void Update () {


        tileWith = TileModel.GetComponent<TileManager>().tileWith;

        if ((byte)rsection - (byte)TileModel.GetComponent<TileManager>().score + TileModel.GetComponent<TileManager>().listsSize >= 0 &&
           (byte)rsection - (byte)TileModel.GetComponent<TileManager>().score + TileModel.GetComponent<TileManager>().listsSize < TileModel.GetComponent<TileManager>().listsSize)
        {
            section = (byte)rsection - (byte)TileModel.GetComponent<TileManager>().score + TileModel.GetComponent<TileManager>().listsSize;
        }


        angle = TileModel.GetComponent<TileManager>().varList[section].angle;
       // dAngle = TileModel.GetComponent<TileManager>().varList[section].angle-
       // AI.transform.rotation =  Quaternion.Euler(0, angle / pi * 180, 0);


        if (TileModel.GetComponent<TileManager>().varList[section].startPos[2] <= TileModel.GetComponent<TileManager>().varList[section].endPos[2]) { upDown = true; }
        else { upDown = false; }

        if (TileModel.GetComponent<TileManager>().varList[section].startPos[0] < TileModel.GetComponent<TileManager>().varList[section].endPos[0]) { rightLeft = true; }
        else { rightLeft = false; }


        sinusas = (float)Math.Sin(TileModel.GetComponent<TileManager>().varList[section].angle);
        kosinusas = (float)Math.Cos(TileModel.GetComponent<TileManager>().varList[section].angle);

        // Randam santiki tarp atstumo, greicio ir posukio kampo



        if (((upDown && (AI.transform.position[2] >= TileModel.GetComponent<TileManager>().varList[section].endPos[2] + sinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2 ||
          AI.transform.position[2] >= TileModel.GetComponent<TileManager>().varList[section].endPos[2] - sinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2)) ||

          (upDown == false && (AI.transform.position[2] <= TileModel.GetComponent<TileManager>().varList[section].endPos[2] + sinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2 || 
          AI.transform.position[2] <= TileModel.GetComponent<TileManager>().varList[section].endPos[2] - sinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2))) 

          &&

          ((rightLeft && (AI.transform.position[0] >= TileModel.GetComponent<TileManager>().varList[section].endPos[0] + kosinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2 || 
          AI.transform.position[0] >= TileModel.GetComponent<TileManager>().varList[section].endPos[0] - kosinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2)) ||

          (rightLeft == false && (AI.transform.position[0] <= TileModel.GetComponent<TileManager>().varList[section].endPos[0] + kosinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2 || 
          AI.transform.position[0] <= TileModel.GetComponent<TileManager>().varList[section].endPos[0] - kosinusas * TileModel.GetComponent<TileManager>().varList[section].width / 2))))
        {
            
                rsection++;
           
        }

        MyPos = AI.transform.position;
        PosToReach = TileModel.GetComponent<TileManager>().varList[section].endPos;

        //  GetComponent<TileManager>().tileLenght = 0;
        // if (varList[section].endPos == AI.transform.position)
        // {
        //  section++;
        //  AI.transform.rotation = Quaternion.Euler(0, varList[section].angle / pi * 180, 0);
        //   }


        //  AI.GetComponent<Physics>().speeds.x = 1;

        deltax = TileModel.GetComponent<TileManager>().varList[section].endPos[0] - AI.transform.position[0];
        deltay = TileModel.GetComponent<TileManager>().varList[section].endPos[2] - AI.transform.position[2];
        distance = (float)Math.Sqrt((TileModel.GetComponent<TileManager>().varList[section].endPos[0] - AI.transform.position[0]) * (TileModel.GetComponent<TileManager>().varList[section].endPos[0] - AI.transform.position[0]) +
              (TileModel.GetComponent<TileManager>().varList[section].endPos[2] - AI.transform.position[2]) * (TileModel.GetComponent<TileManager>().varList[section].endPos[2] - AI.transform.position[2]));

        engels = (float)Math.Asin(deltax / distance); //reikia siekt kad engels == myengels

        ///steering
        ///
        // Kampas kuri sudaro playerio ziuresena ir tikslas










        // engels = (TileModel.GetComponent<TileManager>().varList[section].endPos[0] - AI.transform.position[0]);
        // if (engels < 0) { engels = engels * (-1) + pi / 2; }

        myEngels = (float)AI.GetComponent<Physics>().aSpace;
        myrot = (float)Math.Sin(engels - myEngels);

        //debugS = (float)Math.Sin(engels);
        // debugC = (float)Math.Cos(engels);

        if (deltax < 0 && deltay > 0) engels = 2 * pi + engels;
        else if (deltay < 0) engels = pi - engels;



            if (Math.Sin(myEngels-engels)<0) // im at myEngels need to achieve engels
            {
                  AI.GetComponent<Physics>().beta = 50* Math.Abs(Math.Sin(myEngels - engels));// 30 * Math.Abs(myrot)+10;
            if (AI.GetComponent<Physics>().beta >= 30)
            {
                AI.GetComponent<Physics>().beta = 30;
            }
            //  AI.GetComponent<Physics>().speeds.x = 0;
            //  AI.GetComponent<Physics>().speeds.z= 0;
        }

            else 
            {
            AI.GetComponent<Physics>().beta = -(50 * Math.Abs(Math.Sin(myEngels - engels)));//* Math.Abs(myrot)+-0;
            if (AI.GetComponent<Physics>().beta <=-30)
            {
                AI.GetComponent<Physics>().beta = -30;
            }
                //  AI.GetComponent<Physics>().speeds.x = 0;
                // AI.GetComponent<Physics>().speeds.z = 0;
            }

        // AI.GetComponent<Physics>().beta = -40;

        // if (AI.GetComponent<Physics>().SpeedIn < 15)

        if (Input.GetKey(KeyCode.H))
        {
            AI.GetComponent<Physics>().angleSpeed = 0;
            AI.GetComponent<Physics>().aSpeed = 0;
            AI.GetComponent<Physics>().SpeedIn = 0;
            AI.GetComponent<Physics>().speeds.x = 0;
            AI.GetComponent<Physics>().speeds.z = 0;
        }

       

            dAngle = Math.Abs(angle - myEngels);

        if (distance/(dAngle*2) <  AI.GetComponent<Physics>().SpeedIn && AI.GetComponent<Physics>().SpeedIn > AImaxSpeed/4 && dAngle>0.01)
        {
            AI.GetComponent<Physics>().force = -20;
        }

       else if (AI.GetComponent<Physics>().SpeedIn < AImaxSpeed) // if (Input.GetKey(KeyCode.I))
            AI.GetComponent<Physics>().force = 20;
        else
        {
            AI.GetComponent<Physics>().force = 0;
        }
      //  AI.GetComponent<Physics>().speeds.x += sinusas * Time.deltaTime;
       // AI.GetComponent<Physics>().speeds.z += kosinusas * Time.deltaTime;
       // AI.GetComponent<Physics>().SpeedIn = 5;
    }
}
