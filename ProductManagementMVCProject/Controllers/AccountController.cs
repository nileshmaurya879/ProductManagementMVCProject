using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementMVCProject.Dto;
using ProductManagementMVCProject.Models;

namespace ProductManagementMVCProject.Controllers
{
    public class AccountController : Controller
    {
        public readonly DemoIdentityMVCProjectContext _demoIdentityMVCProjectContext;
        public readonly IMapper _mapper;
        public AccountController(DemoIdentityMVCProjectContext demoIdentityMVCProjectContext, IMapper mapper) { 
            _demoIdentityMVCProjectContext = demoIdentityMVCProjectContext;
            _mapper = mapper;
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(tblUserDto tblUserDto)
        {
            if(ModelState.IsValid)
            {
                if(tblUserDto != null)
                {
                    tblUserDto.UserId = Guid.NewGuid();
                    var data = _mapper.Map<TblUser>(tblUserDto);
                    await _demoIdentityMVCProjectContext.TblUsers.AddAsync(data);
                    await _demoIdentityMVCProjectContext.SaveChangesAsync();
                    return RedirectToAction("Account","Index");
                }
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(ModelState.IsValid)
            {
                if(loginDto != null)
                {
                    var data = await _demoIdentityMVCProjectContext.TblUsers.FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.UserPassword == loginDto.Password);
                    if(data != null)
                    {
                        HttpContext.Session.SetString("useremail", data.Email);
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("useremail")))
            {
                HttpContext.Session.Remove("useremail");
            }
            return RedirectToAction("Login", "Account");
        }

    }
}
