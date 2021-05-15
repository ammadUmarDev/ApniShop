using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Auth;
using ApniShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ApniShop.Controllers
{
    public class AuthController : Controller

    {
        private static string ApiKey = "AIzaSyCr8qSKn57RdUnu_m-o7BlTvC5PZSgnFtg";
        private static string Bucket = "asp-mvc-with-android.appspot.com";

        // GET: Auth
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(model.Email, model.Password, model.Name, true);
                ModelState.AddModelError(string.Empty, "Please Verify your email then login");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                //Verification
                if (this.Request.IsAuthenticated)
                {
                    //return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception e)
            {
                //Info
                Console.WriteLine(e);
                throw;
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            try
            {
                //Verification
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    string token = ab.FirebaseToken;
                    var user = ab.User;
                    if (token != "")
                    {
                        this.SignInUser(user.Email, token, false);
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        //Setting
                        ModelState.AddModelError(string.Empty, "Invalid Username or Password");

                    }
                }
            }
            catch (Exception ex)
            {
                //Info
                Console.Write(ex);
            }

            return this.View(model);
        }

        private void SignInUser(string email, string token, bool isPersistenet)
        {
            //Intialization
            var claims = new List<Claim>();
            try
            {
                //Setting
                claims.Add(new Claim(ClaimTypes.Email, email));
                claims.Add(new Claim(ClaimTypes.Authentication, token));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authentationManager = ctx.Authentication;
                //Sign In
                authentationManager.SignIn(new AuthenticationProperties() {IsPersistent = isPersistenet},
                    claimIdenties);
            }
            catch (Exception ex)
            {
                //Info
                throw ex;

            }
        }

        private void ClaimIdenties(string username, bool isPersistent)
        {
            //Intialization
            var claims = new List<Claim>();
            try
            {
                //Setting
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception ex)
            {
                //Info
                throw ex;

            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                //Verification
                if (Url.IsLocalUrl(returnUrl))
                {
                    //Info
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                //Info
                throw ex;

            }

            return this.RedirectToAction("LogOff", "Account");

        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
    }
}