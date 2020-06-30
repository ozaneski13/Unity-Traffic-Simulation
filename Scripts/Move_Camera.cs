using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour

{
    public GameObject drone;//Drone'a erişmek için kullanacağımız GameObject değişkeni.
    private Vector3 offset;//Kameranın Drone ile aynı anda hareket etmesi için yapılacak hesaplamada kullanılacak olan değişken.
    void Start()

    {
        offset = transform.position - drone.transform.position;//Kameranın Drone ile arasındaki mesafeyi Update içinde 
    }                                                         //kapatması için yapılan hesaplama.
    void LateUpdate()

    {
        transform.position = drone.transform.position + offset;//Kameranın Drone'u takip etmesi için Position'ının değiştirilmesi.
    }
}
