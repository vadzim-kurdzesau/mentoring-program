using AdoNetFundamentals.Models;
using AdoNetFundamentals.Repositories;

namespace AdoNetFundamentals.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var repository = new OrderRepository("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ado_net_fundamentals");

        repository.Add(new Order
        {
            Status = "Done",
            CreatedDate = DateTime.Now,
            ProductId = 2,
        });

        //repository.Delete(4);

        //var result = repository.Get(2);

        //repository.Add(new Product
        //{
        //    Name = "TestUpdated",
        //    Description = "TestUpdated",
        //    Weight = 10.1f,
        //    Height = 5.0f,
        //    Width = 12.5f,
        //    Length = 8.14f,
        //});
    }
}