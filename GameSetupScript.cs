using UnityEngine;
using System.Collections;

public class GameSetupScript : MonoBehaviour
{

    public float map_size;
    public float player_number;
    public GameObject[] players;
    public LineRenderer grid_lines;

    public bool pause = true;
    public float pause_speed = .01f;
    public float game_timer = 0;
    public float quarter_timer = 0;
    public float quarter_quarter_timer = 0;
    private bool start = false;
    public int squad_count = 2;

    public GameObject[] squads;
    public GameObject[] things_to_disable_on_start;

    // Use this for initialization
    void Start()
    {
        var count = 0;
        for (int i = 0; i <= map_size + 1; i++)
        {
            grid_lines.SetPosition((i * 3), new Vector3(i * 3, -1.5f, 0));
            grid_lines.SetPosition(1 + (i * 3), new Vector3(i * 3, -1.5f, map_size * 3));
            grid_lines.SetPosition(2 + (i * 3), new Vector3(i * 3, -1.5f, 0));
            count = i * 3;
        }

        grid_lines.SetPosition(count, new Vector3(0, -1.5f, 0));
        count++;

        for (int i = 0; i <= map_size; i++)
        {
            grid_lines.SetPosition(count + i * 3, new Vector3(0, -1.5f, i * 3));
            grid_lines.SetPosition(count + 1 + i * 3, new Vector3(map_size * 3, -1.5f, i * 3));
            grid_lines.SetPosition(count + 2 + i * 3, new Vector3(0, -1.5f, i * 3));
        }

        foreach (GameObject squad in squads)
        {
            squad.GetComponent<SquadControlScript>().UpdateDirectives();
        }
    }

    void ResetTimers()
    {
        game_timer = 0;
        quarter_quarter_timer = 0;
        quarter_timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pause) Time.timeScale = pause_speed;
        else Time.timeScale = 1;

        game_timer += Time.deltaTime;
        quarter_timer += Time.deltaTime;
        quarter_quarter_timer += Time.deltaTime;

        if (quarter_timer > 80)
        {
            foreach (GameObject squad in squads) { if (squad != null) squad.GetComponent<SquadControlScript>().UpdateDirectives(); }
            quarter_timer = 0;
            quarter_quarter_timer = 0;
        }
        if (quarter_quarter_timer > 20)
        {
            foreach (GameObject squad in squads) {
                if (squad != null) squad.GetComponent<SquadControlScript>().UpdateOrders();
            }
            quarter_quarter_timer = 0;
        }
    }

    public void GameStart()
    {
        foreach (GameObject squad in squads)
        {
            if (squad != null)
            {
                squad.GetComponent<SquadControlScript>().GoGoShipSpawn();
            }
        }
        ResetTimers();
        foreach (GameObject thing in things_to_disable_on_start)
        {
            thing.gameObject.SetActive(false);
        }
        foreach (GameObject squad in squads)
        {
            if (squad != null)
            {
                squad.GetComponent<SquadControlScript>().UpdateOrders();
            }
        }
    }

    public void GetSquad(GameObject new_squad)
    {
        squads[squad_count] = new_squad;
        squad_count++;
    }
}
