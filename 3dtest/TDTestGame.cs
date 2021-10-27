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


        List<Pyramid> triangleObjects = new List<Pyramid>();
        List<Plane> planeObjects = new List<Plane>();

        public static Random random = new Random();

        float rotSpeed = -200f;

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
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();



            cam = new Camera(this, new Vector3(0f, 0f, 500f), new Vector3(0f, 0f, 0f));
            world = new World(cam.camTarget);


            // Set up shader
            viewEffect = new BasicEffect(GraphicsDevice);
            viewEffect.Alpha = 1f;
            viewEffect.LightingEnabled = false;      // Maybe false
            viewEffect.VertexColorEnabled = true;


            // Add some triangles!
            for (int i = -3000; i <= 3000; i+=200) {

                for (int j = -3000; j <= 3000; j+=200) {

                    Pyramid triangle = new Pyramid(random.Next(80, 120) / 100f, GraphicsDevice,
                    new Vector3(i, random.Next(200, 1200), j), random.Next(0, 3));

                    triangleObjects.Add(triangle);
                }
            }

            Plane ground = new Plane(100f, GraphicsDevice, new Vector3(0f, -20f, 0f), 0);
            planeObjects.Add(ground);


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

                cam.RotateCamHorizontal(100f * (float)gameTime.ElapsedGameTime.TotalSeconds);

                //cam.camRot.Y += 2.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //cam.UpdateLookAt();
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right)) {

                cam.RotateCamHorizontal(-100f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {

                cam.RotateCameraVertical(100f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {

                cam.RotateCameraVertical(-100f * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }


            if(Keyboard.GetState().IsKeyDown(Keys.W)) {

                cam.MoveCam(Vector3.Normalize(cam.camTarget - cam.camPos) * Constants.camMoveSpeed *
                    (float) gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S)) {

                cam.MoveCam(Vector3.Normalize(cam.camPos - cam.camTarget) * Constants.camMoveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A)) {

                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(90f));
                Vector3 tmpVec = Vector3.Transform(cam.camTarget - cam.camPos, rotationMatrix) + cam.camPos;
                tmpVec.Y *= 0;

                cam.MoveCam(Vector3.Normalize(tmpVec - cam.camPos) * Constants.camMoveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D)) {

                Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(-90f));
                Vector3 tmpVec = Vector3.Transform(cam.camTarget - cam.camPos, rotationMatrix) + cam.camPos;
                tmpVec.Y *= 0;

                cam.MoveCam(Vector3.Normalize(tmpVec - cam.camPos) * Constants.camMoveSpeed *
                    (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            // Toggle orbit & orbit

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {

                cam.ToggleOrbit();
            }

            if (cam.orbit) {

                cam.Orbit();
            }


            if(Keyboard.GetState().IsKeyDown(Keys.F)) {

                _graphics.ToggleFullScreen();
                IsMouseVisible = !IsMouseVisible;
            }


            // Render/handle camera actually
            cam.CreateLookAt();


            foreach (Pyramid triangle in triangleObjects) {

                triangle.RotateObject(triangle.RotateAxis,
                    rotSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            //rotSpeed += 10f * (float) gameTime.ElapsedGameTime.TotalSeconds;

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


            foreach (Pyramid triangle in triangleObjects) {

                GraphicsDevice.SetVertexBuffer(triangle.buffer);

                foreach (EffectPass pass in viewEffect.CurrentTechnique.Passes) {

                    pass.Apply();
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, triangle.vertices - 1);
                }
            }


            foreach (Plane plane in planeObjects) {

                GraphicsDevice.SetVertexBuffer(plane.buffer);

                foreach (EffectPass pass in viewEffect.CurrentTechnique.Passes) {

                    pass.Apply();
                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, plane.vertices - 1);
                }
            }


            base.Draw(gameTime);
        }
    }
}