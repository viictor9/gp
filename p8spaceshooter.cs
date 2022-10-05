// 1. PlayerController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public float destroyTime=5f;
    public Transform muzzleSpawnPosition;

    private void Update()
    {
       PlayerMovement(); 
       PlayerShoot();
    }

    void PlayerMovement(){
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xPos, yPos, 0)* speed * Time.deltaTime;
        transform.Translate(movement); 
    }
    void PlayerShoot(){
        if(Input.GetKeyDown(KeyCode.Space)){
            SpawnMissile();
            SpawnMuzzleFlash();
        }
    }
    void SpawnMuzzleFlash(){
        GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
        muzzle.transform.SetParent(null);
        Destroy(muzzle,destroyTime);
    }
    void SpawnMissile(){
        GameObject gm = Instantiate(missile,missileSpawnPosition);
        
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Enemy"){
            GameObject gm = Instantiate(GameManager.instance.explosion,transform.position,transform.rotation);
            Destroy(gm,2f);
            Destroy(this.gameObject);
            //Game Over Screen Will Appear Here
        }
    }
}



// 2. MissileController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*missileSpeed*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision){

        if(collision.gameObject.tag=="Enemy"){
            GameObject gm = Instantiate(GameManager.instance.explosion,transform.position,transform.rotation);
            Destroy(gm,2f);

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}






// 3. GameManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float minInstantiateValue;
    public float maxInstantiateValue;
    public float enemyDestroyTime=10f;
    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;
    private void Awake(){
        instance=this;
    }

    private void Start(){
        InvokeRepeating("InstantiateEnemy",1f,2f);
    }
    void InstantiateEnemy(){
        Vector3 enemypos = new Vector3(Random.Range(minInstantiateValue,maxInstantiateValue),6f);
        GameObject enemy = Instantiate(enemyPrefab,enemypos,Quaternion.Euler(0f,0f,180f));
        Destroy(enemy,enemyDestroyTime);
    }
}


// 4. EnemyController.cs:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    void Update()
    {  
        transform.Translate(Vector3.up*speed*Time.deltaTime);
    }
}
