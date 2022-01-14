using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebAPI.Controllers;
using BL;
using Models;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;


namespace Tests;

public class ControllerTest
{
    [Fact]
    public void StoreControllerListShouldReturnListOfStores()
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        Store testOne = new Store
        {
            StoreID = 100,
            StoreName = "Test One",
            City = "City One",
            State = "State One",
            SalesTax = 4
        };
        Store testTwo = new Store
        {
            StoreID = 200,
            StoreName = "Test two",
            City = "City Two",
            State = "State Two",
            SalesTax = 8
        };
        mockBL.Setup(x => x.GetAllStores()).Returns(
                new List<Store>
                {
                    testOne,
                    testTwo
                }
            );

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        var restoCntrllr = new StoreController(mockBL.Object, cache);

        //Act
        var result = restoCntrllr.Get();

        //Assert
        Assert.IsType<List<Store>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
    }
}
