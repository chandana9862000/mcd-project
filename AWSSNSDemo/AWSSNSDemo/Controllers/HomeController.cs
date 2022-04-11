using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSSNSDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IAmazonSimpleNotificationService SnsClient { get; set; }
        public HomeController(IAmazonSimpleNotificationService SnsClient)
        {
            this.SnsClient = SnsClient;

        }
        [HttpPost("CreateTopic/{topicName}/{displayNameValue}")]
        public async Task<int> CreateTopic(string topicName, string displayNameValue)
        {
            var topicRequest = new CreateTopicRequest
            {
                Name = topicName
            };
            var topicResponse = await SnsClient.CreateTopicAsync(topicRequest);

            var topicAttrRequest = new SetTopicAttributesRequest
            {
                TopicArn = topicResponse.TopicArn,
                AttributeName = "DisplayName",
                AttributeValue = displayNameValue
            };
            await SnsClient.SetTopicAttributesAsync(topicAttrRequest);
            return (int)topicResponse.HttpStatusCode;
        }
        [HttpPost("CreateEmailSubscription/{topicName}/{endPoint}")]
        public async Task<int> CreateEmailSubscription(string topicName, string endPoint)
        {
            var topicResponse = SnsClient.FindTopicAsync(topicName);
            var subscribeRequest = new SubscribeRequest();
            subscribeRequest.TopicArn = topicResponse.Result.TopicArn;
            subscribeRequest.Protocol = "Email";
            subscribeRequest.Endpoint = endPoint;
            var response = await SnsClient.SubscribeAsync(subscribeRequest);
            return (int)response.HttpStatusCode;
        }

        [HttpPost("PublishMessage/{topicName}/{messageBody}/{subject}")]
        public async Task<int> PublishMessage(string topicName, string messageBody, string subject)
        {
            var topicArn = SnsClient.FindTopicAsync(topicName).Result.TopicArn;
            var publishRequest = new PublishRequest();
            publishRequest.TopicArn = topicArn;
            publishRequest.Message = messageBody;
            publishRequest.Subject = subject;
            var response = await SnsClient.PublishAsync(publishRequest);
            return (int)response.HttpStatusCode;
        }
    }
}
