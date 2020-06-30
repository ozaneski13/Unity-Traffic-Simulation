using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement1 : MonoBehaviour

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
    private float tempx;
    /*public Vector3 middlePointCheck = new Vector3(7f, -5.8f, -31);
    public Vector3 radius = new Vector3(0.3f, 0f, 0.3f);*/
    void Start()
    {
        tempx = this.gameObject.transform.position.x;
        stopTimer = 0f;
        check = GameObject.Find("Check");//Point Script'ine ulaşmak için tanımlanan değişken.
        rb = GetComponent<Rigidbody>(); //Aracın Rigidbody'sine ulaşmak için tanımlanan değişken.

        scriptObject = GameObject.Find("TrafficControl");//Işıkların durumunu kontrol edebilmek için Script'e ulaşım sağlanması.
        script = scriptObject.GetComponent<TrafficLightsControlBETA>();//TrafficLightsControlBETA Script'ine erişim sağlanması.

        targetRotation = transform.rotation;//Alınan mesafeyi hesaplamak için tutylan değişken.
    }

    void Update()
    {
        dif = (targetRotation.x - transform.position.x) + (targetRotation.z - transform.position.z);//Her karede katedilen mesafe hesabı.
        transform.Translate(speed * Vector3.forward * Time.deltaTime);//Kareler arası geçen zamana göre araçları ilerletir.

        if (tempx < this.gameObject.transform.position.x)

            if (this.gameObject.transform.position.x > 15f)

                Destroy(this.gameObject);

        tempx = this.gameObject.transform.position.x;

        if (script.red_1.activeInHierarchy && !flag)//Kırmızı yandıysa ve Collision yaşanmadıysa harekete devam etmeli.
                                                    //Yani araç kırmızı ışığın altına gelene kadar hareket etmeli.
        {
            speed = 0.75f;

            /*if (Physics.OverlapBox(middlePointCheck, radius).Length > 0)

                speed = 0;*/
        }

        else if ((script.red_1.activeInHierarchy || (!script.red_1 && script.yellow_1 && !script.green_1)) && flag)//Kırmızı yandıysa VE Collision yaşandıysa araç durmalı.
                                                                                                                   //Kırmızıya gelene kadar bir araçla Collision yaşanmalı veya
        {                                                                                                        //kırmızı ışığın Collider'ı ile Collision yaşanmış olmalı.
            speed = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        else if (!script.red_1.activeInHierarchy && !flag)//Kırmızı ışık yanmıyorsa ve Collision yaşanmadıysa hareket etmeli.

        {
            speed = 0.75f;

            /*if (Physics.OverlapBox(middlePointCheck, radius).Length > 0 && !(this.gameObject.transform.position.x < 7.9f) && !(this.gameObject.transform.position.x > 6.1f))

                speed = 0;

            else speed = 1;*/
        }

        else//Yaşanacak bir anomali durumunda araç dursun. Ama Velocity sabit kalsın ki anomali mi yoksa normal durma koşulu mu anlaşılsın.
        {
            speed = 0;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (this.gameObject.transform.position.x < 7f && !checkPoint)//Araç ışıkları geçtikten sonra aracın puanı ait olduğu yolun puanından
                                                                     // düşülsün. Sadece bir kere düşülmesi için checkPoint Flag'ı tutulsun.
        {
            if (this.gameObject.name.Equals("Bus_2") || this.gameObject.name.Equals("Bus_2(Clone)"))//Araç otobüs ise 2 araba ise 1 puan düşülsün.

                check.GetComponent<Point>().point1 -= 2;

            else

                check.GetComponent<Point>().point1 -= 1;

            checkPoint = true;
        }

        if (flag == false && this.gameObject.transform.position.x < 8)//Trafik ışığını geçtiği noktada aracı isTrigger yaparak olası
                                                                      //Collider çarpışmaları önlenir.
            GetComponent<BoxCollider>().isTrigger = true;

        if (speed == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            stopTimer += Time.deltaTime;

            if (stopTimer > 4f && script.green_1.activeInHierarchy)

                speed = 0.75f;
        }

        else

            stopTimer = 0f;

        if (this.gameObject.transform.position.x < 8.0f)//Trafik ışığını halihazırda geçmiş aracın
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

            if (collision.gameObject.CompareTag("Detector") && this.gameObject.transform.position.x < 8.0f)//Tekrardan halihazırda trafik 
                                                                                                           //ışığını geçmiş aracın sarı 
                flag = false;                                                                            //ışık yanınca durmaması için alınan
        }                                                                                               //bir önlem.
    }

    private void OnCollisionStay(Collision collision)//Cisimler çarpmaya devam ettiği her karede 
                                                     //çalışır ta ki araçların çarpışması bitene kadar.
    {
        try
        {
            if (script.green_1.activeInHierarchy)//Yeşil ışık yandıysa en öndeki aracın ilerlemesi için flag değişkeni false olmalı.
                                                 //Mantıken bütün araçların flag değişkeni aynı anda false olacağı için ve araçlar
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