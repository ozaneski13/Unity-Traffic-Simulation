using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeft : MonoBehaviour
{
    private bool flag = false;//Araç istenilen noktaya gelene kadar False olacak değişken.
    void Update()
    {
        if (this.gameObject.transform.position.x < 7.3f && !flag)//İstenilen noktaya gelindiğinde aracın yönü değiştirilecek ve
                                                                //Flag True yapılarak birden fazla dönme durumları engellenecek.
        {
            transform.Rotate(transform.rotation.x, -90f, transform.rotation.z);
            flag = true;
        }
    }
}