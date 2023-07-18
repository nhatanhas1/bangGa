using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject enemy;

    public List<Transform> _moveLocation = new List<Transform>();

    [SerializeField]
    float timer;
    [SerializeField]
    float spawnDelay;

    Camera _camera;

    float verScreenSize;
    float horScreenSize;
    
    void Start()
    {
        spawnDelay = 2;
        _camera = Camera.main;
        verScreenSize = _camera.orthographicSize * 2;
        horScreenSize = verScreenSize * Screen.width/Screen.height;
        Debug.Log("verScreenSize" + verScreenSize + " horScreenSize" + horScreenSize);
    }

    // Update is called once per frame
    void Update()
    {
       SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if(enemy == null) { return; }
        if (timer <= Time.time)
        {
            timer = Time.time + spawnDelay;
            //Instantiate(enemy, _spawnLocation[1]);
            var enemyTmp =  Instantiate(enemy, new Vector3(Random.Range(-horScreenSize/2, horScreenSize/2), verScreenSize / 2, 0), Quaternion.identity);
            enemyTmp.GetComponent<EnemyController>().levelController = this;
        }
        //Debug.Log(Time.time);
    }
}
