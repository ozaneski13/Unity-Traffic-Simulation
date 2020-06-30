using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
public class RunPython : MonoBehaviour
{
    private string path1 = "Assets/Resources/screenshotsA/";
    private string path2 = "Assets/Resources/screenshotsB/";
    private string path;
    private string outputPath;
    public char road;
    private string pyName;

    void Start() { 
    if (road == 'a')
        {
            path = path1;
            outputPath = "Assets/Resources/outputA/";
            pyName = "imagesA.py";
        }
        else if (road == 'b')
        {
            path = path2;
            outputPath = "Assets/Resources/outputB/";
            pyName = "imagesB.py";
        }

        python();
    }
    public void python()
    {
        ProcessStartInfo pythonInfo = new ProcessStartInfo();
        Process python;
        //pythonInfo.FileName = @"C:\Users\Ozan\Anaconda3\python.exe";
        pythonInfo.FileName = @"cmd.exe";
        pythonInfo.Arguments = "/c python "+pyName+" --images C:/Users/Ozan/Desktop/TrafficSimulation2K/"+path+" --outputs C:/Users/Ozan/Desktop/TrafficSimulation2K/"+outputPath;
        pythonInfo.CreateNoWindow = false;
        pythonInfo.UseShellExecute = true;
        python = Process.Start(pythonInfo);
        //python.WaitForExit();
        //python.Close();
    }
}