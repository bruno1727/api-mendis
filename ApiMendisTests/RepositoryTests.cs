using ApiMendis.Notifications;
using ApiMendis.User;
using ApiMendis;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiMendisTests
{
    public class RepositoryTests
    {
        [Fact]
        public async Task Insert_CalledInsertAsync()
        {
            var cacheMock = new Mock<IDistributedCache>();
            var entityTest = new EntityTest();

            var service = new Repository<EntityTest>(cacheMock.Object);
            var result = await service.InsertAsync(entityTest);

            cacheMock.Verify(mock => mock.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task Insert_WhenSuccess_ReturnsTrueAsync()
        {
            var cacheMock = new Mock<IDistributedCache>();
            var entityTest = new EntityTest();

            var service = new Repository<EntityTest>(cacheMock.Object);
            var result = await service.InsertAsync(entityTest);

            Assert.True(result);
        }
    }
}
