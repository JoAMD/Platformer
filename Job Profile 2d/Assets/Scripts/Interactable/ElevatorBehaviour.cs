using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : InteractableObject
{
    [SerializeField] private bool isPlayerInRange = false;

    public Collider2D triggerInteractableCollider;
    public List<Transform> floors;
    public float speed;
    public int currentFloor = 0;
    public bool isGoingUp = true;

    public override void OnInteract()
    {
        Debug.Log("running on interact");
        if (isPlayerInRange)
        {
            Debug.Log("Player started elevator");
            MoveElevator();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
    }


    private void MoveElevator()
    {
        triggerInteractableCollider.enabled = false;
        if(currentFloor == floors.Count - 1)
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
        triggerInteractableCollider.enabled = true;
    }

}
