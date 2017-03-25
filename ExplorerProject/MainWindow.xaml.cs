using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Forms;

namespace ExplorerProject
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			PopulateTreeView();

			
		}

		public void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.DataContext = this;
		}

		private void PopulateTreeView()
		{
			var folderProvider = new FolderProvider();

			var folders = folderProvider.GetFolders(PathProperty);

			this.Loaded += MainWindow_Loaded;
			treeView.DataContext = folders;
		}

		public string PathProperty
		{
			get { return (string)GetValue(PathPropertyProperty); }
			set { SetValue(PathPropertyProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PathProperty.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PathPropertyProperty =
			DependencyProperty.Register("PathProperty", typeof(string), typeof(MainWindow), new PropertyMetadata(AppDomain.CurrentDomain.BaseDirectory));

		private void btnFileChoice_Click(object sender, RoutedEventArgs e)
		{
			ChooseFolder();
		}

		public void ChooseFolder()
		{

			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();

				if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					PathProperty = fbd.SelectedPath;
					PopulateTreeView();
					//string[] files = Directory.GetFiles(fbd.SelectedPath);
					//System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
				}
			}
		}
	}
}
