using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour 
{
    private List<Mouse> mouse;
    private int tempNumber;

    void Start()
    {
        mouse = new List<Mouse>();
    }

	void Update ()
	{
        UserInput();
    }

    private void UserInput()
	{
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			Debug.Log("左下角出锤");
            GetMouse();
            tempNumber = 6;
            Comparison();
		}
		else if(Input.GetKeyDown(KeyCode.Keypad2))
		{
            Debug.Log("中下方出锤");
            GetMouse();
            tempNumber = 7;
            Comparison();
        }
		else if( Input.GetKeyDown(KeyCode.Keypad3))
		{
            Debug.Log("右下角出锤");
            GetMouse();
            tempNumber = 8;
            Comparison();
        }
		else if(Input.GetKeyDown(KeyCode.Keypad4))
		{
            Debug.Log("中左方出锤");
            GetMouse();
            tempNumber = 3;
            Comparison();
        }
		else if(Input.GetKeyDown(KeyCode.Keypad5))
		{
            Debug.Log("中间出锤");
            GetMouse();
            tempNumber = 4;
            Comparison();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Debug.Log("中右方出锤");
            GetMouse();
            tempNumber = 5;
            Comparison();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Debug.Log("左上角出锤");
            GetMouse();
            tempNumber = 0;
            Comparison();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Debug.Log("中上方出锤");
            GetMouse();
            tempNumber = 1;
            Comparison();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Debug.Log("右上角出锤");
            GetMouse();
            tempNumber = 2;
            Comparison();
        }

    }

    private void GetMouse()
    {
        GameObject[] ms = GameObject.FindGameObjectsWithTag("Mouse");
        foreach (var o in ms)
        {
            if (!mouse.Contains(o.GetComponent<Mouse>()))
            {
                mouse.Add(o.GetComponent<Mouse>());
            }
        }
    }

    private void Comparison()
    {
        if (mouse.Count == 0)
        {
            Debug.Log("没有地鼠可打");
            return;
        }

        for (int i = 0; i < mouse.Count; i++)
        {
            if (tempNumber == (int)mouse[i].GetHole)
            {
                if (mouse[i] == null)
                    return;
                Destroy(mouse[i].gameObject);
                mouse.RemoveAt(i);
                Debug.Log("击中了对应位置的地鼠");
                return;
            }
        }
    }



}
