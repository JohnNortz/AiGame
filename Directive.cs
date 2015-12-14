using UnityEngine;
using System.Collections;

public class Directive : MonoBehaviour {

    public float leash;
    public string directive_type;
    public Vector3 grid;
    public int grid_height;
    
    public Directive()
    {
        leash = 1;
        directive_type = "defensive";
        grid = new Vector3(1, 0, 1);
        grid_height = 0;
    }
}
