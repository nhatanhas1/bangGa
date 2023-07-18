using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float timer;
    [SerializeField]
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3;
        timer = 5;
        damage = 10;
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up  * moveSpeed * Time.deltaTime);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.GetComponent<IDamageable>() != null)
            {
                IDamageable damageable = collision.GetComponent<IDamageable>();
                damageable.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

   
}
