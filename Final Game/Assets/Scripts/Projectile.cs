using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final.Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] Transform target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null)
            {
                return;
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Transform target)
        {
            this.target = target.transform;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            speed = 0;
            if (other.GetComponent<Deer>() != null)
            {
                Debug.Log("hit");
                other.GetComponent<Deer>().Die();
            }

            Destroy(gameObject);
        }
    }
}