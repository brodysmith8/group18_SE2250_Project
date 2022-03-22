using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject camera; 
    
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int spawnXOffset = 4;

    private float lastXCoord; 

    private GameObject spawnedEnemy;

    public HeroKnight player;
    public GameObject playerObject;

    // spawn one enemy initially at the start 
    // Start is called before the first frame update
    void Start()
    {
        lastXCoord = camera.transform.position.x;
        print("Start xCoord: " + lastXCoord);

        playerObject = GameObject.Find("HeroKnight");
        player = playerObject.GetComponent<HeroKnight>();

        spawnedEnemy = Instantiate(enemyPrefab, new Vector2(6, 0), new Quaternion(0, 0, 0, 0));
    }


    /* spawn enemy when spawn condition is met (current cam x coord is 2 units greater than it was at the last spawn event) 
     * with a boolean value that once its defeated the script captures the x value of the camera and repeats the same cycle.    
    */

    // Update is called once per frame
    void Update()
    {
        // this is a spawn condition.


        // When spawnedEnemy = null, then it is despawned, indicating that it is dead.
        if (camera.transform.position.x - lastXCoord > 2 && (spawnedEnemy == null)) {
            lastXCoord = camera.transform.position.x;
            spawnedEnemy = Instantiate(enemyPrefab, new Vector2(playerObject.transform.position.x + spawnXOffset, 0), new Quaternion(0, 0, 0, 0));
        }
    }
}
