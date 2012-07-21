using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CloudUri.DAL.Entities;
using CloudUri.SAL.Services;
using CloudUri.Web.Security;
using CloudUri.Web.ViewModels;

namespace CloudUri.Web.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    public class ProfileController : Controller
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class
        /// </summary>
        /// <param name="accountService"></param>
        public ProfileController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_accountService.GetUserByName(User.Identity.Name));
        }

        //
        // GET: /Account/SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        //
        // POST: /Account/SignIn
        [HttpPost]
        public ActionResult SignIn(SignInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _accountService.ValidateUser(logInViewModel.UserName, logInViewModel.Password);
                if (user != null)
                {
                    SimpleSessionPersister.Username = user.Username;
                    SimpleSessionPersister.Roles = user.Roles.Select(x => x.Name).ToList();
                    if (logInViewModel.StayLoggedIn)
                    {
                        FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(SimpleSessionPersister.Username, true, 10080);
                        string encrypt = FormsAuthentication.Encrypt(formsAuthenticationTicket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                        Response.Cookies.Add(cookie);
                    }

                    return RedirectToAction("Index", "Feed");
                }
                ModelState.AddModelError(string.Empty, "User name or password are incorrect");
            }

            return View();
        }

        [Authorize]
        public ActionResult SignOut()
        {
            SimpleSessionPersister.Username = null;
            SimpleSessionPersister.Roles = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "About");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SingUpViewModel singUpViewModel)
        {
            if (ModelState.IsValid)
            {
                string errorMessage;
                User user = _accountService.CreateUser(singUpViewModel.UserName, singUpViewModel.Email, singUpViewModel.Password, out errorMessage);
                if (errorMessage != null)
                {
                    ModelState.AddModelError(string.Empty, errorMessage);
                }
                else
                {
                    SimpleSessionPersister.Username = user.Username;
                    SimpleSessionPersister.Roles = user.Roles.Select(x => x.Name).ToList();
                    return RedirectToAction("Index", "About");
                }
            }
            return View();
        }
    }
}
