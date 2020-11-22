using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using Thanos.Models;
using Task = Thanos.Models.Task;

namespace Thanos.Sentinel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HueSensorAlarmController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] Task task)
        {
            var light = JsonConvert.DeserializeObject<HueLight>(task.Properties[Statics.TASK_RESPONSE_STRING].ToString());
            var startTime = DateTime.Parse(task.Properties[Statics.RESPONSE_ACTION_STARTIME_STRING].ToString());
            var duration = DateTime.Parse(task.Properties[Statics.RESPONSE_ACTION_DURATION_STRING].ToString());
            var endTime = startTime.Add(new TimeSpan(duration.Ticks));
            if (DateTime.Now.TimeOfDay >= startTime.TimeOfDay && DateTime.Now.TimeOfDay <= endTime.TimeOfDay)
            {
                if (light.state.on)
                {

                    var client = new SendGridClient("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");
                    var from = new EmailAddress("celestialhome2019@gmail.com", "Sentinel");
                    var subject = "Alarm !! Code RED";
                    var to = new EmailAddress("namit.ts@gmail.com", "Namit.T.S");
                    var plainTextContent = "Intruder Alert.";
                    var htmlContent = string.Format("<h1 style=\"color: #5e9ca0;\"><span style=\"color: #ff0000;\">CODE RED !!!&nbsp;</span></h1><h2 style=\"color: #2e6c80;\">Intruder alert</h2><p><img src=\"https://www.vippng.com/png/detail/164-1644776_thief-robber-png-cartoon-thief-png.png\" alt=\"\" width=\"164\" height=\"132\" /></p><p>{0}</p><p><span style=\"color: #339966;\">- Sentinel&nbsp;</span></p>", task.Properties[Statics.TASK_DESCRIPTION_STRING]);
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    var response = client.SendEmailAsync(msg).Result;
                    //sound alarm       
                }
            }
        }
    }
}