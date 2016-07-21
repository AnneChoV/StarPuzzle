using UnityEngine;
using System.Collections;

public class LineDrawing : MonoBehaviour {

    LineRenderer Renderer;

    public Transform Star1;
    public Transform Star2;

	// Use this for initialization
	void Start () {

        Renderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Star1 != null && Star2 != null)
        {
            Vector3[] ListOfLines = new Vector3[4];
            ListOfLines[0] = Star1.position;
            ListOfLines[1] = Star2.position;
            ListOfLines[2] = Star1.position;
            ListOfLines[3] = Star2.position;

            Renderer.SetPositions(ListOfLines);
        }

	}
}
