﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading;

public class SpawnCar : MonoBehaviour
{
    public int carTypeDice;//Rastgele yaratılacak araç türü için belirlenen değişken.
    public int i = 0;//myGameObject dizisini ilerletecek iterator.
    private int sp;
    public float timeCounter;
    public bool flag = false;//Sürekli araç yaratılacağı için özellikler eklenirken doğru araca eklendiğinin kontrolü için tanımlanan flag.
    private bool checkSP;
    public GameObject[] myGameObject = new GameObject[100];//Yaratılan araçların tutulduğu array.
    public GameObject check;//Rastgele yaratılacak aracın türüne göre puanlara ulaşmak için tanımlanan değişken.
    public Vector3 spawnPoint;//Hangi yoldan Spawn edileceğine göre koordinat tutacak değişken.
    public Vector3 spawnPointCheck;//Araçların yaratıldığı noktanın başka bir GameObject tarafından işgal edilip edilmediğini kontrol edecek koordinatları tutan Vector3 değişkeni.
    public Vector3 radius;//spawnPointCheck koordinatının hangi aralığına bakılacak olduğunun tanımlayan Vector3 değişkeni.
    public Quaternion spawnRotaion;//Hangi yoldan Spawn edileceğine göre araçların yönünü tutacak değişken.
    void Start()
    {
        checkSP = true;
        check = GameObject.Find("Check");//Puanların hesaplandığı Script'e ulaşım.
        timeCounter = 5.0f;
        /*myGameObject = Resources.LoadAll("Resources", typeof(GameObject)).Cast<GameObject>().ToArray();*/

        if (this.gameObject.CompareTag("SpawnPoint_1"))//Aracın yaratılacak koordinatlarının belirlenmesi ve koordinat kontrolü.

        {
            sp = 1;
            spawnRotaion = Quaternion.Euler(0f, -90f, 0f);//Yaratılacak aracın yaratım noktasına göre yönünün tanımlanması.
            spawnPoint = new Vector3(13f, -5.9f, -31.2f);//Yaratılacak aracın yaratım noktasına göre başlangıç koordinatlarının tanımlanması.
            spawnPointCheck = new Vector3(13f, -5.85f, -31.2f);//Yaratılacak aracın başlangıç koordinatında başka araç var mı diye yapılacak konrolün aralığı.
            radius = new Vector3(0.5f, 0, 0);//Yapılacak kontrolün yarıçapı.
        }

        else if (this.gameObject.CompareTag("SpawnPoint_2"))

        {
            sp = 2;
            spawnRotaion = Quaternion.Euler(0f, -90f, 0f);
            spawnPoint = new Vector3(13f, -5.9f, -30.8f);
            spawnPointCheck = new Vector3(13f, -5.85f, -30.8f);
            radius = new Vector3(0.5f, 0, 0);
        }

        else if (this.gameObject.CompareTag("SpawnPoint_3"))

        {
            sp = 3;
            spawnRotaion = Quaternion.Euler(0f, -180f, 0f);
            spawnPoint = new Vector3(7.25f, -5.9f, -25f);
            spawnPointCheck = new Vector3(7.25f, -5.85f, -25f);
            radius = new Vector3(0, 0, 0.5f);
        }

        else if (this.gameObject.CompareTag("SpawnPoint_4"))

        {
            sp = 4;
            spawnRotaion = Quaternion.Euler(0f, -180f, 0f);
            spawnPoint = new Vector3(6.75f, -5.9f, -25f);
            spawnPointCheck = new Vector3(6.75f, -5.85f, -25f);
            radius = new Vector3(0, 0, 0.5f);
        }

        StartCoroutine(SpawnGameObject());
    }

    IEnumerator SpawnGameObject()//4 farklı Prefab arasından rastgele seçerek GameObject oluşturulur.

    {
        if ((sp == 1 || sp == 3) && check)
        {
            yield return new WaitForSeconds(0.5f);
            checkSP = false;
        }

        if (Physics.OverlapBox(spawnPointCheck, radius).Length > 0)//Araçların yaratılacak koordinat aralığında GameObject mevcut mu kontrolü.

            Debug.Log("Araç mevcut! Yeni araç yaratılmadı!");

        else

        {
            carTypeDice = UnityEngine.Random.Range(0, 4);//Yaratılacak araç türünün belirlenmesi.*/

            if (carTypeDice == 0)

                myGameObject[i] = Resources.Load("Bus_2", typeof(GameObject)) as GameObject;//Prefab'ler Resources klasöründen alınarak
                                                                                            //diziye atılır.
            else if (carTypeDice == 1)

                myGameObject[i] = Resources.Load("Car_1", typeof(GameObject)) as GameObject;

            else if (carTypeDice == 2)

                myGameObject[i] = Resources.Load("Police_car", typeof(GameObject)) as GameObject;

            else if (carTypeDice == 3)

                myGameObject[i] = Resources.Load("Taxi", typeof(GameObject)) as GameObject;

            myGameObject[i].tag = "Vehicle";//Diziye atılan araçlara diğer Script'lerde kullanılması için "Vehicle" Tag'i tanımlanır.

            if (!flag)//myGameObject dizisinin i. elemanı boş mu diğer kontrol edilir.
                      //Boş ise elemanın içinde olmayan özellikler kontrol edilerek eklenir.
            {       //Dizinin içine atılan ve yaratılmak üzere olan araçların özelliklerinin tanımlanması.
                if (myGameObject[i].GetComponent(typeof(Rigidbody)) == null)//Yaratılacak araca RigidBody bileşeninin eklenmesi
                                                                            //ve bu bileşene özelliklerin tanımlanması.
                {
                    myGameObject[i].AddComponent(typeof(Rigidbody));
                    myGameObject[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                    myGameObject[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
                    myGameObject[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
                }

                if (myGameObject[i].GetComponent(typeof(BoxCollider)) == null)//Yaratılacak araca BoxCollider bileşeninin eklenmesi
                                                                              //ve bu bileşene özelliklerin tanımlanması.
                {
                    myGameObject[i].AddComponent(typeof(BoxCollider));

                    if (myGameObject[i].name == "Bus_2")//Farklı araçlar için farklı boyutta BoxCollider tanımlanması.

                        myGameObject[i].GetComponent<BoxCollider>().size = new Vector3(0, 0, 4.5f);

                    else

                        myGameObject[i].GetComponent<BoxCollider>().size = new Vector3(0, 0, 2.7f);
                }

                if (this.gameObject.CompareTag("SpawnPoint_1") || this.gameObject.CompareTag("SpawnPoint_2"))//Araçların yaratıldıkları
                                                                                                             //noktaya göre hareket Script'i
                {                                                                                          //tanımlanması.
                    if (this.gameObject.CompareTag("SpawnPoint_1"))

                    {
                        /*myGameObject[i].GetComponent<TurnLeft>().enabled = true;
                        myGameObject[i].GetComponent<TurnRight>().enabled = false;*/
                        myGameObject[i].GetComponent<TurnLeft>().enabled = false;
                        myGameObject[i].GetComponent<TurnRight>().enabled = false;
                    }

                    else if (this.gameObject.CompareTag("SpawnPoint_2"))

                    {
                        myGameObject[i].GetComponent<TurnLeft>().enabled = false;
                        myGameObject[i].GetComponent<TurnRight>().enabled = false;
                    }

                    myGameObject[i].GetComponent<CarMovement1>().enabled = true;
                    myGameObject[i].GetComponent<CarMovement2>().enabled = false;

                    if (myGameObject[i].name.Equals("Bus_2"))//Yaratılan aracın türüne içinde bulunduğu yola puan atamasının yapılması.
                                                             //Entegrasyon işlemleri yapıldıktan sonra kaldırılacaktır.
                        check.GetComponent<Point>().point1 += 2;

                    else

                        check.GetComponent<Point>().point1 += 1;
                }

                else if (this.gameObject.CompareTag("SpawnPoint_3") || this.gameObject.CompareTag("SpawnPoint_4"))

                {
                    if (this.gameObject.CompareTag("SpawnPoint_3"))

                    {
                        myGameObject[i].GetComponent<TurnLeft>().enabled = false;
                        myGameObject[i].GetComponent<TurnRight>().enabled = false;
                    }

                    else if (this.gameObject.CompareTag("SpawnPoint_4"))

                    {
                        /*myGameObject[i].GetComponent<TurnLeft>().enabled = false;
                        myGameObject[i].GetComponent<TurnRight>().enabled = true;*/
                        myGameObject[i].GetComponent<TurnLeft>().enabled = false;
                        myGameObject[i].GetComponent<TurnRight>().enabled = false;
                    }

                    myGameObject[i].GetComponent<CarMovement2>().enabled = true;
                    myGameObject[i].GetComponent<CarMovement1>().enabled = false;

                    if (myGameObject[i].name.Equals("Bus_2"))

                        check.GetComponent<Point>().point2 += 2;

                    else

                        check.GetComponent<Point>().point2 += 1;
                }

                myGameObject[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);//Yaaratılacak aracın boyutlarının ayarlanması.

                Instantiate(myGameObject[i], spawnPoint, spawnRotaion);//Aracın istenilen noktada ve yönde bakacak şekilde yaratılması.

                i++;//İterasyonun ilerletilmesi.
                flag = true;//Sıradaki aracın diziye atılmasına izin veren bayrak.
            }

            flag = false;
        }

        if (i == myGameObject.Length)//myGameObject dizisinin sonuna gelindi ise diziyi yeni gelecek araçlar için boşalt.

        {
            //Array.Clear(myGameObject, 0, myGameObject.Length);
            for (int j = 0; j < myGameObject.Length / 2; j++)

                myGameObject[j] = null;

            i = 0;
        }

        yield return new WaitForSeconds(timeCounter);
        StartCoroutine(SpawnGameObject());
    }
}