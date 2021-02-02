using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomController : MonoBehaviour
{
    //Input Light through serialized field
    [SerializeField] private PostProcessVolume volume = null;
    private Bloom bloom = null;
    [SerializeField] private float nightBloom = 6f;
    [SerializeField] private float dayBloom = 1f;

    private float TimeOfDay;
    
    private void Start()
    {
        volume.profile.TryGetSettings<Bloom>(out bloom);

    }


    // Update is called once per frame
    void Update()
    {
        //Get time of day from parent
        //Updates based on the time of day split up in 24f
        TimeOfDay = GetComponentInParent<TimeManagement>().TimeOfDay;
        if (Application.isPlaying)
        {
            UpdateBloom(TimeOfDay / 24f);
        }
        else
        {
            UpdateBloom(TimeOfDay / 24f);
        }
    }

    private void UpdateBloom(float timePercent)
    {
        if (bloom != null)
        {
            //Checks if it is turning into night or day
            if (timePercent < 0.25f || timePercent > 0.8f)
            {
                //Dims the light if it is turning into day
                if (timePercent < 0.25f && timePercent > 0.15f)
                {
                    bloom.intensity.value -= 0.01f;
                }
                //turns on light if it is turning into night
                else if (timePercent < 0.9f && timePercent > 0.8f && bloom.intensity < 6f)
                {
                    bloom.intensity.value += 0.01f;
                }
                //if it otherwise fails it turns on the light
                else
                {
                    bloom.intensity.value = nightBloom;
                }

            }
            //turns off lights
            else
            {
                bloom.intensity.value = dayBloom;
            }
        }

    }
}
