using System.Collections.Generic;
using CloudUri.DAL.Entities;

namespace CloudUri.SAL.Services
{
    public interface IFeedsService
    {
        List<Message> GetMessagesForUser(string username);
    }
}