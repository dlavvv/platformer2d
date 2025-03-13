using UnityEngine;

public class PlatformUpDown : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public bool moveUpFirst;

    private Vector3 startPosition;

    void Start()
    {
        // Record the initial position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        float direction = moveUpFirst ? 1.0f : -1.0f;
        float x = Mathf.PingPong(Time.time * speed, range) - (range / 2.0f);
        transform.position = new Vector3(startPosition.x, startPosition.y + x * direction, startPosition.z);
    }
}
