using UnityEngine;
using System.Collections;

public class SquadSetupScript : MonoBehaviour {


    public int team;
    public int squad_count;

    public GameObject[] Ships;
    public Directive early_directive;
    public Vector3 early_grid;
    public Directive middle_directive;
    public Vector3 middle_grid;
    public Directive late_directive;
    public Directive end_directive;
    public Directive end_over_directive;
    public Vector3 starting_position;
    public Vector3[] offsets;

    public GameObject squad_command_object;
    public SquadControlScript squad_s;

    public int pre_ship_cost;
    public int squad_cost;
    public int squad_games;
    public int squad_wins;
    public int squad_ship_deaths;
    public int squad_kills;
    public int squad_leader_kills;
    public int squad_leader_deaths;
    public string squad_leader_name;
    public string squad_name;
    public Sprite squad_symbol;

    public GameObject squad_controller_prefab;
    public GameObject movementDial;


    // Use this for initialization
    void Start () {
        squad_command_object = Instantiate(squad_controller_prefab, transform.position, Quaternion.identity) as GameObject;
        squad_s = squad_command_object.GetComponent<SquadControlScript>();
        Ships = new GameObject[6];
        end_directive = gameObject.AddComponent<Directive>() as Directive;
        middle_directive = gameObject.AddComponent<Directive>() as Directive;
        early_directive = gameObject.AddComponent<Directive>() as Directive;
        late_directive = gameObject.AddComponent<Directive>() as Directive;
    }
	
	// Update is called once per frame
	public void click_directive (int quarter) {
        var Dial = Instantiate(movementDial, transform.localPosition, Quaternion.identity) as GameObject;
        var DialScript = Dial.GetComponent<MovementDialScript>();
        DialScript.asker = this.gameObject;
        DialScript.quarter = quarter;
        switch (quarter)
        {
            case 1:
                DialScript.DIR = early_directive;
                break;
            case 2:
                DialScript.DIR = middle_directive;
                break;
            case 3:
                DialScript.DIR = late_directive;
                break;
            case 4:
                DialScript.DIR = end_directive;
                break;
        }
        DialScript.DrawBoard();
        Dial.GetComponent<RectTransform>().SetParent(this.transform.parent);
        Dial.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        Dial.GetComponent<RectTransform>().localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - 150, this.transform.localPosition.z);

        early_grid = early_directive.grid;
        middle_grid = middle_directive.grid;

}

    public void UpdateController()
    {

        squad_s.early_directive.grid = early_directive.grid;
        squad_s.early_directive.grid_height = early_directive.grid_height;
        squad_s.early_directive.directive_type = early_directive.directive_type;
        squad_s.early_directive.leash = early_directive.leash;


        squad_s.middle_directive.grid = middle_directive.grid;
        squad_s.middle_directive.grid_height = middle_directive.grid_height;
        squad_s.middle_directive.directive_type = middle_directive.directive_type;
        squad_s.middle_directive.leash = middle_directive.leash;

        squad_s.late_directive.grid = late_directive.grid;
        squad_s.late_directive.grid_height = late_directive.grid_height;
        squad_s.late_directive.directive_type = late_directive.directive_type;
        squad_s.late_directive.leash = late_directive.leash;

        squad_s.end_directive.grid = end_directive.grid;
        squad_s.end_directive.grid_height = end_directive.grid_height;
        squad_s.end_directive.directive_type = end_directive.directive_type;
        squad_s.end_directive.leash = end_directive.leash;


        
        Debug.Log("Finished Updating Controller");
    }
}
