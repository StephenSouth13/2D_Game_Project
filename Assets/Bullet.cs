using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float lifeTime = 2f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * bulletSpeed;
        Destroy(gameObject, lifeTime);
    }
}