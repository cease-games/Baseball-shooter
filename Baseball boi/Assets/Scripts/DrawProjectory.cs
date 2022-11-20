using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YOLO{
public class DrawProjectory : MonoBehaviour
{
    public Rigidbody projectile;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment = 10;
    Animator animator;
    BallSpawner ballSpawner;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        ballSpawner = GetComponent<BallSpawner>();
        cam = Camera.main;
        lineVisual.positionCount = lineSegment;
    }

    // Update is called once per frame
    void Update()
    {
        LaunchProjectile();
    }
    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculaterPosInTime(vo, i/(float)lineSegment);
            lineVisual.SetPosition(i, pos);
        }
    }
    Vector3 CalculaterPosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y)*(time*time))+(vo.y *time)+ shootPoint.position.y;

        result.y = sY;

        return result;
    }
    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(camRay,out hit,100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up *0.1f;

            Vector3 vo = CalculateVelocity(hit.point, shootPoint.position,1f);

            Visualize(vo);

            if(Input.GetMouseButtonDown(0))
            {
                animator.SetBool("isShooting",true);
                Rigidbody obj = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                obj.velocity = vo;
            }
        }
        else
        {
            cursor.SetActive(false);
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //Define the distance x and y first
        Vector3 distance = target -origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y=0;

        //create a float the represent our diestance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz/time;
        float Vy = Sy/time + 0.5f *Mathf.Abs(Physics.gravity.y)*time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y =Vy;

        return result;
    }
}
}

