using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform focalPoint;
    
    public bool hasPowerUp;

    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;
    
    private Coroutine powerUpCoroutine;
    
    public GameObject powerupIndicator;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
        
        powerupIndicator.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * focalPoint.forward);

        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero;
        }
        
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

    }

    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(10f);
        hasPowerUp = false;
        
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                var enemyRb =  colision.gameObject.GetComponent<Rigidbody>();
                // var v = enemyRb.linearVelocity;
                // v.Normalize();
                //
                //var v = enemyRb.linearVelocity.normalized;
               
                var dir = enemyRb.transform.position - transform.position;
                dir.Normalize();
                enemyRb.AddForce(dir * 10f ,ForceMode.Impulse);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            
            print("tesss");
            powerupIndicator.SetActive(true);
            
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);
            }
            powerUpCoroutine = StartCoroutine(PowerUpCooldown());
        }
        
    }
}
