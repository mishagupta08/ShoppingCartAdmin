using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ShoppingCartAdmin.Models
{
    public class DashboardViewModel
    {
        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<AdminUserDetail> AdminUserDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public string SelectedMenu { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public AdminUserDetail UserDetail { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<GroupMaster> GroupDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public GroupMaster GroupDetail { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<StateMaster> StateDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public StateMaster StateDetail { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<BannerMaster> BannerDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public BannerMaster BannerDetail { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<BrandMaster> BrandDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public BrandMaster BrandDetail { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<CategoryMaster> CategoryDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public CategoryMaster CategoryDetail { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<SubCategoryMaster> SubCategoryDetailList { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public SubCategoryMaster SubCategoryDetail { get; set; }

        /// <summary>
        /// gets or sets user detail
        /// </summary>
        public ProductMaster ProductDetail { get; set; }

        /// <summary>
        /// gets or sets offer image detail
        /// </summary>
        public OfferImageMaster OfferImageDetail { get; set; }

        /// <summary>
        /// gets or sets offer image detail list
        /// </summary>
        public IList<OfferImageMaster> OfferImageDetailList { get; set; }

        /// <summary>
        /// gets or sets admin user details list
        /// </summary>
        public IList<ProductMaster> ProductDetailList { get; set; }

        /// <summary>
        /// gets or sets offer image detail list
        /// </summary>
        public IList<CustomerMaster> CustomerDetailList { get; set; }

        /// <summary>
        /// gets or sets product image list
        /// </summary>
        public IList<ProductImage> ProductImagesList { get; set; }

        /// <summary>
        /// gets or sets Order detail list
        /// </summary>
        public IList<OrderDetail> OrderDetailList { get; set; }

        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        public HttpPostedFileBase[] files { get; set; }
    }
}