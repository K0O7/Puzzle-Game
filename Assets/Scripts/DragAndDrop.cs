using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject selectedPiece;
    public GameObject winBanner;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("piece"))
            {
                if (!hit.transform.GetComponent<PicesScript>().inRightPosition)
                {
                    selectedPiece = hit.transform.gameObject;
                    selectedPiece.GetComponent<PicesScript>().selected = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(selectedPiece != null)
            {
                selectedPiece.GetComponent<PicesScript>().selected = false;
            }
            selectedPiece = null;
        }

        if (selectedPiece != null)
        {
            selectedPiece.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
