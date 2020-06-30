using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement2 : MonoBehaviour

{
    public Quaternion targetRotation;//Aracın aldığı mesafeyi hesaplamak için tanımlanan değişken.
    public float dif = 0;//Aracın aldığı mesafenin tutulduğu değişken.
    public float speed = 0.75f;//Araçların default hız değeri.
    public bool flag = false;//True olması Collision olduğu False olması ise Collision olmadığı anlamına gelmektedir.
    public bool checkPoint = false;//Aracın puanının azaltılıp azaltılmadığının değerini tutan değişken.
    public GameObject scriptObject;//Işıkların durumunu kontrol edebilmek ve TrafficControl objesinin tuttuğu TrafficControlBeta Script'ine erişim sağlanması.
    public GameObject check;//Yolların puanlarına erişim sağlanması.
    public TrafficLightsControlBETA script;//TrafficControlBeta Script'ine erişebilmek için tanımlanan değişken.
    public Rigidbody rb;//Araçların durduğu anda tüm hızlarını sıfırlamak için tanımlanan değişken.
    private float stopTimer;
    private float tempz;
    void Start()
    {
        stopTimer = 0f;
        check = GameObject.Find("Check");//Point Script'ine ulaşmak için tanımlanan değişken.
        rb = GetComponent<Rigidbody>(); //Aracın Rigidbody'sine ulaşmak için tanımlanan değişken.

        scriptObject = GameObject.Find("TrafficControl");//Işıkların durumunu kontrol edebilmek için Script'e ulaşım sağlanması.
        script = scriptObject.GetComponent<TrafficLightsControlBETA>();//Script'e erişim sağlanması.

        targetRotation = transform.rotation;//Alınan mesafeyi hesaplamak için tutylan değişken.
    }

    void Update()
    {
        dif = (targetRotation.x - transform.position.x) + (targetRotation.z - transform.position.z);//Her karede katedilen mesafe hesabı.
        transform.Translate(speed * Vector3.forward * Time.deltaTime);//Kareler arası geçen zamana göre araçları ilerletir.

        if (tempz < this.gameObject.transform.position.z)

            if (this.gameObject.transform.position.z > -22f)

                Destroy(this.gameObject);

        tempz = this.gameObject.transform.position.z;

        if (script.red_2.activeInHierarchy && !flag)//Kırmızı yandıysa ve Collision yaşanmadıysa harekete devam etmeli.
                                                    //Yani araç kırmızı ışığın altına gelene kadar hareket etmeli.
            speed = 0.75f;

        else if ((script.red_2.activeInHierarchy || (!script.red_2 && script.yellow_2 && !script.green_2)) && flag)//Kırmızı yandıysa VE Collision yaşandıysa araç durmalı.
                                                                                                                   //Kırmızıya gelene kadar bir araçla Collision yaşanmalı veya
        {                                                                                                        //kırmızı ışığın Collider'ı ile Collision yaşanmış olmalı.
            speed = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        else if (!script.red_2.activeInHierarchy && !flag)//Kırmızı ışık yanmıyorsa ve Collision yaşanmadıysa hareket etmeli.

            speed = 0.75f;

        else//Yaşanacak bir anomali durumunda araç dursun. Ama Velocity sabit kalsın ki anomali mi yoksa normal durma koşulu mu anlaşılsın.
        {
            speed = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (this.gameObject.transform.position.z < -31f && !checkPoint)//Araç ışıkları geçtikten sonra aracın puanı ait olduğu yolun puanından
                                                                       // düşülsün. Sadece bir kere düşülmesi için checkPoint Flag'ı tutulsun.
        {
            if (this.gameObject.name.Equals("Bus_2") || this.gameObject.name.Equals("Bus_2(Clone)"))//Araç otobüs ise 2 araba ise 1 puan düşülsün.

                check.GetComponent<Point>().point2 -= 2;

            else

                check.GetComponent<Point>().point2 -= 1;

            checkPoint = true;
        }

        if (flag == false && this.gameObject.transform.position.z < -29.85f)//Trafik ışığını halihazırda geçmiş aracın
                                                                            //sarı ışık yanınca durmaması için alınan bir önlem.
            GetComponent<BoxCollider>().isTrigger = true;

        if (speed == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            stopTimer += Time.deltaTime;

            if (stopTimer > 4f && script.green_2.activeInHierarchy)

                speed = 0.75f;
        }

        else

            stopTimer = 0f;

        if (this.gameObject.transform.position.z < -30.2f)//Trafik ışığını halihazırda geçmiş aracın
                                                          //sarı ışık yanınca durmaması için alınan bir önlem.
            flag = false;

        if (dif > 30f)//Alınan mesafe bu kadarsa araç yok edilsin. Haritanın sonuna gelebilmesi için kat etmesi gereken mesafe.

            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)//Collision yaşandığı an bir kere çalışır.

    {
        if (collision.gameObject.CompareTag("Vehicle") || collision.gameObject.CompareTag("Detector"))//Çarpan cismin araç olduğunun kontrolü.

        {
            flag = true;

            if (collision.gameObject.CompareTag("Detector") && this.gameObject.transform.position.z < -30.2f)//Tekrardan halihazırda trafik
                                                                                                             //ışığını geçmiş aracın sarı
                flag = false;                                                                              //ışık yanınca durmaması için alınan   
        }                                                                                                 //bir önlem.
    }

    private void OnCollisionStay(Collision collision)//Cisimler çarpmaya devam ettiği her karede çalışır ta ki araçların
                                                     //çarpışması bitene kadar.
    {
        try
        {
            if (script.green_2.activeInHierarchy)//Yeşil ışık yandıysa en öndeki aracın ilerlemesi için flag false olmalı.
                                                 //Mantıken bütün araçların flag'i aynı anda false olacağı için ve araçlar
            {                                  //aynı anda hareket edecekleri için ekstra kontrole ihtiyaç duyulmamıştır.
                flag = false;
            }
        }

        catch (Exception e)

        {
            Debug.LogException(e, this);
        }
    }

    private void OnCollisionExit(Collision collision)//Collision bittiği zaman sadece bir kere çalışır.

    {
        speed = 0.75f;
    }
}