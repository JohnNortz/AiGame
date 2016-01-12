using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquadEditPanelScript : MonoBehaviour {

    public GameObject asker;
    public SquadSetupScript askerScript;
    public int ship_number = 1;
    public int ship_cost;
    public int squad_mods_cost;
    public int squad_cost;
    public string ship_type;
    public string[] mods_list;
    public GameObject mods;
    public GameObject ships_available;
    public GameObject Ship;
    public Text mods_cost;
    public Text total_cost;
    public Text single_cost;
    public Image ship_image;
    public Text ship_full_name_text;
    public Text hp_text;
    public Text armor_text;
    public Text speed_text;
    public string squad_name;
    public string squad_leader_name;
    public string user_name;
    public GameObject loaded_squad_button;
    public InputField squad_name_input;
    public InputField squad_leader_name_input;
    public Squad squad;

    public GameObject fighter_obj;
    public int f_cost;
    public GameObject support_obj;
    public int s_cost;
    public GameObject bomber_obj;
    public int b_cost; 

    // Use this for initialization
    void Start () {
        askerScript = asker.GetComponent<SquadSetupScript>();
        this.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        if (askerScript.ship_number > 0)
        {
            Ship = askerScript.Ship;
            ship_number = askerScript.ship_number;
            squad_name = askerScript.squad_name;
            ship_choice_click(Ship);
        }
	}

    public void ship_choice_click (GameObject ship)
    {
        var s = ship.GetComponent<ShipScript>();
        askerScript.Ship = ship;
        Ship = ship;    
        ship_image.sprite = s.ship_image;
        ship_type = s.type;
        ship_full_name_text.text = s.ship_full_name;
        hp_text.text = s.hit_points.ToString();
        armor_text.text = s.armored.ToString();
        speed_text.text = s.move_speed.ToString();
    }
    public void ship_choice_click_plus (string _ship_type)
    {
        if (_ship_type == "fighter") ship_cost = f_cost;
        if (_ship_type == "support") ship_cost = s_cost;
        if (_ship_type == "bomber") ship_cost = b_cost;
        ship_type = _ship_type;
        squad_cost = ship_number * ship_cost;
        total_cost.text = squad_cost.ToString();
        single_cost.text = (squad_cost / ship_number).ToString();
        askerScript.per_ship_cost = (squad_cost / ship_number);
    }

    public void enter_squad_name (string name)
    {
        squad_name = name;
        askerScript.squad_name = squad_name;
        squad_name_input.text = name;
    }

    public void enter_squad_leader_name(string name)
    {
        squad_leader_name = name;
        askerScript.squad_leader_name = squad_name;
        squad_leader_name_input.text = name;
    }

    public void plus_ship()
    {
        ship_number++;
        squad_cost = ship_number * ship_cost;
        total_cost.text = squad_cost.ToString();
    }

    public void minus_ship()
    {
        ship_number--;
        if (ship_number < 1) ship_number = 1;
        squad_cost = ship_number * ship_cost;
        total_cost.text = squad_cost.ToString();
    }
    // Update is called once per frame
    public void UpdateController () {
        askerScript.ship_number = ship_number;
        squad_cost = ship_number * ship_cost;
        askerScript.squad_cost = squad_cost;
        askerScript.Ship = Ship;
        askerScript.Update_Squad();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TeamSetupScript>().UpdateCost();
        Destroy(this.gameObject);
    }

    public void SaveSquad()
    {

        var squaa = new Squad(squad_name, squad_leader_name, user_name, ship_type, ship_number, (int)squad_mods_cost, mods_list) as Squad;
        squad = squaa;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SquadSaveLoad>().SaveSquad(squad);
    }

    public void LoadSquadList()
    {
        var pos = new Vector3(this.GetComponent<RectTransform>().localPosition.x - 400, this.GetComponent<RectTransform>().localPosition.y - 300, this.GetComponent<RectTransform>().localPosition.z);
        var count = 0;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TeamSetupScript>().Reload();
        var squads = GameObject.FindGameObjectWithTag("GameController").GetComponent<TeamSetupScript>().saved_squads;
        foreach (Squad squad in squads)
        {             
            var position = pos + new Vector3(0, 25 * count, 0);
            var button = Instantiate(loaded_squad_button, position, Quaternion.identity) as GameObject;
            button.transform.parent = this.transform;
            button.GetComponent<RectTransform>().localPosition = position;
            button.GetComponent<LoadedSquadScript>().squad = squad;
            button.GetComponent<LoadedSquadScript>().asker = this;
            count++;
        }
    }

    public void AssignSquad(Squad _squad)
    {
        ship_number = _squad.ship_number;
        ship_type = _squad.Ship;
        mods_list = _squad.mods;
        switch (ship_type)
        {
            case "fighter":
                ship_choice_click(fighter_obj);
                break;
            case "bomber":
                ship_choice_click(bomber_obj);
                break;
            case "support":
                ship_choice_click(support_obj);
                break;
        }
        ship_choice_click_plus(ship_type);
        plus_ship();
        minus_ship();
        enter_squad_name(_squad.squad_name);
        enter_squad_leader_name(_squad.squad_leader_name);
    }
}
