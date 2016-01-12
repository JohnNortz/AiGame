using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquadButtonDisplayScript : MonoBehaviour {


    public GameObject Ship;
    public ShipScript ShipS;
    public Text label;
    public int team;
    public Camera Cam;
    public int squad_count;
    public int ship_count;
    public Color team_0_color;
    public Color team_1_color;
    public Color dead_color;
    public RawImage image;

    // Use this for initialization
    void Start () {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        var reverse = 1; if (team == 1)
        {
            reverse = -1;
        }
        this.GetComponent<RectTransform>().localPosition = new Vector3((-800 + 50 * squad_count) * reverse, 500, 0);
        
        Cam = Camera.main;

        ShipS = Ship.GetComponent<ShipScript>();
        if (team == 1)
        {
            this.GetComponent<Image>().color = team_1_color;
            this.GetComponentInChildren<Text>().color = team_1_color;
            image.color = team_1_color;
        }
        else
        {
            this.GetComponent<Image>().color = team_0_color;
            image.color = team_0_color;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
