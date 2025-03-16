using Sollies.Rbac.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Sollies.Rbac.FunctionalTests
{
    public class PermSetInfoTest
    {
        [Fact]
        public void PermsetInfo_Reflection()
        {
            string[] perms = { "PermissionsForceTwoFactor", "PermissionsEmailMass" };

            List<UserPermissionsDto> permsetInfo = new List<UserPermissionsDto>();

            permsetInfo.Add(new UserPermissionsDto
            {
                PermissionsForceTwoFactor = true
            });
            foreach (var perm in perms)
            {

                var param = Expression.Parameter(typeof(UserPermissionsDto), "p");
                var exp = Expression.Lambda<Func<UserPermissionsDto, bool>>(
                    Expression.Equal(
                        Expression.Property(param, perm),
                        Expression.Constant(true)
                    ),
                    param
                );

                var p = permsetInfo.SingleOrDefault(x => exp.Compile()(x));

                if (p != null)
                {
                    Console.WriteLine("Permission found");
                }
                else
                {
                    Console.WriteLine("Permission not found");
                }
            }
        }
    }
}
