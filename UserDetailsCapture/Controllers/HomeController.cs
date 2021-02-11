namespace UserDetailsCapture.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using DataAccessLibrary.Models;

    using UserDetailsCapture.Models;

    using static DataAccessLibrary.BusinessLogic.UserDataCapture;

    /// <summary>
    /// Defines the <see cref="HomeController" />.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The UserDetails.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult UserDetails()
        {
            ViewBag.Message = "User Details Capture Form";
            return View(new Models.UserDetailsCaptureModel());
        }

        /// <summary>
        /// The UserDetails.
        /// </summary>
        /// <param name="model">The model<see cref="UserDetailsCaptureModel"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserDetails(UserDetailsCaptureModel model)
        {
            VerifyPassword(model.Password, model);

            if (ModelState.IsValid)
            {
                // Get the birthday in the order we need to submit to SQL 
                string strDate = model.Day + "/" + model.Month + "/" + model.Year;
                string[] dateString = strDate.Split('/');
                DateTime finalDateString = Convert.ToDateTime(dateString[1] + "/" + dateString[0] + "/" + dateString[2]);

                model.Birthday = finalDateString;

                CreateUser(model.Id, model.FirstName, model.LastName, model.Email,
                    model.Password, model.Country, model.Colour, model.Birthday, model.CellNumber, model.Comments);
                
                return RedirectToAction("Index");
            }
            return View();
        }

        private void CreateUser(object id, string firstName, string lastName, string email, string password, string country, string colour, DateTime birthday, int cellNumber, string comments)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The VerifyPassword.
        /// </summary>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <param name="model">The model<see cref="UserDetailsCaptureModel"/>.</param>
        /// <returns>The <see cref="ViewResult"/>.</returns>
        private ViewResult VerifyPassword(string password, UserDetailsCaptureModel model)
        {
            //You can set these from your custom service methods

            string input = password;

            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMinimum8Chars = new Regex(@".{8,}");

            bool isValidated = hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input);

            if (!isValidated)
            {
                return View(new UserDetailsCaptureModel());
            }
            return View();
        }

        /// <summary>
        /// The ViewUser.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult ViewUser()
        {
            ViewBag.Message = "User";
            List<UserDetailsCaptureModelDB> data = LoadUserDetails().ToList();
            List<UserDetailsCaptureModel> users = new List<UserDetailsCaptureModel>();
            foreach (var row in data)
            {
                users.Add(new UserDetailsCaptureModel
                {
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Email = row.Email,
                    Password = row.Password,
                    Birthday = row.Birthday,
                    CellNumber = row.CellNumber,
                    Colour = row.Colour,
                    Comments = row.Comments,
                    Country = row.Country,

                });

            }
            return View(new UserDetailsCaptureModel() as IEnumerable<UserDetailsCaptureModel>);
        }

    }
}
