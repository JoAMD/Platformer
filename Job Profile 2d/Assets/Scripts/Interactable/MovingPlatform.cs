using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Moving platform
///  Use InteractableObject Oninteract similar function for check for player in range to activate platform then
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed;
    public int currentWaypoint = 0;
    public bool isGoingUpOrPositive = true;
    public bool isObjectMoving = true;

    private bool isDoneOneMove = false;

    private void Start()
    {
        StartCoroutine(MoveObjectInInterval());
    }

    private IEnumerator MoveObjectInInterval()
    {
        while (isObjectMoving)
        {
            isDoneOneMove = false;
            MoveObject();
            //wait for secs? performance?
            yield return new WaitUntil(() => isDoneOneMove);
            yield return new WaitForSeconds(1.5f);
        }
    }

    /// <summary>
    /// Move Object
    /// Decides direction according to isGoingUpOrPositive
    /// </summary>
    protected void MoveObject()
    {
        if (currentWaypoint == waypoints.Count - 1)
        {
            isGoingUpOrPositive = false;
        }
        else if (currentWaypoint == 0)
        {
            isGoingUpOrPositive = true;
        }

        if (isGoingUpOrPositive)
        {
            StartCoroutine(ObjectLerpCo(1));
        }
        else
        {
            StartCoroutine(ObjectLerpCo(-1));
        }
    }

    /// <summary>
    /// Move object up and down (up through the list) according to waypoints list and direction
    /// </summary>
    /// <param name="direction"> direction of movement of object, +1 means positive means up the list from 0 to the end </param>
    /// <returns></returns>
    protected IEnumerator ObjectLerpCo(int direction)
    {
        float t = 0;
        Vector3 startPos = waypoints[currentWaypoint].position;
        Vector3 endPos = waypoints[currentWaypoint + direction].position;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        currentWaypoint += direction;
        isDoneOneMove = true;
    }
}
