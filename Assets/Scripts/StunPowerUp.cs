using System;
using UnityEngine;

public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject [] allenemis = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemyObj in allenemis)
            {
                Enemy enemyScript = enemyObj.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.StartCoroutine(enemyScript.StunRoutine(stunDuration));

                }
            }
            Destroy(gameObject);
        }
    }
}
