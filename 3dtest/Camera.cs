using System;
using Microsoft.Xna.Framework;

namespace TDTestGame {
    class Camera : GameComponent {

        public Vector3 camPos;
        public Vector3 camRot;
        public Vector3 camTarget;


        public Matrix projectionMatrix;    // This holds the to be rendered part
        public Matrix viewMatrix;          // ^^^


        public bool orbit { get; private set; }    // Used for rotating, panning and moving camera

        public Camera(Game game, Vector3 camPosit, Vector3 camRotat) : base(game) {

            camPos = camPosit;
            camRot = camRotat;
            camTarget = new Vector3(0f, 0f, 0f);     // Virtually move out of screen to look at object
            

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(Constants.fieldOfView),
                game.GraphicsDevice.Viewport.AspectRatio,
                0.01f, 10000f
            );


            viewMatrix = Matrix.CreateLookAt(
                camPos,
                camTarget,
                Vector3.Up
            );
        }


        public void MoveCam(Vector3 movement) {

            camPos += movement;
            camTarget += movement;
        }


        public void MoveTarget(Vector3 movement) {

            camTarget += movement;
        }


        public void UpdateLookAt() {

            Matrix rotationMatrix = Matrix.CreateRotationX(camRot.X) * Matrix.CreateRotationY(camRot.Y);

            Vector3 targetOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);

            camTarget = camPos + targetOffset;
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


        public void RotateCamHorizontal(float amount) {

            Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(amount));
            camTarget = Vector3.Transform(camTarget - camPos, rotationMatrix) + camPos;
        }


        public void RotateCameraVertical(float amount) {

            Matrix rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(amount));
            camTarget = Vector3.Transform(camTarget - camPos, rotationMatrix) + camPos;
        }
    }
}
