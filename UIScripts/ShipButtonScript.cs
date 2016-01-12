using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipButtonScript : MonoBehaviour {

    public GameObject Ship;
    public string label = "";
    public Text labelT;
    public int team;
    public Camera Cam;
    public RectTransform CanvasRect;
    public RectTransform thisRect;
    public Image img;
    public Sprite team0;
    public Sprite team1;
    public Color team0_font_color;
    public Color team1_font_color;
    private Color set_color;
    // Use this for initialization
    void Start()
    {
        Cam = Camera.main;
        CanvasRect = Cam.GetComponent<CameraScript>().canvas.GetComponent<RectTransform>();
        this.GetComponent<RectTransform>().SetParent(CanvasRect);
        img = this.GetComponent<Image>();
        thisRect = this.GetComponent<RectTransform>();
        thisRect.localScale = new Vector3(1, 1, 1);
        labelT.text = label;
        if (team == 0)
        {
            img.sprite = team0;
            labelT.color = team0_font_color;
        }
        if (team == 1)
        {
            img.sprite = team1;
            labelT.color = team1_font_color;
        }
        set_color = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        img.color = set_color;
        if (Ship == null) Destroy(this.gameObject);
        var point = Cam.WorldToScreenPoint(Ship.transform.position);
        var _point = point;
        _point.x = (-Screen.width * .5f) + point.x;
        _point.y = (-Screen.height * .5f) + point.y;
        thisRect.localPosition = _point;
        if (Vector3.Distance(Ship.transform.position, Cam.transform.position) < 0) img.color = new Color(1, 1, 1, 0f);
        var x = Vector3.Distance(Ship.transform.position, Cam.transform.position);
        var z = .75f + (1 * (2 / x));
        thisRect.transform.localScale = new Vector3(z, z, 1);
        if (Ship.tag == "Dead" || x < 1f) img.color = new Color(1, 1, 1, .04f);
    
    }

    public void SetCameraTarget()
    {
        Cam.GetComponent<CameraScript>().Target = Ship;
    }
}
