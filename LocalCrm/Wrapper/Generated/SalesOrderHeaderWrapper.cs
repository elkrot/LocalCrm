using System;
using System.Linq;
using LocalCrm.Model;
using System.ComponentModel.DataAnnotations;

namespace LocalCrm.Wrapper
{
  public partial class SalesOrderHeaderWrapper : ModelWrapper<SalesOrderHeader>
  {
    public SalesOrderHeaderWrapper(SalesOrderHeader model) : base(model)
    {
    }

    public System.Int32 SalesOrderId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 SalesOrderIdOriginalValue => GetOriginalValue<System.Int32>(nameof(SalesOrderId));

    public bool SalesOrderIdIsChanged => GetIsChanged(nameof(SalesOrderId));
        [Required]
        public System.String OrderNo
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String OrderNoOriginalValue => GetOriginalValue<System.String>(nameof(OrderNo));

    public bool OrderNoIsChanged => GetIsChanged(nameof(OrderNo));



        #region TrackNumber
        public System.String TrackNumber
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String TrackNumberOriginalValue => GetOriginalValue<System.String>(nameof(TrackNumber));

        public bool TrackNumberIsChanged => GetIsChanged(nameof(OrderNo));
        #endregion

        


    public System.DateTime OrderDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }

    public System.DateTime OrderDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(OrderDate));

    public bool OrderDateIsChanged => GetIsChanged(nameof(OrderDate));

    public System.Nullable<System.Decimal> OrderTotal
    {
      get { return GetValue<System.Nullable<System.Decimal>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Decimal> OrderTotalOriginalValue => GetOriginalValue<System.Nullable<System.Decimal>>(nameof(OrderTotal));

    public bool OrderTotalIsChanged => GetIsChanged(nameof(OrderTotal));

    public System.Nullable<System.Int32> SalesPersonId
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Int32> SalesPersonIdOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(SalesPersonId));

    public bool SalesPersonIdIsChanged => GetIsChanged(nameof(SalesPersonId));

    public System.Nullable<System.Int32> CustomerId
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Int32> CustomerIdOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(CustomerId));

    public bool CustomerIdIsChanged => GetIsChanged(nameof(CustomerId));

    public System.Nullable<System.Decimal> OrderPrice
    {
      get { return GetValue<System.Nullable<System.Decimal>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Decimal> OrderPriceOriginalValue => GetOriginalValue<System.Nullable<System.Decimal>>(nameof(OrderPrice));

    public bool OrderPriceIsChanged => GetIsChanged(nameof(OrderPrice));

    public System.Nullable<System.Decimal> Prepayment
    {
      get { return GetValue<System.Nullable<System.Decimal>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Decimal> PrepaymentOriginalValue => GetOriginalValue<System.Nullable<System.Decimal>>(nameof(Prepayment));

    public bool PrepaymentIsChanged => GetIsChanged(nameof(Prepayment));

    public System.Nullable<System.DateTime> ShipDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.DateTime> ShipDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ShipDate));

    public bool ShipDateIsChanged => GetIsChanged(nameof(ShipDate));

    public System.Nullable<System.Int32> CityId
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Int32> CityIdOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(CityId));

    public bool CityIdIsChanged => GetIsChanged(nameof(CityId));

    public System.Nullable<System.Int32> TransportCompanyId
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Int32> TransportCompanyIdOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(TransportCompanyId));

    public bool TransportCompanyIdIsChanged => GetIsChanged(nameof(TransportCompanyId));

    public System.Nullable<System.Decimal> ShipingCost
    {
      get { return GetValue<System.Nullable<System.Decimal>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Decimal> ShipingCostOriginalValue => GetOriginalValue<System.Nullable<System.Decimal>>(nameof(ShipingCost));

    public bool ShipingCostIsChanged => GetIsChanged(nameof(ShipingCost));

    public System.Nullable<System.DateTime> ReceiptDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.DateTime> ReceiptDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ReceiptDate));

    public bool ReceiptDateIsChanged => GetIsChanged(nameof(ReceiptDate));

    public System.Nullable<System.Decimal> ReceivedForDelivery
    {
      get { return GetValue<System.Nullable<System.Decimal>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Decimal> ReceivedForDeliveryOriginalValue => GetOriginalValue<System.Nullable<System.Decimal>>(nameof(ReceivedForDelivery));

    public bool ReceivedForDeliveryIsChanged => GetIsChanged(nameof(ReceivedForDelivery));

    public System.Nullable<System.Decimal> ReceivedForOrder
    {
      get { return GetValue<System.Nullable<System.Decimal>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Decimal> ReceivedForOrderOriginalValue => GetOriginalValue<System.Nullable<System.Decimal>>(nameof(ReceivedForOrder));

    public bool ReceivedForOrderIsChanged => GetIsChanged(nameof(ReceivedForOrder));

    public System.Nullable<System.Int32> Status
    {
      get { return GetValue<System.Nullable<System.Int32>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.Int32> StatusOriginalValue => GetOriginalValue<System.Nullable<System.Int32>>(nameof(Status));

    public bool StatusIsChanged => GetIsChanged(nameof(Status));

    public System.String Comment
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));

    public bool CommentIsChanged => GetIsChanged(nameof(Comment));

    public System.Nullable<System.DateTime> ModifiedDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.DateTime> ModifiedDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ModifiedDate));

    public bool ModifiedDateIsChanged => GetIsChanged(nameof(ModifiedDate));
 
    public CityWrapper City { get; private set; }
 
//    public PersonWrapper Person { get; private set; }
 
//    public PersonWrapper Person1 { get; private set; }
 
    public TransportCompanyWrapper TransportCompany { get; private set; }
    
    protected override void InitializeComplexProperties(SalesOrderHeader model)
    {
      /*if (model.City == null)
      {
        throw new ArgumentException("City cannot be null");
      }
      City = new CityWrapper(model.City);
      RegisterComplex(City);
      if (model.Person == null)
      {
        throw new ArgumentException("Person cannot be null");
      }
      Person = new PersonWrapper(model.Person);
      RegisterComplex(Person);
      if (model.Person1 == null)
      {
        throw new ArgumentException("Person1 cannot be null");
      }
      Person1 = new PersonWrapper(model.Person1);
      RegisterComplex(Person1);

      if (model.TransportCompany == null)
      {
        throw new ArgumentException("TransportCompany cannot be null");
      }
      TransportCompany = new TransportCompanyWrapper(model.TransportCompany);
      RegisterComplex(TransportCompany);*/
    }
  }
}
