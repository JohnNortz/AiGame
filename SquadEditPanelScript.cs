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

	// Use this for initialization
	void Start () {
        askerScript = asker.GetComponent<SquadSetupScript>();
        this.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
	}

    public void ship_choice_click (GameObject ship)
    {
        askerScript.Ship = ship;
        Ship = ship;
    }
    public void ship_choice_click_plus (int cost)
    {
        ship_cost = cost;
        squad_cost = ship_number * ship_cost;
        total_cost.text = squad_cost.ToString();
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

        Destroy(this.gameObject);
    }
}
