using UnityEngine;

public class PlatformSideways : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float range;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float x = Mathf.PingPong(Time.time * speed, range) - (range / 2.0f);
        transform.position = new Vector3(startPosition.x + x, startPosition.y, startPosition.z);
    }
}
