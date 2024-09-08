using UnityEngine;
using UnityEngine.UI;
using EMotion;

public class data_display : MonoBehaviour 
{    
    public Text text_J1_Pos;
    public Text text_J1_Spd;

    public Text text_J2_Pos;
    public Text text_J2_Spd;

    public Text text_J3_Pos;
    public Text text_J3_Spd;

    public Text text_J4_Pos;
    public Text text_J4_Spd;

    public Text text_J5_Pos;
    public Text text_J5_Spd;

    public Text text_J6_Pos;
    public Text text_J6_Spd;
 
    // Update is called once per frame
    void Update () 
	{
        text_J1_Pos.text = EthLink.StatusMotRT.PosDataJ[1].ToString();
        text_J1_Spd.text = EthLink.StatusMotRT.SpdDataJ[1].ToString();

        text_J3_Pos.text = EthLink.StatusMotRT.PosDataJ[3].ToString();
        text_J3_Spd.text = EthLink.StatusMotRT.SpdDataJ[3].ToString();

        text_J5_Pos.text = EthLink.StatusMotRT.PosDataJ[5].ToString();
        text_J5_Spd.text = EthLink.StatusMotRT.SpdDataJ[5].ToString();


        text_J2_Pos.text = EthLink.StatusMotRT.PosDataJ[2].ToString();
        text_J2_Spd.text = EthLink.StatusMotRT.SpdDataJ[2].ToString();

        text_J4_Pos.text = EthLink.StatusMotRT.PosDataJ[4].ToString();
        text_J4_Spd.text = EthLink.StatusMotRT.SpdDataJ[4].ToString();

        text_J6_Pos.text = EthLink.StatusMotRT.PosDataJ[6].ToString();
        text_J6_Spd.text = EthLink.StatusMotRT.SpdDataJ[6].ToString();
    }
}
