
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ApniShop.Controllers
{
    public class ProductController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "7udJ5fY8Fx1wDM4q5bpzMYmqLgDKTLsZZ1Q9UlPY",
            BasePath = "https://apnishop-6fbca-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Models.Product product)
        {
            try
            {
                AddProductToFirebase(product);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

        private void AddProductToFirebase(Models.Product product)
        {
            //throw new NotImplementedException();
            client = new FireSharp.FirebaseClient(config);
            var data = product;
            PushResponse response = client.Push("Products/", data);
            data.ID = response.Result.name;
            SetResponse setResponse = client.Set("Products" + data.ID, data);

        }
    }
}