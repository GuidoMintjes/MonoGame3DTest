using System;
using Microsoft.Xna.Framework;

namespace TDTestGame {
    class Camera {

        Vector3 camPos;
        public Vector3 camTarget;


        public Matrix projectionMatrix;    // This holds the to be rendered part
        public Matrix viewMatrix;          // ^^^


        public bool orbit { get; private set; }    // Used for rotating, panning and moving camera

        public Camera(float aspectRatio) {

            camPos = new Vector3(0f, 0f, 100f);
            camTarget = new Vector3(0f, 0f, 0f);     // Virtually move out of screen to look at object


            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(Constants.fieldOfView),
                aspectRatio,
                0.01f, 1000f
            );


            viewMatrix = Matrix.CreateLookAt(
                camPos,
                camTarget,
                Vector3.Up
            );
        }


        public void MoveCam(Vector3 movement) {

            camPos += movement;
        }


        public void MoveTarget(Vector3 movement) {

            camTarget += movement;
        }


        public void Zoom(Vector3 zoom) {

            MoveCam(zoom);
        }


        public void ToggleOrbit() {

            orbit = !orbit;
        }


        public void Orbit() {

            Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1f));
            camPos = Vector3.Transform(camPos, rotationMatrix);
        }


        public void CreateLookAt() {

            viewMatrix = Matrix.CreateLookAt(
                camPos,
                camTarget,
                Vector3.Up
            );
        }
    }
}
