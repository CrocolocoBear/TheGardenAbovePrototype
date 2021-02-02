using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLightManager : MonoBehaviour
{
    //Input Light through serialized field
    [SerializeField] private Light SpotLight = null;
    private float TimeOfDay;
    [SerializeField] private float intensity = 1f;
    //private AudioSource dayAmbience;
    //private AudioSource nightAmbience;

    /*private void Start()
    {
        dayAmbience = GetComponent<AudioSource>();
        nightAmbience = GetComponent<AudioSource>();
    }*/

    // Update is called once per frame
    void Update()
    {
        //Get time of day from parent
        TimeOfDay = GetComponentInParent<TimeManagement>().TimeOfDay;
        if (Application.isPlaying)
        {
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        if (SpotLight != null)
        {
            //Checks if it is turning into night or day
            if (timePercent < 0.25f || timePercent > 0.8f)
            {
                //Dims the light if it is turning into day
                if (timePercent < 0.25f && timePercent > 0.1f)
                {
                    SpotLight.intensity -= 0.005f;
                    
                    Debug.Log("Day " + TimeOfDay);
                }
                //turns on light if it is turning into night
                else if (timePercent < 0.9f && timePercent > 0.8f && SpotLight.intensity < intensity)
                {
                    SpotLight.intensity += 0.005f;

                    Debug.Log("Night " + TimeOfDay);
                }
                //if it otherwise fails it turns on the light
                else 
                {
                    SpotLight.intensity = intensity;
                }
                
            }
            //turns off lights
            else
            {
                SpotLight.intensity = 0;
            }
        }

    }
}
