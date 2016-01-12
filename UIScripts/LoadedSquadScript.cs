using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadedSquadScript : MonoBehaviour {

    public Squad squad;
    public string squad_name;
    public SquadEditPanelScript asker;

    void Start()
    {
        squad_name = squad.squad_name;
        this.GetComponentInChildren<Text>().text = squad_name;
    }

    public void clicked()
    {
        asker.AssignSquad(squad);
    }
}
