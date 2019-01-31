using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SemiTransparentImgDisplay.Model
{
    public class GridViewColumnModel
    {
        public double Width { get; set; }

        public object Header { get; set; }

        public GridViewColumnModel(GridViewColumn col)
        {
            Width = col.ActualWidth;
            Header = col.Header;
        }

        public GridViewColumnModel(double width, object header)
        {
            Width = width;
            Header = header;
        }

        public GridViewColumnModel()
        {
            
        }
    }
}
