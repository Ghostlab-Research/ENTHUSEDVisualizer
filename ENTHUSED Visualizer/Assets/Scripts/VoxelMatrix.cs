using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Part of ENTHUSED (Engine for the Negotiation of the Timing of Hints Using Spatiotemporal Evaluation Data)
/// Data structure infrastructure. 
/// 
/// ???-->simonson.au@northeastern.edu
/// Last major updates April 2024
/// </summary>

public class VoxelMatrix : MonoBehaviour
{
    public List<Voxel> voxels;
    public string trackedObjectName;

    public void Start(){
        if(voxels == null){
            voxels = new List<Voxel>();
        }
    }
}
