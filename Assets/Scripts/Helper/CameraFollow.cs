using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public GameObject target;
    [SerializeField] float Speed;
    [SerializeField] float offsetY,offsetZ;
    void Update()
    {
        if (target != null)
        {
            Vector3 pos = new Vector3(target.GetComponentInParent<Transform>().position.x, transform.position.y + offsetY, target.GetComponentInParent<Transform>().position.z+offsetZ);
            transform.position = Vector3.MoveTowards(transform.position, pos, Speed);
        }
    }
}
