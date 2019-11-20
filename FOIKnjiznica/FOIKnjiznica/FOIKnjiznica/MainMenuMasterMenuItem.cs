using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOIKnjiznica
{

    public class MainMenuMasterMenuItem
    {
        public MainMenuMasterMenuItem()
        {
            TargetType = typeof(MainMenuMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}