using Microsoft.AspNetCore.Mvc;
using Sks365.MessageBrokers.Brokers;
using Sks365.MessageBrokers.TestApi.DummyTest;

namespace Sks365.MessageBrokers.TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitMqTestController : Controller
{
    /// <summary>
    /// example https://localhost:5001/rabbitmqtest/send?producer=rmq-producer-03&rk=3
    /// </summary>
    /// <param name="messageBroker"></param>
    /// <param name="producer"></param>
    /// <param name="rk"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("send")]
    public bool SendMessage([FromServices] IMessageBroker messageBroker,string producer, string rk)
    {
        TestingEvent depositAddedEvent = new TestingEvent()
        {
            TestId = Random.Shared.Next(20, 155)
        };
        return messageBroker.GetProducer(producer).Publish(depositAddedEvent, rk);
    }
}