using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TDTestGame {
    public class TDTestGame : Game {
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        // Used for 3d rendering
        Camera cam;
        World world;


        BasicEffect viewEffect; // Viewport for world


        // This is a triangle
         // Position and colour of vertices that make up a triangle
                    // Vertices are stored


        Triangle triangle;

        public int totalVertices;


        List<Triangle> triangleObjects = new List<Triangle>();


        public TDTestGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {

            cam = new Camera(GraphicsDevice.Viewport.AspectRatio);
            world = new World(cam.camTarget);


            // Set up shader
            viewEffect = new BasicEffect(GraphicsDevice);
            viewEffect.Alpha = 1f;
            viewEffect.LightingEnabled = false;      // Maybe false
            viewEffect.VertexColorEnabled = true;


            triangle = new Triangle(1f, GraphicsDevice, new Vector3(10f, 0f, 0f));
            Triangle triangle2 = new Triangle(1f, GraphicsDevice, new Vector3(-100f, 0f, 0f));
            
            triangleObjects.Add(triangle);
            triangleObjects.Add(triangle2);


            foreach (Triangle triangle in triangleObjects) {

                totalVertices += triangle.vertices;
            }


            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // Move camera checks
            if(Keyboard.GetState().IsKeyDown(Keys.Left)) {

                triangleObjects[1].MoveObject(new Vector3(-1f, 0f, 0f));

                //cam.MoveCam(new Vector3(-1f, 0f, 0f));
                //cam.MoveTarget(new Vector3(-1f, 0f, 0f));
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {

                triangleObjects[1].MoveObject(new Vector3(1f, 0f, 0f));

                //cam.MoveCam(new Vector3(1f, 0f, 0f));
                //cam.MoveTarget(new Vector3(1f, 0f, 0f));
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {

                triangleObjects[0].RotateObject(1, 1f);
                triangleObjects[1].RotateObject(0, 1f);

                //cam.MoveCam(new Vector3(0f, -1f, 0f));
                //cam.MoveTarget(new Vector3(0f, -1f, 0f));
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {

                triangleObjects[0].RotateObject(1, -1f);
                triangleObjects[1].RotateObject(0, -1f);

                //cam.MoveCam(new Vector3(0f, 1f, 0f));
                //cam.MoveTarget(new Vector3(0f, 1f, 0f));
            }


            // Zoom camera checks
            if (Keyboard.GetState().IsKeyDown(Keys.OemPlus)) {

                cam.Zoom(new Vector3(0f, 0f, 1f));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.OemMinus)) {

                cam.Zoom(new Vector3(0f, 0f, -1f));
            }


            // Toggle orbit & orbit
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {

                cam.ToggleOrbit();
            }

            if (cam.orbit) {

                cam.Orbit();
            }


            // Render/handle camera actually
            cam.CreateLookAt();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {

            viewEffect.Projection = cam.projectionMatrix;
            viewEffect.View = cam.viewMatrix;
            viewEffect.World = world.worldMatrix;
            

            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            // Don't draw faces that can't be seen
            RasterizerState rasterizer = new RasterizerState();
            rasterizer.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizer;


            foreach (Triangle triangle in triangleObjects) {

                GraphicsDevice.SetVertexBuffer(triangle.buffer);

                foreach (EffectPass pass in viewEffect.CurrentTechnique.Passes) {

                    pass.Apply();
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, triangle.vertices - 1);
                }
            }


            base.Draw(gameTime);
        }
    }
}