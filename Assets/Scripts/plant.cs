using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
public class plant : MonoBehaviour
{
    // Start is called before the first frame update
    public float time = 0.0f;
    bool timestart;

    public GameObject stage1;
    public GameObject terrain;
    public KeyCode plantkey;
    public bool hold;
    
    void Update()
    {
        hold = false;
        Vector3 position = transform.localPosition;
        bool plantflower = Input.GetKey(plantkey);
        hold = terrain.gameObject.GetComponent<terrain>().pressed;
        if (hold)
        {
            stage1.gameObject.SetActive(true);
            
        }
        

    }
    
}