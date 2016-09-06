using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View.Player
{
    class PlayerCamera : MonoBehaviour
    {
        private new Camera camera = null;
        private float zoom = 20f;

        public PlayerComponent Player = null;

        public PlayerCamera()
        {
        }

        public void Awake()
        {
            camera = GetComponent<Camera>();
            //camera.projectionMatrix = Matrix4x4.Perspective((float)Math.PI / 2f, camera.aspect, 1f, 1000000f);
        }

        public void Update()
        {
            if (Player != null)
            {
                camera.transform.SetParent(Player.transform);
                camera.transform.localPosition = Vector3.zero;
                camera.transform.localRotation = Quaternion.identity;
                camera.transform.Translate(Vector3.up * zoom + Vector3.back * zoom / 10f);
                camera.transform.LookAt(Player.transform);
            }
        }
    }
}
