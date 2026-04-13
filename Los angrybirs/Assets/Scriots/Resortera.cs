    using UnityEngine;
using UnityEngine.InputSystem;

public class Resortera : MonoBehaviour
{
   private Controles imputActions;
    
    
    private SpringJoint2D spring;
    private Rigidbody2D rb;

    private bool isDragging = false;
    private bool hasLaunched = false;

    [Header("Slingshot")]
    public Transform slingshotPoint;
    public float maxDistance = 10;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        imputActions = new Controles();
    }

    void Start()
    {
        
        transform.position = slingshotPoint.position;

       
        spring.autoConfigureDistance = false;
        spring.distance = 0;
        spring.enabled = false;

        
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnMouseDown()
    {
        Debug.Log("CLICK detectado");
        if (hasLaunched) return;

        isDragging = true;
        spring.enabled = false; 
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)slingshotPoint.position;

        if (direction.magnitude > maxDistance)
        {
            direction = direction.normalized * maxDistance;
        }

        transform.position = (Vector2)slingshotPoint.position + direction;
    }

    void OnMouseUp()
    {
        GameManager.Instance.UseBird();
        if (!isDragging) return;

        isDragging = false;
        hasLaunched = true;

        
        rb.bodyType = RigidbodyType2D.Dynamic;

        
        spring.connectedAnchor = slingshotPoint.position;
        spring.enabled = true;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        isDragging = false;
        hasLaunched = true;

        rb.bodyType = RigidbodyType2D.Dynamic;

        Vector2 force = (slingshotPoint.position - transform.position) * 300f;
        rb.AddForce(force);


        spring.enabled = false;

        Invoke(nameof(Release), 0.08f);

        Invoke(nameof(SpawnNewBird), 2f);
    }

    void SpawnNewBird()
    {
        FindFirstObjectByType<BirdSpawner>().SpawnBird();
    }


    void Release()
    {
        spring.enabled = false;
    }
}
