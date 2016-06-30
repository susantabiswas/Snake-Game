using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();

        public Form1()
        {
            InitializeComponent();

            //For setting default settings
            new Settings();

            //for starting the timer and setting the timer interval for tick
            GameTimer.Interval = 1000 / Settings.Speed;
            GameTimer.Tick += UpdateScreen;
            GameTimer.Start();

            //Start the Game
            StartGame();

        }

        //for every new gameplay session
        private void StartGame()
        {
            status_l.Visible = false;

            //Set the default settings for gameplay
            new Settings();

            //For a new game start the snake body should be cleared
            Snake.Clear();

            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);

            score_l.Text = Settings.Score.ToString();
            CreateFood();


        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            label4.Text = Highscore.GetHighScore().ToString();
            //Check for Game Over
            if (Settings.IsGameOver)
            {
                //Check if Enter is pressed
                if (GameInput.PressedKey(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (GameInput.PressedKey(Keys.Right) && Settings.InGameDirection != Direction.Left)
                    Settings.InGameDirection = Direction.Right;
                else if (GameInput.PressedKey(Keys.Left) && Settings.InGameDirection != Direction.Right)
                    Settings.InGameDirection = Direction.Left;
                else if (GameInput.PressedKey(Keys.Up) && Settings.InGameDirection != Direction.Down)
                    Settings.InGameDirection = Direction.Up;
                else if (GameInput.PressedKey(Keys.Down) && Settings.InGameDirection != Direction.Up)
                    Settings.InGameDirection = Direction.Down;

                MoveSnake();
            }

            canvas.Invalidate();
        }

        private void MoveSnake()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                //Move the head
                if (i == 0)
                {
                    switch (Settings.InGameDirection)
                    {
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                    }

                    //Maximum X ,Y coordinates
                    int MaxX = canvas.Size.Width / Settings.Width;
                    int MaxY = canvas.Size.Height / Settings.Height;

                    //Finding whether there is collision of snake with the screen border
                    if (Snake[i].X >= MaxX || Snake[i].Y >= MaxY || Snake[i].X < 0 || Snake[i].Y < 0)
                    {
                        GameOver();

                    }

                    // Finding whether there is collision of snake with snake's own body
                    for (int k = 1; k < Snake.Count; k++)
                    {
                        if (Snake[i].X == Snake[k].X && Snake[i].Y == Snake[k].Y)
                        {
                            GameOver();
                        }
                    }

                    //Finding whether there is collision of snake with food
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        EatFood();
                    }
                }

                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        //for creating a food object and placing it randomly inside the gamescreen
        private void CreateFood()
        {
            int MaxX = canvas.Size.Width / Settings.Width;
            int MaxY = canvas.Size.Height / Settings.Height;
            Random rand = new Random();

            //creating food object with random coordinates in the canvas
            food = new Circle { X = rand.Next(0, MaxX), Y = rand.Next(0, MaxY) };

        }






        private void GameOver()
        {
            Settings.IsGameOver = true;
        }

        private void EatFood()
        {
            //For adding circle to snake's body on eating the food
            Circle circle = new Circle { X = Snake[Snake.Count - 1].X, Y = Snake[Snake.Count - 1].Y };
            Snake.Add(circle);

            //for updating the score
            Settings.Score = Settings.Score + Settings.Points;
            score_l.Text = Settings.Score.ToString();

            CreateFood();


        }

        //for Drawing snake's body and end game screen message
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics draw = e.Graphics;

            if (!Settings.IsGameOver)
            {
                //Draw snake's body 
                for (int i = 0; i < Snake.Count; i++)
                {
                    Brush SnakeColour;

                    if (i == 0)
                        SnakeColour = Brushes.Black;        //For snake head
                    else
                        SnakeColour = Brushes.Blue;             //For rest of snake's body

                    //For drawing the snake
                    draw.FillEllipse(SnakeColour,
                        new Rectangle(Snake[i].X * Settings.Width, Snake[i].Y * Settings.Height, Settings.Width, Settings.Height));

                    //For drawing the food
                    draw.FillEllipse(Brushes.Red,
                        new Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height));
                }
            }

            else
            {
                string message;
             //   MessageBox.Show(Highscore.SetHighScore(Settings.Score).ToString());
                //if the current score is the new highscore
                if (Highscore.SetHighScore(Settings.Score)==true)
                {
                     message = "Reached High Score !!\nYour Score is: " + Settings.Score + "\n\nPress ENTER Key to try again";
                    
                }
                //if the current score is not the new highscore
                else
                {
                     message = "GAME OVER!!!\nYour Score is: " + Settings.Score + "\n\nPress ENTER Key to try again";
                }
                status_l.Text = message;
                status_l.Visible = true;
            }
        }


        //when a key is pressed then it makes the state of that key in the KeyTable true which implies that an GameInput has been given
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameInput.ChangeState(e.KeyCode, true);
        }

        //when a key is released then it makes the state of that key in the KeyTable false
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameInput.ChangeState(e.KeyCode, false);
        }


        #region Labels used in the Game
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        #endregion

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
