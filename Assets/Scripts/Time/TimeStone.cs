using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStone : MonoBehaviour
{
    private float TimeOfDay;


    // Update is called once per frame
    void Update()
    {
        //(Replace with a reference to the game time)
        TimeOfDay = GetComponentInParent<TimeManagement>().TimeOfDay;
    }
}
