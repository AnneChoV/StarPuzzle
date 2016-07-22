using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject Star;
    public GameObject StarParent;

    public GameObject CardPrefab;
    public GameObject CardParent;

    StarBehaviour LinkingStar;
    StarBehaviour FirstStar;

    public Sprite StarBase;
    public Sprite StarFirst;

    public GameObject[] Cards;

    public LayerMask Default;
    public LayerMask Ignore;
    public LayerMask RayMask;

	// Use this for initialization
	void Start () {

        LinkingStar = null;
        int StarNumber = 40;    //Number of stars
        int CardNumber = 7;     //Number of cards
        Vector3 CurrentCardPos = new Vector3(-5, 0, 0); //The starting position of cards

        //Spawning stars
        for (int i = 0; i < StarNumber; i++) {

            float XCoord = Random.Range(-6.0f, 6.0f);
            float YCoord = Random.Range(-4.0f, 4.0f);

            Vector3 Pos;
            Pos.x = XCoord;
            Pos.y = YCoord;
            Pos.z = 0;

            Quaternion Zero;
            Zero.x = 0;
            Zero.y = 0;
            Zero.z = 0;
            Zero.w = 0;

            GameObject StarClone = (GameObject) Instantiate(Star, Pos, Zero);
            StarClone.transform.parent = StarParent.transform;
            StarClone.name = "Star " + i;
        }


        //Spawning cards


        for (int i = 0; i < CardNumber; i++)
        {
            int randCardNumber = Random.Range(3, 10);

            GameObject CardClone = (GameObject)Instantiate(CardPrefab, CurrentCardPos, transform.rotation);
            CardClone.transform.parent = CardParent.transform;
            CardClone.name = "Card " + i;

            CardClone.GetComponent<CardBehaviour>().initialize(randCardNumber);

            CurrentCardPos.x += 2;
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

            LinkingStar.SetLineTarget(StarToLink.GetComponent<Transform>());
            LinkingStar = null;
            FirstStar.GetComponentInChildren<SpriteRenderer>().sprite = StarBase;
            FirstStar = null;

            return true;
        }
        else if (LinkingStar != null && StarToLink != LinkingStar)
        {

            RaycastHit2D Hit = Physics2D.Raycast(LinkingStar.transform.position, StarToLink.transform.position - LinkingStar.transform.position);

            if (Hit)
            {

                if (Hit.transform.name == StarToLink.transform.name)
                {
                    LinkingStar.SetLineTarget(StarToLink.GetComponent<Transform>());
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
