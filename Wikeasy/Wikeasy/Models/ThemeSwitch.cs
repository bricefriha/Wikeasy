using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Wikeasy.Models
{
    public class ThemeSwitch
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public ICommand Command { get; set; }

    }
}