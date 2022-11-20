 using UnityEngine;
using System.Collections;

namespace YOLO
{
    public class BallSpawner : MonoBehaviour
    {
        public Transform startPoint;
        public Transform endPoint;
        public Transform curvePoint;

        public GameObject ballPrefab;
        public Ball currentBall;

        // private IEnumerator Start()
        // {
        //     yield return new WaitForSeconds(2f);

        //     SpawnBall();
        // }
        public void SpawnBall()
        {
            GameObject ballIns = Instantiate(ballPrefab, startPoint.position, Quaternion.identity);     

            // Init Ball here   
            currentBall = ballIns.GetComponent<Ball>();
            currentBall.Initialize(startPoint.position, curvePoint.position, endPoint.position);    
        }
    }
}
