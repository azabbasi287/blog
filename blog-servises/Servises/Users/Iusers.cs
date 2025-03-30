using blog_servises.DTOs.Users;
using CodeYad_Blog.CoreLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog_servises.Servises.Users
{
    public interface Iusers
    {
        OperationResult RegisterUser(UserRegisterDto registerDto);
    }
}

