using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
/// skirtingu plicu keliai
/// kiek juostu
///dvieju ar vienos juostos
///paskaiciuoti kiekvienos juostos baigimo/ pradzios kord / kiekvienos sankryzos judejimo kordinates (AI tikslam)
///// blenderio - unity importai skiriasi per 2 kartus
///sukurti lista kuri kampa ime // ta lista susiet su sankryzom
/// </summary>









public class TileManager : MonoBehaviour
{


    public GameObject[] tilePrefab; // x asies prefabai
    public GameObject[] tilePrefabS; // x asies  sankryzos
    public GameObject ind; // x asies  sankryzos

    public Transform playerTransform; // playerio informacija

    private bool upDown, rightLeft;

    public byte listsSize = 3; // kiek ruozu vienu metu generuosime

    float offset; // kiek nuo pradzios nukelti ruozo dali
    Vector3 offsetVec; // nuo kur pradesim ruoza
    Vector3 scaleVec;
    int Nuo = 6, Iki = 12; // kokio ilgio ruozas
    public byte tileLenght = 12, sizeNum; // vieno ruozo dalies ilgis
    public byte tileWith = 48;
    private int sankryzosParametrai = 48 / 2;
    float pi = (float)Math.PI;
    float angleABS = 0;
    byte randKey;
    public int score = 0;


    private float sinusas, kosinusas;
    private float generatedAngle;


    // 4 Fors
    private byte x;


    public struct Kintamieji // VarList struct ( nusako visus sekcijos parametrus)
    {
        public Vector3 startPos;
        public Vector3 endPos;
        public byte size;
        public float angle;
        public byte width, eismoSK;
        public int juostuSK;

    };

    public struct JuostuKintamieji // reikia perdaryt i intrukciju kintamuosius arba sukurt nauja // skirtas nusakyti kiekvienos juostos parametrus
    {
        public Vector3[] startPos;
        public Vector3[] endPos;
        public bool[] priesinisEismas;
        public byte size;
        public float angle;
        public float angleSankryza; // nustatysim kazkoki kampa kurio masina tures laikytis

    };



    private List<GameObject> objektai;
    private List<GameObject> indi;

    public Kintamieji[] varList;
    public JuostuKintamieji[] juostosList;

    GameObject go;
    float[] angleList;
    byte[] widthList;





    //////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    private void Start()
    {
        angleList = new float[] {
            -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36, -pi / 36,
             -pi / 18,  -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18, -pi / 18,
            -pi / 9, -pi / 9, -pi / 9, -pi / 9, -pi / 9, -pi / 9, -pi / 9, -pi / 9, -pi / 9,
           -pi / 6, -pi / 6, -pi / 6, -pi / 6, -pi / 6, -pi / 6,
            -pi / 3, -pi / 3, -pi / 3,
            -pi / 2,
            0, 0, 0,
            pi / 2,
            pi / 3,  pi / 3,  pi / 3,
            pi / 6,  pi / 6,  pi / 6,  pi / 6,  pi / 6,
            pi / 9, pi / 9, pi / 9, pi / 9, pi / 9, pi / 9, pi / 9, pi / 9, pi / 9, pi / 9,
           pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18, pi / 18,
            pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36, pi / 36,
        }; // 9 element i // 12 viena juosta // kelkrasciai 4 arba 2
        widthList = new byte[] { 14, 16, 26, 28, 38, 40, 52, 56, 80, 104 }; // 1 // 1 // 2 // 2 // 3 // 3 // 4 //  6 // 8
        // widthList = new byte[] { 26, 26, 26, 26, 26, 26, 26, 26, 26, 26 }; // 1 // 1 // 2 // 2 // 3 // 3 // 4 //  6 // 8

        objektai = new List<GameObject>();
        indi = new List<GameObject>();
        varList = new Kintamieji[listsSize];

        


        juostosList = new JuostuKintamieji[listsSize];
        for (x = 0; x < listsSize; x++)
        {
            juostosList[x].startPos = new Vector3[8]; // iki 8 eismo juostu del to 8
            juostosList[x].endPos = new Vector3[8];
            juostosList[x].priesinisEismas = new bool[8];
        }

        //////////////////////////////////////// AI

       // AIlist = new AIkintamieji[ AILimit];

        ////////////////////////////////////////

        sizeNum = 6;

        generateList();
        spawnList();
        Push();
        generateList();
        spawnList();


      //  varList[1].endPos[0] = 0;
      //  varList[1].endPos[2] = 0;
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////




    void Update()// kai pasiekia pries paskutinio listo pabaiga, trinam paskutini ir generatinam nauja
    {
        if (varList[1].startPos[2] <= varList[1].endPos[2]) { upDown = true; }
        else { upDown = false; }

        if (varList[1].startPos[0] < varList[1].endPos[0]) { rightLeft = true; }
        else { rightLeft = false; }

        sinusas = (float)Math.Sin(varList[1].angle);
        kosinusas = (float)Math.Cos(varList[1].angle);


        if (((upDown && (playerTransform.position[2] >= varList[1].endPos[2] + sinusas * tileWith / 2 || playerTransform.position[2] >= varList[1].endPos[2] - sinusas * tileWith / 2)) ||
            (upDown == false && (playerTransform.position[2] <= varList[1].endPos[2] + sinusas * tileWith / 2 || playerTransform.position[2] <= varList[1].endPos[2] - sinusas * tileWith / 2))) &&

            ((rightLeft && (playerTransform.position[0] >= varList[1].endPos[0] + kosinusas * tileWith / 2 || playerTransform.position[0] >= varList[1].endPos[0] - kosinusas * tileWith / 2)) ||
            (rightLeft == false && (playerTransform.position[0] <= varList[1].endPos[0] + kosinusas * tileWith / 2 || playerTransform.position[0] <= varList[1].endPos[0] - kosinusas * tileWith / 2))))
        {

            deleteList();
            Push();
            generateList();
            spawnList();

        }
    }



    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void generateList() // 
    {
        score++;
        //// Kelio plocio parinkimas, juostu skaiciau parinkimas, ir i kuria puse kiek juostu parinkimas
        randKey = (byte)UnityEngine.Random.Range(0, 2);

        if (sizeNum == 0)
        {
            sizeNum += randKey;
        }
        else if (sizeNum == 9)
        {
            sizeNum -= randKey;
        }
        else
        {
            randKey = (byte)UnityEngine.Random.Range(0, 3);

            if (UnityEngine.Random.Range(0, 3) == 2) { sizeNum++; }
            else if (UnityEngine.Random.Range(0, 3) == 1) { sizeNum--; }
        }

        varList[listsSize - 1].width = widthList[sizeNum];
        varList[listsSize - 1].juostuSK = (varList[listsSize - 1].width / 12);
        varList[listsSize - 1].eismoSK = (byte)UnityEngine.Random.Range(varList[listsSize - 1].juostuSK / 2, varList[listsSize - 1].juostuSK + 1);

        ////

        varList[listsSize - 1].startPos = varList[listsSize - 2].endPos;
        varList[listsSize - 1].startPos[0] += tileLenght * (float)Math.Sin(varList[listsSize - 2].angle);
        varList[listsSize - 1].startPos[2] += tileLenght * (float)Math.Cos(varList[listsSize - 2].angle);
        tileWith = varList[listsSize - 1].width;


        generatedAngle = angleList[UnityEngine.Random.Range(0, angleList.Length-1)];
        varList[listsSize - 1].angle = varList[listsSize - 2].angle + generatedAngle;

        varList[listsSize - 1].size = (byte)UnityEngine.Random.Range(Nuo, Iki + 1);

        sankryzosParametrai = varList[listsSize - 1].width / 2;

        /////////////////////////////////////////////////////////////////////////////////////////////// instrukciju sk = listSize * 2
     
        ///////////////////////////////////////////////////////////////////////////////////////////////
       /* for (x = 0; x < varList[listsSize - 1].juostuSK; x++)
        {
            if ((varList[listsSize - 1].width == 14 || varList[listsSize - 1].width == 26 || varList[listsSize - 1].width == 38) && x == 0)
            {
                juostosList[listsSize - 1].startPos[x].x = varList[listsSize - 1].startPos[0] + (sankryzosParametrai) * (float)Math.Sin(varList[listsSize - 2].angle);
                juostosList[listsSize - 1].startPos[x].z = varList[listsSize - 1].startPos[2] + (sankryzosParametrai) * (float)Math.Cos(varList[listsSize - 2].angle);


                juostosList[listsSize - 1].startPos[x].x += (float)(sankryzosParametrai - 7) * (float)(Math.Sin(varList[listsSize - 1].angle) - Math.Cos(varList[listsSize - 1].angle));
                juostosList[listsSize - 1].startPos[x].z += (float)(sankryzosParametrai - 7) * (float)(Math.Cos(varList[listsSize - 1].angle) + Math.Sin(varList[listsSize - 1].angle));

            }
            else if (x == 0)
            {
                juostosList[listsSize - 1].startPos[x].x = varList[listsSize - 1].startPos[0] + (sankryzosParametrai) * (float)Math.Sin(varList[listsSize - 2].angle);
                juostosList[listsSize - 1].startPos[x].z = varList[listsSize - 1].startPos[2] + (sankryzosParametrai) * (float)Math.Cos(varList[listsSize - 2].angle);

                juostosList[listsSize - 1].startPos[x].x += (float)(sankryzosParametrai - 8) * (float)(Math.Sin(varList[listsSize - 1].angle) - Math.Cos(varList[listsSize - 1].angle));
                juostosList[listsSize - 1].startPos[x].z += (float)(sankryzosParametrai - 8) * (float)(Math.Cos(varList[listsSize - 1].angle) + Math.Sin(varList[listsSize - 1].angle));
            }
            else
            {
                juostosList[listsSize - 1].startPos[x].x = juostosList[listsSize - 1].startPos[x - 1].x + (12) * (float)Math.Cos(varList[listsSize - 1].angle);
                juostosList[listsSize - 1].startPos[x].z = juostosList[listsSize - 1].startPos[x - 1].z - (12) * (float)Math.Sin(varList[listsSize - 1].angle);
            }

            juostosList[listsSize - 1].endPos[x].x = juostosList[listsSize - 1].startPos[x].x + 12 * (varList[listsSize - 1].size + 0.5f) * (float)Math.Sin(varList[listsSize - 1].angle);
            juostosList[listsSize - 1].endPos[x].z = juostosList[listsSize - 1].startPos[x].z + 12 * (varList[listsSize - 1].size + 0.5f) * (float)Math.Cos(varList[listsSize - 1].angle);


            if (x < varList[listsSize - 1].eismoSK) { juostosList[listsSize - 1].priesinisEismas[x] = false; }
            else { juostosList[listsSize - 1].priesinisEismas[x] = true; }


            go = Instantiate(tilePrefab[1]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = juostosList[listsSize - 1].startPos[x];

            go = Instantiate(tilePrefab[1]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = juostosList[listsSize - 1].endPos[x];


        }*/
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    private void spawnList() // sudeliojam visa lista ir atspawninam
    {
        offsetVec = varList[listsSize - 1].startPos;



        offsetVec[0] += (sankryzosParametrai) * (float)Math.Sin(varList[listsSize - 2].angle);
        offsetVec[2] += (sankryzosParametrai) * (float)Math.Cos(varList[listsSize - 2].angle);



        go = Instantiate(tilePrefabS[0]) as GameObject;
        go.transform.SetParent(transform);
        scaleVec.Set(sankryzosParametrai * 2, 1f, sankryzosParametrai * 2);
        go.transform.localScale = scaleVec;
        go.transform.rotation = Quaternion.Euler(0, varList[listsSize - 1].angle / pi * 180, 0);
        go.transform.position = offsetVec;
        objektai.Add(go);




        offsetVec[2] += (tileLenght / 2 + sankryzosParametrai) * (float)Math.Cos(varList[listsSize - 1].angle);
        offsetVec[0] += (tileLenght / 2 + sankryzosParametrai) * (float)Math.Sin(varList[listsSize - 1].angle);



        for (x = 0; x < varList[listsSize - 1].size; x++)
        {

            go = Instantiate(tilePrefab[0]) as GameObject;


            go.transform.SetParent(transform);
            go.transform.rotation = Quaternion.Euler(0, varList[listsSize - 1].angle / pi * 180, 0);
            scaleVec.Set(varList[listsSize - 1].width, 1f, 12f);
            go.transform.localScale = scaleVec;

            go.transform.position = offsetVec;


            offsetVec[0] += tileLenght * (float)Math.Sin(varList[listsSize - 1].angle);
            offsetVec[2] += tileLenght * (float)Math.Cos(varList[listsSize - 1].angle);

            objektai.Add(go);
        }

        offsetVec[0] -= 3 * tileLenght / 2 * (float)Math.Sin(varList[listsSize - 1].angle);
        offsetVec[2] -= 3 * tileLenght / 2 * (float)Math.Cos(varList[listsSize - 1].angle);
        varList[listsSize - 1].endPos = offsetVec;


        varList[listsSize - 1].size++;
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    private void Push() // sutraukia lista vel i gala
    {
        for (x = 0; x < listsSize - 1; x++)
        {
            varList[x] = varList[x + 1];
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void deleteList() // sunaikina apatini lista
    {
       // score--;

        for (x = 0; x < varList[0].size; x++)
        {
            Destroy(objektai[0]);
            objektai.RemoveAt(0);
        }

    }






    //// AImanger bygem cgec
    ///
    ////////////////////////////////////////////// AI
    /*

    public struct AIkintamieji
    {
        public Vector3 POS; // pozicija parenkama pagal instrukciju lista
        public bool priesinisEismas; // nusprendziamas pagal eismo juosta ( istrukcijos Numeri)
        public float speed; // greitis sugeneruojamas random (intervale priklausomai nuo juostu sk kurios gaunamos is instrukcijos num)
        public float angle; // sutampa pagal instrukcijos Num
        public byte Instruct; // parenkamas pagal galima dydi tuo metu
    };

    byte AILimit = 5, AImaxNum = 20, AInum=0, AIrandROW;
    int AIsize;
    byte y = 0;
    AIkintamieji AIlist; // juosta ir kelintas juostoje
    GameObject AI;

    /// <summary>
    /// turime lista masinu (AI)
    /// kiekvienas listo narys turi - Pozicija, eismo krypti, greiti, tila (paterna kuriuo seka), savo eile juostoje / | | | \ -> / | | \
    /// narys yra istrinamas tada kai virsyja 
    /// </summary>


    // masina yra liste kuriam pagal atstuma susortinta vieta eilej
    // eile
    // eile ir kelinta masina eileje ir kuriam tile sekcijai priskirta




    void AImanagerUpdate()
    {
        // jei trina tilu lina tai istrina ir visas masinas nuo jo
        // jei nuvaro toliau nei tilai irgi trina

        // AI skirstom taip - dvigubas masyvas, pirmas parametras nurodo kelintam tile, kitas nurodo juosta
        for (x = 0; x<AIsize; x++) // pereina per visus AI ir patikrina ar jie uz ar pries tilus
        {

                if (((upDown && (AIlist.POS[2] < varList[0].startPos[2] + sinusas * tileWith / 2 || AIlist.POS[2] < varList[0].startPos[2] - sinusas * tileWith / 2)) ||
           (upDown == false && (AIlist.POS[2] > varList[0].startPos[2] + sinusas * tileWith / 2 || AIlist.POS[2] > varList[0].startPos[2] - sinusas * tileWith / 2))) &&

           ((rightLeft && (AIlist.POS[0] < varList[0].startPos[0] + kosinusas * tileWith / 2 || AIlist.POS[0] < varList[0].startPos[0] - kosinusas * tileWith / 2)) ||
           (rightLeft == false && (AIlist.POS[0] > varList[0].startPos[0] + kosinusas * tileWith / 2 || AIlist.POS[0] > varList[0].startPos[0] - kosinusas * tileWith / 2))) 
           || 
           ((upDown && (AIlist.POS[2] > varList[listsSize-1].startPos[2] + sinusas * tileWith / 2 || AIlist.POS[2] > varList[listsSize - 1].startPos[2] - sinusas * tileWith / 2)) ||
           (upDown == false && (AIlist.POS[2] < varList[listsSize - 1].startPos[2] + sinusas * tileWith / 2 || AIlist.POS[2] < varList[listsSize - 1].startPos[2] - sinusas * tileWith / 2))) &&

           ((rightLeft && (AIlist.POS[0] > varList[listsSize - 1].startPos[0] + kosinusas * tileWith / 2 || AIlist.POS[0] > varList[listsSize - 1].startPos[0] - kosinusas * tileWith / 2)) ||
           (rightLeft == false && (AIlist.POS[0] < varList[listsSize - 1].startPos[0] + kosinusas * tileWith / 2 || AIlist.POS[0] < varList[listsSize - 1].startPos[0] - kosinusas * tileWith / 2)))
           )
                {
                    DeleteAI();
                    PushAI();
                    GenerateAI();
                    SpawnAI();

                }
            
        }
        





        
    }

    void GenerateAI()
    {
        //israndominam kur spawninsim prieky playerio ar uz jo

        
        randKey = (byte)UnityEngine.Random.Range(0, 2);

        if (randKey == 1) // prieky playerio
        {

        }
        else // uz playerio
        {

        }
       

        AIlist.POS =  


        go = Instantiate(tilePrefabS[0]) as GameObject;
        go.transform.SetParent(transform);
        scaleVec.Set(sankryzosParametrai * 2, 1f, sankryzosParametrai * 2);
        go.transform.localScale = scaleVec;
        go.transform.rotation = Quaternion.Euler(0, varList[listsSize - 1].angle / pi * 180, 0);
        go.transform.position = offsetVec;
        objektai.Add(go);



    }

    void SpawnAI()
    {

    }

    void PushAI()
    {

    }

    void DeleteAI()
    {

    }


*/
}











/*
private void deleteList(int buvesAmount, List<GameObject> posList)
{

        Destroy(posList[0]);
        posList.RemoveAt(0);
}
}



GameObject go;
go = Instantiate(tilePrefab[0]) as GameObject;
            go.transform.SetParent(transform);
            go.transform.position = Vector3.forward* spawnZ;
spawnZ += tileLenght;
            zPosList.Add(go);

*/

