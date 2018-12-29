using AIProject.Graph;
using AIProject.MazeGame;
using StateRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AIProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool IS_PRINT_DETAILED_SOLUTION = false;
        public static bool IS_PRINT_SOLUTION = true;
        public static int TIME_BETWEEN_ITERATIONS = 160;

        /// <summary>
        /// 0=DFS, 1=BFS, 2=A star
        /// </summary>
        public static int ALGORITHM_CHOOSER = 2;

        public MainWindow()
        {
            InitializeComponent();
            //MazeState state = new MazeState();
            //var dfs = new DeepFirstSearcher(state);
            //dfs.Search();
            //var bfs = new BreadthFirstSearcher(state);
            //bfs.Search();
            //var ast = new AStarSearcher(state);
            //ast.Search();
        }

        int width, height;
        WriteableBitmap writeableBitmap;

        private static int SIZE_OF_RECTANGLES = 20;
        public static List<Node> opens = new List<Node>();
        public static List<Node> closeds = new List<Node>();

        public static Node SearchIsReady = new Node(null);

        private void ViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortContainer.ActualWidth;
            height = (int)this.ViewPortContainer.ActualHeight;
            writeableBitmap = BitmapFactory.New(width, height);
            ViewPort.Source = writeableBitmap;
            PrintTheMap();



            CompositionTarget.Rendering += CompositionTarget_Rendering;
            MazeState state = new MazeState();
            if (ALGORITHM_CHOOSER == 0)
            {
                var dfs = new DeepFirstSearcher(state);
                new Thread(() => dfs.Search()).Start();
            }
            if (ALGORITHM_CHOOSER == 1)
            {
                var bfs = new BreadthFirstSearcher(state);
                new Thread(() => bfs.Search()).Start();
            }
            if (ALGORITHM_CHOOSER == 2)
            {
                var ast = new AStarSearcher(state);
                new Thread(() => ast.Search()).Start();
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {



            for (int i = 0; i < opens.Count; i++)
            {
                var color = Colors.Yellow;

                writeableBitmap.FillEllipseCentered((opens[i].CurrentState as MazeState).PosX * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES / 2, (opens[i].CurrentState as MazeState).PosY * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES / 2, SIZE_OF_RECTANGLES / 2, SIZE_OF_RECTANGLES / 2, color);

            }

            for (int i = 0; i < closeds.Count; i++)
            {
                var color = Colors.Orange;

                writeableBitmap.FillEllipseCentered((closeds[i].CurrentState as MazeState).PosX * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES / 2, (closeds[i].CurrentState as MazeState).PosY * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES / 2, SIZE_OF_RECTANGLES / 2, SIZE_OF_RECTANGLES / 2, color);

            }


            while (SearchIsReady!=null && SearchIsReady.CurrentState != null && IS_PRINT_SOLUTION)
            {
                opens.Clear();
                closeds.Clear();
                var color = Colors.DarkGreen;
                writeableBitmap.FillEllipseCentered((SearchIsReady.CurrentState as MazeState).PosX * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES / 2, (SearchIsReady.CurrentState as MazeState).PosY * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES / 2, SIZE_OF_RECTANGLES / 2, SIZE_OF_RECTANGLES / 2, color);

                SearchIsReady = SearchIsReady.Parent;
            }

        }

        private void PrintTheMap()
        {
            for (int y = 0; y < Map.CurrentMap.GetLength(0); y++)
            {
                for (int x = 0; x < Map.CurrentMap.GetLength(1); x++)
                {
                    var color = Colors.White;
                    switch (Map.CurrentMap[y, x])
                    {
                        case 0:
                            color = Colors.Red;
                            break;
                        case 1:
                            color = Colors.LightGray;
                            break;
                        case 2:
                            color = Colors.Green;
                            break;
                        case 3:
                            color = Colors.Blue;
                            break;
                    }
                    writeableBitmap.FillRectangle(x * SIZE_OF_RECTANGLES, y * SIZE_OF_RECTANGLES, x * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES, y * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES, color);
                    writeableBitmap.DrawRectangle(x * SIZE_OF_RECTANGLES, y * SIZE_OF_RECTANGLES, x * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES, y * SIZE_OF_RECTANGLES + SIZE_OF_RECTANGLES, Colors.Black);

                }
            }
        }
    }
}
