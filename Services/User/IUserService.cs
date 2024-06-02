using DTU_Sport_UI.Models;
using System.Threading.Tasks;


namespace DTU_Sport_UI.Services
{
    public interface IUserService
    {
    Task<bool> UpdateBioAsync(BioModel bioModel);
    Task<string> GetBioAsync();
    Task<List<UserDto>> GetAllUsersAsync();
    Task<bool> AssignRoleAsync(AssignRoleModel model);
    Task<bool> DeleteUserAsync(string username);
    Task<bool> SignUpAsync(SignUpModel model);
    }
}
