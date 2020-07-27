using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            GameObject elevatorGb = Object.Instantiate(Resources.Load<GameObject>("ElevatorRes"));

            //GameObject playerGb = Object.Instantiate(Resources.Load<GameObject>("PlayerJoRes"), startElevatorPos, Quaternion.identity);

            Transform elevatorHolder = elevatorGb.transform.GetChild(0);
            Collider2D elevatorFloor = elevatorHolder.transform.GetChild(0).GetComponent<Collider2D>();
            Vector3 startElevatorPos;
            ElevatorBehaviour interactableObject = elevatorHolder.GetComponent<ElevatorBehaviour>();

            for (int i = 0; i < 10; i++)
            {
                startElevatorPos = elevatorHolder.position;
                interactableObject.MoveElevator();
                yield return new WaitForSeconds(1f);
                yield return new WaitUntil(() => elevatorFloor.enabled);

                Assert.AreNotEqual(elevatorHolder.position, startElevatorPos);

            }

            yield return null;
        }
    }
}
