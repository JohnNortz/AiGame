using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipScript : MonoBehaviour {

    public string order_directive = "search_and_destroy";
    public string directive = "search_and_destroy";

    public Directive orders;

    public Vector3 order_grid;
    public int order_grid_height;

    public Vector3 order_vector;
    public Vector3 heading_vector;
    public Vector3 target_vector;
    public Vector3 formation_vector;
    public Vector3 grid;
    public Vector3 origin;
    public GameObject target_obj;

    public float leash_distance;
    public Vector3 heading;
    public bool near_side;
    public int ram_damage;

    public int hit_points;
    public int armored;
    public GameObject[] on_death_spawn;
    public GameObject part_explosion;

    public string type;
    public int team;
    public float leash_mod;
    public float move_speed;
    public float turn_speed;
    private float _turn_speed;
    public float sight_range;
    public float distance_to_target;
    public float distance_to_order;
    public int quart = 4;
    public bool dead;

    public float set_leash;
    public string set_directive_type;
    public Vector3 set_grid;
    public int set_grid_height;
    public int squad_count;
    public int ship_count;


    public SquadControlScript squad;
    private GameObject[] targets;
    public GameObject[] attacks;
    public GameObject[] mods;
    public GameObject[] extras;
    public GameObject[] Effects;
    public bool leader = false;
    public int map_size;

    public float behavior_timer;
    public float behavior_time_agressive = 5;
    public float behavior_time_formation = 3;
    public float behavior_time_defensive = 3;
    public float behavior_time_search_and_destroy = 6;

    // Use this for initialization
    void Start() {
        orders = gameObject.AddComponent<Directive>() as Directive;
        orders.grid = squad.early_directive.grid;
        Debug.Log("orders: " + orders.grid);
        _turn_speed = turn_speed;
        directive = order_directive;
        behavior_timer = 0;
        if (!near_side) this.transform.Rotate(new Vector3(0, 180, 0));
        if (target_obj != null) transform.LookAt(target_obj.transform.position);
        grid = new Vector3((Mathf.Floor(transform.position.x / 3)), (transform.position.y / 3), (Mathf.Floor(transform.position.z / 3)));
        foreach (GameObject gun in attacks)
        {
            gun.GetComponent<GunScript>().team = team;
        }
        foreach (GameObject extra in extras)
        {
            switch (extra.name) {
                case "Ship_y_line":
                    var y = Instantiate(extra, this.transform.position, Quaternion.identity) as GameObject;
                    var yscript = y.GetComponent<ShipYCircleScript>();
                    yscript.team = team;
                    yscript.ship = this.gameObject;
                    break;

                case "Capital_y_line":
                    var c = Instantiate(extra, this.transform.position, Quaternion.identity) as GameObject;
                    var cscript = c.GetComponent<ShipYCircleScript>();
                    cscript.team = team;
                    cscript.ship = this.gameObject;
                    break;

                case "Button":
                    var x = Instantiate(extra, this.transform.position, Quaternion.identity) as GameObject;
                    var xscript = x.GetComponent<ShipButtonScript>();
                    xscript.team = team;
                    xscript.Ship = this.gameObject;
                    break;

                case "ButtonDisplay":
                    var z = Instantiate(extra, Vector3.zero, Quaternion.identity) as GameObject;
                    z.GetComponent<RectTransform>().SetParent(Camera.main.GetComponent<CameraScript>().canvas.GetComponent<RectTransform>());
                    var zscript = z.GetComponent<ShipDisplayScript>();
                    zscript.Ship = this.gameObject;
                    zscript.label_text = this.name;
                    zscript.hp_full = this.hit_points;
                    zscript.team = team;
                    zscript.squad_count = squad_count;
                    zscript.ship_count = ship_count;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        grid = new Vector3((Mathf.Floor(transform.position.x / 3)), (transform.position.y / 3), (Mathf.Floor(transform.position.z / 3)));

        distance_to_target = Vector3.Distance(transform.position, target_vector);
        distance_to_order = Vector3.Distance(transform.position, order_vector);

        if (target_obj == null && directive == "missile")
        {
            target_vector = this.transform.position + this.transform.forward;
        }
        if (target_obj != null) { target_vector = target_obj.transform.position; }
        else { target_vector = order_vector; }
        var speed = move_speed;
        switch (directive)
        {
            case "agressive":
                speed += .03f;
                if (Mathf.Sin(behavior_timer / 2) > .8f) turn_speed = _turn_speed + 40f;
                else turn_speed = _turn_speed;
                heading_vector = target_vector;
                break;

            case "formation":
                if (leader) heading_vector = order_vector;
                else
                {
                    heading_vector = formation_vector;
                }
                speed -= .03f;
                break;

            case "defensive":
                target_vector = order_vector;
                speed -= .04f;
                break;

            case "search_and_destroy":
                speed += .04f;
                if (Mathf.Sin(behavior_timer / 2) > .8f) turn_speed = _turn_speed + 30f;
                else turn_speed = _turn_speed;
                heading_vector = target_vector;
                break;
                
            case "rendevous":
                heading_vector = order_vector;
                if (distance_to_order < .2)
                {
                    turn_speed = 0;
                    move_speed = 0;
                }
                 
                break;

            case "missile":
                if (target_obj == null)
                {
                    heading_vector = this.transform.position + this.transform.forward;
                } else
                {
                    heading_vector = target_vector;
                }
                orders = null;
                order_grid = Vector3.zero;
                leash_distance = 0;
                if (distance_to_target < .05f) Ram();
                move_speed += .04f * Time.deltaTime;
                var rotateM = Quaternion.LookRotation(heading_vector - transform.position);
                var angleM = Quaternion.Angle(transform.rotation, rotateM);
                float timeToCompleteM = angleM / (turn_speed);
                float donePercentageM = Mathf.Min(1F, Time.deltaTime / (timeToCompleteM));
                transform.rotation = Quaternion.Slerp(transform.rotation, rotateM, donePercentageM);
                speed = move_speed;
                break;
            case "dead":
                turn_speed = 0;
                directive = "dead";
                set_directive_type = "dead";
                order_directive = "dead";
                break;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        var rotate = Quaternion.LookRotation(heading_vector - transform.position);
        var angle = Quaternion.Angle(transform.rotation, rotate);
        float timeToComplete = angle / (turn_speed);
        float donePercentage = Mathf.Min(1F, Time.deltaTime / (timeToComplete));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, donePercentage);
        heading = this.transform.forward;

        behavior_timer -= Time.deltaTime;
        if (behavior_timer < 0 && !dead) BehaviorSwitch();
        //Debug.DrawLine(this.transform.position, (grid * 3 + new Vector3(1.5f, 0, 1.5f)), white);
        Debug.DrawLine(this.transform.position, heading_vector, Color.yellow);
        Debug.DrawLine(this.transform.position, order_vector, Color.blue);


        //Debug.DrawLine((origin * 3) + (Vector3.up * .3f) + new Vector3(1.5f, 0, 1.5f), (origin * 3) + new Vector3(1.5f, 0, 1.5f) - (Vector3.up * .3f), Color.white);
        //if (near_side)
        //{
        //    Debug.DrawLine((origin * 3) + (orders.grid[quart] * 3) + (Vector3.up * .3f) + new Vector3(1.5f, 0, 1.5f), (origin * 3) + (orders.grid[quart] * 3) - (Vector3.up * .3f) + new Vector3(1.5f, 0, 1.5f), Color.white);

        //    Debug.DrawLine((origin * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) + (orders.grid[0] * 3) + new Vector3(1.5f, 0, 1.5f), Color.yellow);
        //    Debug.DrawLine((origin * 3) + (orders.grid[0] * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) + (orders.grid[1] * 3) + new Vector3(1.5f, 0, 1.5f), Color.yellow);
        //    Debug.DrawLine((origin * 3) + (orders.grid[1] * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) + (orders.grid[2] * 3) + new Vector3(1.5f, 0, 1.5f), Color.yellow);
        //    Debug.DrawLine((origin * 3) + (orders.grid[2] * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) + (orders.grid[3] * 3) + new Vector3(1.5f, 0, 1.5f), Color.yellow);
        //}
        //else
        //{
        //    Debug.DrawLine((origin * 3) - (orders.grid[quart] * 3) + (Vector3.up * .3f) + new Vector3(1.5f, 0, 1.5f), (origin * 3) - (orders.grid[quart] * 3) - (Vector3.up * .3f) + new Vector3(1.5f, 0, 1.5f), Color.white);

        //    Debug.DrawLine((origin * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) - (orders.grid[0] * 3) + new Vector3(1.5f, 0, 1.5f), Color.magenta);
        //    Debug.DrawLine((origin * 3) - (orders.grid[0] * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) - (orders.grid[1] * 3) + new Vector3(1.5f, 0, 1.5f), Color.magenta);
        //    Debug.DrawLine((origin * 3) - (orders.grid[1] * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) - (orders.grid[2] * 3) + new Vector3(1.5f, 0, 1.5f), Color.magenta);
        //    Debug.DrawLine((origin * 3) - (orders.grid[2] * 3) + new Vector3(1.5f, 0, 1.5f), (origin * 3) - (orders.grid[3] * 3) + new Vector3(1.5f, 0, 1.5f), Color.magenta);
        //}
        //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward * - (leash_distance * 3), Color.cyan);
    }

    public void BehaviorSwitch()
    {

        directive = order_directive;

        switch (directive)
        {
            case "agressive":
                heading_vector = target_vector;
                behavior_timer = behavior_time_agressive;
                GetTargets();
                if (Vector3.Distance(this.transform.position, order_vector) > leash_distance * 3) heading_vector = order_vector;
                break;

            case "formation":
                behavior_timer = behavior_time_formation;
                if (leader) heading_vector = order_vector;
                else
                {
                    heading_vector = formation_vector;
                }
                break;

            case "defensive":
                behavior_timer = behavior_time_defensive;
                break;

            case "search_and_destroy":
                if (target_obj != null)
                {
                    heading_vector = target_vector;
                }
                else
                {
                    heading_vector = order_vector;
                }
                if (Vector3.Distance(this.transform.position, order_vector) > leash_distance * 3) heading_vector = order_vector;
                behavior_timer = behavior_time_search_and_destroy;
                GetTargets();
                break;

            case "rendevous":
                break;

            case "missile":
                break;
            case "dead":
                directive = "dead";
                set_directive_type = "dead";
                order_directive = "dead";
                behavior_timer = 100000;
                break;
        }
    }

    public void GetTargets()
    {
        var _ships = GameObject.FindGameObjectsWithTag("Ship");
        int _count = 0;
        targets = new GameObject[_ships.Length];
        foreach (GameObject ship in _ships)
        {
            if (ship != null && ship.GetComponent<ShipScript>() != null && ship.GetComponent<ShipScript>().team != team && ship.name != "Missile")
            {
                if (ship != null && Vector3.Distance(transform.position, ship.transform.position) < sight_range * 3)
                {
                    targets[_count] = ship;
                    _count++;
                }
            }else if (ship.GetComponent<TargetLocationScript>() != null && ship.GetComponent<TargetLocationScript>().team != team)
            {
                if (ship != null && Vector3.Distance(transform.position, ship.transform.position) < sight_range * 3)
                {
                    targets[_count] = ship;
                    _count++;
                }
            } 
        }
        target_obj = null;
        var dis = sight_range * 3;
        foreach (GameObject ship in targets)
        {
            if (ship != null)
            {
                if (Vector3.Distance(this.transform.position, ship.transform.position) < dis) target_obj = ship;
                dis = Vector3.Distance(transform.position, ship.transform.position);
            }
        }
    }

    public void DirectiveChange(Vector3 _grid, int _grid_height, string _type, float _leash)
    {
        if (!dead)
        {
            orders.grid = _grid;
            orders.leash = _leash;
            orders.grid_height = _grid_height;
            orders.directive_type = _type;
            origin = grid;
            DirectiveQuart();
        }
    }

    public void DirectiveQuart()
    {
        if (!dead)
        {
            set_directive_type = orders.directive_type;
            set_grid_height = orders.grid_height;
            set_grid = new Vector3(orders.grid.x, orders.grid_height, orders.grid.z);
            if (near_side) { order_grid = set_grid - new Vector3(-1, 0, -1); }
            else { order_grid = new Vector3(8 - set_grid.x, order_grid_height, 8 - set_grid.z); }

            order_directive = set_directive_type;
            leash_distance = orders.leash;

            Vector3 ord = order_grid * 3;

            if (ord.y > 4.5f) ord.y = 4.5f;
            if (ord.y < -4.5f) ord.y = -4.5f;
            order_vector = ord + new Vector3(1.5f, 0, 1.5f) + new Vector3((Random.Range(-.3f, .3f)), (Random.Range(-.3f, .3f)), (Random.Range(-.3f, .3f)));
        }
    }

    public void TakeDamage(int hit_ammount, int _team)
    {
        Debug.Log("got hit by team: " + _team + "    for damage: " + hit_ammount + "   hp remaining: " + hit_points);
        if (armored != 0) hit_ammount -= armored;
        if (hit_ammount < 0) hit_ammount = 0;
        hit_points = hit_points - hit_ammount;

        if (hit_points <= 0)
        {
            Kill();
        }
    }

    public void Ram()
    {
        target_obj.GetComponent<ShipScript>().TakeDamage(ram_damage, team);
        Kill();
    }

    void OnTriggerEnter(Collider other)
    {
        if (directive == "missile" && behavior_timer < -1 && other.tag == "Ship" && other.GetComponent<ShipScript>().team != team)
        {
            other.transform.GetComponent<ShipScript>().TakeDamage(ram_damage, team);
            Kill();
        }
    }

    public void Kill()
    {
        if (!dead)
        {
            foreach (GameObject gib in on_death_spawn)
            {
                var corpse = Instantiate(gib, transform.position, this.transform.rotation) as GameObject;
            }
            if (directive == "missile") Destroy(this.gameObject);

            float off_angle = 30f;
            transform.Rotate(Random.Range(-off_angle, off_angle), Random.Range(-off_angle, off_angle), Random.Range(-off_angle, off_angle));

            dead = true;
            this.tag = "Dead";
            directive = "dead";
            set_directive_type = "dead";
            order_directive = "dead";
            dead = true;
            move_speed = .01f;
            _turn_speed = 1;
            orders = null;
            order_vector = new Vector3(this.transform.position.x + Random.Range(-2, 2), this.transform.position.y + Random.Range(-2, 2), this.transform.position.z + Random.Range(-2, 2));
            target_vector = new Vector3(this.transform.position.x + Random.Range(-2, 2), this.transform.position.y + Random.Range(-2, 2), this.transform.position.z + Random.Range(-2, 2));
            BehaviorSwitch();
            foreach (GameObject gun in attacks)
            {
                var boom = Instantiate(part_explosion, gun.transform.position, Quaternion.identity) as GameObject;
                Destroy(gun.gameObject);
            }
            foreach (GameObject Effect in Effects)
            {
                Destroy(Effect.gameObject);
            }
        }
    }

}
