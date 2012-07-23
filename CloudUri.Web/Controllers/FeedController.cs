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
        private const int ItemsPerPage = 10;

        private readonly IFeedsService _feedsService;
        private readonly IDevicesService _devicesService;

        public FeedController(IDevicesService devicesService, IFeedsService feedsService)
        {
            _devicesService = devicesService;
            _feedsService = feedsService;
        }

        //
        // GET: /Feed/
        public ActionResult Index(string deviceType, int page = 1)
        {
            List<DeviceType> deviceTypesForUser = _devicesService.GetDeviceTypesForUser(User.Identity.Name);

            int pagesTotal;
            List<Message> messagesForUser = deviceType != All ? _feedsService.GetMessagesForUser(User.Identity.Name, deviceType, ItemsPerPage, page, out pagesTotal)
                : _feedsService.GetMessagesForUser(User.Identity.Name, ItemsPerPage, page, out pagesTotal);

            List<string> deviceTypes = new List<string> { All };
            deviceTypes.AddRange(deviceTypesForUser.Select(x => x.Name));

            FeedViewModel feedViewModel = new FeedViewModel
                                              {
                                                  DeviceTypes = deviceTypes,
                                                  Messages = messagesForUser,
                                                  SelectedDeviceType = deviceType,
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
