using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;
using UnityEngine.UI;
using TMPro;

public class Tstst : MonoBehaviour
{
    public float spd = 1f;
    public string nm;
    public TextMeshProUGUI tmpug;

    [Space]
    public GameObject hairGO;
    public Material hairMat;
    public int colint;
    public Color col1;
    public Color col2;
    public Color col3;
    public Color col4;
    public Color col5;
    public float dark;

    int tmp;
    // Start is called before the first frame update
    void Start()
    {
        //tmp = UFE.config.player1Character.moves[0].attackMoves[0].totalFrames;
    }

    // Update is called once per frame
    void Update()
    {
        hairGO = GameObject.Find("hair_00");
        //var changeColor = hairGO.GetComponent<SpriteRenderer>();
        //changeColor.color = Color.red;
        //hairMat = hairGO.GetComponent<SpriteRenderer>().material;
        //UFE.config.player1Character.moves[0].basicMoves.idle._animationSpeed = spd;
        //UFE.config.player1Character.moves[0].basicMoves.moveForward._animationSpeed = spd;
        //UFE.config.player1Character.moves[0].basicMoves.moveBack._animationSpeed = -spd;
        //UFE.config.player1Character.moves[0].attackMoves[0]._animationSpeed = spd;
        ///nm = UFE.config.player1Character.moves[0].attackMoves[0].name;

        if (Input.GetKeyUp(KeyCode.Alpha1))
            SetSpd(1);
        if (Input.GetKeyUp(KeyCode.Alpha2))
            SetSpd(1.5f);
        if (Input.GetKeyUp(KeyCode.Alpha3))
            SetSpd(2);
        if (Input.GetKeyUp(KeyCode.Alpha4))
            SetSpd(2.5f);
        if (Input.GetKeyUp(KeyCode.Alpha5))
            SetSpd(3);

        if (Input.GetKeyUp(KeyCode.Z))
            colint = 1;
        if (Input.GetKeyUp(KeyCode.X))
            colint = 2;
        if (Input.GetKeyUp(KeyCode.C))
            colint = 3;
        if (Input.GetKeyUp(KeyCode.V))
            colint = 4;
        if (Input.GetKeyUp(KeyCode.B))
            colint = 5;
        if (Input.GetKeyUp(KeyCode.N))
            colint = 6;

        /*
        if (colint == 1)
            hairMat.SetColor("_ColorRGBA_Color_1", col1);
        if (colint == 2)
            hairMat.SetColor("_ColorRGBA_Color_1", col2);
        if (colint == 3)
            hairMat.SetColor("_ColorRGBA_Color_1", col3);
        if (colint == 4)
            hairMat.SetColor("_ColorRGBA_Color_1", col4);
        if (colint == 5)
            hairMat.SetColor("_ColorRGBA_Color_1", col5);
        if (colint == 6)
        {
            hairMat.SetColor("_ColorRGBA_Color_1", col1);
            hairMat.SetFloat("_Darkness_Fade_1", dark);
        }
        else
            hairMat.SetFloat("_Darkness_Fade_1", 0);
            */

    }

    public void SetSpd (float nspd)
    {
        spd = nspd;
        tmpug.text = "" + spd + "x";
    }
}
