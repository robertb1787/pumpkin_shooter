using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PumpkinShooter.Game
{
    public class Enemy : MonoBehaviour
    {
        [Header("Score")]
        public int scoreValue = 100;

        [Header("Movement")]
        [SerializeField] private float _verticalAmplitude = 2.5f;
        [SerializeField] private float _verticalFrequency = 2.5f;

        [Header("Physics")]
        [SerializeField] private Rigidbody _rigidBody = null;

        [HideInInspector]
        public UnityEvent<int> onKill = new UnityEvent<int>();

        private Vector3 _startPosition = Vector3.zero;
        // Start is called before the first frame update
        void Start()
        {
            _startPosition = transform.position;
        }
        private void OnEnable()
        {
            GameManager.CUSTOMUPDATE.AddListener(CustomUpdate);
        }

        private void OnDisable()
        {
            GameManager.CUSTOMUPDATE.RemoveListener(CustomUpdate);
        }

        void CustomUpdate(float deltaTime)
        {
            float positionOffset = Mathf.Sin(Time.timeSinceLevelLoad / _verticalFrequency) * _verticalAmplitude;
            transform.position = new Vector3(_startPosition.x, _startPosition.y + positionOffset, _startPosition.z);
        }

        void Die()
        {
            _rigidBody.useGravity = true;
            Destroy(gameObject, 3);
            Destroy(this);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Cannonball>())
            {
                onKill.Invoke(scoreValue);
                _rigidBody.AddForceAtPosition(collision.transform.forward, collision.GetContact(0).point, ForceMode.Impulse);
                Die();
            }
        }
    }
}