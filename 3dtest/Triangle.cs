using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace TDTestGame {
    class Triangle {

        public int vertices;

        public Vector3 position;

        public VertexPositionColor[] triangle;

        public VertexBuffer buffer;


        public Triangle(float size, GraphicsDevice graphicsDevice, Vector3 pos) {

            // Create the triangles
            triangle = new VertexPositionColor[12];
            triangle[0] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            triangle[1] = new VertexPositionColor(new Vector3(-20f, -20f, -20f) * size, Color.Green);
            triangle[2] = new VertexPositionColor(new Vector3(20f, -20f, -20f) * size, Color.Blue);

            triangle[3] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            triangle[4] = new VertexPositionColor(new Vector3(-20f, -20f, 20f) * size, Color.Blue);
            triangle[5] = new VertexPositionColor(new Vector3(-20f, -20f, -20f) * size, Color.Green);

            triangle[6] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            triangle[7] = new VertexPositionColor(new Vector3(20f, -20f, 20f) * size, Color.Green);
            triangle[8] = new VertexPositionColor(new Vector3(-20f, -20f, 20f) * size, Color.Blue);

            triangle[9] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            triangle[10] = new VertexPositionColor(new Vector3(20f, -20f, 20f) * size, Color.Green);
            triangle[11] = new VertexPositionColor(new Vector3(20f, -20f, -20f) * size, Color.Blue);

            buffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), 12, BufferUsage.None);
            buffer.SetData(triangle);

            MoveObject(pos);

            position = pos;

            vertices += 12;
        }


        public void MoveObject(Vector3 pos) {

            buffer.GetData(triangle);

            for (int i = 0; i < buffer.VertexCount; i++) {

                triangle[i].Position += pos;
            }

            buffer.SetData(triangle);

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


            buffer.GetData(triangle);

            for (int i = 0; i < buffer.VertexCount; i++) {

                triangle[i].Position = Vector3.Transform(triangle[i].Position, rotationMatrix);
            }

            buffer.SetData(triangle);
        }
    }
}
