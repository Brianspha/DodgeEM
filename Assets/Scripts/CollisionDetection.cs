using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public float RepelStrength = 500;
    private GameObject enemy;
    private Vector2 movetowards;

    // Start is called before the first frame update
    private GameManager manager;
    private bool canMove = true;
    private Vector2 originalPosition;
    public void Start()
    {
        Debug.Log("start");
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        //if (canMove)
        //{
        //    enemy.transform.position = new Vector2(enemy.transform.position.x - 1, enemy.transform.position.y - 1);
        //}
    }

    private void CheckWhichSide()
    {
        float y = 0f;
        float x = 0f;
        if (enemy.transform.position.y <= manager.MinY)
        {
            y = manager.MinY;
        }
        if (enemy.transform.position.y >= manager.MaxY)
        {
            y = manager.MaxY;
        }
        else if (enemy.transform.position.y >= manager.MaxX)
        {
            x = manager.MaxX;
        }
        else if (enemy.transform.position.y <= manager.MinX)
        {
            x = manager.MinX;
        }
        enemy.transform.position = new Vector2(x, y);
        canMove = false;
    }
    private bool CheckBoundry()
    {
        return (enemy.transform.position.y >= manager.MinY && enemy.transform.position.y <= manager.MaxY && enemy.transform.position.y <= manager.MaxX && enemy.transform.position.y >= manager.MinX);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * RepelStrength, ForceMode2D.Force);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
    }

}
