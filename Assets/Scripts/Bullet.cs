using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public float defaultY;
    public Vector3 currentFacing;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += currentFacing*speed*Time.deltaTime;
    }
}
