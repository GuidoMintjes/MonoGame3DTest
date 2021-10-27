using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDTestGame {
    class GenericObject {

        public int vertices;

        public Vector3 position;

        public VertexPositionColor[] objectMesh;

        public VertexBuffer buffer;

        public int RotateAxis;


        protected void InitializeObject(Vector3 pos, int axis) {

            position = pos;

            RotateAxis = axis;

            vertices += objectMesh.GetLength(0);
        }


        public void MoveObject(Vector3 pos) {

            buffer.GetData(objectMesh);

            for (int i = 0; i < buffer.VertexCount; i++) {

                objectMesh[i].Position += pos;
            }

            buffer.SetData(objectMesh);

            position += pos;
        }


        public void RotateObject(int axis, float amount) {

            Matrix rotationMatrix;

            switch (axis) {

                case 0:
                    rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(amount));
                    break;

                case 1:
                    rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(amount));
                    break;

                case 2:
                    rotationMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(amount));
                    break;

                default:
                    rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(amount));
                    break;
            }


            buffer.GetData(objectMesh);

            for (int i = 0; i < buffer.VertexCount; i++) {

                objectMesh[i].Position = Vector3.Transform(objectMesh[i].Position - position, rotationMatrix) + position;
            }

            buffer.SetData(objectMesh);
        }
    }
}
