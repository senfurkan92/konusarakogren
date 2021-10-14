using DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.ViewModels
{
    public class SignInUpVM 
    {
        public SignInDto SignInDto { get; set; }

        public SignUpDto SignUpDto { get; set; }
    }
}
