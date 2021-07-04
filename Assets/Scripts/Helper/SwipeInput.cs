using UnityEngine;
public class SwipeInput : MonoBehaviour
{

    public Vector2 direction;
    Vector2 initialPos;
    public Animator carChoosePanel;
    private void Start()
    {
        
    
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            direction = ((Vector2)(Input.mousePosition) - initialPos);
           
        }
  
    }
}
