using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScript : MonoBehaviour {


    private Vector3 dragOrigin;
    public float cameraMaxPos;
    private float cameraZOffset = -7;
    private Vector3 currentPosition = Vector3.zero;
    private Vector3 translation;
    private Vector3 TargetPos;
    private Vector3 StoredPosition;
    private Quaternion StoredAngle;

    public bool Chase = false;
    public float panSpeed;
    public float zoomSpeed;
    public float ScrollArea;
    public Vector3 ChaseOffset;
    public float orbitDistance = 3.5F;
    public int orbitCameraSpeed = 1;

    float xSpeed = 175.0F;
    float ySpeed = 175.0F;
    int yMinLimit = -80; //Lowest vertical angle in respect with the target.
    int yMaxLimit = 80;
    float minDistance = .1f; //Min distance of the camera from the target
    int maxDistance = 30;
    private float x = 0.0F;
    private float y = 0.0F;
    public float waitTimer;
    public float waitTime;
    public Canvas canvas;
    public GameObject Target;
    public bool free = false;
    public bool locked = false;
    
    // Use this for initialization
	void Start () {

        cameraMaxPos = PlayerPrefs.GetInt("mapSize");
        
        StoredAngle = transform.rotation;
        Chase = true;

        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Target == null) Target = GameObject.FindGameObjectWithTag("Ship");
        if (Chase)
        {
            if (Target == null)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    waitTimer = 0;
                }
            }

            orbitDistance += Input.GetAxis("Mouse ScrollWheel") * orbitDistance;
            orbitDistance = Mathf.Clamp(orbitDistance, minDistance, maxDistance);

            //Detect mouse drag;
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02F;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02F;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);
            var LRotation = transform.rotation;
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            if (!free && Target.tag == "Ship")
            {
                if (Target.GetComponent<ShipScript>().target_obj != null && Target.tag != "Dead")
                {
                    var lookPos = Target.GetComponent<ShipScript>().target_obj.transform.position - transform.position;
                    LRotation = Quaternion.LookRotation(lookPos);
                }
                else
                {
                    free = true;
                }
            }
            var position2 = rotation * new Vector3(0f, 0f, -orbitDistance) + Target.transform.position;

            transform.position = Vector3.Slerp(transform.position, position2, orbitCameraSpeed * Time.deltaTime);

            //tranvar lookPossform.position = Target.transform.position + ChaseOffset;
            if (free)
            {
                var lookPos = Target.transform.position - transform.position;
                LRotation = Quaternion.LookRotation(lookPos);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, LRotation, Time.deltaTime * 10);
        }

    }

    public void CameraTarget()
    {
        free = false;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

}
