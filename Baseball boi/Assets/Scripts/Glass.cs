using UnityEngine;

namespace YOLO
{
    public class Glass : DestructibleBase
    {
        public GameObject brokenObjectPrefab;
        public float forceMagnitude = 100f;

        public override void DestroyOnCollision(Collision collision)
        {
            base.DestroyOnCollision(collision);

            Destroy(gameObject);
            GameObject brokenIns = Instantiate(brokenObjectPrefab, transform.position, transform.rotation);
            brokenIns.transform.localScale = transform.localScale;
            
            foreach(Transform child in brokenIns.transform)
            {
                if(child.TryGetComponent<Rigidbody>(out Rigidbody childBody))
                {
                    childBody.AddExplosionForce(forceMagnitude, collision.contacts[0].point, 5f);
                    childBody.AddForce(-Vector3.forward * 2f, ForceMode.VelocityChange);
                }
            }

            Destroy(brokenIns, 3f);
        }
    }
}