using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public static Queue<string> console = new Queue<string>();
    public static int limit = 10;

    public static void Log(string s)
    {
        Debug.Log(s);
        if (console.Count >= limit)
        {
            console.Dequeue();
        }
        console.Enqueue(DateTime.Now.ToString("HH:mm:ss") + " - " + s);
    }

    private string getOutput()
    {
        StringBuilder sb = new StringBuilder();
        foreach (string s in Console.console)
        {
            sb.Append("\n");
            sb.Append(s);
        }
        return sb.ToString();
    }

    public void Update()
    {
        string s = getOutput();
        GetComponent<Text>().text = s;
    }
}
