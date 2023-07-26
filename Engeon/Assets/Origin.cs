using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform origin;
    public LineRenderer Xaxis;
    public LineRenderer Yaxis;
    //public Camera camera;
    
    void Start()
    {
        origin = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Xaxis.SetWidth(0.1f, 0.1f);
        Yaxis.SetWidth(0.1f, 0.1f);

        Xaxis.SetPosition(0, -Horizon());
        Xaxis.SetPosition(1, Horizon());

        Yaxis.SetPosition(0, Vertical());
        Yaxis.SetPosition(1, -Vertical());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(origin.position, 2);
    }

    private Vector3 Horizon()
    {
        return new Vector3(Screen.width,0,0);
    }

    private Vector3 Vertical()
    {
        return new Vector3(0, Screen.height, 0);
    }
}
