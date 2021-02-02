using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//Based on Kyle Andrews Tutorial: https://youtu.be/RCfBtN5ZAtA

public class CloudData
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;
    private bool _isActive;

    public bool isActive
    {
        get
        {
            return _isActive;
        }
    }

    public int x;
    public int y;

    public float distFromCam;

    public Matrix4x4 matrix
    {
        get
        {
            return Matrix4x4.TRS(pos, rot, scale);
        }
    }

    public CloudData(Vector3 pos, Vector3 scale, Quaternion rot, int x, int y, float distFromCam)
    {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
        SetActive(true);
        this.x = x;
        this.y = y;
        this.distFromCam = distFromCam;
    }

    public void SetActive(bool desState)
    {
        _isActive = desState;
    }
}




public class GenerateClouds : MonoBehaviour
{
    //Meshes
    public Mesh cloudMesh;
    public Material cloudMat;

    //Cloud Data
    public float cloudSize = 1;
    public float maxScale = 3f;

    //Noise Generation
    public float timeScale = 0.05f;
    public float texScale = 0.1f;

    //Cloud Scaling Info
    public float minNoiseSize = 0.6f;
    public float sizeScale = 0.5f;

    //Culling Data
    public Camera cam;
    public int maxDist = 100;

    //Number of batches, creates square amount (int x and int y)
    public int batchesToCreate = 6;

    private Vector3 prevCamPos;
    private float offsetX = 1;
    private float offsetY = 1;
    private List<List<CloudData>> batches = new List<List<CloudData>>();
    private List<List<CloudData>> batchesToUpdate = new List<List<CloudData>>();

    private void Start()
    {
        for(int batchesX= 0; batchesX < batchesToCreate; batchesX++)
        {
            for(int batchesY = 0; batchesY < batchesToCreate; batchesY++)
            {
                BuildCloudBatch(batchesX, batchesY);
            }
        }
    }

    //Loops through X and Y values for 30x30 clouds, limited because of Graphics.DrawMeshInstanciated
    private void BuildCloudBatch(int xLoop, int yLoop)
    {
        //Mark a  batch if it's within range of our camera
        bool markBatch = false;
        //This is our current cloud batch that we're making
        List<CloudData> currBatch = new List<CloudData>();

        for(int x = 0; x < 31; x++)
        {
            for(int y = 0; y < 31; y++)
            {
                //Add a cloud for each loop
                AddCloud(currBatch, x + xLoop * 31, y + yLoop * 31);
            }
        }

        //Check if the batch should be marked
        markBatch = CheckForActiveBatch(currBatch);

        //Add the newest batch to the batches list
        batches.Add(currBatch);

        //If the batch is marked add it to the batchesToUpdate list
        if (markBatch)
        {
            batchesToUpdate.Add(currBatch);
        }
    }

    //This method checks to see if the current batch has a cloud that is within our cameras range
    //Returns true if a cloud is within range
    //Return false if no clouds are within range

    private bool CheckForActiveBatch(List<CloudData> batch)
    {
        foreach (var cloud in batch)
        {
            cloud.distFromCam = Vector3.Distance(cloud.pos, cam.transform.position);
            if (cloud.distFromCam < maxDist)
            {
                return true;
            }
        }
        return false;
    }

    //This method creates our clouds as a CloudData object

    private void AddCloud(List<CloudData> currBatch, int x, int y)
    {
        //First we set our new clouds position
        Vector3 position = new Vector3(transform.position.x + x * cloudSize, transform.position.y, transform.position.z + y * cloudSize);

        //We set our new clouds distance to the camera so we can use it later
        float disToCam = Vector3.Distance(new Vector3(x, transform.position.y, y), cam.transform.position);

        //finally we add our new cloudData cloud to the currrent batch
        currBatch.Add(new CloudData(position, Vector3.zero, Quaternion.identity, x, y, disToCam));
    }

    //We need to generate our noise
    //We update our offsets to the noise 'rolls' throug hthe cloud objects

    private void Update()
    {
        MakeNoise();
        offsetX += Time.deltaTime * timeScale;
        offsetX += Time.deltaTime * timeScale;
    }

    private void MakeNoise()
    {
        if (cam.transform.position == prevCamPos)
        {
            UpdateBatches();
        }
        else
        {
            prevCamPos = cam.transform.position;
            UpdateBatchList();
            UpdateBatches();
        }
        RenderBatches();
        prevCamPos = cam.transform.position;
    }

    //This method updates our clouds
    //First we loop through all of our batches in the batchesToUpdate list
    //For each batch we need to the get each floud with another loop
    private void UpdateBatches()
    {
        foreach(var batch in batchesToUpdate)
        {
            foreach(var cloud in batch)
            {
                //get noise size based on clouds pos, noise texture scale, and our offset amount
                float size = Mathf.PerlinNoise(cloud.x * texScale + offsetX, cloud.y * texScale + offsetY);

                //If our cloud has a size that's above our visible cloud threshold we need to show it
                if(size > minNoiseSize)
                {
                    //Get the current scale of the cloud
                    float localScaleX = cloud.scale.x;

                    //Active any clouds
                    if(!cloud.isActive)
                    {
                        cloud.SetActive(true);
                        cloud.scale = Vector3.zero;
                    }

                    //If not max size, scale up
                    if(localScaleX < maxScale)
                    {
                        ScaleCloud(cloud, 1);

                        //Limit our max size
                        if(cloud.scale.x > maxScale)
                        {
                            cloud.scale = new Vector3(maxScale, maxScale, maxScale);
                        }
                    }
                }

                //Active and it shouldn't be, let's scale down
                else if(size < minNoiseSize)
                {
                    float localScaleX = cloud.scale.x;
                    ScaleCloud(cloud, -1);

                    //When the cloud is really small we can just set it to 0 and hide it
                    if(localScaleX <= 0.1)
                    {
                        cloud.SetActive(false);
                        cloud.scale = Vector3.zero;
                    }
                }
            }
        }
    }
    
    //This method sets our cloud to a new size

    private void ScaleCloud(CloudData cloud, int direction)
    {
        cloud.scale += new Vector3(sizeScale * Time.deltaTime * direction, sizeScale * Time.deltaTime * direction, sizeScale * Time.deltaTime * direction);
    }

    //This method clears our batchesToUpdate list because we only want visible batches within this list
    private void UpdateBatchList()
    {
        //Clears our list
        batchesToUpdate.Clear();

        //Loop through all the generated batches
        foreach(var batch in batches)
        {
            //If a single cloud is within range we need to add the batch to the update list
            if(CheckForActiveBatch(batch))
            {
                batchesToUpdate.Add(batch);
            }
        }
    }

    //This method loops through all the batches to update and draws their meshes to the sceen
    private void RenderBatches()
    {
        foreach(var batch in batchesToUpdate)
        {
            Graphics.DrawMeshInstanced(cloudMesh, 0, cloudMat, batch.Select((a) => a.matrix).ToList());
        }
    }
}
