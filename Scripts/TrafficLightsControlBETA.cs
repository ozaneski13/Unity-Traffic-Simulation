using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Threading;
using UnityEngine.UI;

public class TrafficLightsControlBETA : MonoBehaviour
{
    public GameObject red_1, yellow_1, green_1;//Yol_1'in trafik ışıkları.
    public GameObject red_2, yellow_2, green_2;//Yol_2'nin trafik ışıkları.
    public GameObject detector1, detector2;//Yol_1 ve Yol_2'nin Collision Detector'leri.
    //public GameObject check;//Yolların puanlamasını yapan Script'in bulunduğu değişken.
    private Collider d1c, d2c;//Detectorlerin BoxCollider'ları.
    public ImageUI myUI;
    public float timer = 0.0f;//Sayaç.
    private int sure, sure2;
    public int aracSayisi, aracSayisi2;
    public float greenTime;
    private string[] str;

    void Start()
    {
        greenTime = 10f;
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

    public void readTextA()
    {
        string[] str = System.IO.File.ReadAllLines("Assets/A.txt");
        Debug.Log("sure gerçek : " + str[0]);
        sure = int.Parse(str[0]);
        //sure = float.Parse(str[0]);//Convert.ToInt32(str[0]);
        //sure = (float)Math.Round(sure, 2);
        Debug.Log("A sure:" + sure);
        aracSayisi = Convert.ToInt32(str[1]);
    }

    public void readTextB()
    {
        string[] str = System.IO.File.ReadAllLines("Assets/B.txt");
        Debug.Log("sure gerçek : " + str[0]);
        sure2 = int.Parse(str[0]);
        //sure2 = float.Parse(str[0]);//Convert.ToInt32(str[0]);
        //sure2 = (float)Math.Round(sure2, 2);
        Debug.Log("B sure:" + sure2);
        aracSayisi2 = Convert.ToInt32(str[1]);
    }
    IEnumerator RoadOneLights()

    {
        timer = 0;

        while (true)//Bu döngü birinci yoldaki trafik akışını başlatmak için çalışmaktadır. Zaman aralıklarına
                    //gelindikçe ışıkların rengi değişmekte ve araçlar bu ışıklara göre hareket etmektedir.
        {
            if (timer > greenTime && timer < greenTime + 3f)

            {
                yellow_1.SetActive(true);

                yellow_2.SetActive(true);
                green_2.SetActive(false);
            }

            if (timer > greenTime + 3f && timer < greenTime + 6f)

            {
                red_1.SetActive(false);
                yellow_1.SetActive(false);
                green_1.SetActive(true);

                red_2.SetActive(true);
                yellow_2.SetActive(false);
            }

            if (timer > greenTime + 6f)

            {
                yellow_2.SetActive(false);
                break;
            }

            // if (timer > 30f)

            //    break;

            yield return null;//Coroutine'in çalışmaya devam etmesi yield return null değeri döndürülmelidir.
        }                    //yield return değeri Coroutine'i durdurmakta ve istenilen başka bir Function'ı 
                             //veyahut Coroutine'i çağırmakta kullanılabilir. Çağırıldığında içinde bulunan kodun çalışması durdurulur.
                             //Bir dahaki karede durdurulan koda döndüğünde ise kaldığı satırdan devam eder. 
                             //yield return değeri bir yeri işaret etmediği takdirde while döngüsü Loop'a girmekte ve Unity çakılmaktadır.
        readTextA();
        readTextB();
        greenTime = ((float)((aracSayisi2) * 3.6f)/2) - (float)(((float)aracSayisi / (float)aracSayisi2) * (((float)sure + 1) / ((float)sure2 + 1)));

       
        if (greenTime < 0)

            greenTime *= -1;
        if (greenTime <= 3)
        {
            greenTime += 2;
        }
        if (greenTime >= 11)
        {
            greenTime = 10;
        }
        Debug.Log("B GT:" + greenTime);
        greenTime = Mathf.RoundToInt(greenTime);
        if (greenTime > 30f)
            greenTime = 30;
        myUI.GetComponent<ImageUI>().tempTime = (int)greenTime;
        timer = 0;
        StopCoroutine(RoadOneLights());//Şuan içinde bulunduğumuz Coroutine durdurularak olası istenmeyen paralel çalışmanın önüne geçilir.
        yield return StartCoroutine(RoadTwoLights());//İkinci yoldaki trafik ışıklarını kontrol eden Coroutine çalıştırılır.
    }

    IEnumerator RoadTwoLights()

    {
        timer = 0;

        while (true)

        {
            if (timer > greenTime && timer < greenTime + 3f)

            {
                yellow_2.SetActive(true);

                yellow_1.SetActive(true);
                green_1.SetActive(false);
            }

            if (timer > greenTime + 3f && timer < greenTime + 6f)

            {
                red_2.SetActive(false);
                yellow_2.SetActive(false);
                green_2.SetActive(true);

                red_1.SetActive(true);
                yellow_1.SetActive(false);
            }

            if (timer > greenTime + 6f)

            {
                yellow_1.SetActive(false);
                break;
            }

            //if (timer > 30f)

            //  break;

            yield return null;
        }


        readTextA();
        readTextB();
        greenTime = ((float)((aracSayisi) * 3.6f)/2) - (float)(((float)aracSayisi2 / (float)aracSayisi) * (((float)sure2 + 1) / ((float)sure + 1)));
       
        if (greenTime < 0)

            greenTime *= -1;

        if (greenTime <= 3)
        {
            greenTime += 2;
        }
        if(greenTime >= 11)
        {
            greenTime = 10;
        }
        Debug.Log("A GT:" + greenTime);
        greenTime = Mathf.RoundToInt(greenTime);
        if (greenTime > 30f)
            greenTime = 30;
        myUI.GetComponent<ImageUI>().tempTime = (int)greenTime;
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
}
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
            if (timer >10.0f && timer < 13.0f)

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
