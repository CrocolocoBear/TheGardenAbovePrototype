using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GCUWebGame;

public class FlowerMixerMachine : MonoBehaviour
{

    [SerializeField] private FlowerCreator FlowerParent1 = null;
    [SerializeField] private FlowerCreator FlowerParent2 = null;
    private FlowerCreator FlowerResult = null;
    [SerializeField] private GameObject Child = null;
    [SerializeField] private GameObject Button = null;
    //[SerializeField] private GameObject FlowerSlot1 = null;
    //[SerializeField] private GameObject FlowerSlot2 = null;
    [SerializeField] private GameObject FlowerEject = null;

    [SerializeField] private Pot first, second;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ReadFlowers();
            MixFlowers();
            OutputFlower();
        }
    }

    private void ReadFlowers()
    {
        FlowerParent1 = first.plant.GetComponent<FlowerCreator>();
        FlowerParent2 = second.plant.GetComponent<FlowerCreator>();
        //FlowerParent1 = FlowerSlot1.GetComponent<BoxCollider>().gameObject.GetComponent<FlowerCreator>();
        //FlowerParent2 = FlowerSlot2.GetComponent<BoxCollider>().gameObject.GetComponent<FlowerCreator>();
        //FlowerParent1 = FlowerSlot1.GetComponentInChildren<Stem>();
        //FlowerParent2 = FlowerSlot2.GetComponentInChildren<Stem>();

    }


    private void MixFlowers()
    {
        int Choice1 = 0;
        int Choice2 = 0;

        FlowerResult = new GameObject().AddComponent<FlowerCreator>();

        if (Random.value > 0.5)
        {
            FlowerResult.flowerPF = FlowerParent1.flowerPF;
            Choice1 = 1;
        }
        else
        {
            FlowerResult.flowerPF = FlowerParent2.flowerPF;
            Choice1 = 2;
        }

        if (Choice1 == 1)
        {

            if (Random.value > 0.75)
            {
                FlowerResult.leavesPF = FlowerParent2.leavesPF;
                Choice2 = 1;
            }
            else
            {
                FlowerResult.leavesPF = FlowerParent1.leavesPF;
                Choice2 = 2;
            }
            
        }
        else if(Choice1 == 2)
        {
            
            if(Random.value > 0.25)
            {
                FlowerResult.leavesPF = FlowerParent1.leavesPF;
                Choice2 = 1;
            }
            else
            {
                FlowerResult.leavesPF = FlowerParent2.leavesPF;
                Choice2 = 2;
            }
            
        }
        
        if(Choice1 == 1 && Choice2 == 1)
        {
            FlowerResult.stemPF = FlowerParent2.stemPF;
        }
        else if(Choice1 == 2 && Choice2 == 2)
        {
            FlowerResult.stemPF = FlowerParent1.stemPF;
        }
        else
        {
            if (Random.value > 0.5)
            {
                FlowerResult.stemPF = FlowerParent1.stemPF;
            }
            else
            {
                FlowerResult.stemPF = FlowerParent2.stemPF;
            }
        }
        

    }

    public void OutputFlower()
    {

        //GameObject Flower = new GameObject("Flower");
        if (Child.GetComponent<FlowerCreator>() == false)
        {
            Child.AddComponent<FlowerCreator>();
        }
        Child.name = "Plant 3";
        Child.GetComponent<FlowerCreator>().flowerPF = FlowerResult.flowerPF;
        Child.GetComponent<FlowerCreator>().leavesPF = FlowerResult.leavesPF;
        Child.GetComponent<FlowerCreator>().stemPF = FlowerResult.stemPF;
        Vector3 pos = new Vector3(0, 0, 0);
        Child.GetComponent<Transform>().position = pos;
        Instantiate<GameObject>(Child, FlowerEject.transform);
    }
}
