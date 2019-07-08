using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{    public class PlayerManager : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        private float speed = 4;

        [Header("Class reference")]
        [SerializeField]
        private NetworkIdentity networkIdentity;

        void Update()
        {
            if(networkIdentity.IsControlling())
            {
                checkMovement();
            }
        }

        private void checkMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        }
    }

}