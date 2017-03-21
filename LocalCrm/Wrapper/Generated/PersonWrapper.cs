using System;   
using System.Linq;
using LocalCrm.Model;

namespace LocalCrm.Wrapper
{
    #region PersonWrapper
public partial class PersonWrapper<T> : ModelWrapper<T> where T: Person
  {
    public PersonWrapper(T model) : base(model)
    {
    }

    public System.Int32 PersonId
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }

    public System.Int32 PersonIdOriginalValue => GetOriginalValue<System.Int32>(nameof(PersonId));

    public bool PersonIdIsChanged => GetIsChanged(nameof(PersonId));



   public System.String FirstName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String FirstNameOriginalValue => GetOriginalValue<System.String>(nameof(FirstName));

    public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

    public System.String MiddleName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String MiddleNameOriginalValue => GetOriginalValue<System.String>(nameof(MiddleName));

    public bool MiddleNameIsChanged => GetIsChanged(nameof(MiddleName));

    public System.String LastName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String LastNameOriginalValue => GetOriginalValue<System.String>(nameof(LastName));

    public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

    public System.String AdditionalContactInfo
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }

    public System.String AdditionalContactInfoOriginalValue => GetOriginalValue<System.String>(nameof(AdditionalContactInfo));

    public bool AdditionalContactInfoIsChanged => GetIsChanged(nameof(AdditionalContactInfo));

    public System.Nullable<System.DateTime> ModifiedDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }

    public System.Nullable<System.DateTime> ModifiedDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ModifiedDate));

    public bool ModifiedDateIsChanged => GetIsChanged(nameof(ModifiedDate));
  }
    #endregion

    #region SalesPersonWrapper
    public partial class SalesPersonWrapper : PersonWrapper<SalesPerson>
    {
        public SalesPersonWrapper(SalesPerson model) : base(model)
        {
        }
    }
    #endregion

    #region CustomerWrapper
    public partial class CustomerWrapper : ModelWrapper<Customer>
    {
        public CustomerWrapper(Customer model) : base(model)
        {
        }

        public System.Int32 PersonId
        {
            get { return GetValue<System.Int32>(); }
            set { SetValue(value); }
        }

        public System.Int32 PersonIdOriginalValue => GetOriginalValue<System.Int32>(nameof(PersonId));

        public bool PersonIdIsChanged => GetIsChanged(nameof(PersonId));

        public System.String PersonType
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String PersonTypeOriginalValue => GetOriginalValue<System.String>(nameof(PersonType));

        public bool PersonTypeIsChanged => GetIsChanged(nameof(PersonType));

        public System.String FirstName
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String FirstNameOriginalValue => GetOriginalValue<System.String>(nameof(FirstName));

        public bool FirstNameIsChanged => GetIsChanged(nameof(FirstName));

        public System.String MiddleName
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String MiddleNameOriginalValue => GetOriginalValue<System.String>(nameof(MiddleName));

        public bool MiddleNameIsChanged => GetIsChanged(nameof(MiddleName));

        public System.String LastName
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String LastNameOriginalValue => GetOriginalValue<System.String>(nameof(LastName));

        public bool LastNameIsChanged => GetIsChanged(nameof(LastName));

        public System.String AdditionalContactInfo
        {
            get { return GetValue<System.String>(); }
            set { SetValue(value); }
        }

        public System.String AdditionalContactInfoOriginalValue => GetOriginalValue<System.String>(nameof(AdditionalContactInfo));

        public bool AdditionalContactInfoIsChanged => GetIsChanged(nameof(AdditionalContactInfo));

        public System.Nullable<System.DateTime> ModifiedDate
        {
            get { return GetValue<System.Nullable<System.DateTime>>(); }
            set { SetValue(value); }
        }

        public System.Nullable<System.DateTime> ModifiedDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(ModifiedDate));

        public bool ModifiedDateIsChanged => GetIsChanged(nameof(ModifiedDate));
    }
    #endregion

}
