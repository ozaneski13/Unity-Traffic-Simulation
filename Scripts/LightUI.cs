using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightUI : MonoBehaviour
{
    public GameObject lightA;
    public GameObject lightB;
    public RawImage rawA;
    public RawImage rawB;

    void Update()
    {
        if (lightA.activeInHierarchy)

            rawA.GetComponent<RawImage>().color = Color.red;

        else if (!lightA.activeInHierarchy)

            rawA.GetComponent<RawImage>().color = Color.green;

        if (lightB.activeInHierarchy)

            rawB.GetComponent<RawImage>().color = Color.red;

        else if (!lightB.activeInHierarchy)

            rawB.GetComponent<RawImage>().color = Color.green;
    }
}
