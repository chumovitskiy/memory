using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    private Color highlightColor = Color.cyan;
    private Color defaultColor = Color.white;

    private const float MOD_BUTTON_SIZE = 0.1f;

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = defaultColor;
        }
    }

    public void OnMouseDown()
    {
        pressingButton();
    }

    public void OnMouseUp()
    {
        unpressingButton();
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
                        pressingButton();
                    } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        unpressingButton();
                    }

                }
            }

        }
    }

    private void pressingButton()
    {
        transform.localScale = new Vector3(transform.localScale.x + MOD_BUTTON_SIZE,
                                           transform.localScale.y + MOD_BUTTON_SIZE,
                                           transform.localScale.z + MOD_BUTTON_SIZE);
    }

    private void unpressingButton()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}
