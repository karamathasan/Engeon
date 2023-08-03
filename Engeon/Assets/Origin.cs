using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform origin;
    public LineRenderer Xaxis;
    public LineRenderer Yaxis;
    public Camera camera;

    public GameObject gridX;
    private List<GameObject> gridXlines;

    public GameObject gridY;
    private List<GameObject> gridYlines;

    public float tickDistance = 2;
    
    void Start()
    {
        origin = gameObject.GetComponent<Transform>();
        camera = Camera.main;

        Xaxis.SetWidth(0.07f, 0.07f);
        Yaxis.SetWidth(0.07f, 0.07f);

        Xaxis.SetPosition(0, -Horizon());
        Xaxis.SetPosition(1, Horizon());

        Yaxis.SetPosition(0, Vertical());
        Yaxis.SetPosition(1, -Vertical());

        drawGridX();
        drawGridY();
    }

    // Update is called once per frame
    void Update()
    {

        FitLineX(Xaxis);
        FitLineY(Yaxis);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(origin.position, 2);
    }

    private Vector3 Horizon()
    {
        //return new Vector3(Screen.width, 0, 0);
        return new Vector3(camera.orthographicSize * camera.aspect ,0,0);

    }

    private Vector3 Vertical()
    {
        //return new Vector3(0, Screen.height, 0);
        return new Vector3(0,camera.orthographicSize,0);
    }
    //needs to add zoom later
    private float OuterBound(Camera camera, string axis)
    {
        float cameraX = camera.transform.position.x;
        float cameraY = camera.transform.position.y;

        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        if (axis == "x")
        {
            return Mathf.Abs(cameraX + Mathf.Sign(cameraX) * cameraWidth);
        }
        else if (axis == "y")
        {
            return Mathf.Abs(cameraY + Mathf.Sign(cameraY) * cameraHeight);
        }
        else return 0.69f;

    }
    private float InnerBound(Camera camera, string axis)
    {
        float cameraX = camera.transform.position.x;
        float cameraY = camera.transform.position.y;

        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        if (axis == "x")
        {
            return Mathf.Abs(cameraX - Mathf.Sign(cameraX) * cameraWidth);
        }
        else if (axis == "y")
        {
            return Mathf.Abs(cameraY - Mathf.Sign(cameraY) * cameraHeight);
        }
        else return 0.69f;
    }
    private void FitLineX(LineRenderer line)
    {
        if (0 > camera.transform.position.y - camera.orthographicSize &&
            0 < camera.transform.position.y + camera.orthographicSize)
        {
            line.SetPosition(0, -Horizon() + new Vector3(camera.transform.position.x - 1,0,0));
            line.SetPosition(1, Horizon() + new Vector3(camera.transform.position.x + 1, 0, 0));
        }
    }
    private void FitLineY(LineRenderer line)
    {
        if (0 > camera.transform.position.x - (camera.orthographicSize * camera.aspect + 1) &&
            0 < camera.transform.position.x + (camera.orthographicSize * camera.aspect + 1))
        {
            line.SetPosition(0, -Vertical() + new Vector3(0, camera.transform.position.y - 1, 0));
            line.SetPosition(1, Vertical() + new Vector3(0, camera.transform.position.y + 1, 0));
        }
    }

    private void drawGridX()
    {
        float leftEdge = camera.transform.position.x - camera.orthographicSize * camera.aspect;
        float rightEdge = camera.transform.position.x + camera.orthographicSize * camera.aspect;
        float initGridline = tickDistance - Mathf.Abs(leftEdge % tickDistance) + leftEdge;
        //Debug.Log(leftEdge % tickDistance);
        //Debug.Log(tickDistance - ((leftEdge) % tickDistance));
        for (int i = 0; i * tickDistance +  initGridline < rightEdge; i++) {
            //Debug.Log("initGridline: " + initGridline  + "\nrightEdge: " + rightEdge + "\ncondition: " + (i * tickDistance < rightEdge));
            Vector3 shift = new Vector3(initGridline + i * tickDistance, 0, 0);
            GameObject gridLineClone = Instantiate(gridX, Vector3.zero, Xaxis.transform.rotation);
            gridLineClone.GetComponent<LineRenderer>().SetWidth(0.02f, 0.02f);
            gridLineClone.GetComponent<LineRenderer>().SetPosition(0, shift + Vertical());
            gridLineClone.GetComponent<LineRenderer>().SetPosition(1, shift - Vertical());
            gridXlines.Add(gridLineClone);
        }
    }

    private void drawGridY()
    {
        float bottomEdge = camera.transform.position.y - camera.orthographicSize;
        float topEdge = camera.transform.position.y + camera.orthographicSize;
        float initGridline = tickDistance - Mathf.Abs(bottomEdge % tickDistance) + bottomEdge;
        for (int i = 0; i * tickDistance + initGridline < topEdge; i++)
        {
            Vector3 shift = new Vector3(0, initGridline + i * tickDistance, 0);
            GameObject gridLineClone = Instantiate(gridY, Vector3.zero, Xaxis.transform.rotation);
            gridLineClone.GetComponent<LineRenderer>().SetWidth(0.02f, 0.02f);
            gridLineClone.GetComponent<LineRenderer>().SetPosition(0, shift + Horizon());
            gridLineClone.GetComponent<LineRenderer>().SetPosition(1, shift - Horizon());
            gridYlines.Add(gridLineClone);
        }
    }
}
