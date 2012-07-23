using System.Collections.Generic;
using CloudUri.DAL.Entities;
using CloudUri.Web.Models;

namespace CloudUri.Web.ViewModels
{
    public class FeedViewModel
    {
        public List<string> DeviceTypes { get; set; }

        public string SelectedDeviceType { get; set; }

        public List<Message> Messages { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}