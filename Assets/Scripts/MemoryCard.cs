using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;

    private int _id;

    public void OnMouseDown()
    {
        changeStatusCard();
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (this.gameObject.name.Equals(hit.collider.gameObject))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        changeStatusCard();
                    }
                }
            }

        }
    }

    public void unreveal()
    {
        cardBack.SetActive(true);
    }

    public int id
    {
        get { return _id; }
    }

    public void setCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    private void changeStatusCard ()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.cardRevealed(this);
        }
    }
}