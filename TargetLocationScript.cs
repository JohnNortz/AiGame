using UnityEngine;
using System.Collections;

public class TargetLocationScript : MonoBehaviour {
    
    public int team; 

	void Start () {
        team = this.transform.parent.GetComponent<ShipScript>().team;
	}
	
}
