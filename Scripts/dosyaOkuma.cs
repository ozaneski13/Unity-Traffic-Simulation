using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class dosyaOkuma : MonoBehaviour
{
    // Start is called before the first frame update

    public int i=0;
    public int carA,carB;
    void Start()
    {
        //InvokeRepeating("oku",2.0f,1.0f);
        
    }

    void oku()
    {
        if(i == 0)
        {
            string dosya = "C:/Users/User/Desktop/TrafficSimulation2/A.txt";


            FileStream fs = new FileStream(dosya, FileMode.Open, FileAccess.Read);
            StreamReader rd = new StreamReader(fs);

            string line = rd.ReadLine();

            string[] parcalar; //tek bir satırda hem bekleme süresi hem de araç sayısı olduğu için bunları ayırmak gerekiyor.
            int beklemeSureleri;  //birden fazla satır olabilir. Her bir satırdaki verileri alabilmek için dizi gerekli.

            int aracSayilari;


            parcalar = line.Split(' ');

            beklemeSureleri = Convert.ToInt32(parcalar[0]);
            aracSayilari = Convert.ToInt32(parcalar[1]);

            line = rd.ReadLine();
            carA = aracSayilari;

            i = 1;
            rd.Close();
            fs.Close();

        }

        else if( i == 1)
        {
            string dosya = "C:/Users/User/Desktop/TrafficSimulation2/B.txt";


            FileStream fs = new FileStream(dosya, FileMode.Open, FileAccess.Read);
            StreamReader rd = new StreamReader(fs);

            string line = rd.ReadLine();

            string[] parcalar; //tek bir satırda hem bekleme süresi hem de araç sayısı olduğu için bunları ayırmak gerekiyor.
            int beklemeSureleri ;  //birden fazla satır olabilir. Her bir satırdaki verileri alabilmek için dizi gerekli.

            int aracSayilari ;

    
            
                parcalar = line.Split(' ');

                beklemeSureleri = Convert.ToInt32(parcalar[0]);
                aracSayilari = Convert.ToInt32(parcalar[1]);

        
            line = rd.ReadLine();
            carB = aracSayilari;

            i = 0;
            rd.Close();
            fs.Close();


        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
