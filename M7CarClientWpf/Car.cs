using System;
using System.ComponentModel;

public class Car : INotifyPropertyChanged
{
    private string id;

    public string Id
    {
        get { return id; }
        set { id = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));}
    }

    private string model;

    public string Model
    {
        get { return model; }
        set { model = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Model")); }
    }

    private string plateNumber;

    public string PlateNumber
    {
        get { return plateNumber; }
        set {
            plateNumber = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PlateNumber"));
        }
    }

    private int price;

    public int Price
    {
        get { return price; }
        set { price = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price")); }
    }

    public string OwnerId { get; set; }

    public Car()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    public Car GetCopy()
    {
        return new Car()
        {
            Model = this.Model,
            PlateNumber = this.PlateNumber,
            Price = this.Price,
            Id = this.Id
        };
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

