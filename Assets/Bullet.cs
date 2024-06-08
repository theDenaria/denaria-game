using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 200f;
    public float lifeTime = 5f;
    public int damage = 10;

    public GameObject bloodEffectPrefab;

    private void Start()
    {
        // Destroy the bullet after 'lifeTime' seconds
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits an enemy
        if (other.gameObject.CompareTag("Player"))
        {
            // Apply damage to the enemy
            // Assuming the enemy has a script with a method `TakeDamage(int amount)`
            Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        }
        else // TODO: Remove
        {
            Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
