using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutscene : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    [SerializeField] public GameObject anim;
    // Start is called before the first frame update
    void Start()
    {
        cam.SetActive(true);
        player.SetActive(false);
        StartCoroutine(FinishScene());

    }

    IEnumerator FinishScene()
    {
        yield return new WaitForSeconds(10);
        player.SetActive(true);
        cam.SetActive(false);
        anim.GetComponent<Animator>().enabled = false;
    }
}
