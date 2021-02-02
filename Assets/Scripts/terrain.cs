using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrain : MonoBehaviour
{
    public bool pressed;
    
    void OnMouseDown()
    {
        pressed = true;
    }
    void Update()
    {
        pressed = false;
    }
}
