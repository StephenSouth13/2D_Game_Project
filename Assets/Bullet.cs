using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }
}
/*using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int direction = 1;
    //public float lifeTime = 2f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * bulletSpeed;
        Destroy(gameObject, lifeTime);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.indentity);
        }
    }
}
*/