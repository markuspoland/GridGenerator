using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GridGenerator
{
    class CreateGrid
    {
        Dictionary<string, string> grid = new Dictionary<string, string>();

        public CreateGrid()
        {

        }

        public Dictionary<string, string> NewGrid(List<TextBox> textBoxes)
        {
            

            foreach (TextBox box in textBoxes)
            {
                grid.Add(box.Name, box.Text);
            }

            return grid;
        }
    }
}
