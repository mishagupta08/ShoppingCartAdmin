using Newtonsoft.Json;
using ShoppingCartAdmin.Models;
using ShoppingCartAdmin.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingCartAdmin
{
    public class Repository
    {
        private string ApiUrl = "http://shopapi.bisplindia.in/api/Admin/";

        private string LoginAdminAction = "LoginAdminUser";

        #region userAction

        private string GetUserListAction = "ManageUser/List";

        private string AddUserAction = "ManageUser/Add";

        private string EditUserAction = "ManageUser/Edit";

        private string DeleteUserAction = "ManageUser/Delete";

        private string GetUserByIdAction = "ManageUser/UserById";

        #endregion userAction

        #region orderAction

        private string GetOrderListAction = "ManageOrder/List";

        private string GetOrderListByStatusAction = "ManageOrder/OrderByStatus";

        #endregion orderAction

        /***Group***/
        #region groupAction

        private string GetGroupListAction = "ManageGroup/List";

        private string AddGroupAction = "ManageGroup/Add";

        private string EditGroupAction = "ManageGroup/Edit";

        private string DeleteGroupAction = "ManageGroup/Delete";

        private string GetGroupByIdAction = "ManageGroup/GroupById";

        #endregion groupAction
        /***State***/

        #region stateAction

        private string GetStateListAction = "ManageState/List";

        private string AddStateAction = "ManageState/Add";

        private string EditStateAction = "ManageState/Edit";

        private string DeleteStateAction = "ManageState/Delete";

        private string GetStateByIdAction = "ManageState/StateById";

        #endregion stateAction

        #region ProductImagesAction

        private string AddProductImagesAction = "ManageProductImages/Add/0";

        private string GetProductImagesAction = "ManageProductImages/ByProductId/";

        private string DeleteProductImageAction = "ManageProductImages/Delete/";

        #endregion ProductImagesAction

        #region CustomerDetailAction

        private string GetCustomerListAction = "ManageCustomer/List";

        #endregion CustomerDetailAction

        #region bannerAction

        private string GetBannerListAction = "ManageBanner/List";

        private string AddBannerAction = "ManageBanner/Add";

        private string EditBannerAction = "ManageBanner/Edit";

        private string DeleteBannerAction = "ManageBanner/Delete";

        private string GetBannerByIdAction = "ManageBanner/BannerById";

        #endregion bannerAction

        #region brandAction

        private string GetBrandListAction = "ManageBrand/List";

        private string AddBrandAction = "ManageBrand/Add";

        private string EditBrandAction = "ManageBrand/Edit";

        private string DeleteBrandAction = "ManageBrand/Delete";

        private string GetBrandByIdAction = "ManageBrand/BrandById";

        #endregion bannerAction

        #region categoryAction

        private string GetCategoryListAction = "ManageCategory/List";

        private string AddCategoryAction = "ManageCategory/Add";

        private string EditCategoryAction = "ManageCategory/Edit";

        private string DeleteCategoryAction = "ManageCategory/Delete";

        private string GetCategoryByIdAction = "ManageCategory/CategoryById";

        #endregion categoryAction

        #region subCategoryAction

        private string GetSubCategoryListAction = "ManageSubCategory/List";

        private string AddSubCategoryAction = "ManageSubCategory/Add";

        private string EditSubCategoryAction = "ManageSubCategory/Edit";

        private string DeleteSubCategoryAction = "ManageSubCategory/Delete";

        private string GetSubCategoryByIdAction = "ManageSubCategory/SubCategoryById";

        private string GetSubCategoryByCategoryIdAction = "GetSubCategoryByCategoryId/";

        #endregion subCategoryAction

        #region ProductAction

        private string GetProductListAction = "ManageProduct/List";

        private string AddProductAction = "ManageProduct/Add";

        private string EditProductAction = "ManageProduct/Edit";

        private string DeleteProductAction = "ManageProduct/Delete";

        private string GetProductByIdAction = "ManageProduct/ProductById";

        #endregion ProductAction

        #region OfferAction

        private string GetOfferListAction = "ManageOffer/List";

        private string AddOfferAction = "ManageOffer/Add";

        private string EditOfferAction = "ManageOffer/Edit";

        private string DeleteOfferAction = "ManageOffer/Delete";

        private string GetOfferByIdAction = "ManageOffer/OfferById";

        #endregion OfferAction

        public async Task<AdminUserDetail> AdminLogin(LoginViewModel loginModel)
        {
            var loginDetail = "{\"Username\":\"UsernameValue\",\"Password\":\"PasswordValue\"}";
            loginDetail = loginDetail.Replace("UsernameValue", loginModel.username).Replace("PasswordValue", loginModel.password);

            var result = await CallPostFunction(loginDetail, LoginAdminAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<AdminUserDetail>(result.ResponseValue);
                return memberDetail;
            }
        }

        /***User Operation start****/

        public async Task<string> AddEditUser(AdminUserDetail detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.Id) ? AddUserAction : EditUserAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<OrderDetail>> GetOrderListByStatus(string status)
        {
            var query = string.Empty;
            if (status == Resources.PendingOrderDetail)
            {
                query = "{\"OrderStatus\":\"Order Not Confirmed\"}";
            }
            else if (status == Resources.CancelOrderDetail)
            {
                query = "{\"OrderStatus\":\"Cancelled\"}";
            }
            else if (status == Resources.DispatchOrderDetail)
            {
                query = "{\"OrderStatus\":\"Dispatch\"}";
            }

            var result = await CallPostFunction(query, GetOrderListByStatusAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var orderList = JsonConvert.DeserializeObject<List<OrderDetail>>(result.ResponseValue);
                return orderList;
            }
        }

        public async Task<List<OrderDetail>> GetOrderList()
        {
            var result = await CallPostFunction(string.Empty, GetOrderListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var orderList = JsonConvert.DeserializeObject<List<OrderDetail>>(result.ResponseValue);
                return orderList;
            }
        }

        public async Task<List<AdminUserDetail>> GetUserList()
        {
            /***Delete It***/

            //var list = new List<AdminUserDetail>();
            //list.Add(new AdminUserDetail
            //{
            //    Id = "1",
            //    IsActive = false,
            //    Password = "TestPassword",
            //    UserName = "TestUsername",
            //    Remarks = "TestRemarks",
            //});

            //list.Add(new AdminUserDetail
            //{
            //    Id = "2",
            //    Password = "TestPassword2",
            //    UserName = "TestUsername2",
            //    Remarks = "TestRemarks2",
            //});

            //list.Add(new AdminUserDetail
            //{
            //    Id = "3",
            //    IsActive = true,
            //    Password = "TestPassword3",
            //    UserName = "TestUsername3",
            //    Remarks = "TestRemarks3",
            //});

            //list.Add(new AdminUserDetail
            //{
            //    Id = "4",
            //    IsActive = true,
            //    Password = "TestPassword4",
            //    UserName = "TestUsername4",
            //    Remarks = "TestRemarks4",
            //});

            //list.Add(new AdminUserDetail
            //{
            //    Id = "5",
            //    IsActive = false,
            //    Password = "TestPassword5",
            //    UserName = "TestUsername5",
            //    Remarks = "TestRemarks5",
            //});

            //return list;

            /***Delete It***/

            var result = await CallPostFunction(string.Empty, GetUserListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<List<AdminUserDetail>>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<AdminUserDetail> GetUserById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetUserByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<AdminUserDetail>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteUserById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteUserAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***User Operation end****/

        /***Group Operation start****/

        public async Task<string> AddEditGroup(GroupMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.GroupId) ? AddGroupAction : EditGroupAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<GroupMaster>> GetGroupList()
        {
            var result = await CallPostFunction(string.Empty, GetGroupListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var groupDetail = JsonConvert.DeserializeObject<List<GroupMaster>>(result.ResponseValue);
                return groupDetail;
            }
        }

        public async Task<GroupMaster> GetGroupById(string id)
        {
            var query = "{\"GroupId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetGroupByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<GroupMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteGroupById(string id)
        {
            var query = "{\"GroupId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteGroupAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***Group Operation end****/

        /***State Operation start****/

        public async Task<string> AddEditState(StateMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.StateId) ? AddStateAction : EditStateAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<StateMaster>> GetStateList()
        {
            var result = await CallPostFunction(string.Empty, GetStateListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<StateMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<StateMaster> GetStateById(string id)
        {
            var query = "{\"StateId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetStateByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<StateMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteStateById(string id)
        {
            var query = "{\"StateId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteStateAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***State Operation end****/

        /***Customer Operation start****/

        public async Task<List<CustomerMaster>> GetCustomerList()
        {
            var result = await CallPostFunction(string.Empty, GetCustomerListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<CustomerMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<CustomerMaster> GetCustomerById(string id)
        {
            var query = "{\"CustomerId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetBannerByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<CustomerMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        /***Customer Operation end****/

        /***Banner Operation start****/

        public async Task<string> AddEditBanner(BannerMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.BannerId) ? AddBannerAction : EditBannerAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<BannerMaster>> GetBannerList()
        {
            var result = await CallPostFunction(string.Empty, GetBannerListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<BannerMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<BannerMaster> GetBannerById(string id)
        {
            var query = "{\"BannerId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetBannerByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<BannerMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteBannerById(string id)
        {
            var query = "{\"BannerId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteBannerAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***Banner Operation end****/

        /***Brand Operation start****/

        public async Task<string> AddEditBrand(BrandMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.BrandId) ? AddBrandAction : EditBrandAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<BrandMaster>> GetBrandList()
        {
            var result = await CallPostFunction(string.Empty, GetBrandListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<BrandMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<BrandMaster> GetBrandById(string id)
        {
            var query = "{\"BrandId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetBrandByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<BrandMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteBrandById(string id)
        {
            var query = "{\"BrandId\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteBrandAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***Brand Operation end****/


        /***Category Operation start****/

        public async Task<string> AddEditCategory(CategoryMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.Id) ? AddCategoryAction : EditCategoryAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<CategoryMaster>> GetCategoryList()
        {
            var result = await CallPostFunction(string.Empty, GetCategoryListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<CategoryMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<CategoryMaster> GetCategoryById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetCategoryByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<CategoryMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteCategoryById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteCategoryAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***Category Operation end****/

        /***SubCategory Operation start****/

        public async Task<string> AddEditSubCategory(SubCategoryMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.Id) ? AddSubCategoryAction : EditSubCategoryAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<SubCategoryMaster>> GetSubCategoryList()
        {
            var result = await CallPostFunction(string.Empty, GetSubCategoryListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<SubCategoryMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<List<SubCategoryMaster>> GetSubCategoryByCategory(string Id)
        {
            var result = await CallPostFunction(string.Empty, GetSubCategoryByCategoryIdAction + Id);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<SubCategoryMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<SubCategoryMaster> GetSubCategoryById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetSubCategoryByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<SubCategoryMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteSubCategoryById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteSubCategoryAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***SubCategory Operation end****/

        /***Product Operation start****/

        public async Task<string> DeleteProductImageById(string id)
        {
            var result = await CallPostFunction(string.Empty, DeleteProductImageAction + id);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<string> AddEditProduct(ProductMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.Id) ? AddProductAction : EditProductAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<ProductMaster>> GetProductList()
        {
            var result = await CallPostFunction(string.Empty, GetProductListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<ProductMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<ProductMaster> GetProductById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetProductByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<ProductMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteProductById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteProductAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<ProductImage>> GetProductImagesList(string id)
        {
            var result = await CallPostFunction(string.Empty, GetProductImagesAction + id);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<ProductImage>>(result.ResponseValue);
                return detail;
            }
        }

        /***Product Operation end****/

        /***Product Operation start****/

        public async Task<string> AddEditOffer(OfferImageMaster detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var action = string.IsNullOrEmpty(detail.Id) ? AddOfferAction : EditOfferAction;
            var result = await CallPostFunction(data, action);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<List<OfferImageMaster>> GetOfferList()
        {
            var result = await CallPostFunction(string.Empty, GetOfferListAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var detail = JsonConvert.DeserializeObject<List<OfferImageMaster>>(result.ResponseValue);
                return detail;
            }
        }

        public async Task<OfferImageMaster> GetOfferById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, GetOfferByIdAction);
            if (result == null || !result.Status)
            {
                return null;
            }
            else
            {
                var memberDetail = JsonConvert.DeserializeObject<OfferImageMaster>(result.ResponseValue);
                return memberDetail;
            }
        }

        public async Task<string> DeleteOfferById(string id)
        {
            var query = "{\"Id\":\"IdValue\"}";
            query = query.Replace("IdValue", id);
            var result = await CallPostFunction(query, DeleteOfferAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        //public async Task<List<ProductMaster>> (string action, string Id)
        //{
        //    var method = ManageProductImagesAction + action + "/" + Id;
        //    var result = await CallPostFunction(query, method);
        //}

        public async Task<string> AddProductImage(List<ProductImage> detail)
        {
            var data = JsonConvert.SerializeObject(detail);
            var result = await CallPostFunction(data, AddProductImagesAction);
            if (result == null || !result.Status)
            {
                return result.ResponseValue;
            }
            else
            {
                return string.Empty;
            }
        }

        /***Product Operation end****/

        private async Task<Response> CallPostFunction(string detail, string action)
        {
            using (var httpClient = new HttpClient())
            {
                // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
                var httpContent = new StringContent(detail, Encoding.UTF8, "application/json");

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(ApiUrl + action, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Response>(responseContent);

                    return result;
                }
            }

            return null;
        }

        private async Task<Response> CallGetFunction(string action)
        {
            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse = await httpClient.GetAsync(ApiUrl + action);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Response>(responseContent);

                    return result;
                }
            }

            return null;
        }


    }
}