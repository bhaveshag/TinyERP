namespace App.Service.Security.UserGroup
{
    using App.Common.Data;
    using App.Common.Mapping;
    using System;
    using System.Collections.Generic;
    using App.Common.Validation.Attribute;

    public class UpdateUserGroupRequest
    {
        [Required("security.addOrUpdateUserGroup.validation.idIsInvalid")]
        public Guid Id { get; set; }
        [Required("security.addOrUpdateUserGroup.validation.nameIsRequire")]
        public string Name { get; set; }
        [Required("security.addOrUpdateUserGroup.validation.keyIsRequire")]
        public string Key { get; set; }
        public string Description { get; set; }
        public IList<Guid> PermissionIds { get; set; }
        
        public UpdateUserGroupRequest(Guid id, string name, string desc)
        {
            this.Id = id;
            this.Name = name;
            this.Key = App.Common.Helpers.UtilHelper.ToKey(name);
            this.Description = desc;
        }
    }
}