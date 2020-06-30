using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : MonoBehaviour
{
    private bool flag = false;//Araç istenilen noktaya gelene kadar False olarak aracın dönmesini engelleyen değişken.
    void Update()
    {
        if (this.gameObject.transform.position.z < -30.75f && !flag)//İstenilen noktaya gelindiğinde aracın yönü değiştirilecek ve
                                                                   //Flag True yapılarak birden fazla dönme durumları engellenecek.
        {
            transform.Rotate(transform.rotation.x, 90f, transform.rotation.z);
            flag = true;
        }
    }
}