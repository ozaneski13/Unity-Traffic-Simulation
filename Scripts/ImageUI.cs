using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEditor;
public class ImageUI : MonoBehaviour
{
    public Button changeUIButton;
    public Text date;
    public Text[] remainTime;
    public Texture2D myTexture;
    public RawImage myRawImage;
    public int tempTime;
    private bool check;
    private string path;
    private int i = 1001;
    private int j = 1001;
    private float myTimer;
    private int myGreenTimer;
    private string imgName;
    private int counter;
    void Start()
    {
        myGreenTimer = 10;
        tempTime = 0;
        myTimer = 0;
        check = true;
        changeUIButton.onClick.AddListener(changeCheck);
        InvokeRepeating("changeImage", 1f, 0.5f);
        InvokeRepeating("UpdateTime", 0f, 1f);
    }
    //Son atılan //
    void Update()
    {
        myTimer += Time.deltaTime;
        if (tempTime != 0 && myGreenTimer - myTimer <= 0)

        {
            myTimer = 0f;
            myGreenTimer = tempTime;
            tempTime = 0;
        }

        if (myGreenTimer - myTimer >= 0)

        {
            if (myGreenTimer - myTimer <= 0)
            {
                remainTime[0].text = (0).ToString();
                remainTime[1].text = (0).ToString();
            }

            else
            {
                remainTime[0].text = ((int)(myGreenTimer - myTimer)).ToString();
                remainTime[1].text = ((int)(myGreenTimer - myTimer)).ToString();
            }
        }
    }
    void UpdateTime()
    {
        date.text = DateTime.Now.ToString();
    }
    void changeCheck()
    {
        check = !check;
    }

    void changeImage()
    {

        if (check)
        {
            imgName = "C:/Users/Ozan/Desktop/TrafficSimulation2K/Assets/Resources/outputA/" + i.ToString() + ".png";
        }
        else if (!check)
        {
            imgName = "C:/Users/Ozan/Desktop/TrafficSimulation2K/Assets/Resources/outputB/" + j.ToString() + ".png";
        }

        bool fileExist = File.Exists(imgName);

        if (fileExist == false)
        {//eger aranan dosya yoksa bekle
            throw new ArgumentOutOfRangeException(imgName, "original");
        }
        else
        {
            byte[] byteArray = File.ReadAllBytes(imgName);
            myTexture = new Texture2D(2, 2);
            bool isLoaded = myTexture.LoadImage(byteArray);
            if (isLoaded)
            {
                myRawImage.texture = myTexture;
            }
            else
            {
                throw new ArgumentOutOfRangeException(imgName + " cannot be done ", "original");
            }
            i += 1;
            j += 1;
            counter++;
            throw new ArgumentOutOfRangeException(imgName + " done ", "original");
        }
    }
}
/*void changeImage()
{
    AssetDatabase.Refresh();

    if (check)
    {
        path = "outputA/" + i.ToString();
    }

    else if (!check)
    {
        path = "outputB/" + j.ToString();
    }

    myTexture = (Texture2D)Resources.Load(path, typeof(Texture2D)) as Texture2D;
    myRawImage.texture = myTexture;
    i++;
    j++;
}
}*/
