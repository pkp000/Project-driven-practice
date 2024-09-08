using UnityEngine;
using EMotion;

public class trailController : MonoBehaviour
{

    //public Transform[] dataPoints; // 存储数据点的数组

    float x_bias = -300;
    float y_bias = 200;
    // float z_bias = 300;
    float r = 301;

    int pointIndex = 0;

    public LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        Draw();

    }

    void Draw()
    {
        // 设置线条的顶点数目
        line.positionCount = 5; // dataPoints.Length;



        // 设置每个顶点的位置
        line.SetPosition(0, new Vector3(-200 - 10, 200 + 10, 50));
        line.SetPosition(1, new Vector3(-200 + 21, 200 + 21, 50));
        line.SetPosition(2, new Vector3(-200 - 21, 200 - 21, 50));
    }

    void Update()
    {
        // 在此更新运动轨迹的点


        float Theta_1 = -EthLink.StatusMotRT.PosDataJ[1] * 6 / 5600;
        float z_Euler_Right_J5 = EthLink.StatusMotRT.PosDataJ[5] * 6 / 5600 + 15;
        float phi_5 = 180 - z_Euler_Right_J5;

        float x_R = r * Mathf.Sin(phi_5 * Mathf.Deg2Rad) * Mathf.Cos(Theta_1 * Mathf.Deg2Rad) + x_bias;
        float y_R = r * Mathf.Sin(phi_5 * Mathf.Deg2Rad) * Mathf.Sin(Theta_1 * Mathf.Deg2Rad) + y_bias;
        // float z_R = r * Mathf.Cos(phi_5 * Mathf.Deg2Rad) + z_bias;


        float Theta_2 = EthLink.StatusMotRT.PosDataJ[2] * 6 / 5600;
        float z_Euler_Left_J6 = EthLink.StatusMotRT.PosDataJ[6] * 6 / 5600 + 15;
        float phi_6 = 180 - z_Euler_Left_J6;

        float x_L = r * Mathf.Sin(phi_6 * Mathf.Deg2Rad) * Mathf.Cos(Theta_2 * Mathf.Deg2Rad) + x_bias;
        float y_L = r * Mathf.Sin(phi_6 * Mathf.Deg2Rad) * Mathf.Sin(Theta_2 * Mathf.Deg2Rad) + y_bias;
        // float z_L = r * Mathf.Cos(phi_6 * Mathf.Deg2Rad) + z_bias;




        DrawTrajectory(new Vector3(x_R, y_R, 50));
        DrawTrajectory(new Vector3(x_L, y_L, 50));

    }

    private void DrawTrajectory(Vector3 position)
    {
        line.positionCount++;
        line.SetPosition(pointIndex++, position);
    }
}
