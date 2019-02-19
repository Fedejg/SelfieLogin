using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SelfieLogin
{
    class Usuario
    {
        public string User {get; set; }
        public string Pass { get; set; }
        public string Alias { get; set; }
        public List<Image> ListTrainedImage = new List<Image>();
    }
}
