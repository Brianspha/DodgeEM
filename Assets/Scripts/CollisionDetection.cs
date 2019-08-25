using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float RepelStrength = 4500;
    private Vector2 movetowards;
    private RipplePostProcessor Ripple;
    // Start is called before the first frame update
    private GameManager manager;
    private Vector2 originalPosition;
    public GameObject enemyDamage;
    PlayerMovement Player;
    public void Start()
    {
        Debug.Log("start");
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Ripple = Camera.main.GetComponent<RipplePostProcessor>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Player.transform.forward * RepelStrength, ForceMode.Force);
            Instantiate(enemyDamage, transform.position, Quaternion.identity);
            Ripple.Ripple();
            manager.IncrementScore();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Touching with: " + collision.gameObject.tag);
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("End Touching with: " + collision.gameObject.tag);
    }


}
