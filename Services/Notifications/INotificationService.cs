namespace DTU_Sport_UI.Services
{
    using DTU_Sport_UI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationService
    {
        Task<List<NotificationDto>> GetUnreadNotificationsAsync();
    }
}

