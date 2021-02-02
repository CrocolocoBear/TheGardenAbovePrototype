using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on Glynn Taylors guide on day and night https://youtu.be/m9hj9PdO328
//This class adjusts the directional light that is used as the sun, 
//as well as changes colours based on the Lighting Preset

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight = null;
    [SerializeField] private LightingPreset Preset = null;
    [SerializeField] private AudioSource dayAmbienceClip;
    [SerializeField] private AudioSource nightAmbienceClip;
    [SerializeField] private AudioSource forestDayClip;
    [SerializeField] private AudioSource forestNightClip;

    //Variables
    private float TimeOfDay;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //Gets time of day from TimeManagement
            TimeOfDay = GetComponent<TimeManagement>().TimeOfDay;
            
            UpdateLighting(TimeOfDay / 24f);

            if ((TimeOfDay >= 5.6 && TimeOfDay <= 18.4) && !dayAmbienceClip.isPlaying && !forestDayClip.isPlaying)
            {
                nightAmbienceClip.Stop();
                dayAmbienceClip.Play();

                forestNightClip.Stop();
                forestDayClip.Play();
            }
            else if ((TimeOfDay <= 5.4 || TimeOfDay >= 18.7) && !nightAmbienceClip.isPlaying && !forestNightClip.isPlaying)
            {
                dayAmbienceClip.Stop();
                nightAmbienceClip.Play();

                forestDayClip.Stop();
                forestNightClip.Play();
            }
        }
        else
        {
            TimeOfDay = GetComponent<TimeManagement>().TimeOfDay;
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set its color
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, timePercent * 180f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}