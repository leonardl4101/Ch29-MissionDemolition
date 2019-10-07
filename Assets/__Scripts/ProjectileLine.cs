using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; // Singleton

    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;


    // This is a property (a method masquerating as a field)
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                // When _poi is set to something new, it resets everything
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    // Use this for initialization
    void Awake()
    {
        S = this; // Set the singleton
        // Get a reerence to the LineRenderer
        line = GetComponent<LineRenderer>();
        // Disable the LineRenderer until it is needed
        line.enabled = false;
        // Initialize the points List
        points = new List<Vector3>();
    }
    // end of Awake method


    // This can be used to clear the line directly
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }
    public void AddPoint()
    {
        // This is called to add a point to the line
        Vector3 pt = poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return;
        }
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            // sets the first two points
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            // enable our line renderer
            line.enabled = true;

        }    // end of it

        else
        {
            // normal behavior of adding a point
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }    // end of else

    }  // end of Addpoint


    public Vector3 lastPoint
    {
        get
        {
            if (points == null) return Vector3.zero;
            return points[points.Count - 1];
        }



    }


    // Update is called once per frame

    void FixedUpdate()
    {
        if (poi == null)
        {
            // If there is no poi, search for one
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }   // end of if
                else
                {
                    return; // Return if it is not the Projectile
                }  // end of else
            }      // end of if

        }
        //If there is a poi, its loc is added every FixedUpdate
        AddPoint();
        if (FollowCam.POI == null)
        {

            poi = null;
        }

    } // end of FixedUpdate method

}
