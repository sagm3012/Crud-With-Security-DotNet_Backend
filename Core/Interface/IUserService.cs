using Auth1.Core.Entity;
using Auth1.Models;

namespace Auth1.Core.Interface
{
    public interface IUserService
    {
        Task<AuthenticateResponse>? Authenticate(AuthenticateRequest user3);   
        Task<List<ApplicationUser>> GetAll();
        Task<int> AddUser(ApplicationUserDTO model);
        Task<int> UpdateUser(ApplicationUserDTO model, int id);
        Task<int> RemoveUser(int id);
        Task<ApplicationUser> GetUserById(int id); //user
        Task<QueryData> GetAll2(TableMetaData metaData);
    }
}
