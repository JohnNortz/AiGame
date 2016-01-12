using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementDialObejctiveScript : MonoBehaviour {

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
    public Vector3 start_pos;
    public GameObject[] tiles;
    public GameObject[] start_poss;

    public Directive end_1;
    public Directive end_2;
    public Directive end_3;
    public Directive end_4;

    public Text set_text;

    void Start()
    {
        DIR = gameObject.AddComponent<Directive>() as Directive;
    }

    public void DrawBoard()
    {
        var _x = 0f;
        var _z = 0f;
        var h_1 = 0;
        var h_2 = 0;
        var h_3 = 0;
        var h_4 = 0;
        foreach (GameObject ztile in tiles)
        {
            ztile.GetComponent<Image>().color = off_color;
            ztile.GetComponentInChildren<RawImage>().color = off_color;
        }
        for (int i = 4; i > 0; i--)
        {
            switch (i)
            {
                case 1:
                    _x = asker.GetComponent<SquadSetupScript>().early_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().early_directive.grid.z;
                    h_1 = asker.GetComponent<SquadSetupScript>().early_directive.grid_height;
                    break;
                case 2:
                    _x = asker.GetComponent<SquadSetupScript>().middle_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().middle_directive.grid.z;
                    h_2 = asker.GetComponent<SquadSetupScript>().middle_directive.grid_height;
                    break;
                case 3:
                    _x = asker.GetComponent<SquadSetupScript>().late_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().late_directive.grid.z;
                    h_3 = asker.GetComponent<SquadSetupScript>().late_directive.grid_height;
                    break;
                case 4:
                    _x = asker.GetComponent<SquadSetupScript>().end_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().end_directive.grid.z;
                    h_4 = asker.GetComponent<SquadSetupScript>().end_directive.grid_height;
                    break;
            }
            foreach (GameObject tile in tiles)
            {
                if (tile.name == (_z.ToString() + "-" + _x.ToString()))
                {
                    if (i == 1)
                    {
                        tile.GetComponent<Image>().color = quater_1_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_1_color;
                    }
                    if (i == 2)
                    {
                        tile.GetComponent<Image>().color = quater_2_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_2_color;
                    }
                    if (i == 3)
                    {
                        tile.GetComponent<Image>().color = quater_3_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_3_color;
                    }
                    if (i == 4)
                    {
                        tile.GetComponent<Image>().color = quater_4_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_4_color;
                    }
                }
            }
        }
        x = _x;
        z = _z;
        var transfer = asker.GetComponent<SquadSetupScript>();
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
    }


    public void UpdateDirective(string tile)
    {

        x = int.Parse("" + tile[2]);
        z = int.Parse("" + tile[0]);

        DIR.grid = new Vector3(x, 0, z);
        SetController();
        DrawBoard();

    }

    public void UpdateStartPosition(int pos)
    {
        var transfer = asker.GetComponent<SquadSetupScript>();
        foreach (GameObject start_img in start_poss) { start_img.GetComponent<Image>().color = off_color; }
        switch (pos)
        {
            case 2:
                start_pos = new Vector3(1.5f, 0, .5f);
                transfer.starting_position = new Vector3(1.5f, 0, .5f);
                start_poss[1].GetComponent<Image>().color = quater_1_color;
                start_poss[1].GetComponentInChildren<RawImage>().color = quater_1_color;
                start_poss[0].GetComponent<Image>().color = off_color;
                start_poss[0].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[3].GetComponent<Image>().color = off_color;
                start_poss[3].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[2].GetComponent<Image>().color = off_color;
                start_poss[2].GetComponentInChildren<RawImage>().color = off_color;
                quarter = 1;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-2");

                quarter = 2;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("3-1");

                quarter = 3;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("4-2");

                quarter = 4;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("4-3");
                transfer.end_over_directive = end_1;
                break;
            case 3:
                start_pos = new Vector3(2.5f, 0, .5f);
                transfer.starting_position = new Vector3(2.5f, 0, .5f);
                start_poss[2].GetComponent<Image>().color = quater_1_color;
                start_poss[2].GetComponentInChildren<RawImage>().color = quater_1_color;
                start_poss[0].GetComponent<Image>().color = off_color;
                start_poss[0].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[1].GetComponent<Image>().color = off_color;
                start_poss[1].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[3].GetComponent<Image>().color = off_color;
                start_poss[3].GetComponentInChildren<RawImage>().color = off_color;

                quarter = 1;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-2");

                quarter = 2;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-4");

                quarter = 3;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-5");

                quarter = 4;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("3-5");
                transfer.end_over_directive = end_2;
                break;
            case 6:
                start_pos = new Vector3(5.5f, 0, .5f);
                transfer.starting_position = new Vector3(5.5f, 0, .5f);
                start_poss[3].GetComponent<Image>().color = quater_1_color;
                start_poss[3].GetComponentInChildren<RawImage>().color = quater_1_color;
                start_poss[0].GetComponent<Image>().color = off_color;
                start_poss[0].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[1].GetComponent<Image>().color = off_color;
                start_poss[1].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[2].GetComponent<Image>().color = off_color;
                start_poss[2].GetComponentInChildren<RawImage>().color = off_color;
                quarter = 1;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-7");

                quarter = 2;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-5");

                quarter = 3;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-4");

                quarter = 4;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("3-4");
                transfer.end_over_directive = end_3;
                break;
            case 7:
                start_pos = new Vector3(6.5f, 0, .5f);
                transfer.starting_position = new Vector3(6.5f, 0, .5f);
                start_poss[0].GetComponent<Image>().color = quater_1_color;
                start_poss[0].GetComponentInChildren<RawImage>().color = quater_1_color;
                start_poss[3].GetComponent<Image>().color = off_color;
                start_poss[3].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[1].GetComponent<Image>().color = off_color;
                start_poss[1].GetComponentInChildren<RawImage>().color = off_color;
                start_poss[2].GetComponent<Image>().color = off_color;
                start_poss[2].GetComponentInChildren<RawImage>().color = off_color;
                quarter = 1;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("2-7");

                quarter = 2;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("3-8");

                quarter = 3;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("4-7");

                quarter = 4;
                DIR.grid_height = 0;
                DIR.leash = 0;
                DIR.directive_type = "defensive";
                UpdateDirective("4-6");

                transfer.end_over_directive = end_4;
                break;
        }

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
                    quarter = 0;
                    transfer.UpdateController();
                    Destroy(this.gameObject);
                }
                break;

        }
    }
}
