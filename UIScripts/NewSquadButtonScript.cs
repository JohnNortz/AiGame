using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewSquadButtonScript : MonoBehaviour {

    public GameObject SquadUIPrefab;
    public int squad_count;


	public void Clicked() {
        var squad = Instantiate(SquadUIPrefab, transform.position, Quaternion.identity) as GameObject;
        squad.transform.parent = this.transform.parent;
        squad.GetComponent<SquadSetupScript>().squad_count = squad_count;
        squad.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        squad.GetComponent<RectTransform>().localPosition = new Vector3(squad.GetComponent<RectTransform>().localPosition.x, squad.GetComponent<RectTransform>().localPosition.y, -.1f);
        this.transform.position = new Vector3(this.transform.position.x + 40, this.transform.position.y, this.transform.position.z);
        squad_count++;
        if (squad_count >= 4) this.GetComponent<RectTransform>().position = new Vector3(this.GetComponent<RectTransform>().localPosition.x + 300, this.GetComponent<RectTransform>().localPosition.y, -.1f);

    }
}
