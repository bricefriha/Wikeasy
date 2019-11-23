using System;

using Wikeasy.Models;

namespace Wikeasy.ViewModels
{
    public class PreviewViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public PreviewViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
