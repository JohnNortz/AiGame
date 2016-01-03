using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquadEditPanelScript : MonoBehaviour {

    public GameObject asker;
    public SquadSetupScript askerScript;
    public int ship_number = 1;
    public int ship_cost;
    public int squad_cost;
    public string ship_type;
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

    // Use this for initialization
    void Start () {
        askerScript = asker.GetComponent<SquadSetupScript>();
        this.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
	}

    public void ship_choice_click (GameObject ship)
    {
        var s = ship.GetComponent<ShipScript>();
        askerScript.Ship = ship;
        Ship = ship;    
        ship_image.sprite = s.ship_image; 
        squad_name = "SquadNamePlaceholder";
        ship_full_name_text.text = s.ship_full_name;
        hp_text.text = s.hit_points.ToString();
        armor_text.text = s.armored.ToString();
        speed_text.text = s.move_speed.ToString();
    }
    public void ship_choice_click_plus (int cost)
    {
        ship_cost = cost;
        squad_cost = ship_number * ship_cost;
        total_cost.text = squad_cost.ToString();
        single_cost.text = (squad_cost / ship_number).ToString();
        askerScript.per_ship_cost = (squad_cost / ship_number);
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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<TeamSetupScript>().UpdateCost();
        Destroy(this.gameObject);
    }
}
