using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educat.Logic
{
    public class Settings
    {
        static Settings()
        {
            ItemsCount = 5;
        }
        public static int ItemsCount { get; set; }
    }
}
