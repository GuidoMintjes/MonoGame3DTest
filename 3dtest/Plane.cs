using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TDTestGame {
    class Plane : GenericObject {

        public Plane(float size, GraphicsDevice graphicsDevice, Vector3 pos, int axis) {

            // Create the triangles
            objectMesh = new VertexPositionColor[12];
            objectMesh[0] = new VertexPositionColor(new Vector3(20f, 0f, 20f) * size, Color.CadetBlue);
            objectMesh[1] = new VertexPositionColor(new Vector3(-20f, 0f, -20f) * size, Color.DarkSlateBlue);
            objectMesh[2] = new VertexPositionColor(new Vector3(-20f, 0f, 20f) * size, Color.RoyalBlue);

            objectMesh[3] = new VertexPositionColor(new Vector3(-20f, 0f, -20f) * size, Color.DarkSlateBlue);
            objectMesh[4] = new VertexPositionColor(new Vector3(20f, 0f, 20f) * size, Color.CadetBlue);
            objectMesh[5] = new VertexPositionColor(new Vector3(20f, 0f, -20f) * size, Color.RoyalBlue);

            buffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), 12, BufferUsage.None);
            buffer.SetData(objectMesh);

            MoveObject(pos);

            base.InitializeObject(pos, axis);
        }
    }
}
