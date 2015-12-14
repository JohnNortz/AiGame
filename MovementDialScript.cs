using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementDialScript : MonoBehaviour {

    public Canvas CanvasObject;
    public GameObject Board;
    public Directive DIR;
    public GameObject asker;
    public int quarter;
    public float x;
    public float z;
    public Color quater_1_color;
    public Color quater_2_color;
    public Color quater_3_color;
    public Color quater_4_color;
    public Color off_color;

    public Text set_text;

    void Start()
    {
        DIR = gameObject.AddComponent<Directive>() as Directive;
    }

    public void DrawBoard()
    {
        var _x = 0f;
        var _z = 0f;
        Debug.Log("got to DrawBoard");
        for (int i = quarter - 1; i < 0; i--)
        {
            switch (quarter)
            {
                case 1:
                    _x = asker.GetComponent<SquadSetupScript>().early_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().early_directive.grid.z;
                    break;
                case 2:
                    _x = asker.GetComponent<SquadSetupScript>().middle_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().middle_directive.grid.z;
                    break;
                case 3:
                    _x = asker.GetComponent<SquadSetupScript>().late_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().late_directive.grid.z;
                    break;
                case 4:
                    _x = asker.GetComponent<SquadSetupScript>().end_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().end_directive.grid.z;
                    break;
            }
            foreach (Transform tile in Board.transform)
            {
                if (tile.name == (_x.ToString() + "-" + _z.ToString()))
                {
                    if (i == 1) { tile.GetComponent<Image>().color = quater_1_color; }
                    if (i == 2) { tile.GetComponent<Image>().color = quater_2_color; }
                    if (i == 3) { tile.GetComponent<Image>().color = quater_3_color; }
                    if (i == 4) { tile.GetComponent<Image>().color = quater_4_color; }
                }
                else { tile.GetComponent<Image>().color = off_color; }
            }
        }
        x = _x;
        z = _z;
        SetText();
        Debug.Log("got to DrawBoard END");
    }
    public void Open1(GameObject _asker)
    {
        Debug.Log("got to OPEN1");
        asker = _asker;
    }

    public void Open(int _quarter)
    {
        Debug.Log("got to OPEN");
        quarter = _quarter;
        SetDirective(quarter);
        DrawBoard();
        Debug.Log("got to OPEN END");
    }
    
    public void UpdateDirective(string tile) {

        var x = int.Parse("" + tile[0]);
        var z = int.Parse("" + tile[2]);

        DIR.grid = new Vector3(x, 0, z);
        SetText();

    }

    public void UpdateDirectiveHeight(int height)
    {
        DIR.grid_height = height;
        SetText();

    }

    public void UpdateDirectiveLeash(int ammount)
    {
        DIR.leash = ammount;
        SetText();

    }

    public void UpdateDirectiveType(int type)
    {
        switch (type)
        {
            case 1:
                DIR.directive_type = "formation";
                break;
            case 2:
                DIR.directive_type = "defensive";
                break;
            case 3:
                DIR.directive_type = "agressive";
                break;
            case 4:
                DIR.directive_type = "search_and_destroy";
                break; 
        }
        SetText();

    }

    public void SetController()
    {
        var transfer = asker.GetComponent<SquadSetupScript>();
        switch (quarter)
        {
            case 1:
                transfer.early_directive.grid = DIR.grid;
                transfer.early_directive.grid_height = DIR.grid_height;
                transfer.early_directive.directive_type = DIR.directive_type;
                transfer.early_directive.leash = DIR.leash;
                break;
            case 2:
                transfer.middle_directive.grid = DIR.grid;
                transfer.middle_directive.grid_height = DIR.grid_height;
                transfer.middle_directive.directive_type = DIR.directive_type;
                transfer.middle_directive.leash = DIR.leash;
                break;
            case 3:
                transfer.late_directive.grid = DIR.grid;
                transfer.late_directive.grid_height = DIR.grid_height;
                transfer.late_directive.directive_type = DIR.directive_type;
                transfer.late_directive.leash = DIR.leash;
                break;
            case 4:
                transfer.end_directive.grid = DIR.grid;
                transfer.end_directive.grid_height = DIR.grid_height;
                transfer.end_directive.directive_type = DIR.directive_type;
                transfer.end_directive.leash = DIR.leash;
                break;

        }
    }

    public void SetDirective(int time)
    {
        var transfer = asker.GetComponent<SquadSetupScript>();
        switch (time)
        {
            case 1:
                DIR = transfer.early_directive;
                DIR.grid = transfer.early_directive.grid;
                DIR.grid_height = transfer.early_directive.grid_height;
                DIR.directive_type = transfer.early_directive.directive_type;
                DIR.leash = transfer.early_directive.leash;
                break;
            case 2:
                DIR = transfer.middle_directive;
                DIR.grid = transfer.middle_directive.grid;
                DIR.grid_height = transfer.middle_directive.grid_height;
                DIR.directive_type = transfer.middle_directive.directive_type;
                DIR.leash = transfer.middle_directive.leash;
                break;
            case 3:
                DIR = transfer.late_directive;
                DIR.grid = transfer.late_directive.grid;
                DIR.grid_height = transfer.late_directive.grid_height;
                DIR.directive_type = transfer.late_directive.directive_type;
                DIR.leash = transfer.late_directive.leash;
                break;
            case 4:
                DIR = transfer.end_directive;
                DIR.grid = transfer.end_directive.grid;
                DIR.grid_height = transfer.end_directive.grid_height;
                DIR.directive_type = transfer.end_directive.directive_type;
                DIR.leash = transfer.end_directive.leash;
                break;

        }
        SetText();
        DrawBoard();
    }

    public void CloseCancel()
    {
        Destroy(this.gameObject);
    }
    
    public void OKButton()
    {
        SetController();
        var transfer = asker.GetComponent<SquadSetupScript>();
        Debug.Log("quarter 1: " + transfer.early_directive.grid);
        Debug.Log("quarter 2: " + transfer.middle_directive.grid);
        Debug.Log("quarter 3: " + transfer.late_directive.grid);
        Debug.Log("quarter 4: " + transfer.end_directive.grid);
        switch (quarter)
        {
            case 1:
                if (transfer.early_directive.grid == DIR.grid &&
                    transfer.early_directive.grid_height == DIR.grid_height &&
                    transfer.early_directive.directive_type == DIR.directive_type &&
                    transfer.early_directive.leash == DIR.leash)
                {
                    asker = null;
                    quarter = 0;
                    transfer.UpdateController();
                    Destroy(this.gameObject);
                }
                break;
            case 2:
                if (transfer.middle_directive.grid == DIR.grid &&
                   transfer.middle_directive.grid_height == DIR.grid_height &&
                   transfer.middle_directive.directive_type == DIR.directive_type &&
                   transfer.middle_directive.leash == DIR.leash)
                {
                    asker = null;
                    quarter = 0;
                    transfer.UpdateController();
                    Destroy(this.gameObject);
                }
                break;
            case 3:
                if (transfer.late_directive.grid == DIR.grid &&
                    transfer.late_directive.grid_height == DIR.grid_height &&
                    transfer.late_directive.directive_type == DIR.directive_type &&
                    transfer.late_directive.leash == DIR.leash)
                {
                    asker = null;
                    quarter = 0;
                    transfer.UpdateController();
                    Destroy(this.gameObject);
                }
                break;
            case 4:
                if (transfer.end_directive.grid == DIR.grid && 
                    transfer.end_directive.grid_height == DIR.grid_height &&
                    transfer.end_directive.directive_type == DIR.directive_type &&
                    transfer.end_directive.leash == DIR.leash)
                {
                    asker = null;
                    quarter = 0;
                    transfer.UpdateController();
                    Destroy(this.gameObject);
                }
                break;

        }
    }

    public void SetText()
    {
        set_text.text = "Order Number: " + quarter + " | x: " + DIR.grid.x + " | z: " + DIR.grid.z + " | Height: " + DIR.grid_height;
    }
}
