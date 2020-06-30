using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleType : MonoBehaviour
{
    private int point = 1;
    public int pointCheck;
    void Start()
    {
        pointCheck = point;

        if (this.gameObject.name.Equals("Bus_2(Clone)"))

            SetPoint();
    }
    private void SetPoint()
    {
        point = 2;
    }
    public int GetPoint()
    {
        return point;
    }
}
