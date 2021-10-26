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
            triangle[0] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size + pos, Color.Red);
            triangle[1] = new VertexPositionColor(new Vector3(-20f, -20f, -20f) * size + pos, Color.Green);
            triangle[2] = new VertexPositionColor(new Vector3(20f, -20f, -20f) * size + pos, Color.Blue);

            triangle[3] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size + pos, Color.Red);
            triangle[4] = new VertexPositionColor(new Vector3(-20f, -20f, 20f) * size + pos, Color.Blue);
            triangle[5] = new VertexPositionColor(new Vector3(-20f, -20f, -20f) * size + pos, Color.Green);

            triangle[6] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size + pos, Color.Red);
            triangle[7] = new VertexPositionColor(new Vector3(20f, -20f, 20f) * size + pos, Color.Green);
            triangle[8] = new VertexPositionColor(new Vector3(-20f, -20f, 20f) * size + pos, Color.Blue);

            triangle[9] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size + pos, Color.Red);
            triangle[10] = new VertexPositionColor(new Vector3(20f, -20f, 20f) * size + pos, Color.Green);
            triangle[11] = new VertexPositionColor(new Vector3(20f, -20f, -20f) * size + pos, Color.Blue);



            buffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), 12, BufferUsage.WriteOnly);
            buffer.SetData(triangle);

            position = pos;

            vertices += 12;
        }


        public VertexBuffer GetBuffer() {


            return buffer;
        }
    }
}
