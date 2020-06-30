using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CaptureScene : MonoBehaviour
{
    public int names;
    private int resWidth;
    private int resHeight;
    private int count = 0;
    private string path1 = "/Resources/screenshotsA/", path2 = "/Resources/screenshotsB/";
    private float timer = 0.0f;
    private DirectoryInfo di;
    void Start()
    {
        for (int i = 0; i < 4; i++)

        {
            if (i == 0)

                di = new DirectoryInfo(@"C:\Users\Ozan\Desktop\TrafficSimulation2K\Assets\Resources\screenshotsA");

            else if (i == 1)

                di = new DirectoryInfo(@"C:\Users\Ozan\Desktop\TrafficSimulation2K\Assets\Resources\screenshotsB");

            else if (i == 2)

                di = new DirectoryInfo(@"C:\Users\Ozan\Desktop\TrafficSimulation2K\Assets\Resources\outputA");

            else if (i == 3)

                di = new DirectoryInfo(@"C:\Users\Ozan\Desktop\TrafficSimulation2K\Assets\Resources\outputB");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        names = 1000;
        resWidth = 1920;//416;//640;//cam.pixelWidth;
        resHeight = 1080;//416;//480;//cam.pixelHeight;
        InvokeRepeating("takePics", 0f, 1f);
    }
    public string ScreenShotName(int width, int height)
    {
        //names++;

        if (count % 2 == 0)

        {
            names++;
            count++;
            return Application.dataPath + path1 + names.ToString() + ".png";
        }

        else

        {
            count++;
            return Application.dataPath + path2 + names.ToString() + ".png";
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
    }
    public void takePics()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ssCam"))
        {
            Camera cam = go.GetComponent<Camera>();
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            cam.Render();
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            RenderTexture.active = rt;
            screenShot.ReadPixels(cam.pixelRect, 0, 0);
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            cam.targetTexture = null;
            RenderTexture.active = null;
            rt.Release();
        }
    }
   
}