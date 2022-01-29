using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RestApiLesson.Tests
{
    public class BaseTestServerFixture
    {
        public TestServer TestServer { get; }
        public PostamatsContext PostamatsContext { get; }
        public HttpClient Client { get; }

        public BaseTestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            PostamatsContext = (PostamatsContext)TestServer.Host.Services.GetService(typeof(PostamatsContext));
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}
