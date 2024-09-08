using UnityEngine;
using EMotion;


public class imitative_move : MonoBehaviour 
{
	float minJ12 = -40000;
    float minJ34 = -5000;

	float wightJ12 = 4;
	float wightJ34 = 1;
	float wightJ56 = 4;

	float normJ12 = 65000 + 40000;
    float normJ34 = 150000 + 5000;
	float normJ56 = 110000;

	float lastJ1 = 0; 
	float lastJ2 = 0;
	float lastJ3 = 0;
	float lastJ4 = 0;
	float lastJ5 = 0;
	float lastJ6 = 0;

    // pulse count
    float J1, J2, J3, J4, J5, J6;

    // Compare the degree of movement
    float moveDegreeR, moveDegreeF;


	
	// Update is called once per frame
	void Update () 
	{
		J1 = (EthLink.StatusMotRT.PosDataJ[1] - minJ12) / normJ12;
        J3 = (EthLink.StatusMotRT.PosDataJ[3] - minJ34) / normJ34;
        J5 = EthLink.StatusMotRT.PosDataJ[5]/ normJ56;

		moveDegreeR = wightJ12 * Mathf.Pow(J1-lastJ1, 2) + wightJ34 * Mathf.Pow(J3 - lastJ3, 2) + wightJ56 * Mathf.Pow(J5 - lastJ5, 2);


        J2 = (EthLink.StatusMotRT.PosDataJ[2] - minJ12) / normJ12;
        J4 = (EthLink.StatusMotRT.PosDataJ[4] - minJ34) / normJ34;
        J6 = EthLink.StatusMotRT.PosDataJ[6] / normJ56;

        moveDegreeF = wightJ12 * Mathf.Pow(J2 - lastJ2, 2) + wightJ34 * Mathf.Pow(J4 - lastJ4, 2) + wightJ56 * Mathf.Pow(J6 - lastJ6, 2);


		if (moveDegreeR >= moveDegreeF)
		{

            EthLink.CmdMotorData.CmdSpdJ1 = EthLink.StatusMotRT.SpdDataJ[2];
            EthLink.CmdMotorData.CmdPosJ1 = EthLink.StatusMotRT.PosDataJ[2];
            EthLink.CmdMotorData.CmdSpdJ3 = EthLink.StatusMotRT.SpdDataJ[4];
            EthLink.CmdMotorData.CmdPosJ3 = EthLink.StatusMotRT.PosDataJ[4];
            EthLink.CmdMotorData.CmdSpdJ5 = EthLink.StatusMotRT.SpdDataJ[6];
            EthLink.CmdMotorData.CmdPosJ5 = EthLink.StatusMotRT.PosDataJ[6];
            Debug.Log("将左臂的位置传给右臂运动");
        }
        else
		{
            EthLink.CmdMotorData.CmdSpdJ2 = EthLink.StatusMotRT.SpdDataJ[1];
            EthLink.CmdMotorData.CmdPosJ2 = EthLink.StatusMotRT.PosDataJ[1];
            EthLink.CmdMotorData.CmdSpdJ4 = EthLink.StatusMotRT.SpdDataJ[3];
            EthLink.CmdMotorData.CmdPosJ4 = EthLink.StatusMotRT.PosDataJ[3];
            EthLink.CmdMotorData.CmdSpdJ6 = EthLink.StatusMotRT.SpdDataJ[5];
            EthLink.CmdMotorData.CmdPosJ6 = EthLink.StatusMotRT.PosDataJ[5];
            Debug.Log("将右臂的位置传给左臂运动");
        }

        lastJ1 = J1;
		lastJ3 = J3;
		lastJ5 = J5;

		lastJ2 = J2;
		lastJ4 = J4;
		lastJ6 = J6;
	}
}
