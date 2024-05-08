using DTU_Sport_UI.Models;

namespace DTU_Sport_UI.Services
{
    public interface IUserService
{
        Task<List<BioModel>> UpdateBioAsync(BioModel bioModel);
}
}
