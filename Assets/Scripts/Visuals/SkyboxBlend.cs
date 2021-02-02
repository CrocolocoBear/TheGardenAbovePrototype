using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxBlend : MonoBehaviour
{

    private float TimeOfDay;

    // Update is called once per frame
    void Update()
    {
        //Get time of day from parent
        TimeOfDay = GetComponent<TimeManagement>().TimeOfDay;
        if (Application.isPlaying)
        {
            Blend(TimeOfDay / 24f);
        }
        else
        {
            Blend(TimeOfDay / 24f);
        }
    }

    private void Blend(float timePercent)
    {
        //Checks if it is turning into night or day
        if (timePercent < 0.25f || timePercent > 0.75f)
        {
            //Dims the light if it is turning into day
            if (timePercent < 0.3f && timePercent > 0.2f && RenderSettings.skybox.GetFloat("_Blend") > 0.02f)
            {
                RenderSettings.skybox.SetFloat("_Blend", RenderSettings.skybox.GetFloat("_Blend") - 0.02f);
            }
            //turns on light if it is turning into night
            else if (timePercent < 0.85f && timePercent > 0.75f && RenderSettings.skybox.GetFloat("_Blend") < 0.98f)
            {
                RenderSettings.skybox.SetFloat("_Blend", RenderSettings.skybox.GetFloat("_Blend") + 0.02f);
            }
        }
        else
        {
            RenderSettings.skybox.SetFloat("_Blend", 0.01f);
        }
    }

}

