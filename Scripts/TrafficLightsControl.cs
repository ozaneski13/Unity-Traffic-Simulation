using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsControl : MonoBehaviour
{
    public GameObject red_1, yellow_1, green_1;//Yol_1'in trafik ışıkları.
    public GameObject red_2, yellow_2, green_2;//Yol_2'nin trafik ışıkları.
    public GameObject detector1, detector2;//Yol_1 ve Yol_2'nin Collision Detector'leri.
    public GameObject check;
    private Collider d1c, d2c;//Detectorlerin BoxCollider'ları.
    //public float greentime1, redtime1, greentime2, redtime2;//Algoritmaya göre ayarlanacak sürelerin değişkenleri.
    public int getPoint1, getPoint2;
    private float timer = 0;//Sayaç.
    
    void Start()
    {
        check = GameObject.Find("Check");
        d1c = detector1.GetComponent<BoxCollider>();//Işıkların altındaki Detector'lerin BoxCollider'larına rahatlıkla 
        d2c = detector2.GetComponent<BoxCollider>();//erişmek için tanımlanan değişkenler.

        red_1.SetActive(true);//Trafik ışıklarının başlangıç durumlarının tanımlanması.
        yellow_1.SetActive(false);
        green_1.SetActive(false);

        red_2.SetActive(false);
        yellow_2.SetActive(false);
        green_2.SetActive(true);
    }

    void Update()
    {
        timer += Time.deltaTime;//Script'in başlamasından itibaren geçen sürenin hesaplanması.
        getPoint1 = check.GetComponent<Point>().point1;
        getPoint2 = check.GetComponent<Point>().point2;

        if (red_1.activeInHierarchy == true)//Kırmızı aktifse ışığa gelince araçların durması için Detector'un BoxCollider'ı aktif hale getirilmeli.

            d1c.enabled = true;

        else if (red_1.activeInHierarchy == false)//Kırmızı ışık sönünce aracın hareket etmesi için Detector'un BoxCollider pasif hale getirilmeli.

            d1c.enabled = false;

        if (red_2.activeInHierarchy == true)

            d2c.enabled = true;

        else if (red_2.activeInHierarchy == false)

            d2c.enabled = false;

        if (timer > 10.0f)//Algoritmasız ışıkların basit yanıp sönmesi.

        {
            yellow_1.SetActive(true);

            yellow_2.SetActive(true);
            green_2.SetActive(false);
        }

        if (timer > 12.0f)

        {
            red_1.SetActive(false);
            yellow_1.SetActive(false);
            green_1.SetActive(true);

            red_2.SetActive(true);
            yellow_2.SetActive(false);
        }

        if (timer > 22.0f)

        {
            yellow_2.SetActive(false);
        }
    }
}
        /*public float timer = 0;
        public int light;

        // Start is called before the first frame update
        void Start()
        {
            if (light == 2)

                gameObject.active = false;
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;


            if (timer > 1.0f && light == 2)

            {
                gameObject.active = true;
            }

            if (timer < 2.0f && light == 1 || light == 2) 

                return;

            else if (timer > 2.0f && light == 1 || light == 2)  

            { 
                gameObject.active = false;
            }
    }*/