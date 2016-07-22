using UnityEngine;
using System.Collections;

public class CardBehaviour : MonoBehaviour
{
    public Sprite[] Cards;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initialize(int randCardNumber)
    {
        //FirstStar.GetComponentInChildren<SpriteRenderer>().sprite = StarBase;

        GetComponent<SpriteRenderer>().sprite = Cards[randCardNumber - 3];
    }
}
