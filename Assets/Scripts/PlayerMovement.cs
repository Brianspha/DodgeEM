using EZCameraShake;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 10;
    public Vector3 MoveVector;
    public float MinX, MaxX, MinZ, MaxZ;
    public float SmashHit = 15;
    public int LastPressed { get; private set; } = -1; //0 left 1 right up 2 down 3
    CameraShaker shaker;
    public float magn = 1000, rough = 500, fadeIn = 1f, fadeOut = 2f;
    public float defaultY;
    Vector3 defaultPos;
    private void Start()
    {
        shaker = Camera.main.GetComponent<CameraShaker>();
        defaultPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(transform.position.y <= 0)
        {
            transform.position = defaultPos;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 1, transform.position.y,transform.position.z), Time.deltaTime * Speed);
            if (MoveVector.x >= MinX)
            {
                transform.position = MoveVector;
                LastPressed = 0;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Time.deltaTime * Speed);
            if (MoveVector.x <= MaxX)
            {
                transform.position = MoveVector;
                LastPressed = 1;
            }
        }
         if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Time.deltaTime * Speed);
            if (MoveVector.z <= MaxZ)
            {
                transform.position = MoveVector;
                LastPressed = 2;
            }
        }
         if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Time.deltaTime * Speed);
            if (MoveVector.z >= MinZ)
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
                    MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - SmashHit, transform.position.y,transform.position.z), Time.deltaTime * SmashHit);
                    break;
                case 1:
                    MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + SmashHit, transform.position.y,transform.position.z), Time.deltaTime * SmashHit);
                    break;
                case 2:
                    MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + SmashHit), Time.deltaTime * SmashHit);
                    break;
                case 3:
                    MoveVector = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - SmashHit), Time.deltaTime * SmashHit);
                    break;
            }
            if (MoveVector.z >= MinZ && MoveVector.z <= MaxZ && MoveVector.x <= MaxX && MoveVector.x >= MinX)
            {
                transform.position = MoveVector;
                shaker.ShakeOnce(magn, rough, fadeIn, fadeOut);
            }
            transform.position = new Vector3(transform.position.x, defaultY, transform.position.z);
        }
        LastPressed = -1;
    }
}
