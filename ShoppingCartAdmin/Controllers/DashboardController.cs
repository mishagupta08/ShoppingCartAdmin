using ShoppingCartAdmin.Models;
using ShoppingCartAdmin.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace ShoppingCartAdmin.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardViewModel dashboard;

        private Repository repository;

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetDashboardMenuView(string selectedMenu)
        {
            if (string.IsNullOrEmpty(selectedMenu))
            {
                return View("Index.cshtml");
            }

            this.dashboard = new DashboardViewModel();
            this.repository = new Repository();
            if (selectedMenu == Resources.NewUser)
            {
                this.dashboard.AdminUserDetailList = await repository.GetUserList();
                if (this.dashboard.AdminUserDetailList == null)
                {
                    this.dashboard.AdminUserDetailList = new List<AdminUserDetail>();
                }
                return View("Menu//UserView", this.dashboard);
            }
            else if (selectedMenu == Resources.NewDepartment)
            {
                this.dashboard.GroupDetailList = await repository.GetGroupList();
                if (this.dashboard.GroupDetailList == null)
                {
                    this.dashboard.GroupDetailList = new List<GroupMaster>();
                }
                return View("Menu//GroupView", this.dashboard);
            }
            else if (selectedMenu.Contains("Order"))
            {
                this.dashboard.SelectedMenu = selectedMenu;
                this.dashboard.OrderDetailList = await repository.GetOrderListByStatus(selectedMenu);
                if (this.dashboard.OrderDetailList == null)
                {
                    this.dashboard.OrderDetailList = new List<OrderDetail>();
                }
                return View("Menu//OrderView", this.dashboard);
            }
            else if (selectedMenu == Resources.StateMaster)
            {

                this.dashboard.StateDetailList = await repository.GetStateList();
                if (this.dashboard.StateDetailList == null)
                {
                    this.dashboard.StateDetailList = new List<StateMaster>();
                }

                return View("Menu//StateView", this.dashboard);
            }
            else if (selectedMenu == Resources.BannerMaster)
            {
                this.dashboard.BannerDetailList = await repository.GetBannerList();
                if (this.dashboard.BannerDetailList == null)
                {
                    this.dashboard.BannerDetailList = new List<BannerMaster>();
                }
                return View("Menu//BannerView", this.dashboard);
            }
            else if (selectedMenu == Resources.BrandMaster)
            {
                this.dashboard.BrandDetailList = await repository.GetBrandList();
                if (this.dashboard.BrandDetailList == null)
                {
                    this.dashboard.BrandDetailList = new List<BrandMaster>();
                }
                return View("Menu//BrandView", this.dashboard);
            }
            else if (selectedMenu == Resources.CategoryMaster)
            {
                this.dashboard.CategoryDetailList = await repository.GetCategoryList();
                if (this.dashboard.CategoryDetailList == null)
                {
                    this.dashboard.CategoryDetailList = new List<CategoryMaster>();
                }
                return View("Menu//CategoryView", this.dashboard);
            }
            else if (selectedMenu == Resources.SubCategoryMaster)
            {
                this.dashboard.SubCategoryDetailList = await repository.GetSubCategoryList();
                if (this.dashboard.SubCategoryDetailList == null)
                {
                    this.dashboard.SubCategoryDetailList = new List<SubCategoryMaster>();
                }
                this.dashboard.CategoryDetailList = await repository.GetCategoryList();
                if (this.dashboard.CategoryDetailList == null)
                {
                    this.dashboard.CategoryDetailList = new List<CategoryMaster>();
                }
                return View("Menu//SubCategoryView", this.dashboard);
            }
            else if (selectedMenu == Resources.OfferImageMaster)
            {
                this.dashboard.ProductDetailList = await repository.GetProductList();
                if (this.dashboard.ProductDetailList == null || this.dashboard.ProductDetailList.Count == 0)
                {
                    this.dashboard.ProductDetailList.Insert(0, new ProductMaster
                    {
                        Id = "0",
                        Name = "-No Subcategory Available-"
                    });
                }
                this.dashboard.OfferImageDetailList = await repository.GetOfferList();
                if (this.dashboard.OfferImageDetailList == null)
                {
                    this.dashboard.OfferImageDetailList = new List<OfferImageMaster>();
                }
                return View("Menu//OfferImageView", this.dashboard);
            }
            else if (selectedMenu == Resources.ProductMaster)
            {
                this.dashboard.CategoryDetailList = await repository.GetCategoryList();
                if (this.dashboard.CategoryDetailList == null)
                {
                    this.dashboard.CategoryDetailList = new List<CategoryMaster>();
                    this.dashboard.CategoryDetailList.Insert(0, new CategoryMaster
                    {
                        Id = "0",
                        Name = "-No Category Available-"
                    });
                }

                if (this.dashboard.CategoryDetailList.Count > 0)
                {
                    this.dashboard.SubCategoryDetailList = await repository.GetSubCategoryByCategory(dashboard.CategoryDetailList.FirstOrDefault().Id);
                }

                if (this.dashboard.SubCategoryDetailList == null)
                {
                    this.dashboard.SubCategoryDetailList = new List<SubCategoryMaster>();
                    this.dashboard.SubCategoryDetailList.Insert(0, new SubCategoryMaster
                    {
                        Id = "0",
                        Name = "-No Subcategory Available-"
                    });
                }

                this.dashboard.BrandDetailList = await repository.GetBrandList();
                if (this.dashboard.BrandDetailList == null)
                {
                    this.dashboard.BrandDetailList = new List<BrandMaster>();
                    this.dashboard.BrandDetailList.Insert(0, new BrandMaster
                    {
                        BrandId = "0",
                        Name = "-No Subcategory Available-"
                    });
                }

                this.dashboard.ProductDetail = new ProductMaster();
                this.dashboard.ProductDetailList = await repository.GetProductList();
                return View("Menu//ProductView", this.dashboard);
            }
            else if (selectedMenu == Resources.CustomerDetail)
            {
                this.dashboard.CustomerDetailList = await repository.GetCustomerList();
                if (this.dashboard.CustomerDetailList == null)
                {
                    this.dashboard.CustomerDetailList = new List<CustomerMaster>();
                }
                return View("Menu//customerView", this.dashboard);
            }

            return View("Index.cshtml");
        }

        public async Task<ActionResult> GetImageView(string editId, string viewName)
        {
            this.dashboard = new DashboardViewModel();
            this.dashboard.ProductDetail = new ProductMaster();
            this.dashboard.ProductDetail.Id = editId;
            this.repository = new Repository();
            this.dashboard.ProductImagesList = await repository.GetProductImagesList(editId);

            return View("Menu//AddProductImages", this.dashboard);
        }

        public async Task<ActionResult> AddProductImageUrls(string prodId, string imageUrls)
        {
            if (string.IsNullOrEmpty(prodId) || string.IsNullOrEmpty(imageUrls))
            {
                return Json("Something went wrong! Please try again later.");
            }

            var urls = imageUrls.Split(';');
            var images = new List<ProductImage>();
            foreach (var url in urls)
            {
                images.Add(new ProductImage
                {
                    Imageurl = url,
                    ProductId = prodId
                });
            }

            repository = new Repository();
            var response = await repository.AddProductImage(images);
            return Json(response);
        }

        public async Task<ActionResult> GetEditView(string editId, string viewName)
        {
            this.dashboard = new DashboardViewModel();
            repository = new Repository();
            string menuName = string.Empty;
            if (viewName == Resources.NewUser)
            {
                this.dashboard.UserDetail = new AdminUserDetail();
                this.dashboard.UserDetail = await repository.GetUserById(editId);
                menuName = "Menu//addUserView";
            }
            else if (viewName == Resources.NewDepartment)
            {
                this.dashboard.GroupDetail = new GroupMaster();
                this.dashboard.GroupDetail = await repository.GetGroupById(editId);
                menuName = "Menu//addDepartmentView";
            }
            else if (viewName == Resources.StateMaster)
            {
                this.dashboard.StateDetail = new StateMaster();
                this.dashboard.StateDetail = await repository.GetStateById(editId);
                menuName = "Menu//addStateView";
            }
            else if (viewName == Resources.BrandMaster)
            {
                this.dashboard.BrandDetail = new BrandMaster();
                this.dashboard.BrandDetail = await repository.GetBrandById(editId);
                menuName = "Menu//addBrandView";
            }
            else if (viewName == Resources.CategoryMaster)
            {
                this.dashboard.CategoryDetail = new CategoryMaster();
                this.dashboard.CategoryDetail = await repository.GetCategoryById(editId);
                menuName = "Menu//addCategoryView";
            }
            else if (viewName == Resources.SubCategoryMaster)
            {
                this.dashboard.SubCategoryDetail = new SubCategoryMaster();
                this.dashboard.SubCategoryDetail = await repository.GetSubCategoryById(editId);
                this.dashboard.CategoryDetailList = await repository.GetCategoryList();
                if (this.dashboard.CategoryDetailList == null)
                {
                    this.dashboard.CategoryDetailList = new List<CategoryMaster>();
                }
                menuName = "Menu//addSubCategoryView";
            }
            else if (viewName == Resources.BannerMaster)
            {
                this.dashboard.BannerDetail = new BannerMaster();
                this.dashboard.BannerDetail = await repository.GetBannerById(editId);
                menuName = "Menu//addBannerView";
            }
            else if (viewName == Resources.OfferImageMaster)
            {
                this.dashboard.OfferImageDetail = new OfferImageMaster();
                this.dashboard.OfferImageDetail = await repository.GetOfferById(editId);
                this.dashboard.ProductDetailList = await repository.GetProductList();
                if (this.dashboard.ProductDetailList == null || this.dashboard.ProductDetailList.Count == 0)
                {
                    this.dashboard.ProductDetailList.Insert(0, new ProductMaster
                    {
                        Id = "0",
                        Name = "-No Subcategory Available-"
                    });
                }
                menuName = "Menu//addOfferImageView";
            }
            else if (viewName == Resources.ProductMaster)
            {
                this.dashboard.ProductDetail = await repository.GetProductById(editId);
                this.dashboard.CategoryDetailList = await repository.GetCategoryList();
                if (this.dashboard.CategoryDetailList == null)
                {
                    this.dashboard.CategoryDetailList = new List<CategoryMaster>();
                    this.dashboard.CategoryDetailList.Insert(0, new CategoryMaster
                    {
                        Id = "0",
                        Name = "-No Category Available-"
                    });
                }

                if (this.dashboard.CategoryDetailList.Count > 0)
                {
                    this.dashboard.SubCategoryDetailList = await repository.GetSubCategoryByCategory(this.dashboard.ProductDetail.CategoryId);
                }

                if (this.dashboard.SubCategoryDetailList == null)
                {
                    this.dashboard.SubCategoryDetailList = new List<SubCategoryMaster>();
                    this.dashboard.SubCategoryDetailList.Insert(0, new SubCategoryMaster
                    {
                        Id = "0",
                        Name = "-No Subcategory Available-"
                    });
                }

                this.dashboard.BrandDetailList = await repository.GetBrandList();
                if (this.dashboard.BrandDetailList == null)
                {
                    this.dashboard.BrandDetailList = new List<BrandMaster>();
                    this.dashboard.BrandDetailList.Insert(0, new BrandMaster
                    {
                        BrandId = "0",
                        Name = "-No Subcategory Available-"
                    });
                }

                menuName = "Menu//addProductView";
            }

            return View(menuName, this.dashboard);
        }

        public async Task<ActionResult> DeleteView(string editId, string viewName)
        {
            var res = string.Empty;
            repository = new Repository();
            if (viewName == Resources.NewUser)
            {
                res = await repository.DeleteUserById(editId);
            }
            else if (viewName == Resources.NewDepartment)
            {
                res = await repository.DeleteGroupById(editId);
            }
            else if (viewName == Resources.StateMaster)
            {
                res = await repository.DeleteStateById(editId);
            }
            else if (viewName == Resources.BrandMaster)
            {
                res = await repository.DeleteBrandById(editId);
            }
            else if (viewName == Resources.CategoryMaster)
            {
                res = await repository.DeleteCategoryById(editId);
            }
            else if (viewName == Resources.SubCategoryMaster)
            {
                res = await repository.DeleteSubCategoryById(editId);
            }
            else if (viewName == Resources.ProductMaster)
            {
                res = await repository.DeleteProductById(editId);
            }
            else if (viewName == Resources.BannerMaster)
            {
                res = await repository.DeleteBannerById(editId);
            }
            else if (viewName == Resources.OfferImageMaster)
            {
                res = await repository.DeleteOfferById(editId);
            }

            return Json(res);
        }

        public async Task<ActionResult> DeleteProductImage(string editId)
        {
            this.dashboard = new DashboardViewModel();
            var res = string.Empty;
            repository = new Repository();
            res = await repository.DeleteProductImageById(editId);
            return Json(res);
        }

        #region group

        public async Task<ActionResult> AddAdminUser(DashboardViewModel model)
        {
            if (model == null || model.UserDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditUser(model.UserDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddGroup(DashboardViewModel model)
        {
            if (model == null || model.GroupDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditGroup(model.GroupDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddState(DashboardViewModel model)
        {
            if (model == null || model.StateDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditState(model.StateDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddBrand(DashboardViewModel model)
        {
            if (model == null || model.BrandDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditBrand(model.BrandDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddCategory(DashboardViewModel model)
        {
            if (model == null || model.CategoryDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditCategory(model.CategoryDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddSubCategory(DashboardViewModel model)
        {
            if (model == null || model.SubCategoryDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditSubCategory(model.SubCategoryDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddProduct(DashboardViewModel model)
        {
            if (model == null || model.ProductDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditProduct(model.ProductDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddBanner(DashboardViewModel model)
        {
            if (model == null || model.BannerDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditBanner(model.BannerDetail);
            return Json(res);
        }

        public async Task<ActionResult> AddOfferImage(DashboardViewModel model)
        {
            if (model == null || model.OfferImageDetail == null)
            {
                return Json("Model is empty.");
            }

            repository = new Repository();
            var res = await repository.AddEditOffer(model.OfferImageDetail);
            return Json(res);
        }

        #endregion group

        public async Task<ActionResult> GetSubCategoryByCategory(string Id)
        {
            var subCategoryList = new List<SubCategoryMaster>();
            this.repository = new Repository();
            if (string.IsNullOrEmpty(Id))
            {
                return null;
            }
            else
            {
                subCategoryList = await this.repository.GetSubCategoryByCategory(Id);
            }

            if (subCategoryList == null || subCategoryList.Count == 0)
            {
                subCategoryList = new List<SubCategoryMaster>();
                subCategoryList.Insert(0, new SubCategoryMaster
                {
                    Id = "0",
                    Name = "-No Subcategory Available-"
                });
            }
            return Json(subCategoryList);
        }

        [HttpPost]
        public ActionResult UploadProductImages(HttpPostedFileBase[] files)
        {
            var list = new List<string>();
            var urlString = string.Empty;
            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        MemoryStream target = new MemoryStream();
                        file.InputStream.CopyTo(target);
                        byte[] data = target.ToArray();
                        //   Byte[] fileBytes = Convert.FromBase64String(fileBytes);
                        var url = AddNewImage(file.FileName, data);
                        list.Add(url);
                        urlString += url + ";";
                        ViewBag.UploadStatus = files.Length.ToString() + " files uploaded successfully.";
                        ViewBag.List = urlString;
                    }
                }
            }
            return Json(urlString);
        }

        [HttpPost]
        public ActionResult AddImage(string data, string fileName)
        {
            var fileData = data.Split(',');
            Byte[] fileBytes = Convert.FromBase64String(fileData[1]);
            var url = AddNewImage(fileName, fileBytes);
            return Json(url);
        }

        private string AddNewImage(string fileName, byte[] fileBytes)
        {
            string myfile = Guid.NewGuid() + "-" + fileName;

            var serverPath = Server.MapPath("~/UploadedImage");
            //var serverPath = "C:\\inetpub\\wwwroot\\ChatService\\UploadedImage";

            if (!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            var path = Path.Combine(serverPath, myfile);
            System.IO.File.WriteAllBytes(path, fileBytes);

            string currentURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + "/UploadedImage/" + myfile;
            //string currentURL = "http://localhost/ChatService/UploadedImage/" + myfile;
            //string currentURL = "D:/UploadedImage/" + myfile;

            var url = Uri.EscapeUriString(currentURL);
            // return url;

            return url;
        }
    }
}