using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Server.Tests.ClientServices
{
    [TestClass]
    public class UserPhotoClientServiceTest: TestsBase
    {
        [TestMethod]
        public async Task GetAllUserPhotoAsync()
        {

            var role = base.SignIn(Role.User);
            var userPhotoClientService = this.CreateUserPhotoClientService();
            var result = await userPhotoClientService.GetAllUserPhotoAsync();
            Assert.IsNotNull(result);
        }
    }
}
