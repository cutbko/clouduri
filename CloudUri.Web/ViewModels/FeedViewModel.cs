using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.Web.ViewModels
{
    public class FeedViewModel
    {
        public Dictionary<int, string> DeviceTypes { get; set; }
        public List<Message> Messages { get; set; } 
    }
}