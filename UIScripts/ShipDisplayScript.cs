using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipDisplayScript : MonoBehaviour {

    public GameObject Ship;
    public ShipScript ShipS;
    public RawImage hp_bar;
    public RectTransform hp_rect;
    public Text label;
    public int team;
    public Camera Cam;
    public string label_text;
    public float hp_full;
    public float full_hp_width;
    public int squad_count;
    public int ship_count;
    public RawImage image;
    public Color team_0_color;
    public Color team_1_color;
    public Color dead_color;
    public Texture indicator_figther;
    public Texture indicator_bomber;
    public Texture indicator_objective;
    public Texture indicator_support;


    // Use this for initialization
    void Start ()
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        var reverse = 1; if (team == 1)
        {
            reverse = -1;
        }
        this.GetComponent<RectTransform>().localPosition = new Vector3((-800 + 50 * ship_count) * reverse, 400 - 80 * squad_count, 0);
        hp_rect = hp_bar.GetComponent<RectTransform>();


        Cam = Camera.main;
        full_hp_width = hp_rect.sizeDelta.x;
        ShipS = Ship.GetComponent<ShipScript>();
        switch (ShipS.type)
        {
            case "fighter":
                image.texture = indicator_figther;

                break;
            case "bomber":
                image.texture = indicator_bomber;

                break;
            case "support":
                image.texture = indicator_support;

                break;
            case "capital":
                image.texture = indicator_objective;

                break;
        }
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
	void Update ()
    {
        if (Ship == null) Destroy(this.gameObject);
        if (Ship.tag == "Dead")
        {
            this.GetComponent<Image>().color = dead_color;
            image.color = dead_color;
        }
        hp_rect.sizeDelta = new Vector2(full_hp_width * (ShipS.hit_points / hp_full), 6);
	}

    public void SetCameraTarget()
    {
        Cam.GetComponent<CameraScript>().Target = Ship;
    }
}
