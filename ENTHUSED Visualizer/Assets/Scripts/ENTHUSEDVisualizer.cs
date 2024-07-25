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
    public ENTHUSEDManager eNTHUSEDManger;
    public float threshold;//for two-color "high score" and "low score" visualiations
    public Material highScoreMat, lowScoreMat;


    // Start is called before the first frame update
    void Start()
    {
        VisualizeMatrix();
        MarkBinaryThreshold(threshold);
    }

    private void VisualizeMatrix(){
        foreach(Voxel voxel in eNTHUSEDManger.voxelMatrix.voxels){
            Vector3 voxelPosition = new Vector3(voxel.x, voxel.y, voxel.z);
            voxel.gameObject.transform.position = voxelPosition;
        }
    }

    private void MarkBinaryThreshold(float threshold){
        foreach(Voxel voxel in eNTHUSEDManger.voxelMatrix.voxels){
            if(voxel.score>threshold){
                voxel.GetComponentInChildren<Renderer>().material = highScoreMat;
            }
            else{
                voxel.GetComponentInChildren<Renderer>().material = lowScoreMat;   
            }
        }
    }
}
