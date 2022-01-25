using CustomExceptions;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class Store
{
    //public List<ProdDetails> localInv = new List<ProdDetails>(); //Will store all inventory
    
    public Store(){}

    /// <summary>
    /// Convert store table data to data row
    /// </summary>
    /// <param name="row"></param>
    public Store(DataRow row)
    {
        this.StoreID = (int) row["StoreId"];
        this.StoreName = row["StoreName"].ToString() ?? "";
        this.City = row["City"].ToString() ?? "";
        this.State = row["State"].ToString() ?? "";
        this.SalesTax = (decimal) row["SalesTax"];
    }


    public Store(String name)
    {
        this.StoreName = StoreName;
    }


    public int StoreID { get; set; }//Each different store has an ID [PK]
    [Required]
    [RegularExpression("^[a-zA-Z0-9 !?']+$", ErrorMessage = "Cannot make store name like that")]
    public string? StoreName { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public decimal SalesTax { get; set; }//Percent tax rate  [NOT IMPLEMENTED YET]



    public override string ToString()
    {
        return ($"Name: {this.StoreName} \nCity: {this.City} \nState: {this.State}");
    }


    /// <summary>
    /// Fill in columns in database row with this instance
    /// </summary>
    /// <param name="row"></param>
    public void ToDataRow(ref DataRow row)
    {
        //row["StoreId"] = this.StoreID;
        row["StoreName"] = this.StoreName;
        row["City"] = this.City;
        row["State"] = this.State;
        //row["SalesTax"] = this.SalesTax;
    }

}