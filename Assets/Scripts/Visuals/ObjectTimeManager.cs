using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeManager : MonoBehaviour
{
    //Input Light through serialized field
    [SerializeField] private GameObject Obj = null;
    private float TimeOfDay;


    // Update is called once per frame
    void Update()
    {
        //Get time of day from parent
        TimeOfDay = GetComponentInParent<TimeManagement>().TimeOfDay;
        if (Application.isPlaying)
        {
            UpdateObject(TimeOfDay / 24f);
        }
        else
        {
            UpdateObject(TimeOfDay / 24f);
        }
    }

    private void UpdateObject(float timePercent)
    {
        if (Obj != null)
        {
            //Checks if it is turning into night or day
            if (timePercent < 0.25f || timePercent > 0.7f)
            {
                //Deactivates mesh if dawn
                if (timePercent < 0.25f && timePercent > 0.1f)
                {
                    Obj.GetComponent<MeshRenderer>().enabled = false;
                }
                //Activates mesh if dusk
                else if (timePercent < 0.8f && timePercent > 0.7f)
                {
                    Obj.GetComponent<MeshRenderer>().enabled = true;
                }
                //Makes sure it is on at night
                else
                {
                    Obj.GetComponent<MeshRenderer>().enabled = true;
                }

            }
            //Makes sure it is off during day
            else
            {
                //Obj.SetActive(false);
                Obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }

    }
}
