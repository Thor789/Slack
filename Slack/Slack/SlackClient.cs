using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Slack.Slack
{
    public class SlackClient
    {
        private readonly Uri _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        // Incoming Webhook URL to private workspace, general channel
        private const string _webhookApp = "https://hooks.slack.com/services/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";

        public SlackClient()
        {
            _webhookUrl = new Uri(_webhookApp);
        }

        public SlackClient(Uri webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        /// <summary>
        /// Async sent the massage to the slack API
        /// </summary>
        /// <param name="message">Message sent from the UI</param>
        /// <param name="channel"></param>
        /// <param name="username"></param>
        /// <returns>Status of the request</returns>
        public async Task<HttpResponseMessage> SendMessageAsync(string message,
            string channel = null, string username = null)
        {
            var slackMessage = new
            {
                text = message,
                channel,
                username,
            };
            var serializedMessage = JsonConvert.SerializeObject(slackMessage);
            var response = await _httpClient.PostAsync(_webhookUrl,
                new StringContent(serializedMessage, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}