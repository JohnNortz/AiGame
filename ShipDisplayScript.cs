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

	// Use this for initialization
	void Start () {
        this.GetComponent<RectTransform>().localPosition = new Vector3(-670 + 50 * ship_count, 400 - 80 * squad_count, 0);
        Cam = Camera.main;
        hp_rect = hp_bar.GetComponent<RectTransform>();
        full_hp_width = hp_rect.sizeDelta.x;
        ShipS = Ship.GetComponent<ShipScript>();
        this.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
    }
	
	// Update is called once per frame
	void Update () {
        hp_rect.sizeDelta = new Vector2(full_hp_width * (ShipS.hit_points / hp_full), 6);
	}

    public void SetCameraTarget()
    {
        Cam.GetComponent<CameraScript>().Target = Ship;
    }
}
