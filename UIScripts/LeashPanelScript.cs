using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeashPanelScript : MonoBehaviour {

    public Button[] buttons;

    // Update is called once per frame
    public void UpdateButtons()
    {
        foreach (Button button in buttons)
        {
            if (transform.parent.GetComponent<MovementDialScript>().DIR.leash.ToString() == button.name)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
}