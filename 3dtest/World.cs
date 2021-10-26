using System;
using Microsoft.Xna.Framework;

namespace TDTestGame {
    class World {

        public Matrix worldMatrix;


        public World(Vector3 originPoint) {

            worldMatrix = Matrix.CreateWorld(
                    originPoint,
                    Vector3.Forward,
                    Vector3.Up
            );
        }
    }
}
