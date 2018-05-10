using ShoppingCartAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCartAdmin.Controllers
{
    public class LoginController : Controller
    {
        private Repository repository;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// validate user detail
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>validation result</returns>
        public async Task<ActionResult> ValidateUser(LoginViewModel loginDetail)
        {
            try
            {
                if (loginDetail == null || string.IsNullOrEmpty(loginDetail.username))
                {
                    return Json("Username is empty.");
                }

                if (string.IsNullOrEmpty(loginDetail.password))
                {
                    return Json("Password is empty.");
                }

                this.repository = new Repository();
                var user = await this.repository.AdminLogin(loginDetail);
                if (user == null)
                {
                    return Json("Invalid Username and Password.");
                }

                Session["LoginUserName"] = user.UserName;
            }
            catch (Exception ex)
            {
                return Json(ex.InnerException);
            }

            return Json(string.Empty);
        }
    }
}