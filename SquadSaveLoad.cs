using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SquadSaveLoad : MonoBehaviour {

    public List<Squad> saved_squads = new List<Squad>();
    
    public void SaveSquad (Squad _Squad) {
        foreach (Squad s in saved_squads)
        {
            if (s.squad_name == _Squad.squad_name) saved_squads.Remove(s);
        }
        saved_squads.Add(_Squad);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saved_squads.gd");
        bf.Serialize(file, this.saved_squads);
        file.Close();
    }

    public void LoadSquads()
    {
        if (File.Exists(Application.persistentDataPath + "/saved_squads.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saved_squads.gd", FileMode.Open);
            this.saved_squads = (List<Squad>)bf.Deserialize(file);
            file.Close();
        }
    }
    

}
