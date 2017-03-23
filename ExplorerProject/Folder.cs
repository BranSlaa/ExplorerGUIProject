using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace ExplorerProject
{
	public class Folder : INotifyPropertyChanged
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public bool? IsChecked { get; set; }
		public string picture { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		void NotifiyPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
	}

	public class File : Folder
	{
		public int Size { get; set; }
	}

	public class DirectoryFolder : Folder
	{
		public List<Folder> Folders { get; set; }

		public DirectoryFolder()
		{
			Folders = new List<Folder>();
		}
	}

	public class FolderProvider
	{
		public List<Folder> GetFolders(string path)
		{
			var items = new List<Folder>();

			var dirInfo = new DirectoryInfo(path);



			foreach (var directory in dirInfo.GetDirectories())
			{

				var folder = new DirectoryFolder
				{
					Name = directory.Name,
					Path = directory.FullName,
					IsChecked = false,
					picture = "folder.png",
				Folders = GetFolders(directory.FullName)
				};

				items.Add(folder);
			}

			foreach (var file in dirInfo.GetFiles())
			{
				var item = new File
				{
					Name = file.Name,
					Path = file.FullName,
					IsChecked = false
					
				};

				items.Add(item);
			}



			return items;
		}
	}
}
