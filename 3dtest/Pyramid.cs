using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace TDTestGame {
    class Pyramid : GenericObject {


        public Pyramid(float size, GraphicsDevice graphicsDevice, Vector3 pos, int axis) {

            // Create the triangles
            objectMesh = new VertexPositionColor[12];
            objectMesh[0] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            objectMesh[1] = new VertexPositionColor(new Vector3(-20f, -20f, -20f) * size, Color.Green);
            objectMesh[2] = new VertexPositionColor(new Vector3(20f, -20f, -20f) * size, Color.Blue);

            objectMesh[3] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            objectMesh[4] = new VertexPositionColor(new Vector3(-20f, -20f, 20f) * size, Color.Blue);
            objectMesh[5] = new VertexPositionColor(new Vector3(-20f, -20f, -20f) * size, Color.Green);

            objectMesh[6] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            objectMesh[7] = new VertexPositionColor(new Vector3(20f, -20f, 20f) * size, Color.Green);
            objectMesh[8] = new VertexPositionColor(new Vector3(-20f, -20f, 20f) * size, Color.Blue);

            objectMesh[9] = new VertexPositionColor(new Vector3(0f, 20f, 0f) * size, Color.Red);
            objectMesh[10] = new VertexPositionColor(new Vector3(20f, -20f, 20f) * size, Color.Green);
            objectMesh[11] = new VertexPositionColor(new Vector3(20f, -20f, -20f) * size, Color.Blue);

            buffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), 12, BufferUsage.None);
            buffer.SetData(objectMesh);

            MoveObject(pos);

            base.InitializeObject(pos, axis);
        }
    }
}
