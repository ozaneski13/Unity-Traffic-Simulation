using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficFlow : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public Button changeTrafficFlow;
    public int counter;
    public float tempTime;
    void Start()
    {
        counter = 0;//0 def 1 high 2 normal 3 low
        changeTrafficFlow.onClick.AddListener(changeFlow);
    }

    void changeFlow()
    {
        counter++;

        if (counter == 0)
        {
            tempTime = 5f;
            changeTrafficFlow.GetComponentInChildren<Text>().text = "Default Trafik Saniye Değeri: " + tempTime.ToString();
        }

        else if (counter == 1)

        {
            tempTime = 2f;
            changeTrafficFlow.GetComponentInChildren<Text>().text = "Yüksek Yoğunluklu Trafik Saniye Değeri: " + tempTime.ToString();
        }

        else if (counter == 2)

        {
            tempTime = 4f;
            changeTrafficFlow.GetComponentInChildren<Text>().text = "Normal Yoğunluklu Trafik Saniye Değeri: " + tempTime.ToString();
        }

        else if (counter == 3)

        {
            tempTime = 8f;
            changeTrafficFlow.GetComponentInChildren<Text>().text = "Düşük Yoğunluklu Trafik Saniye Değeri: " + tempTime.ToString();
        }

        else if (counter == 4)

            counter = 0;

        for (int i = 0; i < 4; i++)

        {
            spawnPoints[i].GetComponentInChildren<SpawnCar>().timeCounter = tempTime;
        }
    }
}