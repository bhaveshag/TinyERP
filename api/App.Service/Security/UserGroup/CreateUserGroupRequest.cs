namespace App.Service.Security.UserGroup
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using App.Common.Validation.Attribute;
    using App.Common.Data;
    using App.Common.Mapping;

    public class CreateUserGroupRequest
    {
        [Required("security.addOrUpdateUserGroup.validation.idIsInvalid")]
        public Guid Id { get; set; }
        [Required("security.addOrUpdateUserGroup.validation.nameIsRequire")]
        public string Name { get; set; }
        [Required("security.addOrUpdateUserGroup.validation.keyIsRequire")]
        public string Key { get; set; }
        public string Description { get; set; }
        public IList<Guid> PermissionIds { get; set; }
        
        public CreateUserGroupRequest(string name, string desc, IList<string> permissionIds = null)
        {
            this.Name = name;
            this.Key = App.Common.Helpers.UtilHelper.ToKey(name);
            this.Description = desc;
            if (permissionIds != null)
            {
                permissionIds.ToList().ForEach(x => this.PermissionIds.Add(Guid.Parse(x)));
            }
            else { this.PermissionIds = new List<Guid>(); }
        }
    }
}