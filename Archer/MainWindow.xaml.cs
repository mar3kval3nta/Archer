using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Archer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Unit movingPlayer;
        private Unit player1;
        private Unit player2;

        public MainWindow()
        {
            InitializeComponent();

            player1 = new Unit(500, 20);
            player1.HitBox = new Rectangle();
            player1.HitBox.Width = 110;
            player1.HitBox.Height = 160;

            player2 = new Unit(500, 20);
            player2.HitBox = new Rectangle() { Width = 110, Height = 160 };

            Random random = new Random();
            int value = random.Next(0, 2);
            if (value == 0)
            {
                movingPlayer = player1;
            }
            else
            {
                movingPlayer = player2;
            }

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawGame();
        }

        private void DrawGame()
        {
            canvas.Children.Clear();
            DrawTerrain();
            DrawPlayer(player1, true);
            DrawPlayer(player2, false);
        }

        private void DrawPlayer(Unit player, bool left)
        {
            Image image = new Image();
            image.Height = player.HitBox.Height;
            image.Width = player.HitBox.Width;
            image.Stretch = Stretch.Fill;
            image.Source = new BitmapImage(new Uri("Img/archer.png", UriKind.Relative));

            Polygon? polygon = null;
            if (movingPlayer == player)
            {
                polygon = new Polygon();
                polygon.Points.Add(new Point(0, 0));
                polygon.Points.Add(new Point(20, 0));
                polygon.Points.Add(new Point(10, 30));
                polygon.Fill = Brushes.Red; 
            }

            if (polygon != null)
            {
                Canvas.SetBottom(polygon, canvas.ActualHeight * 0.05 + image.Height + 20);
            }
            Canvas.SetBottom(image, canvas.ActualHeight * 0.05);
            if (left)
            {
                if (polygon != null)
                {
                    Canvas.SetLeft(polygon, image.Width / 2);
                }
                Canvas.SetLeft(image, 20);
            }
            else
            {
                if (polygon != null)
                {
                    Canvas.SetRight(polygon, image.Width / 2);
                }
                Canvas.SetRight(image, 20);
                image.LayoutTransform = new ScaleTransform(-1, 1);
            }

            canvas.Children.Add(image);
            if (polygon != null)
            {
                canvas.Children.Add(polygon);
            }

        }

        private void DrawTerrain()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = canvas.ActualWidth;
            rectangle.Height = canvas.ActualHeight * 0.1;
            rectangle.Fill = Brushes.Green;

            Canvas.SetLeft(rectangle, 0);
            Canvas.SetBottom(rectangle, 0);

            canvas.Children.Add(rectangle);
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (movingPlayer == player1)
            {
                movingPlayer = player2;
            }
            else
            {
                movingPlayer = player1;
            }
            DrawGame();
        }
    }
}
