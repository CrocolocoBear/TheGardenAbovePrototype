using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on Glynn Taylors guide on day and night https://youtu.be/m9hj9PdO328
//Changes time between 0 and 24, Is always on to be able to show time of day in scene view

[ExecuteAlways]
public class TimeManagement : MonoBehaviour
{
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    private void Update()
    {

        if (Application.isPlaying)
        {
            TimeChange();

            //Debug.Log(TimeOfDay);
        }
    }

    //Currently uses a check if space is pressed to speed up time, will be changed in future.
    public void TimeChange()
    {
        //speeds up time
        if (Input.GetKey(KeyCode.T))
        {
            TimeOfDay += Time.deltaTime*2;
        }
        else
        //normal speed
        {
            TimeOfDay += Time.deltaTime / 6;
        }
        TimeOfDay %= 24; //Modulus to ensure always between 0-24
    }
}
