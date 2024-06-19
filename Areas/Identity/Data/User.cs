using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChineseNetflix.Areas.Identity.Data;

public class User : IdentityUser
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string NickName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
