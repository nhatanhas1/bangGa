using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour , IDamageable
{
    Rigidbody2D rb;

    [SerializeField]
    Vector2 moveDir;

    float horVelocity;
    float vertVelocity;
    [SerializeField]
    float moveSpeed = 2;


    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float maxHp;
    [SerializeField]
    float currentHp;

    //Camera cam;

    float verMoveBorder;
    float horMoveBorder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 6;
        maxHp = 100;
        currentHp = maxHp;
        //cam = Camera.main;

        float worldHeight = Camera.main.orthographicSize * 2;
        float worldWidth = worldHeight * Screen.width / Screen.height;
        var playerCollider = GetComponent<CircleCollider2D>();

        horMoveBorder = worldWidth  - playerCollider.radius;
        verMoveBorder = worldHeight - playerCollider.radius;

        Debug.Log("hor " + horMoveBorder/2 + " ver " + verMoveBorder/2);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();

        
    }

    private void FixedUpdate()
    {       
        rb.velocity = moveDir * moveSpeed ;
    }

    void Move()
    {
        horVelocity = Input.GetAxisRaw("Horizontal");
        vertVelocity = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(horVelocity, vertVelocity).normalized;

        if (((transform.position.x < -horMoveBorder / 2) && moveDir.x < 0) ||
             ((transform.position.x > horMoveBorder / 2) && moveDir.x > 0) ||
             ((transform.position.y < -verMoveBorder / 2) && moveDir.y < 0) ||
             ((transform.position.y > verMoveBorder / 2) && moveDir.y > 0))
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 6;
        }
    }


    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Debug.Log("space key was pressed");
            Instantiate(bullet,transform);
        }
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
        Debug.Log(currentHp);
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    //void OnGUI()
    //{
    //    Vector3 point = new Vector3();
    //    Event currentEvent = Event.current;
    //    Vector2 mousePos = new Vector2();

    //    // Get the mouse position from Event.
    //    // Note that the y position from Event is inverted.
    //    mousePos.x = currentEvent.mousePosition.x;
    //    mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

    //    point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

    //    GUILayout.BeginArea(new Rect(20, 20, 250, 120));
    //    GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
    //    GUILayout.Label("Mouse position: " + mousePos);
    //    GUILayout.Label("World position: " + point.ToString("F3"));
    //    GUILayout.EndArea();
    //}
}
