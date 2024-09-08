using UnityEngine;
using UnityEngine.UI;
using EMotion;
using System;
using NPOI.SS.Formula.Functions;


public class CmdRobot : MonoBehaviour
{
    public InputField inputField_J2_Pos;
    public InputField inputField_J2_Spd;
    public InputField inputField_J2_Tor;

    public InputField inputField_J4_Pos;
    public InputField inputField_J4_Spd;
    public InputField inputField_J4_Tor;

    public InputField inputField_J6_Pos;
    public InputField inputField_J6_Spd;
    public InputField inputField_J6_Tor;

    public InputField inputField_J1_Pos;
    public InputField inputField_J1_Spd;
    public InputField inputField_J1_Tor;

    public InputField inputField_J3_Pos;
    public InputField inputField_J3_Spd;
    public InputField inputField_J3_Tor;

    public InputField inputField_J5_Pos;
    public InputField inputField_J5_Spd;
    public InputField inputField_J5_Tor;

    public Animation ani_J1;
    public Animation ani_J2;
    public Animation ani_J3;
    public Animation ani_J4;
    public Animation ani_J5;
    public Animation ani_J6;

    public Text text_Ani_J1;
    public Text text_Ani_J2;

    private int j12_Min = -40000;//J1 J2位置最小
    private int j12_Max = 65000;//J1 J2位置最大
    private int j34_Min = -5000;//J3 J4位置最小
    private int j34_Max = 150000;//J3 J4位置最大
    private int j56_Min = 0;//J5 J6位置最小
    private int j56_Max = 110000;//J5 J6位置最大

    private int spd_Min = 100000;//髋节速度最小值
    private int spd_Max = 1000000;//髋节速度最大值

    private float tor_Min = -0.7f;//扭矩最小值
    private float tor_Max = 0.7f;//扭矩最大值

    public int j1;
    public double steps = 0;
    public bool flag = false;

    private void Start()
    {
        InputFiledLimit.LimitFloat(inputField_J1_Tor);//只能输入浮点数
        InputFiledLimit.LimitFloat(inputField_J2_Tor);//只能输入浮点数
        InputFiledLimit.LimitFloat(inputField_J3_Tor);//只能输入浮点数
        InputFiledLimit.LimitFloat(inputField_J4_Tor);//只能输入浮点数
        InputFiledLimit.LimitFloat(inputField_J5_Tor);//只能输入浮点数
        InputFiledLimit.LimitFloat(inputField_J6_Tor);//只能输入浮点数
    }

    private void Update()
    {
        steps = steps + 0.001;

        if (flag)
        {
            EthLink.CmdMotorData.CmdPosJ2 = EthLink.StatusMotRT.PosDataJ[1];
            EthLink.CmdMotorData.CmdSpdJ2 = EthLink.StatusMotRT.SpdDataJ[1];
        }

        //j1= (int)(20000 * Math.Sin(steps));
        //Debug.Log(j1);
    }

        public void shoudata2()
        {
        EthLink.CmdMotorData.CmdSpdJ2=EthLink.StatusMotRT.SpdDataJ[1];
        }

        //对应第一个Move按钮
        public void MoveJ1()
    {        
        if (!IsNullJ1() && IsRangeMoveJ1())
        {
            flag = true;
            //EthLink.CmdMotorData.CmdPosJ1 = int.Parse(inputField_J1_Pos.text);
            //EthLink.CmdMotorData.CmdSpdJ1 = int.Parse(inputField_J1_Spd.text);
            //EthLink.CmdMotorData.CmdTorJ1 = Convert.ToInt32(float.Parse(inputField_J1_Tor.text)*1000);

            EthLink.CmdMotorData.CmdPosJ1 =  (int)(20000 * Math.Sin(steps));
            EthLink.CmdMotorData.CmdSpdJ1 = 500000;
            ssss = (int)(20000 * Math.Sin(steps))- EthLink.StatusMotRT.PosDataJ[1];

            tor = p * ssss + i * () + d * ();

            EthLink.CmdMotorData.CmdTorJ1 = tor;


            EthLink.CmdMotorData.CmdTorJ1 = Convert.ToInt32(float.Parse(inputField_J1_Tor.text) * 1000);








        }
        else
        {
            if(ani_J1.isPlaying)
            {
                ani_J1.Stop();
            }
            ani_J1.Play();
        } 
    }

    //对应第二个Move按钮
    public void MoveJ2()
    {
        if (!IsNullJ2() && IsRangeMoveJ2())
        {
            EthLink.CmdMotorData.CmdPosJ2 = int.Parse(inputField_J2_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ2 = int.Parse(inputField_J2_Spd.text);
            EthLink.CmdMotorData.CmdTorJ2 = Convert.ToInt32(float.Parse(inputField_J2_Tor.text)*1000);
        }
        else
        {
            if (ani_J2.isPlaying)
            {
                ani_J2.Stop();
            }
            ani_J2.Play();
        }
    }

    //对应第三个Move按钮
    public void MoveJ3()
    {
        if(!IsNullJ3() && IsRangeMoveJ3())
        {
            EthLink.CmdMotorData.CmdPosJ3 = int.Parse(inputField_J3_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ3 = int.Parse(inputField_J3_Spd.text);
            EthLink.CmdMotorData.CmdTorJ3 = Convert.ToInt32(float.Parse(inputField_J3_Tor.text)*1000);
        }
        else
        {
            if (ani_J3.isPlaying)
            {
                ani_J3.Stop();
            }
            ani_J3.Play();
        }
    }

    //对应第四个Move按钮
    public void MoveJ4()
    {
        if(!IsNullJ4() && IsRangeMoveJ4())
        {
            EthLink.CmdMotorData.CmdPosJ4 = int.Parse(inputField_J4_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ4 = int.Parse(inputField_J4_Spd.text);
            EthLink.CmdMotorData.CmdTorJ4 = Convert.ToInt32(float.Parse(inputField_J4_Tor.text)*1000);
        }
        else
        {
            if (ani_J4.isPlaying)
            {
                ani_J4.Stop();
            }
            ani_J4.Play();
        }
    }

    //对应第五个Move按钮
    public void MoveJ5()
    {
        if (!IsNullJ5() && IsRangeMoveJ5())
        {
            EthLink.CmdMotorData.CmdPosJ5 = int.Parse(inputField_J5_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ5 = int.Parse(inputField_J5_Spd.text);
            EthLink.CmdMotorData.CmdTorJ5 = Convert.ToInt32(float.Parse(inputField_J5_Tor.text) * 1000);
        }
        else
        {
            if (ani_J5.isPlaying)
            {
                ani_J5.Stop();
            }
            ani_J5.Play();
        }
    }

    //对应第六个Move按钮
    public void MoveJ6()
    {
        if (!IsNullJ6() && IsRangeMoveJ6())
        {
            EthLink.CmdMotorData.CmdPosJ6 = int.Parse(inputField_J6_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ6 = int.Parse(inputField_J6_Spd.text);
            EthLink.CmdMotorData.CmdTorJ6 = Convert.ToInt32(float.Parse(inputField_J6_Tor.text) * 1000);
        }
        else
        {
            if (ani_J6.isPlaying)
            {
                ani_J6.Stop();
            }
            ani_J6.Play();
        }
    }

    //对应Finall按钮
    public void FinallMove()
    {
        if(!IsNullFinall() && IsRangeFinall())
        {
            EthLink.CmdMotorData.CmdPosJ1 = int.Parse(inputField_J1_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ1 = int.Parse(inputField_J1_Spd.text);
            EthLink.CmdMotorData.CmdTorJ1 = Convert.ToInt32(float.Parse(inputField_J1_Tor.text) * 1000);

            EthLink.CmdMotorData.CmdPosJ2 = int.Parse(inputField_J2_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ2 = int.Parse(inputField_J2_Spd.text);
            EthLink.CmdMotorData.CmdTorJ2 = Convert.ToInt32(float.Parse(inputField_J2_Tor.text) * 1000);

            EthLink.CmdMotorData.CmdPosJ3 = int.Parse(inputField_J3_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ3 = int.Parse(inputField_J3_Spd.text);
            EthLink.CmdMotorData.CmdTorJ3 = Convert.ToInt32(float.Parse(inputField_J3_Tor.text) * 1000);

            EthLink.CmdMotorData.CmdPosJ4 = int.Parse(inputField_J4_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ4 = int.Parse(inputField_J4_Spd.text);
            EthLink.CmdMotorData.CmdTorJ4 = Convert.ToInt32(float.Parse(inputField_J4_Tor.text) * 1000);

            EthLink.CmdMotorData.CmdPosJ5 = int.Parse(inputField_J5_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ5 = int.Parse(inputField_J5_Spd.text);
            EthLink.CmdMotorData.CmdTorJ5 = Convert.ToInt32(float.Parse(inputField_J5_Tor.text) * 1000);

            EthLink.CmdMotorData.CmdPosJ6 = int.Parse(inputField_J6_Pos.text);
            EthLink.CmdMotorData.CmdSpdJ6 = int.Parse(inputField_J6_Spd.text);
            EthLink.CmdMotorData.CmdTorJ6 = Convert.ToInt32(float.Parse(inputField_J6_Tor.text) * 1000);
        }
        else
        {
            if (ani_J1.isPlaying)
            {
                ani_J1.Stop();
            }
            if(ani_J2.isPlaying)
            {
                ani_J2.Stop();
            }
            if(ani_J3.isPlaying)
            {
                ani_J3.Stop();
            }
            if(ani_J4.isPlaying)
            {
                ani_J4.Stop();
            }
            if (ani_J5.isPlaying)
            {
                ani_J5.Stop();
            }
            if (ani_J6.isPlaying)
            {
                ani_J6.Stop();
            }
            ani_J1.Play();
            ani_J2.Play();
            ani_J3.Play();
            ani_J4.Play();
            ani_J5.Play();
            ani_J6.Play();
        }
    }

    private bool IsNullJ1()
    {
        if (inputField_J1_Pos.text.Trim() == null || inputField_J1_Pos.text.Trim() == "" || inputField_J1_Spd.text.Trim() == null || inputField_J1_Spd.text.Trim() == "" || inputField_J1_Tor.text.Trim() == null || inputField_J1_Tor.text.Trim() == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsNullJ2()
    {
        if (inputField_J2_Pos.text.Trim() == null || inputField_J2_Pos.text.Trim() == "" || inputField_J2_Spd.text.Trim() == null || inputField_J2_Spd.text.Trim() == "" || inputField_J2_Tor.text.Trim() == null || inputField_J2_Tor.text.Trim() == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsNullJ3()
    {
        if (inputField_J3_Pos.text.Trim() == null || inputField_J3_Pos.text.Trim() == "" || inputField_J3_Spd.text.Trim() == null || inputField_J3_Spd.text.Trim() == "" || inputField_J3_Tor.text.Trim() == null || inputField_J3_Tor.text.Trim() == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsNullJ4()
    {
        if (inputField_J4_Pos.text.Trim() == null || inputField_J4_Pos.text.Trim() == "" || inputField_J4_Spd.text.Trim() == null || inputField_J4_Spd.text.Trim() == "" || inputField_J4_Tor.text.Trim() == null || inputField_J4_Tor.text.Trim() == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsNullJ5()
    {
        if (inputField_J5_Pos.text.Trim() == null || inputField_J5_Pos.text.Trim() == "" || inputField_J5_Spd.text.Trim() == null || inputField_J5_Spd.text.Trim() == "" || inputField_J5_Tor.text.Trim() == null || inputField_J5_Tor.text.Trim() == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsNullJ6()
    {
        if (inputField_J6_Pos.text.Trim() == null || inputField_J6_Pos.text.Trim() == "" || inputField_J6_Spd.text.Trim() == null || inputField_J6_Spd.text.Trim() == "" || inputField_J6_Tor.text.Trim() == null || inputField_J6_Tor.text.Trim() == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsNullFinall()
    {
        if(IsNullJ6() || IsNullJ5() || IsNullJ4() || IsNullJ3() || IsNullJ2() || IsNullJ1())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeMoveJ1()
    {
        if (int.Parse(inputField_J1_Pos.text) >= j12_Min && int.Parse(inputField_J1_Pos.text) <= j12_Max && int.Parse(inputField_J1_Spd.text) >= spd_Min && int.Parse(inputField_J1_Spd.text) <= spd_Max && float.Parse(inputField_J1_Tor.text) >= tor_Min && float.Parse(inputField_J1_Tor.text) <= tor_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeMoveJ2()
    {
        if (int.Parse(inputField_J2_Pos.text) >= j12_Min && int.Parse(inputField_J2_Pos.text) <= j12_Max && int.Parse(inputField_J2_Spd.text) >= spd_Min && int.Parse(inputField_J2_Spd.text) <= spd_Max && float.Parse(inputField_J2_Tor.text) >= tor_Min && float.Parse(inputField_J2_Tor.text) <= tor_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeMoveJ3()
    {
        if (int.Parse(inputField_J3_Pos.text) >= j34_Min && int.Parse(inputField_J3_Pos.text) <= j34_Max && int.Parse(inputField_J3_Spd.text) >= spd_Min && int.Parse(inputField_J3_Spd.text) <= spd_Max && float.Parse(inputField_J3_Tor.text) >= tor_Min && float.Parse(inputField_J3_Tor.text) <= tor_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeMoveJ4()
    {
        if (int.Parse(inputField_J4_Pos.text) >= j34_Min && int.Parse(inputField_J4_Pos.text) <= j34_Max && int.Parse(inputField_J4_Spd.text) >= spd_Min && int.Parse(inputField_J4_Spd.text) <= spd_Max && float.Parse(inputField_J4_Tor.text) >= tor_Min && float.Parse(inputField_J4_Tor.text) <= tor_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeMoveJ5()
    {
        if (int.Parse(inputField_J5_Pos.text) >= j56_Min && int.Parse(inputField_J5_Pos.text) <= j56_Max && int.Parse(inputField_J5_Spd.text) >= spd_Min && int.Parse(inputField_J5_Spd.text) <= spd_Max && float.Parse(inputField_J5_Tor.text) >= tor_Min && float.Parse(inputField_J5_Tor.text) <= tor_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeMoveJ6()
    {
        if (int.Parse(inputField_J6_Pos.text) >= j56_Min && int.Parse(inputField_J6_Pos.text) <= j56_Max && int.Parse(inputField_J6_Spd.text) >= spd_Min && int.Parse(inputField_J6_Spd.text) <= spd_Max && float.Parse(inputField_J6_Tor.text) >= tor_Min && float.Parse(inputField_J6_Tor.text) <= tor_Max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRangeFinall()
    {
        if(IsRangeMoveJ1() && IsRangeMoveJ2() && IsRangeMoveJ3() && IsRangeMoveJ4() && IsRangeMoveJ5() && IsRangeMoveJ6())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
