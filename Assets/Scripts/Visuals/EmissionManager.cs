using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionManager : MonoBehaviour
{
    //Input Light through serialized field
    [SerializeField] private Material[] material = null;
    private float TimeOfDay;
    private int[] repitions;
    //How much it brightens during dusk (Default is 100), more repitions means brighter
    [SerializeField] int duskRepeats = 100;
    //How much it dims during dawn (default is 50), more repitions means darker
    [SerializeField] int dawnRepeats = 50;
    //Default dimmingValue (emission multiplied by this at dawn) is 0.95f
    [SerializeField] float dimmingValue = 0.95f;
    //Default brightenValue (emission multiplied by this at dusk) is 1.015f
    [SerializeField] float brightenValue = 1.015f;

    private void Start()
    {
        repitions = new int[material.Length];
    }
    // Update is called once per frame
    void Update()
    {
        //Get time of day from parent
        TimeOfDay = GetComponentInParent<TimeManagement>().TimeOfDay;
        if (Application.isPlaying)
        {
            //Updates based on the time of day split up in 24f
            UpdateEmission(TimeOfDay / 24f);
        }
        else
        {
            UpdateEmission(TimeOfDay / 24f);
        }
    }

    private void UpdateEmission(float timePercent)
    {
        for (int count = 0; count < material.Length; count++)
        {
            if (material != null)
            {
                //resets the repitions of the current material to 0 so it can repeat next cycle
                if (timePercent > 0.35f && timePercent < 0.75f)
                {
                    repitions[count] = 0;
                }
                if (timePercent > 0f && timePercent < 0.15f)
                {
                    repitions[count] = 0;
                }
                //Checks if it is turning into night or day
                //Dims the light if it is turning into day
                else if (timePercent < 0.25f && timePercent > 0.15f)
                {
                    if (repitions[count] < dawnRepeats)
                    {
                        material[count].SetColor("_EmissionColor", material[count].GetColor("_EmissionColor") * dimmingValue);
                        repitions[count]++;
                    }
                }
                //Brightens emission at nighttime
                else if (timePercent < 0.9f && timePercent > 0.8f )
                {
                    if (repitions[count] < duskRepeats)
                    {
                        material[count].SetColor("_EmissionColor", material[count].GetColor("_EmissionColor") * brightenValue);
                        repitions[count]++;
                    }
                }
                //Makes sure emission is properly off at daytime and that engine confirms it
                else if (timePercent > 0.25f && timePercent < 0.3f)
                {
                    material[count].DisableKeyword("_EMISSION");
                    material[count].globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
                    RendererExtensions.UpdateGIMaterials(GetComponent<Renderer>());
                    DynamicGI.SetEmissive(GetComponent<Renderer>(), Color.black);
                    DynamicGI.UpdateEnvironment();
                }
                //Makes sure emission is properly on at nighttime and that engine confirms it
                else if (timePercent < 0.8f && timePercent > 0.75f && repitions[count] < 1)
                {
                    material[count].EnableKeyword("_EMISSION");
                    material[count].globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                    material[count].SetColor("_EmissionColor", material[count].GetColor("_Color") * 0.2f);
                    RendererExtensions.UpdateGIMaterials(GetComponent<Renderer>());
                    DynamicGI.SetEmissive(GetComponent<Renderer>(), material[count].GetColor("_Color") * 0.2f);
                    DynamicGI.UpdateEnvironment();
                    repitions[count]++;
                }
            }
        }
    }
}