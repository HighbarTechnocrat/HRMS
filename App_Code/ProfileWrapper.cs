using System;
using System.Web;
using System.Web.Security;
using SecurityLib;

/// <summary>
/// A wrapper around profile information, incuding
/// credit card encryption functionality.
/// </summary>
public class ProfileWrapper
{
    private string firstname;
    private string lastname;
    private string address;
    private string address1;
    private string address2;
    private string city;
    private string state;
    private string pinCode;
   private string country;
   private string mobile;
   private string phone;
    private string birthday;
    private string addsource;
    private decimal memberdiscount; 
  private bool mailings;
  private string email;
    private string macno;
    private string panno;
    private string countrycode;
  //  private string area;
    //add newly

    private string assignedpoints ;
    private string usedpoints;

    // add newly Customer Id
    private string customerid;

    //vendor reg. and company details
    private string companyname;
    private int startyear;
    private string officephone;
    private string website;
    private string companydesc;
    private string productdesc;
    private string firmtype;        //Proprietorship,Partnership,Pvt. Ltd,Public. Ltd., Organization
    private string businessnature;  //Manufacturer, Trader, Services, Export/Import 
    private string businesscat;
    private string vendorplan;      //Standard, Silver, Gold, Platinum
    private string turnover;
    private string logoimage;
    private string headerimage;
    private DateTime regdate;
    private DateTime expirydate;
    private string status;          //A:Active, P:Pending, S:Suspended
    private string vatno;
    private string cstno;
    private string bstno;
    private string servicetaxno;
    private string terms;
    private string newsletter;

    public string Countrycode
    {
        get
        {
            return countrycode;
        }
        set
        {
            countrycode = value;
        }
    }

    public string FirstName
    {
        get
        {
            return firstname;
        }
        set
        {
            firstname = value;
        }
    }
    public string LastName
    {
        get
        {
            return lastname;
        }
        set
        {
            lastname = value;
        }
    }
    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            address = value;
        }
    }

    public string Address1
    {
        get
        {
            return address1;
        }
        set
        {
            address1 = value;
        }
    }
    public string Address2
    {
        get
        {
            return address2;
        }
        set
        {
            address2 = value;
        }
    }
    
    public string City
  {
    get
    {
        return city;
    }
    set
    {
        city = value;
    }
  }
    public string PinCode
  {
    get
    {
        return pinCode;
    }
    set
    {
        pinCode = value;
    }
  }
    public string Country
  {
    get
    {
        return country;
    }
    set
    {
        country = value;
    }
  }
    public string State
  {
    get
    {
        return state;
    }
    set
    {
        state = value;
    }
  }
    
    public string Mobile
  {
    get
    {
        return mobile;
    }
    set
    {
        mobile = value;
    }
  }
    public string Phone
  {
    get
    {
        return phone;
    }
    set
    {
       phone = value;
    }
  }

    //public string Panno
    //{
    //    get
    //    {
    //        return panno;
    //    }
    //    set
    //    {
    //        panno = value;
    //    }
    //}
    public string Birthday
    {
        get
        {
            return birthday;
        }
        set
        {
            birthday = value;
        }
    }

    public string Addsource
    {
        get
        {
            return addsource;
        }
        set
        {
            addsource = value;
        }
    }

    public decimal Memberdiscount
    {
        get
        {
            return memberdiscount;
        }
        set
        {
            memberdiscount = value;
        }
    }
    public bool Mailings
  {
    get
    {
        return mailings;
    }
    set
    {
        mailings = value;
    }
  }
    public string Email
  {
    get
    {
        return email;
    }
    set
    {
        email = value;
    }
  }
    public string Macno
    {
        get
        {
            return macno;
        }
        set
        {
            macno = value;
        }
    }

    public string AssignedPoints
    {
        get
        {
            return assignedpoints;
        }
        set
        {
            assignedpoints = value;
        }
    }
    public string UsedPoints
    {
        get
        {
            return usedpoints;
        }
        set
        {
            usedpoints = value;
        }
    }
    public string CustomerId
    {
        get
        {
            return customerid;
        }
        set
        {
            customerid = value;
        }
    }

    //vendor details
    public string CompanyName
    {
        get
        {
            return companyname;
        }
        set
        {
            companyname = value;
        }
    }

    public int Startyear
    {
        get
        {
            return startyear;
        }
        set
        {
            startyear = value;
        }
    }

    public string OfficePhone
    {
        get
        {
            return officephone;
        }
        set
        {
            officephone = value;
        }
    }

    public string Website
    {
        get
        {
            return website;
        }
        set
        {
            website = value;
        }
    }

    public string CompanyDesc
    {
        get
        {
            return companydesc;
        }
        set
        {
            companydesc = value;
        }
    }


    public string ProductDesc
    {
        get
        {
            return productdesc;
        }
        set
        {
            productdesc = value;
        }
    }

    public string FirmType
    {
        get
        {
            return firmtype;
        }
        set
        {
            firmtype = value;
        }
    }

    public string BusinessNature
    {
        get
        {
            return businessnature;
        }
        set
        {
            businessnature = value;
        }
    }


    public string BusinessCat
    {
        get
        {
            return businesscat;
        }
        set
        {
            businesscat = value;
        }
    }

    public string VendorPlan
    {
        get
        {
            return vendorplan;
        }
        set
        {
            vendorplan = value;
        }
    }

    public string Turnover
    {
        get
        {
            return turnover;
        }
        set
        {
            turnover = value;
        }
    }

    public string Logoimage
    {
        get
        {
            return logoimage;
        }
        set
        {
            logoimage = value;
        }
    }

    public string Headerimage
    {
        get
        {
            return headerimage;
        }
        set
        {
            headerimage = value;
        }
    }

    public DateTime Regdate
    {
        get
        {
            return regdate;
        }
        set
        {
            regdate = value;
        }
    }

    public DateTime Expirydate
    {
        get
        {
            return expirydate;
        }
        set
        {
            expirydate = value;
        }
    }


    public string Status
    {
        get
        {
            return status;
        }
        set
        {
            status = value;
        }
    }

    public string Vatno
    {
        get
        {
            return vatno;
        }
        set
        {
            vatno = value;
        }
    }

    public string Cstno
    {
        get
        {
            return cstno;
        }
        set
        {
            cstno = value;
        }
    }

    public string Bstno
    {
        get
        {
            return bstno;
        }
        set
        {
            bstno = value;
        }
    }

    public string Servicetaxno
    {
        get
        {
            return servicetaxno;
        }
        set
        {
            servicetaxno = value;
        }
    }
    public string Terms
    {
        get
        {
            return terms;
        }
        set
        {
            terms = value;
        }
    }

    public string Newsletter
    {
        get
        {
            return newsletter;
        }
        set
        {
            newsletter = value;
        }
    }

    //public string Area
    //{
    //    get
    //    {
    //        return area;
    //    }
    //    set
    //    {
    //        area = value;
    //    }
    //}
    
  public ProfileWrapper()
     
  {
      ProfileCommon profile =
      HttpContext.Current.Profile as ProfileCommon;
      firstname = profile.FirstName;
      lastname = profile.LastName;
      address = profile.Address;
      address1 = profile.Address1;
      address2 = profile.Address1;
      city = profile.City;
      pinCode = profile.PinCode;
      country = profile.Country;
      State = profile.State;
      mobile = profile.MobileNo;
      phone = profile.LandPhone;
      mailings = profile.Mailings;
      birthday = profile.birthday;
      addsource = profile.addsource;
      memberdiscount = profile.Memberdiscount;
      macno = profile.Macno;
      email = profile.Email;
      //panno = profile.Panno;
      // new added
      assignedpoints = profile.AssignedPoints;
      usedpoints = profile.UsedPoints;
      customerid = profile.CustomerId;
      //vendor details
      companyname = profile.CompanyName;
      startyear = profile.Startyear;
      officephone = profile.OfficePhone;
      website = profile.Website;
      companydesc = profile.CompanyDesc;
      productdesc = profile.ProductDesc;
      firmtype = profile.FirmType;
      businessnature = profile.BusinessNature;
      businesscat = profile.BusinessCat;
      vendorplan = profile.VendorPlan;
      turnover = profile.Turnover;
      logoimage = profile.Logoimage;
      headerimage = profile.Headerimage;
      regdate = profile.Regdate;
      expirydate = profile.Expirydate;
      status = profile.Status;
      vatno = profile.Vatno;
      cstno = profile.Cstno;
      bstno = profile.Bstno;
      servicetaxno = profile.Servicetaxno;
      terms = profile.Terms;
      newsletter = profile.Newsletter;
      countrycode = profile.countrycode;
     // area = profile.area;
 
  }

  public void UpdateProfile()
  {
      ProfileCommon profile =
      HttpContext.Current.Profile as ProfileCommon;
      profile.Address1 = address;
      profile.Address1 = address1;
      profile.Address2 = address2;
      profile.City = city;
      profile.State = state;
      profile.PinCode = pinCode;
      profile.Country = country;
      profile.MobileNo = mobile;
      profile.LandPhone = phone;
      profile.birthday = birthday;
      profile.addsource = addsource;
      profile.Mailings = mailings;
      profile.Macno = macno;
    //   profile.area=area;
      Membership.GetUser(profile.UserName).Email = email;

      // new added
      profile.AssignedPoints = assignedpoints;
      profile.UsedPoints = usedpoints;
      profile.CustomerId = customerid;
      //vendor details

      profile.CompanyName = companyname;
      profile.Startyear = startyear;
      profile.OfficePhone = officephone;
      profile.Website = website;
      profile.CompanyDesc = companydesc;
      profile.ProductDesc = productdesc;
      profile.FirmType = firmtype;
      profile.BusinessNature = businessnature;
      profile.BusinessCat = businesscat;
      profile.VendorPlan = vendorplan;
      profile.Turnover = turnover;
      profile.Logoimage = logoimage;
      profile.Headerimage = headerimage;
      profile.Regdate = regdate;
      profile.Expirydate = expirydate;
      profile.Status = status;
      profile.Vatno = vatno;
      profile.Cstno = cstno;
      profile.Bstno = bstno;
      profile.Servicetaxno = servicetaxno;
      profile.Terms = terms;
      profile.Newsletter = newsletter;
      profile.countrycode = countrycode;
  }
}