using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SemiTransparentImgDisplay.Model
{
    public class GridViewColumnCollectionModel : List<GridViewColumnModel>
    {
        public GridViewColumnCollectionModel(GridViewColumnCollection columns)
        {
            foreach (var column in columns)
            {
                base.Add(new GridViewColumnModel(column));
            }
        }

        public GridViewColumnCollectionModel() 
        {
            
        }
    }
}
