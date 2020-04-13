using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject[] heroes;
    public LayerMask unwalkable;

    private Vector3 worldPosition;
    private Vector3 putHeroCoord;
    private bool isPicked=false;
    private bool cannotPickNow = false;
    private GameObject pickedHero;
    private int index;

    private void Update()
    {
        if (isPicked)
        {
            if (Input.GetMouseButton(0))
            {
                dragAndDropHero();
            }
            if (Input.GetMouseButtonUp(0))
            {
                putHero();
            }
        }
    }

    public void pickHeroes(int index)
    {
        isPicked = true;
        this.index = index;
    }

    private void dragAndDropHero()
    {
        if (!cannotPickNow)
        {
            GameObject go;
            go = Instantiate(heroes[index]) as GameObject;
            pickedHero = go;
        }

        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        putHeroCoord = new Vector3(worldPosition.x,0.5f,worldPosition.z);
        pickedHero.transform.position = putHeroCoord;
        cannotPickNow = true;
    }

    private void putHero()
    {
        isPicked = false;
        cannotPickNow = false;
        if (Physics.OverlapSphere(pickedHero.transform.position, 1f, unwalkable).Length <= 0)
        {
            pickedHero.GetComponent<Player>().spawnThisPlayer();
        }
        else
        {
            Destroy(pickedHero);
        }
    }
}
