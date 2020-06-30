using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public Vector3 originalPos;
    public Rigidbody rb;
    void Start()
    {
        originalPos = transform.position;//Drone'un başlangıç noktası.
        rb = GetComponent<Rigidbody>();//Drone'un hızını ve ivmesini kontrol edebilmek için tanımlanan değişken.
    }
    void Update()//Drone hareketi için tuşlara basılınca verilecek tepkilerin tanımlanması.
    {
        if (Input.GetKey(KeyCode.W))

            rb.AddForce(Vector3.forward);//Basılan tuşa göre gidilmek istenen yöne doğru ivme verilir.

        if (Input.GetKey(KeyCode.A))

            rb.AddForce(Vector3.left);

        if (Input.GetKey(KeyCode.S))

            rb.AddForce(Vector3.back);

        if (Input.GetKey(KeyCode.D))

            rb.AddForce(Vector3.right);

        if (Input.GetKey(KeyCode.E))//E tuşuna basıldığında Drone yükselir.

            rb.AddForce(Vector3.up);

        if (Input.GetKey(KeyCode.R))//R tuşuna basıldığında Drone alçalır.

            rb.AddForce(Vector3.down);

        if (Input.GetKey(KeyCode.T))//T tuşuna basıldığında Drone güncel konumunda sabit kalır.

        {
            rb.velocity = Vector3.zero;//Sahip olduğu hız sıfırlanır.
            rb.angularVelocity = Vector3.zero;//Sahip olduğu ivme sıfırlanır.
        }

        if (Input.GetKey(KeyCode.Q))//Q tuşuna basıldığında Drone başlangıç noktasına döndürülür.

        {
            rb.velocity = Vector3.zero;//Sahip olduğu hız sıfırlanır.
            rb.angularVelocity = Vector3.zero;//Sahip olduğu ivme sıfırlanır.
            transform.position = originalPos;//Position'ı başlangıç noktası olarak tanımlanır.
        }
    }
}
