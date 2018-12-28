using AIProject.Graph;
using AIProject.MazeGame;
using StateRepresentation;
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

namespace AIProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool ISPRINTDETAILEDSOLUTION = false;
        public static bool ISPRINTSOLUTION = true;
        public MainWindow()
        {
            InitializeComponent();
            MazeState state = new MazeState();
            var dfs = new DeepFirstSearcher(state);
            dfs.Search();
            var bfs = new BreadthFirstSearcher(state);
            bfs.Search();
            var ast = new AStarSearcher(state);
            ast.Search();
        }

        int width, height;
        WriteableBitmap writeableBitmap;

        private void ViewPort_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortContainer.ActualWidth;
            height = (int)this.ViewPortContainer.ActualHeight;
            writeableBitmap = BitmapFactory.New(width,height);
            ViewPort.Source = writeableBitmap;

            CompositionTarget.Rendering +=CompositionTarget_Rendering;
                        
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //writeableBitmap.DrawLine(0,0,width,height,Colors.Red);
            //throw new NotImplementedException();
        }
    }
}
