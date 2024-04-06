using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementMVCProject.Dto;
using ProductManagementMVCProject.Models;

namespace ProductManagementMVCProject.Controllers
{
    public class CategoryController : Controller
    {
        public readonly DemoIdentityMVCProjectContext _demoIdentityMVCProject;
        public readonly IMapper _mapper;

        public CategoryController(DemoIdentityMVCProjectContext demoIdentityMVCProject,IMapper mapper) { 
            _demoIdentityMVCProject = demoIdentityMVCProject;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetallCetgory()
        {
            var data = await _demoIdentityMVCProject.TblCategories.ToListAsync();
            return View(_mapper.Map<IEnumerable<tblCategoryDto>>(data));
        }
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(tblCategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var addCategory = _mapper.Map<TblCategory>(categoryDto);
                await _demoIdentityMVCProject.TblCategories.AddAsync(addCategory);
                await _demoIdentityMVCProject.SaveChangesAsync();
                return RedirectToAction("GetallCetgory", "Category");
            }
           return View();
        }
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var data = await _demoIdentityMVCProject.TblCategories.FirstOrDefaultAsync(x=> x.CategoryId == id);
            return View(_mapper.Map<tblCategoryDto>(data));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int categoryId, tblCategoryDto categoryDto)
        {
            if(categoryId == categoryDto.CategoryId)
            {
                var data = await _demoIdentityMVCProject.TblCategories.AsNoTracking().FirstOrDefaultAsync(x=> x.CategoryId == categoryId);
                if(data != null)
                {
                    var updateCategory = _mapper.Map<TblCategory>(categoryDto);
                    _demoIdentityMVCProject.TblCategories.Update(updateCategory);
                }
            }
            await _demoIdentityMVCProject.SaveChangesAsync();
            return RedirectToAction("GetallCetgory", "Category");
        }
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data = await _demoIdentityMVCProject.TblCategories.FirstOrDefaultAsync(x=> x.CategoryId == id);
            if(data != null)
                _demoIdentityMVCProject.TblCategories.Remove(data);
            await _demoIdentityMVCProject.SaveChangesAsync();
            return RedirectToAction("GetallCetgory", "Category");
        }
    }
}
