using BusinessObject.Shared.Models.Blogs;
using Common.Shared;
using MassTransit;
using MassTransit.Transports;

namespace MicroBlogs.Microservices.Consumer
{
    public class BlogConsumer : IConsumer<Blog>
    {
        private readonly ILogger _logger;

        public BlogConsumer(ILogger<BlogConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Blog> context)
        {
            //https://localhost:7199/gateway/order

            #region Receive data from Queue on RabbitMQ            

            var data = context.Message;

            #endregion

            #region Business rule process anh/or call other API Service

            //Validate the Data
            //Store to Database
            //Notify the user via Email / SMS

            #endregion

            #region Logger

            if (data != null)
            {
                string messageLog = string.Format("[{0}] RECEIVE data from RabbitMQ.orderQueue: {1}", DateTime.Now.ToString(), JsonUtils.ConvertObjectToJSONString(data));

                JsonUtils.WriteLoggerFile(messageLog);

                _logger.LogInformation(messageLog);

            }

            #endregion

        }
    }
}
