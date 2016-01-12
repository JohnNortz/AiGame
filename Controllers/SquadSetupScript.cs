using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquadSetupScript : MonoBehaviour {


    public int team;
    public int squad_count;
    public Squad saved_squad;
    public string user_name;

    public GameObject Ship;
    public int ship_number;
    public Directive early_directive;
    public Directive middle_directive;
    public Directive late_directive;
    public Directive end_directive;
    public Directive end_over_directive;
    public Vector3 starting_position;
    public Vector3[] offsets;

    public GameObject squad_command_object;
    public SquadControlScript squad_s;
    public GameObject SquadSetupPanel;

    public string[] mods;
    public int mod_cost;

    public Image ship_image;
    public GameObject mini_ship_number_image;
    public Image mini_ship_number_image_first;
    public Text ship_full_name_text;
    public Text hp_text;
    public Text armor_text;
    public Text speed_text;
    public Text squad_cost_text;
    public Text squad_name_text;

    public int per_ship_cost;
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
        if (squad_command_object == null) squad_command_object = Instantiate(squad_controller_prefab, transform.position, Quaternion.identity) as GameObject;
        squad_s = squad_command_object.GetComponent<SquadControlScript>();
        end_directive = gameObject.AddComponent<Directive>() as Directive;
        middle_directive = gameObject.AddComponent<Directive>() as Directive;
        early_directive = gameObject.AddComponent<Directive>() as Directive;
        late_directive = gameObject.AddComponent<Directive>() as Directive;
        
        if (squad_name == null)
        {
            click_setup();
        }
    }
	
	// Update is called once per frame
	public void click_directive (int quarter) {
        var Dial = Instantiate(movementDial, transform.localPosition, Quaternion.identity) as GameObject;
        if (Dial.GetComponent<MovementDialScript>() != null)
        {
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
        }
        else if (Dial.GetComponent<MovementDialObejctiveScript>() != null)
        {
            var DialScript = Dial.GetComponent<MovementDialObejctiveScript>();
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
            
        }


    }

    public void click_setup()
    {
        var SetUp = Instantiate(SquadSetupPanel, transform.localPosition, Quaternion.identity) as GameObject;
        var SetUpScript = SetUp.GetComponent<SquadEditPanelScript>();
        SetUpScript.asker = this.gameObject;

    }

    public void Update_Squad()
    {
        if (Ship != null)
        {
            var s = Ship.GetComponent<ShipScript>();
            for (int i = 2; i <= ship_number; i++)
            {
                var img = Instantiate(mini_ship_number_image, mini_ship_number_image_first.GetComponent<RectTransform>().localPosition, Quaternion.identity) as GameObject;
                img.GetComponent<RectTransform>().parent = this.transform;
                var pos = mini_ship_number_image_first.GetComponent<RectTransform>().localPosition;
                pos.x = pos.x + (15 * i);
                img.GetComponent<RectTransform>().localPosition = pos;
            } 
            ship_image.sprite = s.ship_image;
            squad_name_text.text = squad_name;
            ship_full_name_text.text = s.ship_full_name;
            hp_text.text = s.hit_points.ToString();
            armor_text.text = s.armored.ToString();
            speed_text.text = s.move_speed.ToString();
            squad_cost_text.text = squad_cost.ToString();
        }
    }

    public void create_squad()
    {
        saved_squad = new Squad(name, squad_leader_name, user_name, Ship.GetComponent<ShipScript>().type, ship_number, mod_cost, mods);
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

        squad_s.ship_number = ship_number;
        squad_s.ship_prefab = Ship;
        squad_s.starting_position = starting_position;
        squad_s.squad_count = squad_count;

        squad_s.ship_worth = per_ship_cost;

        Debug.Log("Finished Updating Controller");
    }
}
