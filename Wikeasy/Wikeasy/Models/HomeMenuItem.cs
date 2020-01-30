using System;
using System.Collections.Generic;
using System.Text;

namespace Wikeasy.Models
{
    public enum MenuItemType
    {
        Home,
        Settings,
        About,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        //public string FontFamily { get; set; }
        public string Icon { get; set; }
    }
}
