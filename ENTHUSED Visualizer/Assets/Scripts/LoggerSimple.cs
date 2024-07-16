/// <summary>
/// For uses specifically related to the ENTHUSED algorithm.
/// Does similar things to Logger.cs, but fewer of them, and talks to fewer other files
/// By Aubrey Simonson
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class LoggerSimple : MonoBehaviour
{
    public GameObject head, leftHand, rightHand;
    private float thisStepStartTime = 0.0f;
    private float positionLogFrequency = 0.3f;//in seconds -- we perform a write operation every time, so maybe don't set this lower than like 0.1

    //path to save file-- name it something like participant8_positions.csv
    //note that this will not overwrite existing files-- it appends to them
    //that seemed like the option which was least likely to cause data loss
    //but also you do need to remember to rename the file
    public string positionFilePath;
    public string interactionFilePath;

    //we use two different ones to prevent possible conflicts
    private StreamWriter positionWriter;
    private StreamWriter interactionWriter;

    // Start is called before the first frame update
    void Start()
    {
        //log position file headers
        LogPositionFileHeaders();

        //start logging
        InvokeRepeating("LogPositions", positionLogFrequency, positionLogFrequency);

        //log interaction file headers
        LogInteractionFileHeaders();
    }

    private void LogPositions()
    {
        //assemble content of the next row
        string toLog = DateTime.Now.ToString("hh:mm:ss.fffffff") + ",";
        toLog += RewriteV3WithoutCommas(head.transform.position) + ",";
        toLog += RewriteV3WithoutCommas(head.transform.eulerAngles) + ",";//transform.rotation will return a quaternion instead
        toLog += RewriteV3WithoutCommas(leftHand.transform.position) + ",";
        toLog += RewriteV3WithoutCommas(leftHand.transform.eulerAngles) + ",";
        toLog += RewriteV3WithoutCommas(rightHand.transform.position) + ",";
        toLog += RewriteV3WithoutCommas(rightHand.transform.eulerAngles) + ",";

        //write to csv
        positionWriter = new StreamWriter(positionFilePath, true);
        positionWriter.WriteLine(toLog);
        //superstition:
        positionWriter.Flush();
        positionWriter.Close();

    }

    //this should get called by whatever process is being used to move on to the next step
    public void LogInteraction(int currentStep)
    {
        string toLog = thisStepStartTime.ToString() + ",";//log start time
        toLog += Time.time.ToString() + ",";//log end time

        //log duration
        float duration = thisStepStartTime - Time.time;
        toLog += duration.ToString() + ",";

        toLog += currentStep.ToString();

        //write to csv
        interactionWriter = new StreamWriter(interactionFilePath, true);
        interactionWriter.WriteLine(toLog);
        //superstition:
        interactionWriter.Flush();
        interactionWriter.Close();

        thisStepStartTime = Time.time;

    }

    private void LogPositionFileHeaders()
    {
        string headers = "timeStamp,headPosition,headRotation,leftHandPosition,leftHandRotation,rightHandPosition,rightHandRotation";
        //write to csv
        positionWriter = new StreamWriter(positionFilePath, true);
        positionWriter.WriteLine(headers);
        //superstition:
        positionWriter.Flush();
        positionWriter.Close();
    }

    private void LogInteractionFileHeaders()
    {
        string headers = "startTime,endTime,duration,currentStep";
        //write to csv
        interactionWriter = new StreamWriter(interactionFilePath, true);
        interactionWriter.WriteLine(headers);
        //superstition:
        interactionWriter.Flush();
        interactionWriter.Close();
    }

    private string RewriteV3WithoutCommas(Vector3 vector3)
    {
        string rewritten = "(";
        rewritten += vector3.x + ": ";
        rewritten += vector3.y + ": ";
        rewritten += vector3.z + ")";
        return rewritten;
    }
}
