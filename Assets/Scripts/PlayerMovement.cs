using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 10;
    public Vector2 moveVector;
    public float MinX, MaxX, MinY, MaxY;

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 1, transform.position.y), Time.deltaTime * Speed);
            if(moveVector.x >= MinX)
            {
                transform.position = moveVector;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 1, transform.position.y), Time.deltaTime * Speed);
            if (moveVector.x <= MaxX)
            {
                transform.position = moveVector;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), Time.deltaTime * Speed);
            if (moveVector.y <= MaxY)
            {
                transform.position = moveVector;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 1), Time.deltaTime * Speed);
            if (moveVector.y >= MinY)
            {
                transform.position = moveVector;
            }
        }
    }
}
