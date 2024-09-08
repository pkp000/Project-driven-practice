using EMotion;
using System;
using System.IO;
using UnityEngine;



public class Move : MonoBehaviour 
{
    // 限定在环内运动功能的成员数据
    private float minRadius = 34f;   // 设置圆环内半径，可更改
    private float maxRadius = 44f;   // 设置圆环外半径，可更改
    public Transform centerPoint;
    

    // 切换手臂相关的成员数据
    public bool isRightArm = true;
    public UnityEngine.UI.Text currentArmDescription;
    public int[] currentArmLabel;
    private int[] rightArmLabel = { 1, 3, 5 };
    private int[] leftArmLabel = {2, 4, 6 };


    // 实现游戏相关功能的成员数据
    public UnityEngine.UI.Text displayScore;
    public UnityEngine.UI.Text startState;
    public double testScore = 100;
    private double scoreDeduction = 0.5;


    // 写入文件相关的成员数据
    private int fileNumber = 0;
    private int roundNumber = 1;


    // 连接机器人相关的成员数据
    private bool isRobotControl = false;


    // 建立坐标系进行机器人正运动学数学推导的成员数据
    private double x_bias = 0;
    private double y_bias = 0;
    private double Theta_1, Theta_2, Theta_3;
    private double ax = 100;
    private double az = 50;
    private double L1 = 340.0;
    private double L2 = 301.5;
    private double y_min = -640; // -543; // -640;
    private double y_max = 980;  //605;  // 980;
    private double z_max = 400;  //366;  // 400;

    private void Start()
    {
        displayScore.text = testScore.ToString("0.00");
        currentArmDescription.text = "右臂";
        currentArmLabel = rightArmLabel;
        InitializationPosition();
    }


    private void Update()
    {
        if (isRobotControl)
        {
            Theta_1 = EthLink.StatusMotRT.PosDataJ[currentArmLabel[0]] * 6 / 5600;
            if(currentArmLabel == rightArmLabel)
            {
                Theta_1 *= -1;
            }

            Theta_2 = EthLink.StatusMotRT.PosDataJ[3] * 6 / 5600;

            Theta_3 = EthLink.StatusMotRT.PosDataJ[currentArmLabel[2]] * 6 / 5600 + 15;



            // D-H方法解出的正运动学表达式，xyz是相对于肩部固定坐标系
            double x = L2 * Mathf.Cos((float)(Theta_1 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_2 * Mathf.Deg2Rad)) *
                       Mathf.Cos((float)(Theta_3 * Mathf.Deg2Rad)) + L2 * Mathf.Cos((float)(Theta_1 * Mathf.Deg2Rad)) *
                       Mathf.Cos((float)(Theta_2 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_3 * Mathf.Deg2Rad)) + L1 *
                       Mathf.Cos((float)(Theta_1 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_2 * Mathf.Deg2Rad)) + ax *
                       Mathf.Cos((float)(Theta_1 * Mathf.Deg2Rad)) + x_bias;


            double y = L2 * Mathf.Sin((float)(Theta_1 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_2 * Mathf.Deg2Rad)) *
                       Mathf.Cos((float)(Theta_3 * Mathf.Deg2Rad)) + L2 * Mathf.Sin((float)(Theta_1 * Mathf.Deg2Rad)) *
                       Mathf.Cos((float)(Theta_2 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_3 * Mathf.Deg2Rad)) + L1 *
                       Mathf.Sin((float)(Theta_1 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_2 * Mathf.Deg2Rad)) + ax *
                       Mathf.Sin((float)(Theta_1 * Mathf.Deg2Rad)) + y_bias;


            double z = L2 * Mathf.Cos((float)(Theta_2 * Mathf.Deg2Rad)) * Mathf.Cos((float)(Theta_3 * Mathf.Deg2Rad)) *
                       - L2 * Mathf.Sin((float)(Theta_2 * Mathf.Deg2Rad)) * Mathf.Sin((float)(Theta_3 * Mathf.Deg2Rad))
                       + L1 * Mathf.Cos((float)(Theta_2 * Mathf.Deg2Rad)) + az;




            // 通过实际位置映射到界面上操控小球
            // 将 (y,z) 转换为极坐标系下的坐标 (r,θ)
            double r = Math.Sqrt(y * y + z * z);
            
            // 将极径 r 映射到 [minRadius, maxRadius] 范围内
            double rMapped = minRadius + (maxRadius - minRadius) * r / Math.Sqrt(y_max * y_max + z_max * z_max);

            // 将极角 θ 映射到 [0,2π] 范围内
            double thetaMapped = 2 * Math.PI / (y_max - y_min) * (y -y_min);

            // 计算映射后的坐标 (y',z')
            double yMapped = rMapped * Math.Cos(thetaMapped);
            double zMapped = rMapped * Math.Sin(thetaMapped);

            float x_UI = (float)yMapped;
            float y_UI = (float)zMapped;
            float z_UI = (float)x * 0 + 63;

            string xy = "x = " + x_UI.ToString() + "  y = " + y_UI.ToString();
            Debug.Log(xy);

            transform.position = new Vector3(x_UI, y_UI, z_UI);
        }


        // 获取当前小球的位置
        Vector3 position = transform.position;

        // 将小球的位置投影到XY平面
        Vector3 projectedPosition = new Vector3(position.x, position.y, centerPoint.position.z);

        // 计算小球到原点的距离
        float distanceToOrigin = Vector3.Distance(projectedPosition, centerPoint.position);


        // 如果小球超出范围
        if (distanceToOrigin > maxRadius)
        {
            testScore -= scoreDeduction;


            // 限制其在圆环内移动
            Vector3 fromOriginToObject = projectedPosition - centerPoint.position;
            fromOriginToObject *= maxRadius / distanceToOrigin;
            projectedPosition = centerPoint.position + fromOriginToObject;
        }


        else if (distanceToOrigin < minRadius)
        {
            testScore -= scoreDeduction;


            // 限制其在圆环内移动
            Vector3 fromOriginToObject = projectedPosition - centerPoint.position;
            fromOriginToObject *= minRadius / distanceToOrigin;
            projectedPosition = centerPoint.position + fromOriginToObject;
        }

        // 更新小球的位置
        transform.position = new Vector3(projectedPosition.x, projectedPosition.y, position.z);

        // 显示分数
        if(testScore < 80 && testScore > 60)
        {
            displayScore.color = Color.yellow;
        }

        else if(testScore < 60)
        {
            displayScore.color = Color.red;
        }

        else if (testScore >= 80)
        {
            displayScore.color = Color.green;
        }

        displayScore.text = testScore.ToString("0.00");
    }


    private void LateUpdate()
    {
        // 用来切换使用哪边的手臂
        if (isRightArm) 
        { 
            currentArmLabel = rightArmLabel;
        }

        else 
        { 
            currentArmLabel = leftArmLabel;
        }
    }


    public void ClickToggleButton()
    {
        InitializationPosition();
        roundNumber++;
        fileNumber = 1;

        if (isRightArm)
        {
            currentArmDescription.text = "左臂";
            isRightArm = false;
        }

        else 
        {
            currentArmDescription.text = "右臂"; 
            isRightArm = true;
        }
    }


    private void InitializationPosition()
    {
        startState.text = "开始";
        // SaveFile();
        transform.position = new Vector3(0,39,63);
        testScore = 100;
        isRobotControl = false;
    }


    private void SaveFile()
    {
        File.WriteAllText(@"d:\ULS_Robotics_MAPS MAX_Excel\得分记录\"
                          +roundNumber.ToString()+"-"+fileNumber.ToString()+".txt", displayScore.text);
        fileNumber++;
    }


    public void RobotControl()
    {
        if(!isRobotControl)
        {
            isRobotControl = true;
            startState.text = "暂停";
        }
        
        else
        {
            isRobotControl = false;
            startState.text = "开始";
        }
    }


    private void StopFrameUpdate()
    {
        // 停止帧更新
        Time.timeScale = 0; 
    }

    private void ContinueFrameUpdate()
    {
        // 恢复帧更新
        Time.timeScale = 1;
    }

    public void Finish()
    {
        // SaveFile();
        InitializationPosition();
    }
}
