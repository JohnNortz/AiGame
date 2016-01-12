using UnityEngine;
using System.Collections;

public class SquadControlScript : MonoBehaviour {

    public Directive early_directive;
    public Vector3 early_grid;
    public Directive middle_directive;
    public Vector3 middle_grid;
    public Directive late_directive;
    public Directive end_directive;
    public Directive end_over_directive;
    public Directive dir;
    public Directive dir1;
    public Directive dir2;
    public Directive dir3;
    public float game_time;
    public GameSetupScript game;
    public int quarter = 0;
    public GameObject ship_prefab;
    public int ship_number;
    public GameObject[] ships;
    public Vector3 starting_position;
    public Vector3[] offsets;
    public int team;
    public int squad_count;
    public int ship_worth;

    public int squad_kills;
    public int squad_deaths;
    public int squad_leader_kills;
    public int squad_leader_deaths;
    public int squad_points;
    public int games_played;
    public int games_won;

    public float warpTimer;
    public float[] warpTimes;
    private float[] warpTimesOriginal;
    public int warp_count = 0;
    public bool going = false;

    void Start()
    {
        
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSetupScript>();
        game.GetSquad(this.gameObject);
        dir = gameObject.AddComponent<Directive>() as Directive;
        dir.grid = early_directive.grid;
        dir.grid_height = early_directive.grid_height;
        dir.leash = early_directive.leash;
        dir.directive_type = early_directive.directive_type;
        early_directive = dir;

        dir1 = gameObject.AddComponent<Directive>() as Directive;
        dir1.grid = middle_directive.grid;
        dir1.grid_height = middle_directive.grid_height;
        dir1.leash = middle_directive.leash;
        dir1.directive_type = middle_directive.directive_type;
        middle_directive = dir1;

        dir2 = gameObject.AddComponent<Directive>() as Directive;
        dir2.grid = late_directive.grid;
        dir2.grid_height = late_directive.grid_height;
        dir2.leash = late_directive.leash;
        dir2.directive_type = late_directive.directive_type;
        late_directive = dir2;

        dir3 = gameObject.AddComponent<Directive>() as Directive;
        dir3.grid = end_directive.grid;
        dir3.grid_height = end_directive.grid_height;
        dir3.leash = end_directive.leash;
        dir3.directive_type = end_directive.directive_type;
        end_directive = dir3;

    }

    void Update()
    {
        if (going)
        {
            warpTimer += Time.deltaTime;
            if (warp_count < ships.Length)
            {
                foreach (float warp in warpTimes)
                {
                    if (warpTimer > warp && warp != 0)
                    {
                        var ship = ships[warp_count];
                        ship.transform.position = starting_position * GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSetupScript>().scale + offsets[warp_count];
                        ship.GetComponent<ShipScript>().DirectiveChange(early_directive.grid, early_directive.grid_height, early_directive.directive_type, early_directive.leash);
                        warp_count++;
                    }
                }
            }
        }
    }

    public void UpdateScore()
    {
        game.UpdateScores();
    }

    public void ControllerReset()
    {
        Debug.Log("???????????????????????????????????");
        quarter = 1;
        warpTimer = 0;
        warp_count = 0;
        warpTimes = warpTimesOriginal;
    }

    public void GoGoShipSpawn()
    {

        dir = gameObject.AddComponent<Directive>() as Directive;
        dir.grid = early_directive.grid;
        dir.grid_height = early_directive.grid_height;
        dir.leash = early_directive.leash;
        dir.directive_type = early_directive.directive_type;

        dir1 = gameObject.AddComponent<Directive>() as Directive;
        dir1.grid = middle_directive.grid;
        dir1.grid_height = middle_directive.grid_height;
        dir1.leash = middle_directive.leash;
        dir1.directive_type = middle_directive.directive_type;

        dir2 = gameObject.AddComponent<Directive>() as Directive;
        dir2.grid = late_directive.grid;
        dir2.grid_height = late_directive.grid_height;
        dir2.leash = late_directive.leash;
        dir2.directive_type = late_directive.directive_type;

        dir3 = gameObject.AddComponent<Directive>() as Directive;
        dir3.grid = end_directive.grid;
        dir3.grid_height = end_directive.grid_height;
        dir3.leash = end_directive.leash;
        dir3.directive_type = end_directive.directive_type;

        going = true;
        int count = 0;
        ships = new GameObject[ship_number];
        var temp = new GameObject[ships.Length];
        foreach (GameObject ship in ships)
        {
            var warpZ = -100;
            if (team == 1) warpZ = 100;
            var spawn = Instantiate(ship_prefab, new Vector3(12.5f + Random.Range(-3f, 3f), Random.Range(-3f, 3f), warpZ), Quaternion.identity) as GameObject;
            var shipS = spawn.GetComponent<ShipScript>();
            shipS.squad = this.GetComponent<SquadControlScript>();
            shipS.team = team;
            if (team == 0) shipS.near_side = true;
            else shipS.near_side = false;
            shipS.map_size = (int)game.map_size;
            temp[count] = spawn;
            shipS.ship_count = count + 1;
            shipS.squad_count = squad_count;
            count++;
        }
        ships = temp;
        warpTimes = new float[temp.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            warpTimes[i] = 3f + Random.Range(0, 10);
        }
        warpTimesOriginal = warpTimes;
    }

    public void UpdateOrders()
    {
        foreach (GameObject ship in ships)
        {
            if(ship != null) ship.GetComponent<ShipScript>().DirectiveQuart();
        }
        early_grid = early_directive.grid;
        middle_grid = middle_directive.grid;
    }

    public void UpdateDirectives()
    {
        Debug.Log("Controller Says: Directives Changeing");
        switch (quarter)
        {
            case 1:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(early_directive.grid, early_directive.grid_height, early_directive.directive_type, early_directive.leash);
                }
                break;
            case 2:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(middle_directive.grid, middle_directive.grid_height, middle_directive.directive_type, middle_directive.leash);
                }
                break;
            case 3:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(late_directive.grid, late_directive.grid_height, late_directive.directive_type, late_directive.leash); ;
                }
                break;
            case 4:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(end_directive.grid, end_directive.grid_height, end_directive.directive_type, end_directive.leash);
                }
                break;
            case 5:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(end_over_directive.grid, end_over_directive.grid_height, end_over_directive.directive_type, end_over_directive.leash);
                }
                break;
            case 6:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(end_over_directive.grid, end_over_directive.grid_height, end_over_directive.directive_type, end_over_directive.leash);
                }
                break;
            case 7:
                foreach (GameObject ship in ships)
                {
                    if (ship != null) ship.GetComponent<ShipScript>().DirectiveChange(end_over_directive.grid, end_over_directive.grid_height, end_over_directive.directive_type, end_over_directive.leash);
                }
                break;
        }
    }
}