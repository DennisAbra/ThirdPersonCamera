using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public LayerMask toHit;
    public float avoidanceStrength = 0.1f;

    SmartCamera parent;
    Vector3 thirtyDegRight;
    Vector3 sixtyDegRight;
    Vector3 thirtyDegLeft;
    Vector3 sixtyDegLeft;

    float t = 0;
    Vector3 initialLocalPos;
    Quaternion initialLocalRotation;

    void Start()
    {
        parent = GetComponentInParent<SmartCamera>();
        initialLocalPos = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLines();
        CollisionAvoidance();
    }

    private void DrawLines()
    {
        Vector3 forwardVector = transform.position - parent.playerObj.transform.position;
        Vector3 rightVector = Vector3.Cross(forwardVector.normalized, Vector3.up) * forwardVector.magnitude;

        thirtyDegRight = parent.playerObj.transform.position + rightVector * Mathf.Sin(30 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(30 * Mathf.Deg2Rad);
        sixtyDegRight = parent.playerObj.transform.position + rightVector * Mathf.Sin(60 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(60 * Mathf.Deg2Rad);
        thirtyDegLeft = parent.playerObj.transform.position - rightVector * Mathf.Sin(30 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(30 * Mathf.Deg2Rad);
        sixtyDegLeft = parent.playerObj.transform.position - rightVector * Mathf.Sin(60 * Mathf.Deg2Rad) + forwardVector * Mathf.Cos(60 * Mathf.Deg2Rad);


        //Linetraces
        Debug.DrawLine(parent.playerObj.transform.position, transform.position);
        Debug.DrawLine(parent.playerObj.transform.position, thirtyDegLeft);
        Debug.DrawLine(parent.playerObj.transform.position, thirtyDegRight);
        Debug.DrawLine(parent.playerObj.transform.position, sixtyDegLeft);
        Debug.DrawLine(parent.playerObj.transform.position, sixtyDegRight);
    }

    void CollisionAvoidance()
    {
        RaycastHit hit;


        if (Physics.Linecast(parent.playerObj.transform.position, transform.position, out hit, toHit))
        {
            //ZOOM IN HERE
            // TODO: start zooming in when 30 degrees angles have been hit
        }
        else if (Physics.Linecast(parent.playerObj.transform.position, thirtyDegLeft, out hit, toHit))
        {
            transform.position = Vector3.Lerp(transform.position, thirtyDegRight, Time.deltaTime * avoidanceStrength);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((parent.transform.position - transform.position)), Time.deltaTime * avoidanceStrength);
            t = 0;
        }
        else if (Physics.Linecast(parent.playerObj.transform.position, sixtyDegLeft, out hit, toHit))
        {
            //TODO: figure out how to allow the player to override this avoidence system
            transform.position = Vector3.Lerp(transform.position, thirtyDegRight, Time.deltaTime * avoidanceStrength);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((parent.transform.position - transform.position)), Time.deltaTime * avoidanceStrength);
            t = 0;
        }
        else if (Physics.Linecast(parent.playerObj.transform.position, thirtyDegRight, out hit, toHit))
        {
            transform.position = Vector3.Lerp(transform.position, thirtyDegLeft, Time.deltaTime * avoidanceStrength);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((parent.transform.position - transform.position)), Time.deltaTime * avoidanceStrength);
            t = 0;
        }
        else if (Physics.Linecast(parent.playerObj.transform.position, sixtyDegRight, out hit, toHit))
        {
            transform.position = Vector3.Lerp(transform.position, thirtyDegLeft, Time.deltaTime * avoidanceStrength);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((parent.transform.position - transform.position)), Time.deltaTime * avoidanceStrength);
            t = 0;
        }
        else
        {

            t += Time.deltaTime;
            if (t > 0.5f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPos, Time.deltaTime * avoidanceStrength * 0.5f);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, initialLocalRotation, Time.deltaTime * avoidanceStrength * 0.5f);
            }
        }

    }
}
