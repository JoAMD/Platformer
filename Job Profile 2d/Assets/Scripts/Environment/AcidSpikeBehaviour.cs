using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpikeBehaviour : PlayerDetector
{
    public int damage = 2;

    private void Start()
    {
        StartCoroutine(DamagePlayerInRange());
    }

    private IEnumerator DamagePlayerInRange()
    {
        while (true)
        {
            if (isPlayerInRange)
            {
                HealthSystem.sRef.TakeDamage(damage);
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
