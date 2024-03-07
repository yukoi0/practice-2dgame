using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;


public class Dragcontroller : MonoBehaviour
{
    [SerializeField] public LineRenderer Line;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public float draglimit = 3f;
    [SerializeField] public float ForceToAdd = 10f;
    private Camera cam;
    private bool isDragging;

    Vector3 MousePosition
    {
        get
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Line.positionCount = 2;
        Line.SetPosition(0, Vector2.zero);
        Line.SetPosition(1, Vector2.zero);
        Line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            DragStart();
        }

        if (isDragging)
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragEnd();
        }


    }
    void DragStart()
    {
        Line.enabled = true;
        isDragging = true;
        Line.SetPosition(0, MousePosition);
    }
    void Drag()
    {
        Vector3 StartPos = Line.GetPosition(0);
        Vector3 currentPos = MousePosition;
        Vector3 distance = currentPos - StartPos;
        if (distance.magnitude <= draglimit)
        {
            Line.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 limitVector = StartPos + (distance.normalized * draglimit);
            Line.SetPosition(1, limitVector);
        }



    }

    void DragEnd()
    {
        isDragging = false;
        Line.enabled = false;


        Vector3 StartPos = Line.GetPosition(0);
        Vector3 currentPos = Line.GetPosition(1);
        Vector3 distance = currentPos - StartPos;
        Vector3 finalForce = distance * ForceToAdd;

        rb.AddForce(-finalForce, ForceMode2D.Impulse);
    }


}

