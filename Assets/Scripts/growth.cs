using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
public class growth : MonoBehaviour
{
    public Camera camera;
    bool plant;
    public GameObject flower;
    void Start()
    {
        Vector3 pos;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Transform objectHit = hit.transform;
            pos.x = hit.point.x;
            pos.y = hit.point.y + 0.03f;
            pos.z = hit.point.z;
            transform.position = pos;
        }
    }
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name != "terrain")
        {
            flower.gameObject.SetActive(false);
        }
            
    }
}
