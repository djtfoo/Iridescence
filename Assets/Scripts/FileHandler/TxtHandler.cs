using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class TxtHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void WriteToTxt(string text, string filepath)
    {
        StreamWriter writer = new StreamWriter(filepath, false);    // append is false - overwrite
        writer.WriteLine(text);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(filepath);
        TextAsset asset = Resources.Load("test") as TextAsset;
    }

    public static string[] GetTxtLines(TextAsset txtfile)
    {
        string[] lines = txtfile.text.Split("\n"[0]);

        return lines;
    }

}
