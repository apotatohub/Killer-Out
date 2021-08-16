using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public Transform target;
    [SerializeField] float Speed;
    [SerializeField] float offsetY, offsetZ;
    private void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        offsetZ = 0;
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 pos = new Vector3(target.position.x, transform.position.y + offsetY, target.position.z+offsetZ);
            transform.position = Vector3.MoveTowards(transform.position, pos, Speed);
        }
    }
}
