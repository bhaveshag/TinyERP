namespace App.IntegrationTest.Security.UserGroup
{
    using Common.Http;
    using Service.Security.UserGroup;
    using Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    [TestClass]
    public class CreateUserGroup : BaseIntegrationTest
    {
        public CreateUserGroup() : base(@"api/usergroups")
        {
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldBeSuccess_WithValidRequest()
        {
            //CreateUserGroupRequest request = new CreateUserGroupRequest($"Name {Guid.NewGuid()}", "desc");
            //IResponseData<CreateUserGroupResponse> response = this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);
            ///* Assert.IsTrue(response.Status == HttpStatusCode.OK); */
            //Assert.IsTrue(response.Errors.Count == 0);
            //Assert.IsTrue(response.Data != null);
            //Assert.IsTrue(response.Data.Id != null && response.Data.Id != Guid.Empty);

            var permissions = new List<string>();
            CreateUserGroupRequest request = new CreateUserGroupRequest($"Name {Guid.NewGuid()}", "desc", permissions);
            IResponseData<CreateUserGroupResponse> response = this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);
            /* Assert.IsTrue(response.Status == HttpStatusCode.OK); */
            Assert.IsTrue(response.Errors.Count == 0);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data.Id != null && response.Data.Id != Guid.Empty);
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroupn_ShouldThroException_WithInValidRequest()
        {
            var permissions = new List<string>();
            var key = Guid.NewGuid().ToString();
            CreateUserGroupRequest request = new CreateUserGroupRequest(string.Empty, string.Empty, permissions);
            IResponseData<CreateUserGroupResponse> response = this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.nameIsRequire"));
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldThroException_WithDuplicatedNameAndKey()
        {
            var permissions = new List<string>();
            CreateUserGroupRequest request = new CreateUserGroupRequest($"Name {Guid.NewGuid()}", "desc", permissions);
            this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);

            IResponseData<CreateUserGroupResponse> response = this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.nameAlreadyExist"));
        }
    }
}
