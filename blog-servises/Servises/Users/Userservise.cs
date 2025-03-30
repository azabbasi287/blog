using blog_datalayer.Context;
using blog_datalayer.Entities;
using blog_servises.DTOs.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog_servises.Servises.Users
{
    public class Userservise : Iusers
    {
        private readonly BlogContext _context;
        public Userservise(BlogContext context)
        {
            _context = context;
        }
        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isFullnameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (isFullnameExist)
            {
                return OperationResult.Error("نام شما تکراری است!");
            }
            var passwordHash = registerDto.Password.EncodeToMd5();
            _context.Users.Add(new User() {
                FullName = registerDto.FullName,
                Password = passwordHash,
                UserName = registerDto.UserName,
                IsDelete = false,
                CreationDate = DateTime.Now,
                Role = UserRole.User
            });
            _context.SaveChanges();
            return OperationResult.Success();
        }
    }
        
 }

