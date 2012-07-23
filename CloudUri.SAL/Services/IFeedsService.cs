using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IFeedsService
    {
        List<Message> GetMessagesForUser(string username);
        List<Message> GetMessagesForUser(string username, int itemsPerPage, int page, out int pagesTotal);
        List<Message> GetMessagesForUser(string username, string deviceType);
        List<Message> GetMessagesForUser(string username, string deviceType, int itemsPerPage, int page, out int pagesTotal);
    }
}