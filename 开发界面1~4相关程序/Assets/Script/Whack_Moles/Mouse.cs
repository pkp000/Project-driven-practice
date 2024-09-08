using NPOI.SS.Formula.Functions;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HoleNumber // 洞编号
{
	One, 
	Two, 
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight,
	Nine
}

public class Mouse : MonoBehaviour 
{
	private HoleNumber hole;
    private Vector3 tempPos;
    private Transform parentTrans;

	public HoleNumber GetHole
	{
		set{ hole = value; }
		get { return hole; }
	}

	public Transform GetParent
	{ 
		set { parentTrans = value; }
		get { return parentTrans; } 
	}


    private void Start () 
	{
		gameObject.tag = "Mouse";
		SetPosition(hole);
    }

	private void SetPosition(HoleNumber hole)
	{
		switch (hole)
		{
			case HoleNumber.One:
				tempPos = new Vector3(-185,55,0);
				break;
			case HoleNumber.Two:
                tempPos = new Vector3(1, 57, 0);
                break;
			case HoleNumber.Three:
                tempPos = new Vector3(199, 52, 0);
                break;
			case HoleNumber.Four:
                tempPos = new Vector3(-212, -7, 0);
                break;
			case HoleNumber.Five:
                tempPos = new Vector3(3, -7, 0);
                break;
			case HoleNumber.Six:
                tempPos = new Vector3(200, -7, 0);
                break;
			case HoleNumber.Seven:
                tempPos = new Vector3(-219, -73, 0);
                break;
			case HoleNumber.Eight:
                tempPos = new Vector3(2, -76, 0);
                break;
			case HoleNumber.Nine:
                tempPos = new Vector3(221, -79, 0);
                break;
		}

		transform.position = tempPos;
		transform.SetParent(parentTrans, false);
	}

}
