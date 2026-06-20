using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace phoenix_prototype
{

    class WindowMetadata
    {
        public string windowsName {  get; set; }
        public double top {  get; set; }
        public double left { get; set; }
        public double height { get; set; }
        public double width { get; set; }


        public WindowMetadata(string windowsName, double top, double left, double height, double width) {
            this.windowsName = windowsName;
            this.top = top;
            this.left = left;
            this.height = height;
            this.width = width;
        }
    }

    internal class ViewManager
    {

        public static bool ExportLayoutToFile(List<Window> windowsList, string filePath)
        {
            List<WindowMetadata> windowMetadataList = new();
            if (windowsList.Count != 0)
            {
                foreach (Window win in windowsList)
                {
                    windowMetadataList.Add(new WindowMetadata(win.Name, win.Top, win.Left, win.Height, win.Width));
                }
            }

            //TODO: Save the list (json format) on a file

            return false;
        }


        public static List<WindowMetadata> ImportLayoutFromFile(string filePath)
        {
            return null;
        }

    }


}
