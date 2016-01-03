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
    private int _hit_points;
    public int armored;
    public GameObject[] on_death_spawn;
    public GameObject part_explosion;

    public string type;
    public int team;
    public float leash_mod;
    public float move_speed;
    public float turn_speed;
    private float _turn_speed;
    public float accel;
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
    public float dodge_time;
    public float dodge_timer;
    public bool dodge_ready;


    public SquadControlScript squad;
    private GameObject[] targets;
    public GameObject[] attacks;
    public GameObject[] mods;
    public GameObject[] extras;
    public GameObject[] Effects;
    public GameObject[] DamageEffects;
    public bool leader = false;
    public int map_size;
    private bool up = false;
    private bool right = false;
    public float speed_real = .2f;
    private int scale;

    public float behavior_timer;
    public float behavior_time_agressive = 5;
    public float behavior_time_formation = 3;
    public float behavior_time_defensive = 3;
    public float behavior_time_search_and_destroy = 6;
    public Sprite ship_image;
    public string ship_full_name;

    // Use this for initialization
    void Start() {
        orders = gameObject.AddComponent<Directive>() as Directive;
        scale = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSetupScript>().scale;
        if(this.name !=  "Missile") orders.grid = squad.early_directive.grid;
        Debug.Log("orders: " + orders.grid);
        _turn_speed = turn_speed;
        directive = order_directive;
        behavior_timer = 0;
        _hit_points = hit_points;
        if (!near_side && this.name != "Missile") this.transform.Rotate(new Vector3(0, 180, 0));
        if (target_obj != null && this.name == "Missile") transform.LookAt(target_obj.transform.position);
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

                case "ShipOverlayButton":
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
    void FixedUpdate() {
        grid = new Vector3((Mathf.Floor(transform.position.x / scale)), (transform.position.y / scale), (Mathf.Floor(transform.position.z / scale)));

        distance_to_target = Vector3.Distance(transform.position, target_vector);
        distance_to_order = Vector3.Distance(transform.position, order_vector);

        if (target_obj != null && target_obj.tag == "dead" && directive == "missile")
        {
            target_vector = this.transform.position + this.transform.forward;
        }
        if (target_obj != null) { target_vector = target_obj.transform.position; }
        else { target_vector = order_vector; }
        var speed = move_speed;
        switch (directive)
        {
            case "dodge":
                var x = transform.right;
                if (!right) x = -x;
                var z = transform.up;
                if (!up) z = -z;
                heading_vector = transform.position + x + z;
                turn_speed = _turn_speed + (_turn_speed * .33f);
                break;
            case "agressive":
                speed += .03f;
                if (Mathf.Sin(behavior_timer / 2) > .8f) turn_speed = _turn_speed + (_turn_speed * .33f);
                else turn_speed = _turn_speed;
                if (Vector3.Distance(target_vector, order_vector) < leash_distance * 3) heading_vector = target_vector;
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
                if (distance_to_target < 2.25) target_vector = this.transform.position + transform.forward + (transform.right * Mathf.Sin(1.5f + behavior_timer));
                speed -= .04f;
                break;

            case "search_and_destroy":
                speed += .04f;
                if (Mathf.Sin(behavior_timer / 2) > .8f) turn_speed = _turn_speed + 30f;
                else turn_speed = _turn_speed;
                if (Vector3.Distance(target_vector, order_vector) < leash_distance) heading_vector = target_vector;
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
                if (distance_to_target < .02f) Ram();
                move_speed += .08f * Time.deltaTime;
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

        dodge_timer += Time.deltaTime;
        if (dodge_timer > dodge_time)
        {
            dodge_ready = true;
        }
        if (distance_to_target > 2 && !dead && this.name != "Capital_1") speed += .15f;
        if (distance_to_target < .8 && !dead) speed -= (speed * .15f);
        if (distance_to_target < .4) BehaviorSwitch("dodge");

        if ((speed - speed_real) > .01f) speed_real += accel * Time.deltaTime;
        if ((speed - speed_real) < -.01f)  speed_real -= accel * Time.deltaTime;

        transform.Translate(Vector3.forward * speed_real * Time.deltaTime);
        var rotate = Quaternion.LookRotation(heading_vector - transform.position);
        var angle = Quaternion.Angle(transform.rotation, rotate);
        float timeToComplete = angle / (turn_speed);
        float donePercentage = Mathf.Min(1F, Time.deltaTime / (timeToComplete));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, donePercentage);
        heading = this.transform.forward;


        behavior_timer -= Time.deltaTime;
        if (behavior_timer < 0 && !dead) BehaviorSwitch(null);
        //Debug.DrawLine(this.transform.position, (grid * 3 + new Vector3(1.5f, 0, 1.5f)), white);
        Debug.DrawLine(this.transform.position, heading_vector, Color.yellow);
        Debug.DrawLine(this.transform.position, order_vector, Color.blue);
        if (target_obj != null )Debug.DrawLine(this.transform.position, target_obj.transform.position, Color.red);


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

    public void BehaviorSwitch(string new_directive)
    {

        if (new_directive == null) directive = order_directive;

        switch (directive)
        {
            case "dodge":
                dodge_ready = false;
                dodge_timer = 0;
                var coin = Random.Range(0, 2);
                if (coin == 1) right = !right;
                if (coin == 2) up = !up;
                var x = transform.right;
                if (!right) x = -x;
                var z = transform.up;
                if (!up) z = -z;
                heading_vector = transform.position + x + z;
                behavior_timer = 1;
                break;
            case "agressive":
                heading_vector = target_vector;
                behavior_timer = behavior_time_agressive;
                GetTargets();
                if (Vector3.Distance(target_vector, order_vector) > leash_distance) heading_vector = order_vector;
                leash_distance = 3 * scale;
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
                leash_distance = 1 * scale;
                break;

            case "search_and_destroy":
                GetTargets();
                if (target_obj != null && Vector3.Distance(this.transform.position, order_vector) > leash_distance * 3)
                {
                    heading_vector = target_vector;
                }
                else
                {
                    heading_vector = order_vector;
                }
                behavior_timer = behavior_time_search_and_destroy;
                leash_distance = 5 * scale;
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
                if (ship != null && Vector3.Distance(transform.position, ship.transform.position) < sight_range * scale)
                {
                    targets[_count] = ship;
                    _count++;
                }
            }else if (ship.GetComponent<TargetLocationScript>() != null && ship.GetComponent<TargetLocationScript>().team != team)
            {
                if (ship != null && Vector3.Distance(transform.position, ship.transform.position) < sight_range * scale)
                {
                    targets[_count] = ship;
                    _count++;
                }
            } 
        }
        target_obj = null;
        var dis = sight_range * scale;
        foreach (GameObject ship in targets)
        {
            if (ship != null)
            {
                if (Vector3.Distance(this.transform.position, ship.transform.position) < dis && Vector3.Distance(order_vector, ship.transform.position) <= leash_distance) target_obj = ship;
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
            if (near_side) { order_grid = set_grid - new Vector3(1, 0, 1); }
            else { order_grid = new Vector3(7 - set_grid.x, order_grid_height, 7 - set_grid.z); }

            order_directive = set_directive_type;
            leash_distance = orders.leash * scale;

            Vector3 ord = order_grid * scale;

            if (ord.y > 1.5f * scale) ord.y = scale * 1.5f;
            if (ord.y < -1.5f * scale) ord.y = -1.5f * scale;
            order_vector = ord + new Vector3(scale * .5f, 0, scale * .5f) + new Vector3((Random.Range(scale * -.33f, scale * .33f)), (Random.Range(scale * -.33f, scale * .33f)), (Random.Range(scale * -.33f, scale * .33f)));
        }
    }

    public void TakeDamage(int hit_ammount, int _team)
    {
        if (armored != 0) hit_ammount -= armored;
        if (hit_ammount < 0) hit_ammount = 0;
        hit_points = hit_points - hit_ammount;
        target_obj = null;
        if (hit_points <= 0)
        {
            speed_real = speed_real * .5f;
            Kill();
        }
        if (hit_points <= (_hit_points * .6f) && DamageEffects.Length >= 1 && DamageEffects[0] != null)
        {
            DamageEffects[0].gameObject.SetActive(true);
        }
        if (hit_points <= (_hit_points * .3f) && DamageEffects.Length >= 2 && DamageEffects[1] != null)
        {
            DamageEffects[1].gameObject.SetActive(true);
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
            foreach (GameObject gun in attacks)
            {
                var boom = Instantiate(part_explosion, gun.transform.position, Quaternion.identity) as GameObject;
                Destroy(gun.gameObject);
            }
            foreach (GameObject Effect in Effects)
            {
                Destroy(Effect.gameObject);
            }
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
            BehaviorSwitch(null);
            squad.UpdateScore();
        }
    }

}
