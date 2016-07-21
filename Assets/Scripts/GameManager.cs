using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject Star;
    public GameObject StarParent;

    StarBehaviour LinkingStar;
    StarBehaviour FirstStar;

    public Sprite StarBase;
    public Sprite StarFirst;

    public LayerMask Default;
    public LayerMask Ignore;
    public LayerMask RayMask;

	// Use this for initialization
	void Start () {

        LinkingStar = null;

        //Spawning
        for (int i = 0; i < 40; i++) {

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

            RaycastHit2D Hit = Physics2D.Raycast(LinkingStar.transform.position, StarToLink.transform.position);

            if (Hit)
            {
                Debug.Log(Hit.transform.name);
                Debug.Log(StarToLink.transform.name);

                if (Hit.transform.name == StarToLink.transform.name)
                {
                    LinkingStar.SetLineTarget(StarToLink.GetComponent<Transform>());
                    LinkingStar = StarToLink;
                    return true;
                }
            }

            return false;
        }
        else
        {

            LinkingStar = StarToLink;
            FirstStar = StarToLink;
            FirstStar.GetComponentInChildren<SpriteRenderer>().sprite = StarFirst; 
            return false;
        }

    }
}
