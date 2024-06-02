using System;

namespace DTU_Sport_UI.Models
{
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string ClubName { get; set; }
    }
}

