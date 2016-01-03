using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

    public Vector3 firing_vector;
    public Vector3 normal_vector;
    public GameObject target;
    public GameObject[] ships = new GameObject[30];
    public GameObject[] in_range;
    public float firing_angle;
    public bool get_once = false;

    public bool ready;
    public float ready_time;
    private float _ready_time;
    public float ready_timer;
    public string gun_normal;
    public int team;
    public bool offensive;

    public GameObject create_ui_object;
    public GameObject projectile;
    public int ammo_count;
    public int ammo_count_current;
    public int projectile_damage;
    public float range;
    public float range_timer;
    public float range_time;

    public ShipScript parent_ship_script;

    // Use this for initialization
    void Start () {
        //firing_vector = /*new Vector3(0, Mathf.Sin(Mathf.Deg2Rad * firing_angle), Mathf.Cos(Mathf.Deg2Rad * firing_angle))*/;
        parent_ship_script = transform.parent.GetComponent<ShipScript>();
        firing_angle = firing_angle * 2;
        ammo_count_current = ammo_count;
        if (range_time == 0) range_time = 3;
    }
	
	// Update is called once per frame
	void Update () {
        range_timer -= Time.deltaTime;
        if (range_timer < 0)
        {
            switch (gun_normal)
            {
                case "forward":
                    normal_vector = this.transform.forward;
                    break;
                case "right":
                    normal_vector = this.transform.right;
                    break;
                case "left":
                    normal_vector = -this.transform.right;
                    break;
                case "up":
                    normal_vector = this.transform.up;
                    break;
                case "down":
                    normal_vector = -this.transform.up;
                    break;
            }


            range_timer = range_time + (Random.Range(0f, .7f));
            var _ships = GameObject.FindGameObjectsWithTag("Ship");
            get_once = true;
            int _count = 0;
            foreach (GameObject ship in _ships)
            {
                if (ship != null && ship.GetComponent<ShipScript>() != null && ship.GetComponent<ShipScript>().team != team)
                {
                    ships[_count] = ship;
                    _count++;
                }
            }
            in_range = new GameObject[30];
            var count = 0;
            foreach(GameObject ship in ships)
            {
                if (ship != null && ship.tag != "Dead")
                {
                    if (offensive)
                    {
                        if (ship.GetComponent<ShipScript>().name != "Missile" && Vector3.Distance(transform.position, ship.transform.position) < range * 3)
                        {
                            in_range[count] = ship;
                            count++;
                        }
                    }
                    else
                    {
                        if (ship != null && Vector3.Distance(transform.position, ship.transform.position) < range * 3)
                        {
                            in_range[count] = ship;
                            count++;
                        }
                    }
                }
                
            }
        }
        var dis = range * 3;
        target = null;
        foreach(GameObject ship in in_range)
        {
            if (ship != null)
            {
                 if (Vector3.Distance(this.transform.position, ship.transform.position) < dis) target = ship;
                 dis = Vector3.Distance(transform.position, ship.transform.position);
            }
        }

        if (ready_timer > ready_time && ammo_count_current > 0)
        {
            ready = true;
        }
        else
        {
            ready_timer += Time.deltaTime;
        }

        if (ready && ammo_count_current > 0 && target != null)
        {
            firing_vector = (this.transform.position - target.transform.position) * (1 / Vector3.Distance(this.transform.position, target.transform.position));
            var angle = Vector3.Angle(normal_vector, firing_vector);
            
            if (angle <= firing_angle * 2)
            {
                var new_proj = Instantiate(projectile, this.transform.position, this.transform.rotation) as GameObject;
                var projectile_script = new_proj.GetComponent<ProjectileScript>();

                projectile_script.firing_vector = target.transform.position;
                projectile_script.parent_ship_script = parent_ship_script;
                projectile_script.damage = projectile_damage;
                projectile_script.target = target;


                ready = false;
                ready_timer = 0;
                ammo_count_current--;
            }
        }
        //Debug.DrawLine(this.transform.position, this.transform.position - normal_vector * range * 3, Color.blue);
        
     }
}
