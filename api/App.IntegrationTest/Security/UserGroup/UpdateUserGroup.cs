namespace App.IntegrationTest.Security.UserGroup
{
    using App.Common.Http;
    using Service.Security.UserGroup;
    using Common.UnitTest;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class UpdateUserGroup : BaseIntegrationTest
    {
        private readonly CreateUserGroupResponse createdUserGroupResponse;
        public UpdateUserGroup() : base(@"api/usergroups/{0}")
        {
            this.createdUserGroupResponse = this.CreateNewUserGroup();
        }

        [TestMethod()]
        public void Security_UserGroup_UpdateUserGroup_ShouldBeSuccess_WithValidRequest()
        {
            UpdateUserGroupRequest request = new UpdateUserGroupRequest(this.createdUserGroupResponse.Id, $"new updated name {Guid.NewGuid()}", "desc");
            IResponseData<string> response = this.Connector.Put<UpdateUserGroupRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count == 0);
        }

        private CreateUserGroupResponse CreateNewUserGroup()
        {
            CreateUserGroupRequest request = new CreateUserGroupRequest($"update UserGroup name {Guid.NewGuid()}", "desc");
            IResponseData<CreateUserGroupResponse> response = this.Connector.Post<CreateUserGroupRequest, CreateUserGroupResponse>(string.Format(this.BaseUrl, string.Empty), request);
            return response.Data;
        }

        [TestMethod()]
        public void Security_UserGroup_UpdateUserGroup_ShouldThrowException_WithEmptyNameAndKey()
        {
            UpdateUserGroupRequest request = new UpdateUserGroupRequest(this.createdUserGroupResponse.Id, string.Empty, "desc");
            IResponseData<string> response = this.Connector.Put<UpdateUserGroupRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.nameIsRequire"));
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldThrowException_WithNotExistedId()
        {
            UpdateUserGroupRequest request = new UpdateUserGroupRequest(Guid.NewGuid(), $"new updated name {Guid.NewGuid()}", "desc");
            IResponseData<string> response = this.Connector.Put<UpdateUserGroupRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.idIsInvalid"));
        }

        [TestMethod()]
        public void Security_UserGroup_CreateUserGroup_ShouldThrowException_WithEmptyId()
        {
            UpdateUserGroupRequest request = new UpdateUserGroupRequest(Guid.Empty, $"new updated name {Guid.NewGuid()}", "desc");
            IResponseData<string> response = this.Connector.Put<UpdateUserGroupRequest, string>(string.Format(this.BaseUrl, request.Id), request);
            Assert.IsTrue(response.Errors.Count > 0);
            Assert.IsTrue(response.Errors.Any(item => item.Key == "security.addOrUpdateUserGroup.validation.idIsInvalid"));
        }
    }
}
