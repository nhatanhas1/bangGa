using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour ,IDamageable
{
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float maxHp;
    [SerializeField]
    float currentHp;
    [SerializeField]
    float damage;

    [SerializeField]
    float enemyLifeTime;

    [SerializeField]
    float maxMoveDistance;

    [SerializeField]
    float enemyStyle;

    public LevelController levelController;

    int locationIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();           
    }

    void Start()
    {
        moveSpeed = 3;
        maxHp = 20;
        damage = 10;
        currentHp = maxHp;
        enemyLifeTime = 10;
        enemyStyle = Random.Range(0, 2);

        Debug.Log("enemyStyle " + enemyStyle);
        locationIndex = 0;

        maxMoveDistance = Camera.main.orthographicSize + 5;
        //StartCoroutine(EnemyLifeTime());
    }

    //public void SetEnemyMovePosition()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        switch (enemyStyle)
        {
            case 0:
                transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
                break;
            case 1:
                if (levelController != null)
                {
                    if (locationIndex < levelController._moveLocation.Count)
                    {
                        if (transform.position != levelController._moveLocation[locationIndex].position)
                        {
                            Debug.Log("enemy Move");
                            transform.position = Vector3.MoveTowards(transform.position, levelController._moveLocation[locationIndex].position, Time.deltaTime* moveSpeed);
                        }
                        else
                        {
                            locationIndex++;
                        }
                    }
                }
                break;
        }

        if(transform.position.y < -maxMoveDistance)
        {
            Dead();
        }
    }

    private void FixedUpdate()
    {
        
        
    }

    public void TakeDamage(float damage)
     {
        if(currentHp > 0)
        {
            currentHp -= damage;
            if(currentHp <= 0)
            {
                Dead();
            }
        }
        //Debug.Log(currentHp);
     }

    void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.GetComponent<IDamageable>() != null)
            {
                IDamageable damageable = collision.GetComponent<IDamageable>();
                damageable.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator EnemyLifeTime()
    {
        yield return new WaitForSeconds(enemyLifeTime);
        Dead();
    }
}
