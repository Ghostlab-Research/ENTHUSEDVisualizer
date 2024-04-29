using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;//for try catch blocks



/// <summary>
/// Takes voxel grids as input and represents them visually in Unity. 
/// Only works for one tracked object right now-- we'll get there
/// We also really need to do this on a step by step basis, because you're instantiating 7000 voxels
/// 
/// ???-->simonson.au@northeastern.edu
/// Last major updates April 2024
/// </summary>

public class ENTHUSEDVisualizer : MonoBehaviour
{

    public string fileName;
    public VoxelMatrix voxelMatrix;
    public Voxel voxelPrefab;
    private Voxel nextVoxel;

    public int stepNum;
    public float threshold;//for two-color "high score" and "low score" visualiations
    public Material highScoreMat, lowScoreMat;


    // Start is called before the first frame update
    void Start()
    {

        var dataset = Resources.Load<TextAsset>(fileName);
        Debug.Log(dataset);
        string[] data = dataset.text.Split("\n");//a string array where every string is one line of the csv
        data = data[1..];//remove column headers. This is... not an efficient way to do that  
        Debug.Log("line 300: " + data[300]);
        CreateMatrixForStep(data, stepNum);
        VisualizeMatrix();
        MarkBinaryThreshold(threshold);
    }

    //creates a voxel matrix for every object for every step. Use carefully. 
    //Depending on your data, this may instantiate several thousand game objects
    private void CreateFullMatrix(string[] data){
        foreach(string line in data){
            try{
                Debug.Log("line: " + line);
                //split line
                string[] lineSplit = line.Split(",");
                //put the parts where they go 
                Debug.Log("line after splitting: " + lineSplit);
                Debug.Log("first element of line after splitting: " + lineSplit[1]);
                nextVoxel = Instantiate(voxelPrefab);
                nextVoxel.x = float.Parse(lineSplit[1]);
                nextVoxel.y = float.Parse(lineSplit[2]);
                nextVoxel.z = float.Parse(lineSplit[3]);
                nextVoxel.stepNumber = int.Parse(lineSplit[4]);
                nextVoxel.score = float.Parse(lineSplit[5]);
                voxelMatrix.voxels.Add(nextVoxel);
            }
            catch (Exception e){
                Debug.Log("Something went wrong when creating the full matrix");
            }
        }
    }

    private void CreateMatrixForStep(string[] data, int stepNum){
        foreach(string line in data){
            try{
                Debug.Log("line: " + line);
                //split line
                string[] lineSplit = line.Split(",");
                //put the parts where they go 
                Debug.Log("line after splitting: " + lineSplit);
                Debug.Log("first element of line after splitting: " + lineSplit[1]);
                if(int.Parse(lineSplit[4])==stepNum){//check that this is from the right steo
                    nextVoxel = Instantiate(voxelPrefab);
                    nextVoxel.x = float.Parse(lineSplit[1]);
                    nextVoxel.y = float.Parse(lineSplit[2]);
                    nextVoxel.z = float.Parse(lineSplit[3]);
                    nextVoxel.stepNumber = int.Parse(lineSplit[4]);
                    nextVoxel.score = float.Parse(lineSplit[5]);
                    voxelMatrix.voxels.Add(nextVoxel);
                }
            }
            catch (Exception e){
                Debug.Log("Something went wrong when creating the step matrix");
            }
        }
    }

    private void VisualizeMatrix(){
        foreach(Voxel voxel in voxelMatrix.voxels){
            Vector3 voxelPosition = new Vector3(voxel.x, voxel.y, voxel.z);
            voxel.gameObject.transform.position = voxelPosition;
        }
    }

    private void MarkBinaryThreshold(float threshold){
        foreach(Voxel voxel in voxelMatrix.voxels){
            if(voxel.score>threshold){
                voxel.GetComponentInChildren<Renderer>().material = highScoreMat;
            }
            else{
                voxel.GetComponentInChildren<Renderer>().material = lowScoreMat;   
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
