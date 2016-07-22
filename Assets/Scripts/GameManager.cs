using UnityEngine;
using System.Collections;

//Player enum
public enum Player
{
    Player1,
    Player2
};

public class GameManager : MonoBehaviour {

    //The StarPrefab
    public GameObject Star;
    //Empty Gameobject to keep all of the stars in
    public GameObject StarParent;

    //The star that was last clicked
    StarBehaviour LinkingStar;
    //The first star in the sequence
    StarBehaviour FirstStar;

    //Sprites for each star
    public Sprite StarBase;
    public Sprite StarFirst;

    public Player Turn; // Tracks whose turn it is

	// Use this for initialization
	void Start () {

        Turn = Player.Player1;

        LinkingStar = null;

        //Spawning
        for (int i = 0; i < 40; i++) {

            float XCoord = Random.Range(-6.0f, 6.0f);
            float YCoord = Random.Range(-4.0f, 4.0f);

            Vector3 Pos = new Vector3(XCoord, YCoord, 0); 

            Quaternion Zero = new Quaternion(0,0,0,0);


            GameObject StarClone = (GameObject) Instantiate(Star, Pos, Zero);
            StarClone.transform.parent = StarParent.transform;
            StarClone.name = "Star " + i;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(1))
        {
            LinkingStar = null;
            FirstStar.GetComponentInChildren<SpriteRenderer>().sprite = StarBase;
            FirstStar = null;

        }
    }

    public bool SetLinkingStar(StarBehaviour StarToLink)
    {

        if (StarToLink == FirstStar)
        {

            LinkingStar.SetLineTarget(StarToLink.GetComponent<Transform>(), Turn);
            LinkingStar = null;
            FirstStar.GetComponentInChildren<SpriteRenderer>().sprite = StarBase;
            FirstStar = null;

            Debug.Log(Turn);
            
            if (Turn == Player.Player2)
            {
                Turn = Player.Player1;
            }
            else
            {
                Turn = Player.Player2;
            }

            return true;
        }
        else if (LinkingStar != null && StarToLink != LinkingStar)
        {

            RaycastHit2D Hit = Physics2D.Raycast(LinkingStar.transform.position, StarToLink.transform.position - LinkingStar.transform.position);

            if (Hit)
            {

                if (Hit.transform.name == StarToLink.transform.name)
                {

                    LinkingStar.SetLineTarget(StarToLink.GetComponent<Transform>(), Turn);
                    LinkingStar = StarToLink;
                    return true;
                }
            }

            return false;
        }
        else if (FirstStar == null)
        {

            LinkingStar = StarToLink;
            FirstStar = StarToLink;
            FirstStar.GetComponentInChildren<SpriteRenderer>().sprite = StarFirst; 
            return false;
        }

        return false;
    }
}
