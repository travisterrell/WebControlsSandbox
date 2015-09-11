using System.Web.Http.Cors;
using System.Web.Mvc;
using TypeAhead.Services;

namespace TypeAhead.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        // TypeAhead Methods
        [Route("")]
        public ActionResult Index()
        {
            ViewBag.Title = "Web Control Sandbox";

            return View();
        }

        [Route("Typeahead")]
        public ActionResult Typeahead()
        {
            return View();
        }

        [Route("GetClients")]
        public JsonResult GetClients(string q)
        {
            var clients = ClientService.GetClientData(q);

            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        [Route("PrefetchClients")]
        public JsonResult PrefetchClients()
        {
            var clients = ClientService.PrefetchClients();

            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        // End TypeAhead Methods



        // Begin Cascading Dropdown Methods
        [Route("Cascade")]
        public ActionResult Cascade()
        {
            ViewBag.Title = "Cascading Dropdown Sandbox";

            return View();
        }

        //End Cascading Dropdown Methods


        #region Utterly Unnecessary Parts
        public ActionResult NothingHere()
        {
            ViewBag.Title = "Move Along";

            return View();
        }
        #endregion
    }
}