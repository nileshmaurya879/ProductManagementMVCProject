using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using ProductManagementMVCProject.Dto;
using ProductManagementMVCProject.Models;
using System.Drawing.Printing;

namespace ProductManagementMVCProject.Controllers
{
    public class ProductController : Controller
    {
        public readonly DemoIdentityMVCProjectContext _demoIdentityMVCProjectContext;
        readonly IMapper _mapper;
        public ProductController(DemoIdentityMVCProjectContext demoIdentityMVCProjectContext, IMapper mapper)
        {
            _demoIdentityMVCProjectContext = demoIdentityMVCProjectContext;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllProduct()
        {
            if (TempData.ContainsKey("Data"))
            {
                var searchData = TempData["Data"].ToString();
                var jsondata = JsonConvert.DeserializeObject<IEnumerable<TblProduct>>(searchData);

                if (jsondata != null)
                {
                    var finaldata = _mapper.Map<IEnumerable<tblProductDto>>(jsondata);

                    return View(finaldata);
                }
            }
          
            TempData.Remove("Data");
            var data = await _demoIdentityMVCProjectContext.TblProducts.Include(x=>x.Category).ToListAsync();
            var pager = new PageModel(data.Count(), 1, 2);
            ViewBag.Pager = pager;
            return View(_mapper.Map<IEnumerable<tblProductDto>>(data));
        }

        public async Task<IActionResult> AddProduct()
        {
            await GetAllCategory();
            return View();
        }
        public async Task GetAllCategory(int select=0)
        {
            var categoryData = await _demoIdentityMVCProjectContext.TblCategories.ToListAsync();
            SelectList selectListItem = new SelectList(categoryData, "CategoryId", "CategoryName", select);
            ViewBag.selecteCategoryData = selectListItem;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(tblProductDto productDto)
        {
            var data = _mapper.Map<TblProduct>(productDto);
            await _demoIdentityMVCProjectContext.TblProducts.AddAsync(data);
            await _demoIdentityMVCProjectContext.SaveChangesAsync();
            return RedirectToAction("GetAllProduct", "Product");
        }

        public async Task<IActionResult> UpdateProducte(int id)
        {
            var data = await _demoIdentityMVCProjectContext.TblProducts.FirstOrDefaultAsync(x => x.ProductId == id);
            await GetAllCategory(data.CategoryId??0);
            return View(_mapper.Map<tblProductDto>(data));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int productId, tblProductDto productDto)
        {
            if(productId == productDto.ProductId)
            {
                var data = await _demoIdentityMVCProjectContext.TblProducts.AsNoTracking().FirstOrDefaultAsync(y=> y.ProductId == productId);
                if(data != null)
                {
                    var updateData = _mapper.Map<TblProduct>(productDto);
                    _demoIdentityMVCProjectContext.TblProducts.Update(updateData);
                    await _demoIdentityMVCProjectContext.SaveChangesAsync();
                    return RedirectToAction("GetAllProduct", "Product");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var data = await _demoIdentityMVCProjectContext.TblProducts.FirstOrDefaultAsync(x=> x.ProductId == Id);
            if(data != null)
            {
                _demoIdentityMVCProjectContext.TblProducts.Remove(data);
                await _demoIdentityMVCProjectContext.SaveChangesAsync();
                return RedirectToAction("GetAllProduct", "Product");
            }
            return View();
        }
        public async Task<IActionResult> SearchProduct(string searchString, string sortOrder, int currentPage = 1, int pageSize = 5)
        {
            TempData["sortProductName"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "productName" ? "productName_desc" : "productName";
            TempData["categoryName"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "categoryName" ? "categoryName_desc" : "categoryName";
            TempData["productPrice"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "productPrice" ? "productPrice_desc" : "productPrice";
            TempData["productDescription"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "productDescription" ? "productDescription_desc" : "productDescription";
         
            var data = _demoIdentityMVCProjectContext.TblProducts.Include(x => x.Category).AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
               data = data.Where(x => x.ProductName.Contains(searchString) || x.ProductPrice.ToString().Contains(searchString) || x.ProductDescription.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "productName_desc":
                    data = data.OrderByDescending(s => s.ProductName);
                    break;
                case "productName":
                    data = data.OrderBy(s => s.ProductName);
                    break;
                case "categoryName_desc":
                    data = data.OrderByDescending(s => s.Category.CategoryName);
                    break;
                case "categoryName":
                    data = data.OrderBy(s => s.Category.CategoryName);
                    break;
                case "productPrice_desc":
                    data = data.OrderByDescending(s => s.ProductPrice);
                    break;
                case "productPrice":
                    data = data.OrderBy(s => s.ProductPrice);
                    break;
                case "productDescription_desc":
                    data = data.OrderByDescending(s => s.ProductDescription);
                    break;
                case "productDescription":
                    data = data.OrderBy(s => s.ProductDescription);
                    break;
                default:
                    data = data.OrderBy(s => s.ProductName);
                    break;
            }

            var pager = new PageModel(data.Count(), currentPage, pageSize);
            TempData["pager"] = pager;

            if (data != null)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var jsonData = JsonConvert.SerializeObject(data, settings);
                TempData["Data"] = jsonData;
                return RedirectToAction("GetAllProduct", "Product");
            }
            return View();
        }
    }
}
