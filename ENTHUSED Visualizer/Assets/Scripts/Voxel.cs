using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Part of ENTHUSED (Engine for the Negotiation of the Timing of Hints Using Spatiotemporal Evaluation Data)
/// Data structure infrastructure.
/// A Voxel Matrix is made of Voxels
/// 
/// ???-->simonson.au@northeastern.edu
/// Last major updates April 2024
/// </summary>
/// 
public class Voxel : MonoBehaviour
{
    public int stepNumber;
    public float x, y, z;
    public float score;
    public string trackedObjectName;
}
