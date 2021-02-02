using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticleSystem = null;
    private float TimeOfDay;


    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            //Fetches time of day from parent
            TimeOfDay = GetComponentInParent<TimeManagement>().TimeOfDay;

            //Updates based on the time of day split up in 24f
            UpdateParticles(TimeOfDay / 24f);
        }
        else
        {
            UpdateParticles(TimeOfDay / 24f);
        }
    }

    private void UpdateParticles(float timePercent)
    {
        var emission = ParticleSystem.emission;
        if (ParticleSystem != null)
        {
            if (timePercent < 0.2f || timePercent > 0.8f)
            {
                //lower emission rate at dusk and dawn
                if (timePercent < 0.2f && timePercent > 0.1f)
                {
                    emission.rateOverTime = 5f;
                }
                else if (timePercent < 0.9f && timePercent > 0.8f)
                {
                    emission.rateOverTime = 5f;
                }
                else
                {
                    //Higher emission at nighttime
                    emission.rateOverTime = 10f;
                }

            }
            else
            {
                //No emission during day
                emission.rateOverTime = 0f;
            }
        }

    }
}
