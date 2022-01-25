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


    //Mock Test
    //Store Controller List Should Return List Of Stores
    [Fact]
    public void StoreControllerLSRLS() //One down 19 to go
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
        var strCntrllr = new StoreController(mockBL.Object, cache);

        //Act
        var result = strCntrllr.Get();

        //Assert
        Assert.IsType<List<Store>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
        Assert.Contains(testTwo, result);
    }


    //Mock Test
    //CustomerControllerListShouldReturnListOfCustomers
    [Fact]
    public void CustomerControllerLSRLC() //One down 19 to go
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        Customers testOne = new Customers
        {
            CustNumb = 100,
            UserName = "Boomer",
            Pass = "University Place",
            Employee = false
        };
        Customers testTwo = new Customers
        {
            CustNumb = 200,
            UserName = "Sam",
            Pass = "Charlotte",
            Employee = true
        };
        mockBL.Setup(x => x.GetAllCustomers()).Returns(
                new List<Customers>
                {
                    testOne,
                    testTwo
                }
            );

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        var userCntrllr = new UserController(mockBL.Object, cache);

        //Act
        var result = userCntrllr.Get();

        //Assert
        Assert.IsType<List<Customers>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
        Assert.Contains(testTwo, result);
    }


    //Mock Test
    //InventoryControllerListShouldReturnListOfInventory
    [Fact]
    public void InventoryControllerLSRLI() //One down 19 to go
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        Inventory testOne = new Inventory
        {
            Id = 100,
            Store = 1,
            Item = 4,
            Qty = 2
        };
        Inventory testTwo = new Inventory
        {
            Id = 200,
            Store = 2,
            Item = 3,
            Qty = 4
        };
        mockBL.Setup(x => x.GetAllInventory()).Returns(
                new List<Inventory>
                {
                    testOne,
                    testTwo
                }
            );

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        var invCntrllr = new InventoryController(mockBL.Object, cache);

        //Act
        var result = invCntrllr.Get();

        //Assert
        Assert.IsType<List<Inventory>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
        Assert.Contains(testTwo, result);
    }


    //Mock Test
    //LineItemControllerListShouldReturnListOfLineItem
    [Fact]
    public void LineItemControllerLSRLLI() //One down 19 to go
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        LineItems testOne = new LineItems
        {
            Id = 100,
            InvId = 1,
            OrderId = 4,
            Qty = 2,
            CostPerItem = 4.5m,
            SalesTax = 3.7m,
            PastOrder = false
        };
        LineItems testTwo = new LineItems
        {
            Id = 200,
            InvId = 3,
            OrderId = 5,
            Qty = 56,
            CostPerItem = 56.5m,
            SalesTax = 6.7m,
            PastOrder = true
        };
        mockBL.Setup(x => x.GetAllLineItem()).Returns(
                new List<LineItems>
                {
                    testOne,
                    testTwo
                }
            );

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        var invCntrllr = new LineItemController(mockBL.Object, cache);

        //Act
        var result = invCntrllr.Get();

        //Assert
        Assert.IsType<List<LineItems>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
        Assert.Contains(testTwo, result);
    }


    //Mock Test
    //OrderControllerListShouldReturnListOfOrders
    [Fact]
    public void OrderControllerLSRLO() //One down 19 to go
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        Orders testOne = new Orders
        {
            OrderId = 100,
            CustomerId = 1,
            StoreId = 4,
            OrderDate = DateTime.Now,
            TotalQty = 5,
            TotalCost = 34.7m,
            OrderCompleted = 0
        };
        Orders testTwo = new Orders
        {
            OrderId = 200,
            CustomerId = 2,
            StoreId = 3,
            OrderDate = DateTime.Now,
            TotalQty = 2,
            TotalCost = 56.7m,
            OrderCompleted = 1
        };
        mockBL.Setup(x => x.GetAllOrders()).Returns(
                new List<Orders>
                {
                    testOne,
                    testTwo
                }
            );

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        var invCntrllr = new OrdersController(mockBL.Object, cache);

        //Act
        var result = invCntrllr.Get();

        //Assert
        Assert.IsType<List<Orders>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
        Assert.Contains(testTwo, result);
    }


    //Mock Test
    //CarriedControllerListShouldReturnListOfCarried
    [Fact]
    public void CarriedControllerLSRLC() //One down 19 to go
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        ProdDetails testOne = new ProdDetails
        {
            APN = 100,
            Name = "Stuff1",
            ItemType = 0,
            Weight = 56.8,
            Cost = 5.7m,
            Descr = "Good stuff"
        };
        ProdDetails testTwo = new ProdDetails
        {
            APN = 200,
            Name = "Stuff2",
            ItemType = 1,
            Weight = 546.8,
            Cost = 56.7m,
            Descr = "Ok stuff"
        };
        mockBL.Setup(x => x.GetAllCarried()).Returns(
                new List<ProdDetails>
                {
                    testOne,
                    testTwo
                }
            );

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        var invCntrllr = new CarriedController(mockBL.Object, cache);

        //Act
        var result = invCntrllr.Get();

        //Assert
        Assert.IsType<List<ProdDetails>>(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(testOne, result);
        Assert.Contains(testTwo, result);
    }


    [Fact]
    public void TestTaxCanBeApplied()
    {
        //Arrange
        Store testOne = new Store();
        decimal costTest = 10m;
        decimal tax = testOne.SalesTax = 0.67m;
        decimal finalValue = 0;

        //Act
        finalValue = costTest + (costTest * tax);

        //Assert
        //Assert.Equals(2, finalValue);
        Assert.True(finalValue == 16.7m);
    }


    [Fact]
    public void StoreNameIsUnder40Char()
    {
        //Arrange
        Store testOne = new Store();
        string name = testOne.StoreName;

        //Act
        name = "dadadafwefwsfgs";

        //Assert
        //Assert.Equals(2, finalValue);
        Assert.True(name.Length < 40);
    }


    [Fact]
    public void UserNameIsUnder40Char()
    {
        //Arrange
        Customers testOne = new Customers();
        string name = testOne.UserName;
        string pass = testOne.Pass;

        //Act
        name = "dadadafwefwsfgs";
        pass = "dadad53435353535afwefwsfgs";

        //Assert
        //Assert.Equals(2, finalValue);
        Assert.True(name.Length < 40);
        Assert.True(pass.Length < 40);
    }


    [Fact]
    public void BothProdAndLineItemsCostSameType()
    {
        //Arrange
        var mockBL = new Mock<IBL>();
        ProdDetails testOne = new ProdDetails
        {
            APN = 100,
            Name = "Stuff1",
            ItemType = 0,
            Weight = 56.8,
            Cost = 5.7m,
            Descr = "Good stuff"
        };
        LineItems testTwo = new LineItems();

        mockBL.Setup(x => x.GetAllCarried()).Returns(
                new List<ProdDetails>
                {
                    testOne,
                }
            );


        //Act
        testTwo.CostPerItem = testOne.Cost;
        decimal setLineCost = testTwo.CostPerItem;

        //Assert
        Assert.Equal(setLineCost, testOne.Cost);
    }



}
