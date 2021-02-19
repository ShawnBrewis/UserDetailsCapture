namespace UserDetailsCapture.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
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
        public ActionResult Index(string userAction)
        {
            if (userAction == "Save")
            {
                ViewBag.SubmitValue = "Save";
            }

            else if (userAction == "Update")
            {
                ViewBag.SubmitValue = "Update";
            }
            else
            {
                ViewBag.SubmitValue = "Save";
            }

            return View();
        }

        // Controller
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {

            return Index(collection);
        } // 

        /// <summary>
        /// The UserDetails.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult UserDetails()
        {
            UserDetailsCaptureModel users = new UserDetailsCaptureModel();
            if (Request.Params["id"] != null)
            {
                int id = int.Parse(Request.Params["id"]);
                List<UserDetailsCaptureModelDB> data = LoadUserDetails(id).ToList();

                // int id = (int)RouteData.Values["id"];
                // Get the birthday in the order we need to submit to SQL 
                string strDate = data[0].Birthday.ToString();
                string[] dateString = strDate.Split('/');

                foreach (var row in data)
                {
                    try
                    {
                        users.Name = row.Name;
                        users.Surname = row.Surname;
                        users.Email = row.Email;
                        users.Password = row.Password;
                        users.CellphoneNumber = row.CellphoneNumber;
                        users.FavouriteColour = row.FavouriteColour;
                        users.Comments = row.Comments;
                        users.Country = row.Country;
                        users.Day = int.Parse(dateString[2].ToString().Substring(0, 2));
                        users.Month = int.Parse(dateString[1].ToString());
                        users.Year = int.Parse(dateString[0].ToString());
                        users.isUpdate = true;
                    }
                    catch (Exception T)
                    {
                        var test = T.Message;
                    }
                }

                ViewBag.SubmitValue = "Update";
                return View(model: users);
            }
            else
            {
                ViewBag.SubmitValue = "Save";
                ViewBag.Message = "User Details Capture Form";
                return View(new Models.UserDetailsCaptureModel());
            }
        }

        /// <summary>
        /// The SendEmail.
        /// </summary>
        /// <param name="model">The model<see cref="UserDetailsCaptureModel"/>.</param>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(UserDetailsCaptureModel model, int id)
        {
            if (ModelState.IsValid)
            {
                string body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.To.Add(new MailAddress("shawntestforwarp@outlook.com"));
                message.Subject = "New User Signup";
                message.Body = string.Format(body, model.Name, model.Email, Message(id));
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        /// <summary>
        /// The Message.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string Message(int id)
        {
            var link = string.Format("https://localhost:44304/Home/UserDetails?id=" + id);

            string message = "<!doctype html><html>  <head>    <meta name=\"viewport\" content=\"width=device-width\">    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">    <title>Simple Transactional Email</title>    <style>@media only screen and (max-width: 620px) {  table[class=body] h1 {    font-size: 28px !important;    margin-bottom: 10px !important;  }  table[class=body] p,table[class=body] ul,table[class=body] ol,table[class=body] td,table[class=body] span,table[class=body] a {    font-size: 16px !important;  }  table[class=body] .wrapper,table[class=body] .article {    padding: 10px !important;  }  table[class=body] .content {    padding: 0 !important;  }  table[class=body] .container {    padding: 0 !important;    width: 100% !important;  }  table[class=body] .main {    border-left-width: 0 !important;    border-radius: 0 !important;    border-right-width: 0 !important;  }  table[class=body] .btn table {    width: 100% !important;  }  table[class=body] .btn a {    width: 100% !important;  }  table[class=body] .img-responsive {    height: auto !important;    max-width: 100% !important;    width: auto !important;  }}@media all {  .ExternalClass {    width: 100%;  }  .ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div {    line-height: 100%;  }  .apple-link a {    color: inherit !important;    font-family: inherit !important;    font-size: inherit !important;    font-weight: inherit !important;    line-height: inherit !important;    text-decoration: none !important;  }  #MessageViewBody a {    color: inherit;    text-decoration: none;    font-size: inherit;    font-family: inherit;    font-weight: inherit;    line-height: inherit;  }  .btn-primary table td:hover {    background-color: #34495e !important;  }  .btn-primary a:hover {    background-color: #34495e !important;    border-color: #34495e !important;  }}</style>  </head>  <body class=\"\" style=\"background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%;\">    <span class=\"preheader\" style=\"color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;\">This is preheader text. Some clients will show this text as a preview.</span>    <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"body\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background-color: #f6f6f6; width: 100%;\" width=\"100%\" bgcolor=\"#f6f6f6\">      <tr>        <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top;\" valign=\"top\">&nbsp;</td>        <td class=\"container\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; max-width: 580px; padding: 10px; width: 580px; margin: 0 auto;\" width=\"580\" valign=\"top\">          <div class=\"content\" style=\"box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px;\">            <!-- START CENTERED WHITE CONTAINER -->            <table role=\"presentation\" class=\"main\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; background: #ffffff; border-radius: 3px; width: 100%;\" width=\"100%\">              <!-- START MAIN CONTENT AREA -->              <tr>                <td class=\"wrapper\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;\" valign=\"top\">                  <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;\" width=\"100%\">                    <tr>                      <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top;\" valign=\"top\">                        <p style=\"font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;\">Hi there,</p>                        <p style=\"font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;\">There is a new user signup. Please cick the link to view.</p>                        <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"btn btn-primary\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; box-sizing: border-box; width: 100%;\" width=\"100%\">                          <tbody>                            <tr>                              <td align=\"left\" style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;\" valign=\"top\">                                <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;\">                                  <tbody>                                    <tr>                                      <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top; border-radius: 5px; text-align: center; background-color: #3498db;\" valign=\"top\" align=\"center\" bgcolor=\"#3498db\"> <a href=" + link + " target=\"_blank\" style=\"border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; display: inline-block; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-decoration: none; text-transform: capitalize; background-color: #3498db; border-color: #3498db; color: #ffffff;\">View Details</a> </td>                                    </tr>                                  </tbody>                                </table>                              </td>                            </tr>                          </tbody>                        </table>                        <p style=\"font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px;\">Kind Regards,<br>Admin@UserDetailsCapture.com</p>                                              </td>                    </tr>                  </table>                </td>              </tr>            <!-- END MAIN CONTENT AREA -->            </table>            <!-- END CENTERED WHITE CONTAINER -->                     </div>        </td>        <td style=\"font-family: sans-serif; font-size: 14px; vertical-align: top;\" valign=\"top\">&nbsp;</td>      </tr>    </table>  </body></html>";
            return message;
        }

        /// <summary>
        /// The Sent.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
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
            if (ModelState.IsValid && ValidatePassword(model.Password))
            {
                // Get the birthday in the order we need to submit to SQL 
                string strDate = model.Day + "/" + model.Month + "/" + model.Year;
                string[] dateString = strDate.Split('/');
                
                DateTime finalDateString = Convert.ToDateTime(dateString[2] + "/" + dateString[1] + "/" + dateString[0]);

                model.Birthday = finalDateString;

                if (Request.Params["id"] != null)
                {
                    ViewBag.SubmitValue = "Update";
                    int rowsAffected = 0;
                    int idToUpdate = int.Parse(Request.Params["id"]);
                    
                    rowsAffected = UpdateUser(idToUpdate, model.Name, model.Surname, model.Email,
                    model.Password, model.Country, model.FavouriteColour, model.Birthday, model.CellphoneNumber, model.Comments);

                    if(rowsAffected > 0)
                    {
                        return RedirectToAction("Update");
                    }
                }

                else
                {
                    int id = CreateUser(model.Name, model.Surname, model.Email,
                    model.Password, model.Country, model.FavouriteColour, model.Birthday, model.CellphoneNumber, model.Comments);

                    SendEmail(model, id);

                    return RedirectToAction("Sent");
                }
            }
            return View();
        }
    }
}
