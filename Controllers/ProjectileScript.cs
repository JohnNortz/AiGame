using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public ShipScript parent_ship_script;
    public int damage;
    public GameObject target;
    public float projectile_velocity;
    public Vector3 firing_vector;
    public float life_time;
    private float time;
    public GameObject explosion;
    public float off_angle;

    public GameObject translator;
    // Use this for initialization
    void Start ()
    {
        var target_script = target.GetComponent<ShipScript>();
        var travel_distance = Vector3.Distance(this.transform.position, target.transform.position);
        var travel_time = (travel_distance / projectile_velocity);
        firing_vector = target.transform.position + (target_script.heading * target_script.speed_real * travel_time);
        travel_distance = Vector3.Distance(this.transform.position, target.transform.position);
        travel_time = (travel_distance / projectile_velocity);
        firing_vector = target.transform.position + (target_script.heading * target_script.speed_real * travel_time);
        this.transform.LookAt(firing_vector);
        transform.Rotate(Random.Range(-off_angle, off_angle), Random.Range(-off_angle, off_angle), Random.Range(-off_angle, off_angle));

        if (translator != null)
        {
            var new_proj = Instantiate(translator, this.transform.position, Quaternion.identity) as GameObject;
            var missile_script = new_proj.GetComponent<ShipScript>();
            missile_script.target_obj = target;
            missile_script.team = parent_ship_script.team;
            missile_script.missile_launcher = parent_ship_script;
            missile_script.ram_damage = damage;
            missile_script.order_directive = "missile";
            new_proj.name = "Missile";
            missile_script.target_vector = target.transform.position;
            missile_script.order_vector = target.transform.position;
            missile_script.order_grid = target_script.grid;

            Destroy(this.gameObject);

        }
        time = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * projectile_velocity * Time.deltaTime);

        time += Time.deltaTime;
        if (time > life_time)
        {
            Destroy(this.gameObject);
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ShipScript>().team != parent_ship_script.team)
        {
            other.gameObject.GetComponent<ShipScript>().TakeDamage(damage, parent_ship_script);
            var explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
        }
    }
}
