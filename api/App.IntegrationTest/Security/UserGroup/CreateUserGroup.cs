namespace App.IntegrationTest.Security.UserGroup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using App.Common.Http;
    using App.Common.UnitTest;
    using App.Service.Security.UserGroup;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CreateUserGroup : BaseIntegrationTest
    {
        public CreateUserGroup()
            : base(@"api/usergroups")
        {
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldBeSuccess_WithValidRequest()
        {
            var permissions = new List<string>();
            CreateUserGroupRequest request = new CreateUserGroupRequest($"Name {Guid.NewGuid()}", "desc", permissions);
            IResponseData<CreateUserGroupResponse> response =
                this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);

            /* Assert.IsTrue(response.Status == HttpStatusCode.OK); */
            Assert.IsTrue(response.Errors.Count == 0);
            Assert.IsTrue(response.Data != null);
            Assert.IsTrue(response.Data.Id != null && response.Data.Id != Guid.Empty);
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldThroException_WithDuplicatedName()
        {
            var permissions = new List<string>();
            CreateUserGroupRequest request = new CreateUserGroupRequest($"Name {Guid.NewGuid()}", "desc", permissions);
            this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);

            IResponseData<CreateUserGroupResponse> response =
                this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(
                response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.nameAlreadyExist"));
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldThroException_WithEmptyName()
        {
            var permissions = new List<string>();
            CreateUserGroupRequest request = new CreateUserGroupRequest(string.Empty, string.Empty, permissions);
            IResponseData<CreateUserGroupResponse> response =
                this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(this.BaseUrl, request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(
                response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.nameIsRequire"));
        }
    }
}