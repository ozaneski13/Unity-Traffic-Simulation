using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButton : MonoBehaviour
{
    public Camera mainCam;
    public Camera panoramicCam;
    public Camera roadA;
    public Camera roadB;
    public Button mainButton;
    public Button sideButton;
    public Text upperText;
    private bool checkMain;
    private bool checkSide;
    void Start()
    {
        Debug.Log("inside");
        checkMain = true;
        checkSide = true;
        mainButton.onClick.AddListener(changeMainCamera);
        sideButton.onClick.AddListener(changeSideCamera);
    }
    void changeMainCamera()
    {
        if (checkMain)

        {
            mainCam.targetDisplay = 3;
            panoramicCam.targetDisplay = 0;
            Debug.Log("changed1");
            mainButton.GetComponentInChildren<Text>().text = "Ana Kamera";
            upperText.text = "Kuşbakışı Kamera";
            checkMain = false;
        }

        else if (!checkMain)
        {
            mainCam.targetDisplay = 0;
            panoramicCam.targetDisplay = 3;
            mainButton.GetComponentInChildren<Text>().text = "Kuşbakışı Kamera";
            Debug.Log("changed2");
            upperText.text = "Ana Kamera";
            checkMain = true;
        }

        roadA.targetDisplay = 1;
        roadB.targetDisplay = 2;
    }

    void changeSideCamera()

    {
        if (checkSide)

        {
            roadA.targetDisplay = 0;
            mainCam.targetDisplay = 1;
            roadB.targetDisplay = 2;
            upperText.text = "Yol A";
            checkSide = false;
        }

        else if (!checkSide)

        {
            roadB.targetDisplay = 0;
            mainCam.targetDisplay = 2;
            roadA.targetDisplay = 1;
            upperText.text = "Yol B";
            checkSide = true;
        }

        panoramicCam.targetDisplay = 3;
    }
}