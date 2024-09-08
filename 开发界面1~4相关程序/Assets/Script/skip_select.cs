using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EMotion;
using System.Threading;
using UdpBasicClass;

public class skip_select : MonoBehaviour
{

    public Sprite NetStatusOK;
    public Sprite NetStatusFail;

    public bool EthernetStatus;
    public bool draw_flag = false;
    public int TestWDGcount;
    public double step = 0;
    public int position = 0;


    public bool MyFollow_sign = false;

    void Awake()
    {
        UDPTypeVal = ModulePara.LocalUDP_IP;
        UdpCloseBit = false;
        BtnStatus = 0;
        isStopReceiveThread = false;
    }
 

    void Start()
    {
        ReceiveDataThread = new Thread(GainData);
        ReceiveDataThread.Priority = System.Threading.ThreadPriority.Highest;
        ReceiveDataThread.Start();

    }


    public GameObject connectedOn;
    public GameObject connectedOff;

    public void isConnected()
    {
        ConnectClick();

        if (EthLink.ServerLinkActBit == true)
        {
            connectedOn.SetActive(true);
            connectedOff.SetActive(false);
        }
        else
        {
            connectedOn.SetActive(false);
            connectedOff.SetActive(true);
        }

        EthLink.CmdMotorData.CmdPosJ1 = 15000;
    }

    // 消息处理内容

    public void OnSelectClick1()
    {
        // 双臂画圆运动

        SceneManager.LoadScene("temp1");

    }


    public void OnSelectClick2()
    {
        // 场景跳转
        SceneManager.LoadScene("temp2");

        MyFollow_sign = true;

    }

    public void OnSelectClick3()
    {
        SceneManager.LoadScene("temp3");
    }

    public void OnSelectClick4()
    {
        GameObject.FindWithTag("Finish").SetActive(false);
    }

    public void OnReturnHomeClick()
    {
        // 停止当前任务，都恢复初始化
        Initialize();

        // 跳转回首页
        SceneManager.LoadScene("temp0");

        MyFollow_sign = false;

    }

    public void Initialize()
    {
        // 位置
        EthLink.CmdMotorData.CmdPosJ1 = 0;
        EthLink.CmdMotorData.CmdPosJ3 = 0;
        EthLink.CmdMotorData.CmdPosJ5 = 0;

        // 速度
        EthLink.CmdMotorData.CmdSpdJ1 = 0;
        EthLink.CmdMotorData.CmdSpdJ3 = 0;
        EthLink.CmdMotorData.CmdSpdJ5 = 0;

        // 扭矩
        EthLink.CmdMotorData.CmdTorJ1 = 0;
        EthLink.CmdMotorData.CmdTorJ3 = 0;
        EthLink.CmdMotorData.CmdTorJ5 = 0;

    }

    /// /////////////////////////////////////////////////////////////////////////////////////



    private Thread ReceiveDataThread;
    //public static bool isCmd; 
    private bool isStopReceiveThread = false;//停止接收数据多线程
    static bool UdpCloseBit;
    public static string UDPTypeVal;
    public static int BtnStatus;//NetBtnSpriteChg调用 
    public static string ssid_Content;//ssid内容
    public Text ssid_Content_Text_Tab1;//ssid_Text
    public void Exit()
    {
        Application.Quit();
    }


    void FixedUpdate()
    {
        EthLink.EthernetStatusWDG();
        if (UdpCloseBit == true)
        {
            StartCoroutine(WaitTime());
            UdpCloseBit = false;
        }

    }

    private void GainData()
    {
        while (!isStopReceiveThread)
        {
            //low down the send FEQ
            Thread.Sleep(90);

            //TX Local CMD system pkg,include system mode and UDP LINK status,Data Package process
            EthLink.CmdMotorData.LocalSysPkg = EthLink.CmdMotorData.CmdSysMode;

            //send function
            EthLink.CmdJointTargetData((byte)ModulePara.EthLinkParaDefs.Robot_ID1,
                                    EthLink.CmdMotorData.CmdPosJ1, EthLink.CmdMotorData.CmdSpdJ1, EthLink.CmdMotorData.CmdTorJ1,
                                    EthLink.CmdMotorData.CmdPosJ2, EthLink.CmdMotorData.CmdSpdJ2, EthLink.CmdMotorData.CmdTorJ2,
                                    EthLink.CmdMotorData.CmdPosJ3, EthLink.CmdMotorData.CmdSpdJ3, EthLink.CmdMotorData.CmdTorJ3,
                                    EthLink.CmdMotorData.CmdPosJ4, EthLink.CmdMotorData.CmdSpdJ4, EthLink.CmdMotorData.CmdTorJ4,
                                    EthLink.CmdMotorData.CmdPosJ5, EthLink.CmdMotorData.CmdSpdJ5, EthLink.CmdMotorData.CmdTorJ5,
                                    EthLink.CmdMotorData.CmdPosJ6, EthLink.CmdMotorData.CmdSpdJ6, EthLink.CmdMotorData.CmdTorJ6,
                                    EthLink.CmdMotorData.CmdPosJ7, 0, 0,
                                    EthLink.CmdMotorData.CmdPosJ8, 0, 0,
                                    EthLink.CmdMotorData.LocalSysPkg,
                                    EthLink.StatusSystem.EthLinkSta);


            //recv function
            EthLink.SysActualDataRecvFB((byte)ModulePara.EthLinkParaDefs.Robot_ID1);

        }

    }

    void UDP_TypeChange(bool Val)
    {
        if (Val == true)
        {
            UDPTypeVal = ModulePara.WirelessUDP_IP;

        }
        if (Val == false)
        {
            UDPTypeVal = ModulePara.LocalUDP_IP;

        }

    }

    public void HwConnectBtnClick()
    {
        switch (BtnStatus)
        {
            case 0:
                ConnectClick();
                BtnStatus = 1;
                break;
            case 1:
                StopSocket();
                BtnStatus = 0;
                break;
            default:
                break;
        }
    }
    public static void ConnectClick()
    {
        UdpSocketClient.InitSocket();
        EthLink.StatusSystem.EthLinkSta = 0x01;
        Local_delay();
        print("UDP port OPEN");
    }



    //Stop Socket connect opereation
    public static void StopSocket()
    {
        EthLink.StatusSystem.EthLinkSta = 0x02;
        print("Prepare to Close Udp");
        UdpCloseBit = true;
    }


    static void Local_delay()
    {
        int i = 0;
        for (i = 0; i < 0xffffff; i++)
        { }
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.2f);
        //Must wait 0.8 sec befor close the UDP connection.
        UdpSocketClient.SocketQuit();
        print("UDP port Closed Sucess");
    }

    public void OnDestroy()                      //关闭或则退出时自动销毁子线程
    {
        //if (thread_Generate != null) { thread_Generate.Abort(); }
        isStopReceiveThread = true;
        if (ReceiveDataThread != null)
        {
            ReceiveDataThread.Interrupt();
            ReceiveDataThread.Abort();
        }
        EthLink.StatusSystem.EthLinkSta = 0x02;
        UdpSocketClient.SocketQuit();
    }



}
