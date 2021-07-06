using UnityEngine;


[RequireComponent(typeof(Rigidbody),typeof(SwipeInput))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [SerializeField] float editorSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float RotationSpeed;
    SwipeInput SwipeInput;
    public Vector3 direction;

   

    private void Awake()
    {
        SwipeInput = GetComponent<SwipeInput>();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            speed = editorSpeed;
        }
        direction = SwipeInput.direction.normalized;
        if (SwipeInput.direction.magnitude >=30f )
        {
            Vector3 _direction = new Vector3(direction.x, 0, direction.y);
            if (_direction.magnitude!=0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), RotationSpeed * Time.deltaTime);
                rb.velocity = transform.forward * speed * 10 * Time.fixedDeltaTime;
            
            }

        }

    }
}
