//=============================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//=============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using AngularSkeleton.Service;
using AngularSkeleton.Service.Model.Users;
using Moq;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xbehave;

namespace AngularSkeleton.Tests.Integration.Api
{
    /// <summary>
    ///     Fixture for testing products endpoint
    /// </summary>
    public class UsersFixture : ApiFixtureBase
    {
        protected Mock<IManagementService> ManagementServiceMock;

        public UsersFixture()
        {
            ManagementServiceMock = new Mock<IManagementService>();
            ServiceFacadeMock.Setup(m => m.Management).Returns(ManagementServiceMock.Object);
        }

        /// <summary>
        ///     PUT /management/users
        /// </summary>
        [Scenario]
        public void CreatingAUser(dynamic newUser)
        {
            "Given a new user".
                f(() =>
                {
                    newUser = JObject.FromObject(new
                    {
                        username = "TEST_USERNAME",
                        email = "TEST_EMAIL",
                        nameFirst = "TEST_NAMEFIRST",
                        nameLast = "TEST_NAMELAST",
                        password = "TEST_PASSWORD",
                        timezoneUtcOffset = -6
                    });
                });

            "When a POST request is made".
                f(() =>
                {
                    ManagementServiceMock.Setup(m => m.CreateUserAsync(It.IsAny<UserAddModel>())).ReturnsAsync(new UserModel {Id = 1});
                    Request.Method = HttpMethod.Post;
                    Request.RequestUri = new Uri(UsersUrl);
                    Request.Content = new ObjectContent<dynamic>(newUser, new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then a '201 Created' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.Created));

            "And the user should be added".
                f(() => ManagementServiceMock.Verify(m => m.CreateUserAsync(It.IsAny<UserAddModel>()), Times.Once()));
        }

        /// <summary>
        ///     DELETE /management/users/{id}
        /// </summary>
        [Scenario]
        [Example(1)]
        public void DeletingAUser(long userId)
        {
            "Given an existing user".
                f(() => ManagementServiceMock.Setup(m => m.DeleteUserAsync(userId)).ReturnsAsync(1));

            "When a DELETE request is made".
                f(() =>
                {
                    Request.Method = HttpMethod.Delete;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", UsersUrl, userId));
                    Request.Content = new ObjectContent<dynamic>(new JObject(), new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then a 200 OK status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the user should be removed".
                f(() => ManagementServiceMock.Verify(m => m.DeleteUserAsync(userId), Times.Once()));
        }

        /// <summary>
        ///     GET /management/users
        /// </summary>
        [Scenario]
        public void RetrievingAllUsers(IList<UserModel> users, IList<UserModel> usersResult)
        {
            "Given existing users".
                f(() =>
                {
                    users = new List<UserModel>
                    {
                        new UserModel {Id = 1},
                        new UserModel {Id = 2}
                    };
                });

            "When they are retrieved".
                f(async () =>
                {
                    ManagementServiceMock.Setup(m => m.GetAllUsersAsync()).ReturnsAsync(users);
                    Request.RequestUri = new Uri(string.Format("{0}", UsersUrl));
                    Response = await Client.SendAsync(Request);
                    usersResult = await Response.Content.ReadAsAsync<IList<UserModel>>();
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the users are returned".
                f(() => usersResult.Count().ShouldBe(users.Count()));
        }

        /// <summary>
        ///     GET /management/users/{id}
        /// </summary>
        [Scenario]
        public void RetrievingAUser(UserModel user, long userId)
        {
            "Given an existing user".
                f(() => ManagementServiceMock.Setup(m => m.GetUserAsync(userId)).ReturnsAsync(new UserModel {Id = userId}));

            "When it is retrieved".
                f(() =>
                {
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", UsersUrl, userId));
                    Response = Client.SendAsync(Request).Result;
                    user = Response.Content.ReadAsAsync<UserModel>().Result;
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the user should be retrieved".
                f(() => ManagementServiceMock.Verify(m => m.GetUserAsync(userId), Times.Once()));

            "And the user is returned".
                f(() => user.ShouldNotBe(null));

            "And the user should have an id".
                f(() => user.Id.ShouldBe(userId));
        }

        /// <summary>
        ///     GET /management/users/me
        /// </summary>
        [Scenario]
        [Example(1)]
        public void RetrievingCurrentUser(int userId, UserModel user)
        {
            "Given an existing user".
                f(() => ManagementServiceMock.Setup(m => m.GetCurrentUserAsync()).ReturnsAsync(new UserModel {Id = userId}));

            "When it is retrieved".
                f(() =>
                {
                    Request.RequestUri = new Uri(string.Format("{0}/me", UsersUrl));
                    Response = Client.SendAsync(Request).Result;
                    user = Response.Content.ReadAsAsync<UserModel>().Result;
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the user should be retrieved".
                f(() => ManagementServiceMock.Verify(m => m.GetCurrentUserAsync(), Times.Once()));

            "And the user is returned".
                f(() => user.ShouldNotBe(null));

            "And the user should have an id".
                f(() => user.Id.ShouldBe(userId));
        }

        /// <summary>
        ///     GET /management/users/{userId}/toggle
        /// </summary>
        [Scenario]
        [Example(1)]
        public void TogglingAUser(long userId)
        {
            "Given an existing task".
                f(() => ManagementServiceMock.Setup(m => m.ToggleUserAsync(userId)).ReturnsAsync(1));

            "When a POST request is made".
                f(() =>
                {
                    Request.Method = HttpMethod.Post;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}/toggle", UsersUrl, userId));
                    Request.Content = new ObjectContent<dynamic>(new JObject(), new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then the task should be deactivated".
                f(() => ManagementServiceMock.Verify(m => m.ToggleUserAsync(userId), Times.Once()));

            "And a 200 OK status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));
        }

        /// <summary>
        ///     PUT /management/users/{id}
        /// </summary>
        [Scenario]
        public void UpdatingAUser(UserModel user, long userId)
        {
            "Given an existing user".
                f(() => ManagementServiceMock.Setup(m => m.UpdateUserAsync(It.IsAny<UserUpdateModel>(), userId)).ReturnsAsync(1));

            "When a PUT request is made".
                f(() =>
                {
                    dynamic dto = JObject.FromObject(new
                    {
                        email = "newmail@skeletonapp.net",
                        isAdmin = false,
                        nameFirst = "TEST_NAMEFIRST",
                        nameLast = "TEST_NAMELAST",
                        timezoneUtcOffset = -5
                    });

                    Request.Method = HttpMethod.Put;
                    Request.RequestUri = new Uri(string.Format("{0}/{1}", UsersUrl, userId));
                    Request.Content = new ObjectContent<dynamic>(dto, new JsonMediaTypeFormatter());
                    Response = Client.SendAsync(Request).Result;
                });

            "Then a '200 OK' status is returned".
                f(() => Response.StatusCode.ShouldBe(HttpStatusCode.OK));

            "And the user should be updated".
                f(() => ManagementServiceMock.Verify(m => m.UpdateUserAsync(It.IsAny<UserUpdateModel>(), userId), Times.Once()));
        }
    }
}