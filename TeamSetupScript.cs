using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeamSetupScript : MonoBehaviour {

    public Text team_cost_text;
    public int team_cost;
    public GameObject[] squads;


    // Update is called once per frame
    public void UpdateCost()
    {
        Debug.Log("----COST-----");
        team_cost = 0;
        foreach (GameObject squad in squads)
        {
            if (squad != null) team_cost += squad.GetComponent<SquadSetupScript>().squad_cost;
        }
        team_cost_text.text = team_cost.ToString();
    }
}
