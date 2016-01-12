using UnityEngine;
using System.Collections;


[System.Serializable]
public class Squad {

    public string Ship;
    public int ship_number;
    public int mod_costs;
    public int squad_cost;
    public string player_name;
    public string squad_name;
    public string squad_leader_name;
    public string[] mods;

    public int early_grid_x;
    public int early_grid_z;
    public string early_directive;
    public int early_grid_height;
    public int middle_grid_x;
    public int middle_grid_z;
    public string middle_directive;
    public int middle_grid_height;
    public int late_grid_x;
    public int late_grid_z;
    public string late_directive;
    public int late_gird_height;
    public int end_grid_x;
    public int end_grid_z;
    public string end_directive;
    public int end_grid_height;

    public int squad_kills;
    public int squad_deaths;
    public int squad_points;
    public int squad_leader_kills;
    public int squad_leader_deaths;
    public int games_fielded;

    public Squad (string name, string leader_name, string user_name, string ship, int _ship_number, int _mod_costs, string[] _mods) {
        squad_name = name;
        squad_leader_name = leader_name;
        player_name = user_name;
        Ship = ship;
        ship_number = _ship_number;
        mod_costs = _mod_costs;
        mods = _mods;
    }

    public void setup_directives(Vector3 _early_grid, string _early_directive, 
        int _early_grid_height, Vector3 _middle_grid, string _middle_directive,
        int _middle_grid_height, Vector3 _late_grid,  string _late_directive, int _late_gird_height,
        Vector3 _end_grid, string _end_directive, int _end_grid_height)
    {
        early_grid_x = (int)_early_grid.x;
        early_grid_z = (int)_early_grid.z;
        early_directive = _early_directive;
        early_grid_height = _early_grid_height;
        middle_grid_x = (int)_middle_grid.x;
        middle_grid_z = (int) _middle_grid.z;
        middle_directive = _middle_directive;
        middle_grid_height = _middle_grid_height;
        late_grid_x = (int)_late_grid.x;
        late_grid_z = (int)_late_grid.z;
        late_directive = _late_directive;
        late_gird_height = _late_gird_height;
        end_grid_x = (int) _end_grid.x;
        end_grid_z = (int) _end_grid.z;
        end_directive = _end_directive;
        end_grid_height = _end_grid_height;
    }
	
}
