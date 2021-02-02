using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based on Glynn Taylors guide on day and night https://youtu.be/m9hj9PdO328 
//This script creates a preset with gradients to adjust

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptables/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
