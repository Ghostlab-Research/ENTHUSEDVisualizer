using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Core of ENTHUSED. Makes voxel grids and keeps track of current ENTHUSED score.
/// 
/// ???-->simonson.au@northeastern.edu
/// Last major updates July 2024
/// </summary>

public class ENTHUSEDManager : MonoBehaviour
{
    public string fileName;
    private Voxel nextVoxel;
    public Voxel voxelPrefab;
    public VoxelMatrix voxelMatrix;
    public int stepNum;//step we're visualizing
    public float enthusedScore;//reset to 0 at the start of every step. This is the main thing we calculate.
    public List<GameObject> trackedObjects;//right now, we only have 1 matrix loaded at a time, so we can only practically do this for one object at a time


    //needs to happen before anything in a Start function
    private void Awake()
    {
        var dataset = Resources.Load<TextAsset>(fileName);
        Debug.Log(dataset);
        string[] data = dataset.text.Split("\n");//a string array where every string is one line of the csv
        data = data[1..];//remove column headers. This is... not an efficient way to do that  
        Debug.Log("line 300: " + data[300]);
        CreateMatrixForStep(data, stepNum);
    }

    private void IncrementScore()
    {
        foreach (var item in trackedObjects)
        {
            enthusedScore += GetENTHUSEDVal(item, voxelMatrix);
        }
    }
    
    //this needs re-written in a way that handles... rounding? It will only return a value if the position exactly matches rn
    private float GetENTHUSEDVal(GameObject item, VoxelMatrix matrix)
    {
        //go find the right value. This is not written in the most efficient possible way.
        foreach(Voxel voxel in matrix.voxels)
        {
            if(item.transform.position.x == voxel.x)
            {
                if(item.transform.position.y == voxel.y)
                {
                    if(item.transform.position.z == voxel.z)
                    {
                        return voxel.score;
                    }
                }
            }
        }

        //if we get here, there's no data for this object in this position-- return the average... once we figure out how to do that... for now we return 0.
        return 0.0f;
    }

    private void CreateMatrixForStep(string[] data, int stepNum)
    {
        foreach (string line in data)
        {
            try
            {
                //Debug.Log("line: " + line);
                //split line
                string[] lineSplit = line.Split(",");
                //put the parts where they go 
                //Debug.Log("line after splitting: " + lineSplit);
                //Debug.Log("first element of line after splitting: " + lineSplit[1]);
                if (int.Parse(lineSplit[4]) == stepNum)
                {//check that this is from the right steo
                    nextVoxel = Instantiate(voxelPrefab);
                    nextVoxel.x = float.Parse(lineSplit[1]);
                    nextVoxel.y = float.Parse(lineSplit[2]);
                    nextVoxel.z = float.Parse(lineSplit[3]);
                    nextVoxel.stepNumber = int.Parse(lineSplit[4]);
                    nextVoxel.score = float.Parse(lineSplit[5]);
                    voxelMatrix.voxels.Add(nextVoxel);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Something went wrong when creating the full matrix: " + e.ToString());
            }
        }
    }

    //creates a voxel matrix for every object for every step. Use carefully. 
    //Depending on your data, this may instantiate several thousand game objects
    private void CreateFullMatrix(string[] data)
    {
        foreach (string line in data)
        {
            try
            {
                Debug.Log("line: " + line);
                //split line
                string[] lineSplit = line.Split(",");
                //put the parts where they go 
                //Debug.Log("line after splitting: " + lineSplit);
                //Debug.Log("first element of line after splitting: " + lineSplit[1]);
                nextVoxel = Instantiate(voxelPrefab);
                nextVoxel.x = float.Parse(lineSplit[1]);
                nextVoxel.y = float.Parse(lineSplit[2]);
                nextVoxel.z = float.Parse(lineSplit[3]);
                nextVoxel.stepNumber = int.Parse(lineSplit[4]);
                nextVoxel.score = float.Parse(lineSplit[5]);
                voxelMatrix.voxels.Add(nextVoxel);
            }
            catch (Exception e)
            {
                Debug.Log("Something went wrong when creating the full matrix: " + e.ToString());
            }
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
