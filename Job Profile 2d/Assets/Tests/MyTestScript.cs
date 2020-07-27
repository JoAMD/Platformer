using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MyTestScript
    {
        // A Test behaves as an ordinary method
        [Test]
        public void MyTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
            //Assert.
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MyTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            GameObject elevatorGb = Object.Instantiate(Resources.Load<GameObject>("ElevatorRes"));
            Vector3 startElevatorPos = elevatorGb.transform.position;

            //GameObject playerGb = Object.Instantiate(Resources.Load<GameObject>("PlayerJoRes"), startElevatorPos, Quaternion.identity);
            ElevatorBehaviour interactableObject = elevatorGb.transform.GetChild(0).GetComponent<ElevatorBehaviour>();

            interactableObject.MoveElevator();

            yield return new WaitForSeconds(2f);

            Assert.AreNotEqual(elevatorGb.transform.position, startElevatorPos);

        }
    }
}
