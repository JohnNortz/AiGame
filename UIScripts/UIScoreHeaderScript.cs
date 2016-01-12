using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScoreHeaderScript : MonoBehaviour {

    public Text team_0_score;
    public Text team_1_score;
    public Text team_0_ships;
    public Text team_1_ships;
    public int team_0_points;
    public int team_1_points;
    public int team_0_numbers;
    public int team_1_numbers;
    public GameObject[] squads;


    // Update is called once per frame
    public void UpdateScore() {
        squads = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSetupScript>().squads;
        team_0_numbers = 0;
        team_1_numbers = 0;
        team_0_points = 0;
        team_1_points = 0;
        foreach (GameObject squad in squads)
        {
            if (squad != null)
            {
                var s = squad.GetComponent<SquadControlScript>();
                var team = s.team;
                foreach (GameObject ship in s.ships)
                {
                    if (ship.tag == "Dead")
                    {
                        if (team == 1)
                        {
                            team_0_points += s.ship_worth;
                        }
                        if (team == 0)
                        {
                            team_1_points += s.ship_worth;
                        }
                    }
                    if (ship.tag == "Ship")
                    {
                        if (team == 1)
                        {
                            team_1_numbers += 1;
                        }
                        if (team == 0)
                        {
                            team_0_numbers += 1;
                        }
                    }
                }
            }
        }

        team_0_ships.text = team_0_numbers.ToString();
        team_1_ships.text = team_1_numbers.ToString();
        team_0_score.text = team_0_points.ToString();
        team_1_score.text = team_1_points.ToString();
    }
}
