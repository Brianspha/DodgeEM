using EZCameraShake;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 10;
    public Vector2 MoveVector;
    public float MinX, MaxX, MinY, MaxY;
    public float SmashHit = 15;
    private int LastPressed = -1; //0 left 1 right up 2 down 3
    CameraShaker shaker;
    public float magn = 1000, rough = 500, fadeIn = 1f, fadeOut = 2f;

    private void Start()
    {
        shaker = Camera.main.GetComponent<CameraShaker>();
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
            MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 1, transform.position.y), Time.deltaTime * Speed);
            if (MoveVector.x >= MinX)
            {
                transform.position = MoveVector;
                LastPressed = 0;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 1, transform.position.y), Time.deltaTime * Speed);
            if (MoveVector.x <= MaxX)
            {
                transform.position = MoveVector;
                LastPressed = 1;
            }
        }
         if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), Time.deltaTime * Speed);
            if (MoveVector.y <= MaxY)
            {
                transform.position = MoveVector;
                LastPressed = 2;
            }
        }
         if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 1), Time.deltaTime * Speed);
            if (MoveVector.y >= MinY)
            {
                transform.position = MoveVector;
                LastPressed = 3;
            }
        }
         if (Input.GetKey(KeyCode.Space))
        {
            switch (LastPressed)
            {
                case 0:
                    MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - SmashHit, transform.position.y), Time.deltaTime * SmashHit);
                    break;
                case 1:
                    MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + SmashHit, transform.position.y), Time.deltaTime * SmashHit);
                    break;
                case 2:
                    MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + SmashHit), Time.deltaTime * SmashHit);
                    break;
                case 3:
                    MoveVector = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - SmashHit), Time.deltaTime * SmashHit);
                    break;
            }
            if (MoveVector.y >= MinY && MoveVector.y <= MaxY && MoveVector.x <= MaxX && MoveVector.x >= MinX)
            {
                transform.position = MoveVector;
                shaker.ShakeOnce(magn, rough, fadeIn, fadeOut);
            }
            LastPressed = -1;
        }
    }
}
