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
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Ship.transform.position;
        var x = Vector3.Distance(Ship.transform.position, Cam.transform.position) * .02f;
        thisRect.transform.localScale = new Vector3(x, x, 1);
        if (Ship.tag == "Dead") img.color = new Color(1,1,1,.2f);
    }

    public void SetCameraTarget()
    {
        Cam.GetComponent<CameraScript>().Target = Ship;
    }
}
