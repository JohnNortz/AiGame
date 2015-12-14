using UnityEngine;
using System.Collections;

public class KillAfterTime : MonoBehaviour {

    public float time;
    private float _time;
    
    // Update is called once per frame
    void Update () {
        _time += Time.deltaTime;
        if (_time > time) Destroy(this.gameObject);
	}
}
