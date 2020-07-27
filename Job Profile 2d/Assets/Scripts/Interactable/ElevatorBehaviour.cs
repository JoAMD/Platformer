using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : InteractableObject
{

    public Collider2D triggerInteractableCollider;
    public List<Transform> floors;
    public float speed;
    public int currentFloor = 0;
    public bool isGoingUp = true;

    /// <summary>
    /// OnInteract function runs when player presses the Interact Button
    /// CHecks for player in range, then move elevator
    /// isPlayerInRange is set in base class InteractableObject (using Player tag as well)
    /// </summary>
    public override void OnInteract()
    {
        //Debug.Log("running on interact");
        if (isPlayerInRange)
        {
            //Debug.Log("Player started elevator");
            MoveElevator();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }


    /// <summary>
    /// Move Elevator
    /// Decides direction according to isGoingUp
    /// Disables trigger collider so that player can'i interact when elevator is moving
    /// </summary>
    public void MoveElevator()
    {
        triggerInteractableCollider.enabled = false; //Disable trigger collider
        if (currentFloor == floors.Count - 1)
        {
            isGoingUp = false;
        }
        else if(currentFloor == 0)
        {
            isGoingUp = true;
        }

        if (isGoingUp)
        {
            StartCoroutine(ElevatorLerpCo(1));
        }
        else
        {
            StartCoroutine(ElevatorLerpCo(-1));
        }
    }

    /// <summary>
    /// Move elevator up and down according to floors list and direction
    /// Re-enables trigger collider so that player can interact again after elevator has stopped
    /// </summary>
    /// <param name="direction"> direction of movement of object, +1 means positive means up the list from 0 to the end </param>
    /// <returns></returns>
    private IEnumerator ElevatorLerpCo(int direction)
    {
        float t = 0;
        Vector3 startPos = floors[currentFloor].position;
        Vector3 endPos = floors[currentFloor + direction].position;
        while(t < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            t += speed * Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        currentFloor += direction;
        triggerInteractableCollider.enabled = true; //Re-enable trigger collider
    }

}
