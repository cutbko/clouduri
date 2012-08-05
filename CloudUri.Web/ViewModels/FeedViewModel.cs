using System.Collections.Generic;
using CloudUri.DAL.Entities;
using CloudUri.Web.Models;

namespace CloudUri.Web.ViewModels
{
    public class FeedViewModel
    {
        public List<string> Devices { get; set; }

        public string SendingDevice { get; set; }

        public string ReceivingDevice { get; set; }

        public List<Message> Messages { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}