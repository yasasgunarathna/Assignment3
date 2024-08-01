using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Store.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public AdministrationController(RoleManager<Role> roleManager, UserManager<User> userManager, StoreContext context,
            IWebHostEnvironment hostEnvironment, IMapper mapper, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();

            if (_signInManager.IsSignedIn(User))
            {
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                users.Remove(user);
            }

            return View(users);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role { Name = model.RoleName };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Roles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return NotFound();
            }

            var model = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name,
                Users = new List<User>()
            };

            foreach (var user in await _userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString());
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return NotFound();
            }

            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            model.Users = new List<User>();
            foreach (var user in await _userManager.GetUsersInRoleAsync(role.Name))
            {
                model.Users.Add(user);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(int roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                model.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = await _userManager.IsInRoleAsync(user, role.Name)
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId.ToString());

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("EditRole", new { id = roleId });
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _context.Users.Include(u => u.Gender).SingleAsync(x => x.Id == id);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.Include(x => x.Gender).SingleOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return NotFound();

            var viewModel = new UserFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = user.GenderId,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                PhotoPath = user.PhotoPath
            };

            return View("EditUser", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    await model.Photo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }

                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                _mapper.Map(model, user);
                user.PhotoPath = uniqueFileName;

                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Users");
            }

            var viewModel = new UserFormViewModel
            {
                Genders = _context.Genders.ToList(),
                GenderId = model.GenderId,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName
            };

            return View("EditUser", viewModel);
        }

        [HttpGet]
        public IActionResult Products()
        {
            var products = _context.Products
                .Include(u => u.Brand)
                .Include(u => u.Color)
                .Include(u => u.Sex)
                .Include(u => u.Category)
                .ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var productFormViewModel = new ProductFormViewModel
            {
                Categories = _context.Categories.ToList(),
                Brands = _context.Brands.ToList(),
                Colors = _context.Colors.ToList(),
                Sexes = _context.Sexes.ToList()
            };

            return View("ProductForm", productFormViewModel);
        }

        [HttpGet]
        public IActionResult Stock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.ProductId == id);

            if (stock == null)
            {
                return View(new StockViewModel { Stock = new List<Stock>(), ProductId = id });
            }

            var viewModel = new StockViewModel
            {
                ProductId = id,
                Name = stock.Name,
                Qty = stock.Qty,
                Stock = _context.Stock.Where(x => x.ProductId == id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Stock(StockViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stock = new Stock
                {
                    Name = model.Name,
                    ProductId = model.ProductId,
                    Qty = model.Qty,
                    Id = model.Id,
                    IsLastElementOrdered = model.Qty == 0
                };

                if (model.Id == 0)
                {
                    await _context.Stock.AddAsync(stock);
                }
                else
                {
                    _context.Stock.Update(stock);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditStock(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction("Stock", new { id = stock.ProductId });
        }

        [HttpGet]
        public IActionResult AddColor()
        {
            return View("ColorForm", new Color());
        }

        [HttpGet]
        public async Task<IActionResult> EditColor(int id)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            return View("ColorForm", color);
        }

        public async Task<IActionResult> DeleteColor(int id)
        {
            var color = await _context.Colors.FirstOrDefaultAsync(x => x.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            _context.Remove(color);
            await _context.SaveChangesAsync();
            return RedirectToAction("Color");
        }

        [HttpGet]
        public IActionResult Color()
        {
            var colors = _context.Colors.ToList();
            return View(colors);
        }

        [HttpGet]
        public IActionResult Category()
        {
            return View(_context.Categories.ToList());
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View("CategoryForm", new Category());
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return View("CategoryForm", category);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Category");
        }

        [HttpGet]
        public IActionResult AddBrand()
        {
            return View("BrandForm", new Brand());
        }

        [HttpGet]
        public IActionResult Brand()
        {
            return View(_context.Brands.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> EditBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }
            return View("BrandForm", brand);
        }

        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("Brand");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Id = model.Id,
                    CategoryId = model.CategoryId,
                    ColorId = model.ColorId,
                    BrandId = model.BrandId,
                    SexId = model.SexId,
                    Description = model.Description,
                    Price = decimal.Parse(model.Price),
                    Name = model.Name
                };

                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                product.PhotoPath = uniqueFileName;
                if (product.Id != 0)
                {
                    _context.Products.Update(product);
                }
                else
                {
                    await _context.Products.AddAsync(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var viewModel = new ProductFormViewModel
            {
                Description = model.Description,
                Name = model.Name,
                ColorId = model.ColorId,
                BrandId = model.BrandId,
                Price = model.Price,
                SexId = model.SexId,
                CategoryId = model.CategoryId,
                Categories = _context.Categories.ToList(),
                Colors = _context.Colors.ToList(),
                Brands = _context.Brands.ToList(),
                Sexes = _context.Sexes.ToList()
            };

            return View("ProductForm", viewModel);
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var productInDb = _context.Products.FirstOrDefault(x => x.Id == id);

            if (productInDb == null)
            {
                return NotFound();
            }

            var viewModel = new ProductFormViewModel();
            _mapper.Map(productInDb, viewModel);

            viewModel.Categories = _context.Categories.ToList();
            viewModel.Brands = _context.Brands.ToList();
            viewModel.Colors = _context.Colors.ToList();
            viewModel.Sexes = _context.Sexes.ToList();

            return View("ProductForm", viewModel);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productInDb = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productInDb == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productInDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Products");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveBrand(Brand model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.Id != 0)
                {
                    model.PhotoPath = uniqueFileName;
                    _context.Brands.Update(model);
                }
                else
                {
                    var brand = new Brand
                    {
                        Name = model.Name,
                        Description = model.Description,
                        PhotoPath = uniqueFileName
                    };
                    await _context.Brands.AddAsync(brand);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Brand");
            }

            var viewModel = new Brand
            {
                Name = model.Name,
                Description = model.Description,
            };

            return View("BrandForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Categories.Update(model);
                }
                else
                {
                    var category = new Category { Name = model.Name };
                    await _context.Categories.AddAsync(category);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Category");
            }

            var viewModel = new Category { Name = model.Name };
            return View("CategoryForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveColor(Color model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != 0)
                {
                    _context.Update(model);
                }
                else
                {
                    var color = new Color { Name = model.Name };
                    await _context.Colors.AddAsync(color);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Color");
            }

            var viewModel = new Color { Name = model.Name };
            return View("ColorForm", viewModel);
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var orders = _context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .ToList();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(x => x.User)
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Brand)
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(order);
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.Include(x => x.OrderProducts).FirstOrDefaultAsync(x => x.Id == id);

            foreach (var orderProduct in order.OrderProducts)
            {
                _context.Remove(orderProduct);
                var stockInDb = _context.Stock.FirstOrDefault(x => x.Id == orderProduct.StockId);
                if (stockInDb == null)
                {
                    var stock = new Stock
                    {
                        Id = orderProduct.StockId,
                        Name = orderProduct.Stock.Name,
                        ProductId = orderProduct.Stock.ProductId,
                        IsLastElementOrdered = false,
                        Qty = orderProduct.Stock.Qty + 1
                    };
                    await _context.Stock.AddAsync(stock);
                }
                else
                {
                    stockInDb.Qty++;
                    stockInDb.IsLastElementOrdered = false;
                    _context.Stock.Update(stockInDb);
                }
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> SentOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            order.IsSend = true;
            order.OrderSendDate = DateTime.Now;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Orders");
        }
    }
}
