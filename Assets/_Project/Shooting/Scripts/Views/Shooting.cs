using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;

    public void Shoot(Vector3 origin, Quaternion direction)
    {
        // Instantiate the bullet at the fire point
        Instantiate(bulletPrefab, origin, direction);
    }
}
