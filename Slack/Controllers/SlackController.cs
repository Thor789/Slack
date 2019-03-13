using Slack.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Slack.Controllers
{
    public class SlackController : Controller
    {
        // GET: Slack
        public ActionResult Slack()
        {
            return View();
        }

        /// <summary>
        /// Asynchrone message sending to the slack channel as an Appli 
        /// </summary>
        /// <param name="textMessage"> Message to send to the stack channel</param>
        /// <returns>Status of the request as Json</returns>
        [AsyncTimeout(8000)]
        [HttpPost]
        public async Task<JsonResult> SendSlackMessageAsync(string Message)
        {
            string status = "";
            try
            {
                // Async send the message throught the SendMessageAsync function in StackClient class
                SlackClient slackClient = new SlackClient();
                var response = await slackClient.SendMessageAsync(Message);

                // Get the status of the request
                string isValid = response.IsSuccessStatusCode ? "valid" : "invalid";
                status = isValid;
            }
            catch
            {
                status = "Error";
            }
            // Return the status of the request (valide, invalide or error)
            return Json(status, JsonRequestBehavior.AllowGet);
        }

    }
}