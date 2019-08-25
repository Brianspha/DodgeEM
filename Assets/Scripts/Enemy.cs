using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float defaultY;
    private Transform player;
    public float speed = 5;
    public GameObject damagePrefab;
    public GameObject deathPrefab;
    public GameObject damageText;
    public float health { get; set; }
    Vector3 orginalPos;
    private GameManager Manager;
    // Start is called before the first frame update
    private void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        orginalPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        MoveTowardsPlayer();
        CheckInBounds();
        transform.position = new Vector3(transform.position.x, defaultY, transform.position.z);
    }

    private void CheckInBounds()
    {
        if (transform.position.y <=0)
        {
            transform.position = orginalPos;
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enemy Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("health: " + health);
            health -= Manager.decrementBy + Time.deltaTime;
            if (health <= 0)
            {
                SpawnDeath();
                SelfDestruct();
                Manager.currentLevelKill++;
                Debug.LogError("Manager.currentLevelKill++ " + Manager.currentLevelKill);
            }
            else
            {
                SpawnDamage();
            }
            Debug.Log("Health: " + health);
        }
    }

    private void SpawnDeath()
    {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }

    private void SpawnDamage()
    {
        Instantiate(damagePrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy Collided with: " + other.gameObject.tag);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
