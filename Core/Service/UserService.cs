using Auth1.Authorition;
using Auth1.Core.Entity;
using Auth1.Core.Interface;
using Auth1.Data;
using Auth1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using MiNET.Blocks;
using System.Drawing;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace Auth1.Core.Service
{
    public class UserService : IUserService
    {             
        private readonly AuthDbContext _authDbContext;
        private readonly IJwtUtils _jwtUtils;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole> _roleStore;
        public UserService(
            AuthDbContext authDbContext,
            IJwtUtils jwtUtils ,
            UserManager<ApplicationUser> userManager

        )
        {
            _authDbContext = authDbContext;
            _jwtUtils = jwtUtils;
            this.userManager = userManager;
            this.userManager = userManager;
            
        }
        
        public async Task<int> AddUser(ApplicationUserDTO model)
        {
            var checkus = await userManager.FindByNameAsync(model.UserName);
            if(checkus != null)
            {
                return 1;
            }
            var hasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser();
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;
            user.MidleName = model.MidleName;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.PassportNumber = model.PassportNumber;
            user.PassportSerie = model.PassportSerie;
            user.JShShIR = model.JShShIR;
            user.PhoneNumber = model.MobilePhone;
            user.MobilePhone = model.MobilePhone;
            user.HomePhone = model.HomePhone;
            user.DateOfBirth = model.DateOfBirth;
            user.Region = model.Region;
            user.PasswordHash = hasher.HashPassword(null, model.PasswordHash);
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName;
           
                _authDbContext.ApplicationUser.Add(user);
                await _authDbContext.SaveChangesAsync();
                return 2;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            ApplicationUser user = new ApplicationUser();
            var userr = await userManager.FindByNameAsync(model.Username);
            var chek = await userManager.CheckPasswordAsync(userr, model.Password);
            if (chek)
            {
                user.Id = userr.Id;
                user.FirstName = userr.FirstName;
                user.SecondName = userr.SecondName;
                user.MidleName = userr.MidleName;
                user.PassportNumber = userr.PassportNumber;
                user.PassportSerie = userr.PassportSerie;
                user.JShShIR = userr.JShShIR;
                user.MobilePhone = userr.MobilePhone.ToString();
                user.HomePhone = userr.HomePhone;
                user.DateOfBirth = userr.DateOfBirth;
                user.Gender = userr.Gender;
                user.UserName = userr.UserName;
                var token = _jwtUtils.GenerateJwtToken(user);
                return new AuthenticateResponse(user, token);
            }
            else
            {
                return null;
            }
        }
        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _authDbContext.ApplicationUser.OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<QueryData> GetAll2(TableMetaData metaData)
        {

            QueryData result = new QueryData();
            result.TotalItems = await _authDbContext.ApplicationUser.CountAsync();

            var query = _authDbContext.ApplicationUser.ToList();

            if (metaData.Filters != null)
            {
                var filters = metaData.Filters;
                if (filters.Name != null && filters.Name.Value != null)
                {

                    string str = filters.Name.Value.ToString();
                    string[] count = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    int WordCount = count.Length;

                    if (WordCount == 1)
                    {
                            query = query.Where(a => a.FirstName.ToUpper().Contains(filters.Name.Value.ToUpper()) 
                            || a.SecondName.ToUpper().Contains(filters.Name.Value.ToUpper())
                            || a.MidleName.ToUpper().Contains(filters.Name.Value.ToUpper())).ToList();
                    }
                    if (WordCount == 2)
                    {
                        query = query.Where(a => (a.FirstName + " " + a.SecondName).ToString().ToUpper().
                        Contains(filters.Name.Value.ToUpper())
                        || (a.SecondName + " " + a.FirstName ).ToString().ToUpper().
                        Contains(filters.Name.Value.ToUpper()) 
                        || a.MidleName.ToUpper().Contains(filters.Name.Value.ToUpper())
                        || (a.FirstName + " " + a.MidleName).ToString().ToUpper().
                        Contains(filters.Name.Value.ToUpper())
                        || (a.MidleName + " " + a.FirstName).ToString().ToUpper().
                        Contains(filters.Name.Value.ToUpper())
                        || (a.SecondName + " " + a.MidleName).ToString().ToUpper().
                        Contains(filters.Name.Value.ToUpper())
                        || (a.MidleName + " " + a.SecondName).ToString().ToUpper().
                        Contains(filters.Name.Value.ToUpper())
                        ).ToList();
                    }
                    if(WordCount > 2)
                    {
                        query = query.Where(a => (a.SecondName + " " + a.FirstName + " " + a.MidleName).ToString().ToUpper()
                        .Contains(filters.Name.Value.ToUpper())
                        || (a.SecondName + " " + a.MidleName + " " + a.FirstName).ToString().ToUpper()
                        .Contains(filters.Name.Value.ToUpper())
                        || (a.FirstName + " " + a.SecondName + " " + a.MidleName).ToString().ToUpper()
                        .Contains(filters.Name.Value.ToUpper())
                        || (a.FirstName + " " + a.MidleName + " " + a.SecondName).ToString().ToUpper()
                        .Contains(filters.Name.Value.ToUpper())
                        || (a.MidleName + " " + a.SecondName + " " + a.FirstName).ToString().ToUpper()
                        .Contains(filters.Name.Value.ToUpper())
                        || (a.MidleName + " " + a.FirstName + " " + a.SecondName).ToString().ToUpper()
                        .Contains(filters.Name.Value.ToUpper())
                        ).ToList();
                    }
                }

                if (filters.Gender != null && filters.Gender.Value != null)
                {
                    query = query.Where(a => a.Gender.ToUpper() == filters.Gender.Value.ToUpper()).ToList();
                }

                if (filters.Passport != null && filters.Passport.Value != null)
                {
                    query = query.Where(a => (a.PassportSerie + a.PassportNumber).ToString().ToUpper().Contains(filters.Passport.Value.ToUpper())).ToList();
                }
                if (filters.DateOfBirth != null && filters.DateOfBirth.Value != null)
                {
                    query = query.Where(a => a.DateOfBirth == filters.DateOfBirth.Value).ToList();
                }
                if (filters.Region != null && filters.Region.Value != null)
                {
                    query = query.Where(a => a.Region.ToUpper() == filters.Region.Value.ToUpper()).ToList();
                }
                if (filters.MobilePhone != null && filters.MobilePhone.Value?.Trim() != null)
                {
                    query = query.Where(a => a.MobilePhone == filters.MobilePhone.Value.Trim()).ToList();
                }
            }
            query = query.Skip(metaData.First).Take(metaData.Rows).ToList();

            result.Items = query.OrderByDescending(x=>x.Id).Select(a => new ApplicationUserDTO()
            {
                Id = a.Id,
                DateOfBirth = a.DateOfBirth,
                Email = a.Email,
                FirstName = a.FirstName,
                SecondName = a.SecondName,
                MidleName = a.MidleName,
                PassportSerie = a.PassportSerie,
                PassportNumber = a.PassportNumber,
                Region = a.Region,
                JShShIR = a.JShShIR,
                Gender = a.Gender,
                MobilePhone = a.MobilePhone,
                HomePhone = a.HomePhone,
            }).ToList();

            return result;
        }   
        public async Task<ApplicationUser> GetUserById(int id)
        {
            var userr = await userManager.FindByIdAsync(id.ToString());
            if (userr != null) {
                var us = new ApplicationUser();
                us.Id = userr.Id;
                us.FirstName = userr.FirstName;
                us.SecondName = userr.SecondName;
                us.MidleName = userr.MidleName;
                us.PassportNumber = userr.PassportNumber;
                us.PassportSerie = userr.PassportSerie;
                us.JShShIR = userr.JShShIR;
                us.MobilePhone = userr.MobilePhone;
                us.HomePhone = userr.HomePhone;
                us.DateOfBirth = userr.DateOfBirth;
                us.Gender = userr.Gender;
                us.UserName = userr.UserName;
                return us;
            };
            return new ApplicationUser();
        }
        public async  Task<int> RemoveUser(int id)
        {
            var user = await _authDbContext.ApplicationUser.FindAsync(id);
            if (user == null) { return 1; }
            _authDbContext.ApplicationUser.Remove(user);
            _authDbContext.SaveChanges();
            return 2;
        }
        public async Task<int> UpdateUser(ApplicationUserDTO model, int id)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var user = await _authDbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return 1;
            }
            user.FirstName = model.FirstName;
            user.SecondName = model.SecondName;
            user.MidleName = model.MidleName;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.PassportNumber = model.PassportNumber;
            user.PassportSerie = model.PassportSerie;
            user.JShShIR = model.JShShIR;
            user.PhoneNumber = model.MobilePhone;
            user.MobilePhone = model.MobilePhone;
            user.HomePhone = model.HomePhone;
            user.DateOfBirth = model.DateOfBirth;
            user.Region = model.Region;
            _authDbContext.ApplicationUser.Update(user);
            await _authDbContext.SaveChangesAsync();
            return  2;
        }
    }
}
