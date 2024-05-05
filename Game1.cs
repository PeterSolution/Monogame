using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Windows.UI.Xaml.Controls;
using Microsoft.Xna.Framework.Input.Touch;
using System.Threading;
using System;
using Microsoft.Xna.Framework.Audio;

namespace WolffAstro
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tekstura;
        Texture2D tlo;
        Rakieta gracz;
        Texture2D control;
        int pomocint = 0;
        Texture2D animrocket;
        int szerokosc;
        Rectangle klatka;
        Rectangle klatkawroga;
        Meteor wrog;
        Texture2D teksurameteoru;
        int meteortyp = 0;
        int savemeteortyp;
        bool ismeteor=false;
        bool isenemy=false;
        bool colision=false;
        bool pause=false;
        bool czywystrzelony=false;
        int[] xy = { 0, 0, 0, 0 };
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = 480; 
            _graphics.PreferredBackBufferHeight = 800; 

            _graphics.ApplyChanges(); 


            Mouse.WindowHandle = Window.Handle;

            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag;
            base.Initialize();
        }
        void execute(object state)
        {
            lock (this)
            {
                if (pomocint >= 5)
                {
                    pomocint = 0;

                }
                else
                {
                    pomocint++;
                }
                Thread.Sleep(500);
            }

        }
        void meteorexecute(object state)
        {
            lock(this)
            {
                if (meteortyp >= 2)
                {
                    meteortyp = 0;
                }
                else
                {
                    meteortyp++;
                }
            }
        }
        int meteorpoz=0;
        void meteorposition(object state)
        {
            lock (this)
            {
                Random r = new Random();
                meteorpoz = r.Next(10, 400);
            }
        }
        protected override void LoadContent()
        {
            tlo = Content.Load<Texture2D>("niebo");
            animrocket = Content.Load<Texture2D>("AnimRakiety");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tekstura = Content.Load<Texture2D>("rocket1");
            gracz = new Rakieta(tekstura);
            control = Content.Load<Texture2D>("control");
            szerokosc = tekstura.Width;
            teksurameteoru = Content.Load<Texture2D>("meteor");
            wrog = new Meteor(teksurameteoru);
            // TODO: use this.Content to load your game content here
        }
        MouseState mouseState;
        protected override void Update(GameTime gameTime)
        {

            Timer tt = new Timer(execute, null, 0, 500);
            Timer tt2 = new Timer(meteorexecute, null, 0, 1500);
            Timer tt3 = new Timer(meteorposition, null, 0, 1000);
            if (!colision)
            {
                if (isenemy == true)
                {
                    wrog.MoveD();

                    xy[0] = (int)wrog.getposition().X;
                    xy[1] = (int)wrog.getposition().X + 113;
                    xy[2] = (int)wrog.getposition().Y;
                    xy[3] = (int)wrog.getposition().Y + animrocket.Height;
                    if (wrog.getposition().Y > 700)
                    {
                        isenemy = false;
                        wrog.changeposition(meteorpoz);
                    }
                }
                else
                {
                    if (isenemy == false)
                    {
                        ismeteor = true;
                        isenemy = true;
                        savemeteortyp = meteortyp;
                        //127,113
                        if (savemeteortyp == 0)
                        {
                            klatkawroga = new Rectangle(0, 0, 127, teksurameteoru.Height);

                            xy[0] = (int)wrog.getposition().X;
                            xy[1] = (int)wrog.getposition().X + 127;
                            xy[2] = (int)wrog.getposition().Y;
                            xy[3] = (int)wrog.getposition().Y + animrocket.Height;
                        }
                        else
                        {
                            if (savemeteortyp == 1)
                            {
                                klatkawroga = new Rectangle(127, 0, 113, teksurameteoru.Height);

                                xy[0] = (int)wrog.getposition().X;
                                xy[1] = (int)wrog.getposition().X + 113;
                                xy[2] = (int)wrog.getposition().Y;
                                xy[3] = (int)wrog.getposition().Y + animrocket.Height;
                            }
                            else
                            {
                                klatkawroga = new Rectangle((teksurameteoru.Width / 3) * meteortyp, 0, animrocket.Width / 3, animrocket.Height);

                                xy[0] = (int)wrog.getposition().X;
                                xy[1] = (int)wrog.getposition().X + (animrocket.Width / 3);
                                xy[2] = (int)wrog.getposition().Y;
                                xy[3] = (int)wrog.getposition().Y + animrocket.Height;
                            }
                        }


                    }

                }
                if (gracz.getposition().X+tekstura.Width > xy[0] && gracz.getposition().X < xy[1])
                {
                    if (gracz.getposition().Y+tekstura.Height > xy[2] && gracz.getposition().Y < xy[3])
                    {
                        SoundEffect soundEffect = Content.Load<SoundEffect>("wybuch");

                        SoundEffectInstance soundInstance = soundEffect.CreateInstance();
                        soundInstance.Play();
                        colision = true;

                    }
                }
                mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    int mouseX = mouseState.X;
                    int mouseY = mouseState.Y;
                    if (mouseX >= 85 && mouseX <= (85 + 53) && mouseY >= (583 + 25) && mouseY <= (583 + 25 + 73))
                    {
                        gracz.MoveU();
                    }
                    if (mouseX >= 85 && mouseX <= (85 + 53) && mouseY >= (583 + 25 + 33 + 73) && mouseY <= (583 + 25 + 73 + 33 + 73))
                    {
                        gracz.MoveD();
                    }
                    if (mouseX >= (0 + 23) && mouseX <= (0 + 23 + 73) && mouseY >= (583 + 80) && mouseY <= (583 + 80 + 53d))
                    {
                        gracz.MoveL();
                    }
                    if (mouseX >= (0 + 23 + 73 + 33) && mouseX <= (0 + 23 + 73 + 73 + 33) && mouseY >= (583 + 80) && mouseY <= (583 + 80 + 53d))
                    {
                        gracz.MoveR();
                    }

                    //button
                    if (mouseX >= (0 + 325) && mouseX <= (0 + 325 + 100) && mouseY >= (583 + 67) && mouseY <= (583 + 67 + 100))
                    {
                        if (czywystrzelony == false)
                        {
                            czywystrzelony=true;

                        }
                    }

                }
                //325-100 67-100





                klatka = new Rectangle((animrocket.Width / 6) * pomocint, 0, animrocket.Width / 6, animrocket.Height);


                TouchCollection touchCollection = TouchPanel.GetState();

                foreach (TouchLocation touchLocation in touchCollection)
                {
                    if (touchLocation.State == TouchLocationState.Pressed)
                    {
                        Vector2 touchPosition = touchLocation.Position;

                        Rectangle controlArea = new Rectangle(0, 583, control.Width, control.Height);

                        if (controlArea.Contains((int)touchPosition.X, (int)touchPosition.Y))
                        {
                            gracz.MoveU();
                        }
                    }
                }


                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.W))
                {
                    gracz.MoveU();
                }
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    gracz.MoveD();
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    gracz.MoveL();
                }
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    gracz.MoveR();
                }

                Draw(gameTime);
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!pause)
            {
                SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

                spriteBatch.Begin();
                spriteBatch.Draw(tlo,Vector2.Zero,Color.White);
                if (ismeteor)
                {
                    Vector2 pozycjameteoru = new Vector2((int)wrog.getposition().X, (int)wrog.getposition().Y);

                    spriteBatch.Draw(teksurameteoru, pozycjameteoru, klatkawroga, Color.White);

                }
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here

                base.Draw(gameTime);

                Vector2 pozycjapoczatkowa = new Vector2((int)gracz.getposition().X, (int)gracz.getposition().Y);
                spriteBatch.Draw(animrocket, pozycjapoczatkowa, klatka, Color.White);


                spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
                spriteBatch.End();
            }
        }
        
    }
}
