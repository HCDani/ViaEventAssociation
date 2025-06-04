using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Infrastructure.Persistence;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents;

namespace IntegrationTests.WebApi {
    [TestClass]
    public class WebApiTests {
        private static WebApplicationFactory<Program> webAppFac = null!;
        private static HttpClient client = null!;

        [ClassInitialize]
        public static void Initialize(TestContext _1) {
            webAppFac = new VeaWebApplicationFactory();
            client = webAppFac.CreateClient();
        }

        [ClassCleanup]
        public static void Cleanup() {
            webAppFac.Dispose();
        }
        private static VEvent LoadEvent(Guid EventId) {
            IServiceScope serviceScope = webAppFac.Services.CreateScope();
            EFCDbContext context = (EFCDbContext)serviceScope.ServiceProvider.GetService(typeof(EFCDbContext))!;
            VEvent vEvent = context.Events.SingleOrDefault(evt => evt.Id == EventId)!;
            return vEvent;
        }
        [TestMethod]
        public async Task CreateEvent_ShouldReturnOk() {
            // act
            HttpResponseMessage response = await client.PostAsync("/api/event/create", JsonContent.Create(new { }));

            // assert part
            CreateEventResponse createEventResponse = (await response.Content.ReadFromJsonAsync<CreateEventResponse>())!;
            VEvent vEvent = LoadEvent(createEventResponse.Id);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            Assert.IsNotNull(vEvent);
        }

        [TestMethod]
        public async Task UpdateTitle_ValidInput_ShouldReturnOk() {
            HttpResponseMessage createdResponse = await client.PostAsync("/api/event/create", JsonContent.Create(new { }));
            CreateEventResponse createEventResponse = (await createdResponse.Content.ReadFromJsonAsync<CreateEventResponse>())!;

            string newTitle = "New Title";
            UpdateEventTitleRequest request = new(createEventResponse.Id,newTitle);

            // act
            HttpResponseMessage response = await client.PostAsync($"/api/event/update_title", JsonContent.Create(request));

            // assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            VEvent vEvent = LoadEvent(createEventResponse.Id);
            Assert.AreEqual(newTitle, vEvent.Title.Value);
        }

    }
}
