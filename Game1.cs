using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Windows.UI.Xaml.Controls;
using Microsoft.Xna.Framework.Input.Touch;
using System.Threading;
using System;
using Microsoft.Xna.Framework.Audio;
using SharpDX.DirectWrite;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace WolffAstro
{
    public class Game1 : Game
    {
        int zestrzelonemeteory = 0;
        private GraphicsDeviceManager _graphics;
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
        bool ismeteor = false;
        bool isenemy = false;
        bool colision = false;
        bool czywystrzelony = false;
        Pocisk pocisk;
        Texture2D pocisktekstura;
        int pocx;
        int[] xy = { 0, 0, 0, 0 };
        bool pocmet = false;
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
            lock (this)
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
        int meteorpoz = 0;
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
            tekstura = Content.Load<Texture2D>("rocket1");
            gracz = new Rakieta(tekstura);
            control = Content.Load<Texture2D>("control");
            szerokosc = tekstura.Width;
            teksurameteoru = Content.Load<Texture2D>("meteor");
            wrog = new Meteor(teksurameteoru);

            pocisktekstura = Content.Load<Texture2D>("pocisk2D");

            pocisk = new Pocisk(pocisktekstura);
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
                if (gracz.getposition().X + tekstura.Width > xy[0] && gracz.getposition().X < xy[1])
                {
                    if (gracz.getposition().Y + tekstura.Height > xy[2] && gracz.getposition().Y < xy[3])
                    {


                        colision = true;

                        SoundEffect soundEffect = Content.Load<SoundEffect>("wybuch");
                        GraphicsDevice.Clear(Color.CornflowerBlue);


                        SoundEffectInstance soundInstance = soundEffect.CreateInstance();
                        soundInstance.Play();

                    }
                }
                mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed&&colision==false)
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

                    if (mouseX >= (0 + 325) && mouseX <= (0 + 325 + 100) && mouseY >= (583 + 67) && mouseY <= (583 + 67 + 100))
                    {
                        if (czywystrzelony == false)
                        {
                            pocmet = false;
                            czywystrzelony = true;
                            pocx = (int)gracz.getposition().X + (tekstura.Width / 2);
                            pocisk.setpoz(pocx, (int)gracz.getposition().Y + 10);
                        }
                    }

                }





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

                if (keyboardState.IsKeyDown(Keys.W) && colision == false)
                {
                    gracz.MoveU();
                }
                if (keyboardState.IsKeyDown(Keys.S) && colision == false)
                {
                    gracz.MoveD();
                }
                if (keyboardState.IsKeyDown(Keys.A) && colision == false)
                {
                    gracz.MoveL();
                }
                if (keyboardState.IsKeyDown(Keys.D) && colision == false)
                {
                    gracz.MoveR();
                }
                if (!pocmet && czywystrzelony && colision == false)
                {
                    if (pocisk.getposition().X >= wrog.getposition().X && pocisk.getposition().X + pocisktekstura.Width <= wrog.getposition().X + teksurameteoru.Width &&
                        pocisk.getposition().Y >= wrog.getposition().Y && pocisk.getposition().Y + pocisktekstura.Height <= wrog.getposition().Y + teksurameteoru.Height)
                    {

                        isenemy = false;
                        wrog.changeposition(meteorpoz);
                        ismeteor = false;
                        pocmet = true;
                        czywystrzelony = false;
                        zestrzelonemeteory++;
                    }
                }
                Draw(gameTime);
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteBatch.Begin();
            spriteBatch.Draw(tlo, Vector2.Zero, Color.White);
            if (czywystrzelony)
            {
                pocisk.MoveU();
                spriteBatch.Draw(pocisktekstura, pocisk.getposition(), Color.White);
            }
            if (pocisk.getposition().Y < 0)
            {
                czywystrzelony = false;
            }

            if (ismeteor)
            {
                Vector2 pozycjameteoru = new Vector2((int)wrog.getposition().X, (int)wrog.getposition().Y);

                spriteBatch.Draw(teksurameteoru, pozycjameteoru, klatkawroga, Color.White);

            }
            GraphicsDevice.Clear(Color.CornflowerBlue);


            base.Draw(gameTime);

            Vector2 pozycjapoczatkowa = new Vector2((int)gracz.getposition().X, (int)gracz.getposition().Y);
            spriteBatch.Draw(animrocket, pozycjapoczatkowa, klatka, Color.White);


            spriteBatch.Draw(control, new Vector2(0, 583), Color.White);
            if (colision)
            {
                SpriteFont font = Content.Load<SpriteFont>("Arial");
                spriteBatch.DrawString(font, "PRZEGRALES", new Vector2(150, 300), Color.Red);
            }
            SpriteFont font2= Content.Load<SpriteFont>("Arial2");
            spriteBatch.DrawString(font2, zestrzelonemeteory.ToString(), new Vector2(420,760), Color.White);
            spriteBatch.End();
        }

    }
}
