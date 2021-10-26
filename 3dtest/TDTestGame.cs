using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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


        List<Triangle> triangleObjects = new List<Triangle>();

        public static Random random = new Random();


        public TDTestGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            
            // Unlock fps
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            // Set window size
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;

            _graphics.ApplyChanges();



            cam = new Camera(GraphicsDevice.Viewport.AspectRatio);
            world = new World(cam.camTarget);


            // Set up shader
            viewEffect = new BasicEffect(GraphicsDevice);
            viewEffect.Alpha = 1f;
            viewEffect.LightingEnabled = false;      // Maybe false
            viewEffect.VertexColorEnabled = true;


            // Add some triangles!
            for (int i = -3000; i <= 3000; i+=200) {

                for (int j = -3000; j <= 3000; j+=200) {

                    Triangle triangle = new Triangle(random.Next(80, 120) / 100f, GraphicsDevice,
                    new Vector3(i, random.Next(-500, 500), j), random.Next(0, 3));

                    triangleObjects.Add(triangle);
                }
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

                cam.RotateCam(100f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {

                cam.RotateCam(-100f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {

                
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {

                
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


            foreach (Triangle triangle in triangleObjects) {

                triangle.RotateObject(triangle.RotateAxis, random.Next(-1000, 100) *
                    (float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            //cam.camTarget = triangleObjects[random.Next(triangleObjects.Count)].position;


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