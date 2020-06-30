/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Threading;

public class TrafficLightsControlBETA : MonoBehaviour
{
    public GameObject red_1, yellow_1, green_1;//Yol_1'in trafik ışıkları.
    public GameObject red_2, yellow_2, green_2;//Yol_2'nin trafik ışıkları.
    public GameObject detector1, detector2;//Yol_1 ve Yol_2'nin Collision Detector'leri.
    //public GameObject check;//Yolların puanlamasını yapan Script'in bulunduğu değişken.
    private Collider d1c, d2c;//Detectorlerin BoxCollider'ları.
    public float greenTimer, greenTimer2, yellowTimer;//Algoritmaya göre ayarlanacak sürelerin değişkenleri.
    public int getPoint1, getPoint2;//Anlık puana göre hesap yapılması için Point Script'inden çekilen puanların tutulduğu değişkenler.
    public float timer = 0.0f;//Sayaç.
    public int carCount1 = 0, carCount2 = 0; // yol a ve yol b 'deki anlık araba sayıları
    public float sure, sure2;
    public int aracSayisi, aracSayisi2;
    public float greenTime;
    private bool checka, checkb;
    private string text;
    void Start()
    {
        greenTime = 10f;
        checka = false;
        checkb = false;
        //InvokeRepeating("dosyaOkuma", 0.0f, 1.0f);

        //check = GameObject.Find("Check");//Puanlama Script'ine erişim.


        d1c = detector1.GetComponent<BoxCollider>();//Işıkların altındaki Detector'lerin BoxCollider'larına
        d2c = detector2.GetComponent<BoxCollider>();//erişmek için tanımlanan değişkenler.

        red_1.SetActive(true);//Trafik ışıklarının başlangıç durumlarının tanımlanması.
        yellow_1.SetActive(false);
        green_1.SetActive(false);

        red_2.SetActive(false);
        yellow_2.SetActive(false);
        green_2.SetActive(true);
        StartCoroutine(RoadOneLights());//Birinci yol için Coroutine'in çalıştırılması.
    }

    public void calculateGreenTime()
    {
        readTextA();
        readTextB();
        UnityEngine.Debug.Log("aracsayisi:" + aracSayisi);
        greenTime = (aracSayisi - aracSayisi2) * 2.7f + (sure - sure2) * (aracSayisi / aracSayisi2);
    }

    public void readTextA()
    {

        while (true)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\User\Desktop\TrafficSimulation2\A.txt");
            string sureStr = file.ReadLine();//süreyi okudu
            string aracSayisiStr = file.ReadLine();//araç sayısını okudu
            if (sureStr == null || aracSayisiStr == null)
            {
                file.Close();
                continue;
            }
            sure = float.Parse(sureStr);
            aracSayisi = int.Parse(aracSayisiStr);

            file.Close();
        }
    }

    public void readTextB()
    {

        while (true)
        {

            System.IO.StreamReader file2 = new System.IO.StreamReader(@"C:\Users\User\Desktop\TrafficSimulation2\B.txt");
            string sureStr2 = file2.ReadLine();//süreyi okudu
            string aracSayisiStr2 = file2.ReadLine();//araç sayısını okudu
            if (sureStr2 == null || aracSayisiStr2 == null)
            {
                file2.Close();
                continue;
            }
            sure2 = float.Parse(sureStr2);
            aracSayisi2 = int.Parse(aracSayisiStr2);

            file2.Close();
        }
    }


    IEnumerator RoadOneLights()

    {
        /*if (checka)
            calculateGreenTime();

        checka = true;
        while (true)//Bu döngü birinci yoldaki trafik akışını başlatmak için çalışmaktadır. Zaman aralıklarına
                    //gelindikçe ışıkların rengi değişmekte ve araçlar bu ışıklara göre hareket etmektedir.
        {
            

            if (timer > 10f && timer < 13f)

            {
                yellow_1.SetActive(true);

                yellow_2.SetActive(true);
                green_2.SetActive(false);
            }

            if (timer > 13f && timer < 16f)

            {
                red_1.SetActive(false);
                yellow_1.SetActive(false);
                green_1.SetActive(true);

                red_2.SetActive(true);
                yellow_2.SetActive(false);
            }

            if (timer > 16f)

            {
                yellow_2.SetActive(false);
                break;
            }

            yield return null;//Coroutine'in çalışmaya devam etmesi yield return null değeri döndürülmelidir.
        }                    //yield return değeri Coroutine'i durdurmakta ve istenilen başka bir Function'ı 
                             //veyahut Coroutine'i çağırmakta kullanılabilir. Çağırıldığında içinde bulunan kodun çalışması durdurulur.
                             //Bir dahaki karede durdurulan koda döndüğünde ise kaldığı satırdan devam eder. 
                             //yield return değeri bir yeri işaret etmediği takdirde while döngüsü Loop'a girmekte ve Unity çakılmaktadır.

        timer = 0;//Timer sıfırlanarak ikinci yoldaki ışıkların çalıştırılması için hazırlanır.
        StopCoroutine(RoadOneLights());//Şuan içinde bulunduğumuz Coroutine durdurularak olası istenmeyen paralel çalışmanın önüne geçilir.
        yield return StartCoroutine(RoadTwoLights());//İkinci yoldaki trafik ışıklarını kontrol eden Coroutine çalıştırılır.
    }

    IEnumerator RoadTwoLights()

    {
        /*if (checkb)
            calculateGreenTime();

        checkb = true;*//*

        while (true)

        {
            

            if (timer > 10f && timer <13f)

            {
                yellow_2.SetActive(true);

                yellow_1.SetActive(true);
                green_1.SetActive(false);
            }

            if (timer > 13f && timer < 16f)

            {
                red_2.SetActive(false);
                yellow_2.SetActive(false);
                green_2.SetActive(true);

                red_1.SetActive(true);
                yellow_1.SetActive(false);
            }

            if (timer > 16f)

            {
                yellow_1.SetActive(false);
                break;
            }

            yield return null;
        }

        timer = 0;
        StopCoroutine(RoadTwoLights());
        yield return StartCoroutine(RoadOneLights());
    }

    void Update()
    {
        timer += Time.deltaTime;//Script'in başlamasından itibaren geçen sürenin hesaplanması.
        //getPoint1 = check.GetComponent<Point>().point1;//Yolların puanına ulaşmak için tanımlanması gereken değişkenler.
        //getPoint2 = check.GetComponent<Point>().point2;

        if (red_1.activeInHierarchy || (!red_1.activeInHierarchy && yellow_1.activeInHierarchy && !green_1.activeInHierarchy))
            //Kırmızı aktifse ışığa gelince araçların durması için Detector'un BoxCollider'ı aktif hale getirilmeli.
            d1c.enabled = true;

        else if (!red_1.activeInHierarchy && !yellow_1.activeInHierarchy)
            //Kırmızı ışık sönünce aracın hareket etmesi için Detector'un BoxCollider pasif hale getirilmeli.
            d1c.enabled = false;

        if (red_2.activeInHierarchy || (!red_2.activeInHierarchy && yellow_2.activeInHierarchy && !green_2.activeInHierarchy))

            d2c.enabled = true;

        else if (!red_2.activeInHierarchy && !yellow_2.activeInHierarchy)

            d2c.enabled = false;



    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsControlBETA : MonoBehaviour
{
    public GameObject red_1, yellow_1, green_1;//Yol_1'in trafik ışıkları.
    public GameObject red_2, yellow_2, green_2;//Yol_2'nin trafik ışıkları.
    public GameObject detector1, detector2;//Yol_1 ve Yol_2'nin Collision Detector'leri.
    public GameObject check;//Yolların puanlamasını yapan Script'in bulunduğu değişken.
    private Collider d1c, d2c;//Detectorlerin BoxCollider'ları.
    //public float greentime1, redtime1, greentime2, redtime2;//Algoritmaya göre ayarlanacak sürelerin değişkenleri.
    public int getPoint1, getPoint2;//Anlık puana göre hesap yapılması için Point Script'inden çekilen puanların tutulduğu değişkenler.
    public float timer = 0.0f;//Sayaç.

    void Start()
    {
        check = GameObject.Find("Check");//Puanlama Script'ine erişim.
        d1c = detector1.GetComponent<BoxCollider>();//Işıkların altındaki Detector'lerin BoxCollider'larına
        d2c = detector2.GetComponent<BoxCollider>();//erişmek için tanımlanan değişkenler.

        red_1.SetActive(true);//Trafik ışıklarının başlangıç durumlarının tanımlanması.
        yellow_1.SetActive(false);
        green_1.SetActive(false);

        red_2.SetActive(false);
        yellow_2.SetActive(false);
        green_2.SetActive(true);

        StartCoroutine(RoadOneLights());//Birinci yol için Coroutine'in çalıştırılması.
    }

    IEnumerator RoadOneLights()

    {
        while (true)//Bu döngü birinci yoldaki trafik akışını başlatmak için çalışmaktadır. Zaman aralıklarına
                    //gelindikçe ışıkların rengi değişmekte ve araçlar bu ışıklara göre hareket etmektedir.
        {
            if (timer > 10.0f && timer < 13.0f)

            {
                yellow_1.SetActive(true);

                yellow_2.SetActive(true);
                green_2.SetActive(false);
            }

            if (timer > 13.0f && timer < 16.0f)

            {
                red_1.SetActive(false);
                yellow_1.SetActive(false);
                green_1.SetActive(true);

                red_2.SetActive(true);
                yellow_2.SetActive(false);
            }

            if (timer > 16.0f)

            {
                yellow_2.SetActive(false);
                break;
            }

            yield return null;//Coroutine'in çalışmaya devam etmesi yield return null değeri döndürülmelidir.
        }                    //yield return değeri Coroutine'i durdurmakta ve istenilen başka bir Function'ı 
                             //veyahut Coroutine'i çağırmakta kullanılabilir. Çağırıldığında içinde bulunan kodun çalışması durdurulur.
                             //Bir dahaki karede durdurulan koda döndüğünde ise kaldığı satırdan devam eder. 
                             //yield return değeri bir yeri işaret etmediği takdirde while döngüsü Loop'a girmekte ve Unity çakılmaktadır.

        timer = 0;//Timer sıfırlanarak ikinci yoldaki ışıkların çalıştırılması için hazırlanır.
        StopCoroutine(RoadOneLights());//Şuan içinde bulunduğumuz Coroutine durdurularak olası istenmeyen paralel çalışmanın önüne geçilir.
        yield return StartCoroutine(RoadTwoLights());//İkinci yoldaki trafik ışıklarını kontrol eden Coroutine çalıştırılır.
    }

    IEnumerator RoadTwoLights()

    {
        while (true)

        {
            if (timer > 10.0f && timer < 13.0f)

            {
                yellow_2.SetActive(true);

                yellow_1.SetActive(true);
                green_1.SetActive(false);
            }

            if (timer > 13.0f && timer < 16.0f)

            {
                red_2.SetActive(false);
                yellow_2.SetActive(false);
                green_2.SetActive(true);

                red_1.SetActive(true);
                yellow_1.SetActive(false);
            }

            if (timer > 16.0f)

            {
                yellow_1.SetActive(false);
                break;
            }

            yield return null;
        }

        timer = 0;
        StopCoroutine(RoadTwoLights());
        yield return StartCoroutine(RoadOneLights());
    }

    void Update()
    {
        timer += Time.deltaTime;//Script'in başlamasından itibaren geçen sürenin hesaplanması.
        getPoint1 = check.GetComponent<Point>().point1;//Yolların puanına ulaşmak için tanımlanması gereken değişkenler.
        getPoint2 = check.GetComponent<Point>().point2;

        if (red_1.activeInHierarchy || (!red_1.activeInHierarchy && yellow_1.activeInHierarchy && !green_1.activeInHierarchy))
            //Kırmızı aktifse ışığa gelince araçların durması için Detector'un BoxCollider'ı aktif hale getirilmeli.
            d1c.enabled = true;

        else if (!red_1.activeInHierarchy && !yellow_1.activeInHierarchy)
            //Kırmızı ışık sönünce aracın hareket etmesi için Detector'un BoxCollider pasif hale getirilmeli.
            d1c.enabled = false;

        if (red_2.activeInHierarchy || (!red_2.activeInHierarchy && yellow_2.activeInHierarchy && !green_2.activeInHierarchy))

            d2c.enabled = true;

        else if (!red_2.activeInHierarchy && !yellow_2.activeInHierarchy)

            d2c.enabled = false;
    }
}*/