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
    public float game_time;
    public GameSetupScript game;
    public int quarter = 0;
    public GameObject[] ships;
    public Vector3 starting_position;
    public Vector3[] offsets;
    public int team;
    public int squad_count;

    private float warpTimer;
    public float[] warpTimes;
    private int warp_count = 0;
    public bool going = false;

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSetupScript>();
        game.GetSquad(this.gameObject);
        end_directive = gameObject.AddComponent<Directive>() as Directive;
        middle_directive = gameObject.AddComponent<Directive>() as Directive;
        early_directive = gameObject.AddComponent<Directive>() as Directive;
        late_directive = gameObject.AddComponent<Directive>() as Directive;

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
                        ship.transform.position = starting_position + offsets[warp_count];
                        warp_count++;
                    }
                }
            }
        }
    }

    public void GoGoShipSpawn()
    {
        going = true;
        var temp = new GameObject[ships.Length];
        int count = 0;
        foreach (GameObject ship in ships)
        {
            var warpZ = -100;
            if (team == 1) warpZ = 100;
            var spawn = Instantiate(ship, new Vector3(12.5f + Random.Range(-3f, 3f), Random.Range(-3f, 3f), warpZ), Quaternion.identity) as GameObject;
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
            warpTimes[i] = 3f + Random.Range(-.5f, 3);
        }
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
        quarter++;
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