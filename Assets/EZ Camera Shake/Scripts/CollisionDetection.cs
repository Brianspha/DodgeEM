using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float RepelStrength = 10;

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-collision.gameObject.transform.position.x, -collision.gameObject.transform.position.y));
        }
    }

}
