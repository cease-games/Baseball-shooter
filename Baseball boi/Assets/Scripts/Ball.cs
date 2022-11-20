using UnityEngine;

namespace YOLO
{
    public class Ball : MonoBehaviour
    {
        public float timeToEndPoint = 2f;
        public float timeToTarget = 1f;
        public float ballVelocity = 20f;

        private float timer;
        private Vector3 startPoint;
        private Vector3 curvePoint;
        private Vector3 endPoint;
        private Vector3 direction;

        private bool wasHit;
        private bool collided;

        private Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            rigid.isKinematic = true;
        }

        public void Initialize(Vector3 startPoint, Vector3 curvePoint, Vector3 endPoint)
        {
            this.startPoint = startPoint;
            this.curvePoint = curvePoint;
            this.endPoint = endPoint;
        }

        public void Hit(Vector3 startPoint, Vector3 endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;

            this.direction = (endPoint - startPoint).normalized;

            wasHit = true;
            rigid.isKinematic = false;
            timer = 0f;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if(timer <= timeToEndPoint && !wasHit)
            {
                rigid.MovePosition(GetBallPosition(timer / timeToEndPoint));
            }     
            else if(wasHit && !collided)
            {
                rigid.velocity = direction * ballVelocity;
            }                   
        }

        private void OnCollisionEnter(Collision other)
        {
            collided = true;  

            Destroy(gameObject);                  
        }

        public Vector3 GetBallPosition(float t)
        {
            float u = 1f - t;
            float tt = t * t;
            float uu = u * u;

            return (uu * startPoint) + (2f * u * t * curvePoint) + (tt * endPoint);
        }
    }
}
