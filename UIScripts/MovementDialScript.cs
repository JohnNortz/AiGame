﻿using UnityEngine;
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
    public Vector3 start_pos;
    public GameObject[] tiles;
    public GameObject[] start_poss;
    public Texture up_sprite;
    public Texture down_sprite;
    public Texture level_sprite;
    public Button[] height_buttons;
    public Button[] behavior_buttons;
    public Color height_panel_lit;
    public Color height_panel_off;

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
            ztile.GetComponentInChildren<RawImage>().texture = level_sprite;
        }
        for (int i = 4; i > 0; i--)
        {
            switch (i)
            {
                case 1:
                    _x = asker.GetComponent<SquadSetupScript>().early_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().early_directive.grid.z;
                    h_1 = asker.GetComponent<SquadSetupScript>().early_directive.grid_height;
                    if(1 == quarter) DrawLeash(_z + "-" + _x);
                    break;
                case 2:
                    _x = asker.GetComponent<SquadSetupScript>().middle_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().middle_directive.grid.z;
                    h_2 = asker.GetComponent<SquadSetupScript>().middle_directive.grid_height;
                    if (2 == quarter) DrawLeash(_z + "-" + _x);
                    break;
                case 3:
                    _x = asker.GetComponent<SquadSetupScript>().late_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().late_directive.grid.z;
                    h_3 = asker.GetComponent<SquadSetupScript>().late_directive.grid_height;
                    if (3 == quarter) DrawLeash(_z + "-" + _x);
                    break;
                case 4:
                    _x = asker.GetComponent<SquadSetupScript>().end_directive.grid.x;
                    _z = asker.GetComponent<SquadSetupScript>().end_directive.grid.z;
                    h_4 = asker.GetComponent<SquadSetupScript>().end_directive.grid_height;
                    if (4 == quarter) DrawLeash(_z + "-" + _x);
                    break;
            }
            foreach (GameObject tile in tiles)
            {
                if (tile.name == (_z.ToString() + "-" + _x.ToString()))
                {
                    if (i == 1) {
                        tile.GetComponent<Image>().color = quater_1_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_1_color;
                        if (h_1 == 1) tile.GetComponentInChildren<RawImage>().texture = up_sprite;
                        if (h_1 == -1) tile.GetComponentInChildren<RawImage>().texture = down_sprite;
                    }
                    if (i == 2) { tile.GetComponent<Image>().color = quater_2_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_2_color;
                        if (h_2 == 1) tile.GetComponentInChildren<RawImage>().texture = up_sprite;
                        if (h_2 == -1) tile.GetComponentInChildren<RawImage>().texture = down_sprite;
                    }
                    if (i == 3) { tile.GetComponent<Image>().color = quater_3_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_3_color;
                        if (h_3 == 1) tile.GetComponentInChildren<RawImage>().texture = up_sprite;
                        if (h_3 == -1) tile.GetComponentInChildren<RawImage>().texture = down_sprite;
                    }
                    if (i == 4) { tile.GetComponent<Image>().color = quater_4_color;
                        tile.GetComponentInChildren<RawImage>().color = quater_4_color;
                        if (h_4 == 1) tile.GetComponentInChildren<RawImage>().texture = up_sprite;
                        if (h_4 == -1) tile.GetComponentInChildren<RawImage>().texture = down_sprite;
                    }
                }
            }
        }
        x = _x;
        z = _z;
        var transfer = asker.GetComponent<SquadSetupScript>();
        SetText();
        DrawBehaviorPanel();
        switch (quarter)
        {
            case 1:
                DrawHeightPanel(transfer.early_directive.grid_height);
                break;
            case 2:
                DrawHeightPanel(transfer.middle_directive.grid_height);
                break;
            case 3:
                DrawHeightPanel(transfer.late_directive.grid_height);
                break;
            case 4:
                DrawHeightPanel(transfer.end_directive.grid_height);
                break;
        }
    }

    public void DrawHeightPanel(int height)
    {
        foreach (Button button in height_buttons)
        {
            button.GetComponent<Image>().color = height_panel_off;
            if (button.name == height.ToString())
            {
                button.GetComponent<Image>().color = height_panel_lit;
            }
        }
    }
    public void DrawBehaviorPanel()
    {
        foreach (Button button in behavior_buttons)
        {
            button.GetComponent<Image>().color = height_panel_off;
            if (button.name == DIR.directive_type)
            {
                button.GetComponent<Image>().color = height_panel_lit;
            }
        }
    }
    public void DrawLeash(string _tile)
    {

        foreach (GameObject ztile in tiles)
        {
            ztile.GetComponent<Image>().color = off_color;
        }
            var c = new Color(0, 0, 0, 1);
        switch (quarter)
        {
            case 1:
                c = quater_1_color;
                break;
            case 2:
                c = quater_2_color;
                break;
            case 3:
                c = quater_3_color;
                break;
            case 4:
                c = quater_4_color;
                break;
        }
        c.a = .85f;
        var _z = int.Parse("" + _tile[2]);
        var _x = int.Parse("" + _tile[0]);

        for (int i = (int)DIR.leash + (int)_x; i >= -DIR.leash + (int)_x; i--)
        {
            for (int j = (int)DIR.leash + (int)_z; j >= -DIR.leash + (int)_z; j--)
            {
                if (Mathf.Abs(i - _x) + Mathf.Abs(j - _z) <= DIR.leash && i > 0 && i < 9 && j > 0 && j < 9)
                {
                    foreach (GameObject tile in tiles)
                    {
                        if (tile.name == (i.ToString() + "-" + j.ToString()))
                        {
                            tile.GetComponent<Image>().color = c;
                        }
                    }
                }
            }
            
        }
    }

    public void Quarter_plus(int z)
    {
        quarter = quarter + z;
        if (quarter == 5) quarter = 1;
        UpdateQuarter(quarter);
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
    
    
    public void UpdateDirective(string tile) {

        x = int.Parse("" + tile[2]);
        z = int.Parse("" + tile[0]);
        
        DIR.grid = new Vector3(x, 0, z);
        SetController();
        SetText();
        DrawBoard();
        Quarter_plus(1);

    }

    public void UpdateQuarter(int q)
    {
        switch (q)
        {
            case 1:
                quarter = 1;
                break;
            case 2:
                quarter = 2;
                break;
            case 3:
                quarter = 3;
                break;
            case 4:
                quarter = 4;
                break;
        }
        SetDirective(q);
    }

    public void UpdateDirectiveHeight(int height)
    {
        DIR.grid_height = height;
        DrawBoard();
        SetText();

    }

    public void UpdateDirectiveLeash(int ammount)
    {
        DIR.leash = ammount;
        SetText();

    }

    public void UpdateStartPosition(int pos)
    {
        var transfer = asker.GetComponent<SquadSetupScript>();
        foreach (GameObject start_img in start_poss) { start_img.GetComponent<Image>().color = off_color; }
        switch (pos)
        {
            case 1:
                start_pos = new Vector3(.5f, 0, .5f);
                transfer.starting_position = new Vector3(.5f, 0, .5f);
                start_poss[0].GetComponent<Image>().color = quater_1_color;
                start_poss[0].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 2:
                start_pos = new Vector3(1.5f, 0, .5f);
                transfer.starting_position = new Vector3(1.5f, 0, .5f);
                start_poss[1].GetComponent<Image>().color = quater_1_color;
                start_poss[1].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 3:
                start_pos = new Vector3(2.5f, 0, .5f);
                transfer.starting_position = new Vector3(2.5f, 0, .5f);
                start_poss[2].GetComponent<Image>().color = quater_1_color;
                start_poss[2].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 4:
                start_pos = new Vector3(3.5f, 0, .5f);
                transfer.starting_position = new Vector3(3.5f, 0, .5f);
                start_poss[3].GetComponent<Image>().color = quater_1_color;
                start_poss[3].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 5:
                start_pos = new Vector3(4.5f, 0, .5f);
                transfer.starting_position = new Vector3(4.5f, 0, .5f);
                start_poss[4].GetComponent<Image>().color = quater_1_color;
                start_poss[4].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 6:
                start_pos = new Vector3(5.5f, 0, .5f);
                transfer.starting_position = new Vector3(5.5f, 0, .5f);
                start_poss[5].GetComponent<Image>().color = quater_1_color;
                start_poss[5].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 7:
                start_pos = new Vector3(6.5f, 0, .5f);
                transfer.starting_position = new Vector3(6.5f, 0, .5f);
                start_poss[6].GetComponent<Image>().color = quater_1_color;
                start_poss[6].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
            case 8:
                start_pos = new Vector3(7.5f, 0, .5f);
                transfer.starting_position = new Vector3(7.5f, 0, .5f);
                start_poss[7].GetComponent<Image>().color = quater_1_color;
                start_poss[7].GetComponentInChildren<RawImage>().color = quater_1_color;
                break;
        }

    }

    public void UpdateDirectiveType(int type)
    {
        switch (type)
        {
            case 1:
                DIR.directive_type = "formation";
                DIR.leash = 0;
                break;
            case 2:
                DIR.directive_type = "defensive";
                DIR.leash = 1;
                break;
            case 3:
                DIR.directive_type = "agressive";
                DIR.leash = 3;
                break;
            case 4:
                DIR.directive_type = "search_and_destroy";
                DIR.leash = 5;
                break; 
        }

        DrawBehaviorPanel();
        DrawBoard();
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
