using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;



[CustomEditor(typeof(FlowerCreator))]
public class FlowerCreatorEditor : Editor
{
    bool firstBuild = false;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FlowerCreator myTarget = (FlowerCreator)target;

        if (GUILayout.Button("Build Flower"))
        {
            if (firstBuild)
            {
                myTarget.ClearFlower();
            }
            else
            {
                firstBuild = true;
            }
            myTarget.BuildFlower(firstBuild);
            
        }

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        //EditorGUILayout.LabelField("Level", myTarget);
    }


}
#endif
public class FlowerCreator : MonoBehaviour
{
    [SerializeField]
    public GameObject stemPF, flowerPF, leavesPF;
    GameObject currStem;
    List<Transform> flowerSlots, leavesSlots;

    public void BuildFlower(bool first)
    {
        bool f = false, l = false;
        if (!first)
        {
            ClearFlower();
        }
        currStem = Instantiate(stemPF, transform);
        currStem.transform.position = Vector3.zero;
        Stem s = currStem.GetComponent<Stem>();
        flowerSlots = new List<Transform>();
        leavesSlots = new List<Transform>();
        flowerSlots.AddRange(s.flowers);
        leavesSlots.AddRange(s.leavesContainer.GetComponentsInChildren<Transform>());
        
        foreach(Transform sl in flowerSlots)
        {
            if (f)
            {
                Instantiate(flowerPF, sl);
            }
            else
            {
                f = true;
            }
            
        }
        foreach (Transform sl in leavesSlots)
        {
            if (l)
            {
                Instantiate(leavesPF, sl);
            }
            else
            {
                l = true;
            }
            
        }
    }
    public void ClearFlower()
    {
        flowerSlots.Clear();
        leavesSlots.Clear();
        GameObject.DestroyImmediate(GetComponent<Transform>().GetChild(0).gameObject);
    }
}

