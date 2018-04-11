namespace InventoryService
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;

    using Newtonsoft.Json;

    public static class InventoryFunction
    {
        [FunctionName("inventoryfunction")]
        public static IActionResult UpdateInventory(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            TraceWriter log)
        {
            log.Info("Request received");
            var requestBody = new StreamReader(req.Body).ReadToEnd();
            var receivedEvent = JsonConvert.DeserializeObject<List<EventGridEvent<Dictionary<string, string>>>>(requestBody)
                ?.SingleOrDefault();
            switch (receivedEvent?.EventType)
            {
                case "Microsoft.EventGrid.SubscriptionValidationEvent":
                    var code = receivedEvent.Data["validationCode"];
                    return new OkObjectResult(new { validationResponse = code });

                case "placeholder":
                    // Make call to database to update inventory.
                    log.Info($"Request processed. Order Id {receivedEvent.Id}");
                    return new OkResult();

                default:
                    return new BadRequestResult();
            }
        }
    }
}