using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bat : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    Vector3 _initialPosition;
    private bool _batWasLaunched; // Default de boolean eh false
    private float _timeGrounded = 0;

    [SerializeField] private float _launchIntensity = 100;

    

    private void Awake()
    {
        _initialPosition = transform.position;
    }

  private void Update()
    {
        //Arrow from initial position to launch position
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);

        //Refresh game after launch -- count time when the bat is on the ground
        if (_batWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeGrounded += Time.deltaTime;
        }

        //Restart game when the bat get away from the map
        if (transform.position.y > 10 || transform.position.x>40 ||transform.position.x<-60||_timeGrounded>2)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
  private void OnMouseDown()
    {
        //Sign that whe are dragging the bat
        GetComponent<SpriteRenderer>().color = Color.green;
        //Enable the arrow line to launch properly
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector3 launchDirection = _initialPosition - transform.position;
        launchDirection.z = 0;

        _batWasLaunched = true;
        //Unable the arrow line after the launch
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().AddForce(launchDirection* _launchIntensity);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
    //Drag your bat with this 
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

}
