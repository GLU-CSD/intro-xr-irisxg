using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Vergeet niet deze namespace toe te voegen voor NavMeshAgent

public class ExplodeOnImpact : MonoBehaviour
{
    public float explosionForce = 500f;      // Kracht van de explosie
    public float explosionRadius = 5f;      // Radius van de explosie
    public float explosionDamage = 50f;     // Schade door de explosie

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Zorg dat vijanden de tag "Enemy" hebben
        {
            Explode();
            Destroy(gameObject); // Verwijder het object na de explosie
        }
    }

    void Explode()
    {
        // Vind alle objecten in de buurt van de explosie
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            // Disable NavMeshAgent voor vijanden
            NavMeshAgent agent = nearbyObject.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = false;
            }
            
            // Breng schade toe aan vijanden met een Health-script
            Health healthScript = nearbyObject.GetComponent<Health>();
            if (healthScript != null)
            {
                healthScript.TakeDamage(explosionDamage);
            }
            
            // Voeg explosiekracht toe
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
}

