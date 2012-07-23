using System;
using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services.Stubs
{
    public class FeedsServiceStub : IFeedsService
    {
        public List<Message> GetMessagesForUser(string username)
        {
            return new List<Message>
                       {
                           new Message
                               {
                                   Key = 1,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                          Name = "Iphonechik"
                                                      },
                                   ToId = null,
                                   CreatedOn = DateTime.Now
                               },
                           new Message
                               {
                                   Key = 2,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                          Name = "Iphonechik"
                                                      },
                                   ToId = 2,
                                   RecieverDevice = new Device
                                                        {
                                                            Name = "IPeduk"
                                                        },
                                   CreatedOn = DateTime.Now.AddDays(-1)
                               }
                       };
        }

        public List<Message> GetMessagesForUser(string username, int itemsPerPage, int page, out int pagesTotal)
        {
            pagesTotal = 10;
            return new List<Message>
                       {
                           new Message
                               {
                                   Key = 1,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                        Name  = "Iphonechik"
                                                      },
                                   ToId = null,
                                   CreatedOn = DateTime.Now
                               },
                               new Message
                               {
                                   Key = 2,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                        Name  = "Iphonechik"
                                                      },
                                   ToId = 2,
                                   RecieverDevice = new Device
                                                        {
                                                            Name = "IPeduk"
                                                        },
                                   CreatedOn = DateTime.Now.AddDays(-1)
                               }
                       };
        }

        public List<Message> GetMessagesForUser(string username, string deviceType)
        {
            if (deviceType != "Windows Phone 7")
                return new List<Message>();

            return new List<Message>
                       {
                           new Message
                               {
                                   Key = 1,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                          Name = "Iphonechik"
                                                      },
                                   ToId = null,
                                   CreatedOn = DateTime.Now
                               },
                           new Message
                               {
                                   Key = 2,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                          Name = "Iphonechik"
                                                      },
                                   ToId = 2,
                                   RecieverDevice = new Device
                                                        {
                                                            Name = "IPeduk"
                                                        },
                                   CreatedOn = DateTime.Now.AddDays(-1)
                               }
                       };
        }

        public List<Message> GetMessagesForUser(string username, string deviceType, int itemsPerPage, int page,
                                                out int pagesTotal)
        {
            pagesTotal = 10;
            return new List<Message>
                       {
                           new Message
                               {
                                   Key = 1,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                          Name = "Iphonechik"
                                                      },
                                   ToId = null,
                                   CreatedOn = DateTime.Now
                               },
                           new Message
                               {
                                   Key = 2,
                                   MessageText = "http://jopatigra.com/",
                                   FromId = 1,
                                   SenderDevice = new Device
                                                      {
                                                          Name = "Iphonechik"
                                                      },
                                   ToId = 2,
                                   RecieverDevice = new Device
                                                        {
                                                            Name = "IPeduk"
                                                        },
                                   CreatedOn = DateTime.Now.AddDays(-1)
                               }
                       };
        }
    }
}