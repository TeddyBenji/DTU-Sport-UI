using DTU_Sport_UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DTU_Sport_UI.Services
{
    public interface IClubService
    {
        Task<List<ClubDto>> GetAllClubsAsync();
        Task<bool> RegisterMemberAsync(string clubName, string username);

        Task<ClubModel> GetClubDetailsAsync(string clubName);

        Task<bool> CreateEventAsync(EventModel eventModel);

        Task<bool> DeleteClubAsync(string clubName);

        Task<bool> CreateClubAsync(ClubModel newClub);
    }
}

