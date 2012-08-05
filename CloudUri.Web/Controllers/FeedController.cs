using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using CloudUri.DAL.Entities;
using CloudUri.SAL.Services;
using CloudUri.Web.Models;
using CloudUri.Web.Security;
using CloudUri.Web.ViewModels;
using Message = CloudUri.DAL.Entities.Message;

namespace CloudUri.Web.Controllers
{
    [Authorize]
    public class FeedController : SecureController
    {
        private const string All = "All";
        private const int ItemsPerPage = 5;

        private readonly IFeedsService _feedsService;
        private readonly IDevicesService _devicesService;

        public FeedController(IDevicesService devicesService, IFeedsService feedsService)
        {
            _devicesService = devicesService;
            _feedsService = feedsService;
        }

        //
        // GET: /Feed/
        public ViewResult Index(string sendingDevice, string receivingDevice, int page = 1)
        {
            string error;
            List<Device> devicesForUser = _devicesService.GetDevicesForUser(User.Identity.Name,out error);
            int pagesTotal;

            List<Message> messagesForUser =  _feedsService.GetMessagesForUser(User.Identity.Name, sendingDevice == All ? null : sendingDevice, receivingDevice == All ? null : receivingDevice, ItemsPerPage, page, out pagesTotal);

            List<string> devices = new List<string> { All };
            devices.AddRange(devicesForUser.Select(x => x.Name));

            FeedViewModel feedViewModel = new FeedViewModel
                                              {
                                                  Devices = devices,
                                                  Messages = messagesForUser,
                                                  SendingDevice = sendingDevice,
                                                  ReceivingDevice = receivingDevice,
                                                  PaginationModel = new PaginationModel
                                                                        {
                                                                            CurrentPage = page,
                                                                            PagesTotal = pagesTotal
                                                                        }
                                              };

            return View(feedViewModel);
        }
    }
}
