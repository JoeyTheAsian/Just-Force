using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class MapDisplay : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        
        protected override void Initialize()
        {
            
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
        }
    }
}
