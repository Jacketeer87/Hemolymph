using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebProjectile : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] public GameObject webPrefab;  // Prefab of the web square
    public float transformTime = 2f;  // Time after which the projectile transforms

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("TransformToWeb", transformTime);  // Schedule transformation
    }

    private void TransformToWeb()
    {
        GameObject web = Instantiate(webPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);  // Destroy the projectile object
    }
}

// Manages the web area effect
public class WebArea : MonoBehaviour
{
    public float slowEffectStrength = 0.5f;
    public float effectDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))  // Assuming objects to slow are tagged as "Enemy"
        {
            StartCoroutine(ApplySlowEffect(other));
        }
    }

    private IEnumerator ApplySlowEffect(Collider2D enemy)
    {
        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
        {
            float originalSpeed = enemyRb.velocity.magnitude;
            enemyRb.velocity *= slowEffectStrength;  // Apply slow

            yield return new WaitForSeconds(effectDuration);

            enemyRb.velocity = enemyRb.velocity.normalized * originalSpeed;  // Restore speed
        }
    }
}
