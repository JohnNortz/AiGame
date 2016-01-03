using UnityEngine;
using System.Collections;

public class ShipYCircleScript : MonoBehaviour
{
    public GameObject ship;
    public int segments;
    public float xradius;
    public float zradius;
    LineRenderer line;
    public int team;
    public Color team0;
    public Color team1;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount(segments);
        line.useWorldSpace = false;
        if (team == 0) line.SetColors(team0, team0);
        if (team == 1) line.SetColors(team1, team1);
    }


    void Update()
    {
        if (ship == null) Destroy(this.gameObject);
        line.SetWidth(.002f * Vector3.Distance(Camera.main.transform.position, ship.transform.position), .002f * Vector3.Distance(Camera.main.transform.position, ship.transform.position));
        
        var grr = Color.grey;
        grr.a = .3f;
        if (ship.tag == "Dead") line.SetColors(grr, grr);
        this.transform.position = new Vector3(ship.transform.position.x, 0f, ship.transform.position.z);

        transform.LookAt(this.transform.position + ship.transform.forward);

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);

        float x = 0f;
        float z = 0f;
        float y = -4f;

        float angle = 2f;

        line.SetPosition(0, new Vector3(x, ((ship.transform.position.y) * 2f), z));
        line.SetPosition(1, new Vector3(x, y, z));
        for (int i = 2; i < (segments); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }
}
