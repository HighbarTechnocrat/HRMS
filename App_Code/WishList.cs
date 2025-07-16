using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

namespace Creative.Commerce
{
    /// <summary>
    /// Shopping cart item
    /// </summary>
    /// <remarks>Stores a single item in the shopping cart</remarks>
    [Serializable]
    public class WishItem
    {
        #region Private Member Variables
        // private member variables: the private storage for the properties
       // private decimal _lineTotal;
        private decimal _retailprice;
        private decimal _ourprice;
        private int _productID;
        private string _productnumber;
        //private string _defaultimage;
        private string _productName;
        private decimal _quantity;
        #endregion

        #region Constructors

        /// <summary>
        /// Create a new cart item
        /// </summary>
        /// <remarks></remarks>
        public WishItem()
        {
            // no initialization
        }

        /// <summary>
        /// Create a new cart item with the supplied values
        /// </summary>
        /// <param name="ProductID">The <see cref="System.Integer">product ID</see></param>
        /// <param name="ProductName">The <see cref="System.String">product name</see></param>
        /// <param name="ProductImageUrl">The <see cref="System.String">URL of the product image</see></param>
        /// <param name="Quantity">The <see cref="System.Integer">quantity required</see></param>
        /// <param name="Price">The <see cref="System.Double">price of the product</see></param>
        /// <remarks></remarks>
        public WishItem(int ProductID,string Productnumber, string ProductName, decimal Quantity, decimal ourprice, decimal retailprice)
        {
            this._productID = ProductID;
            this._productnumber = Productnumber;
            this._productName = ProductName;
           // this._defaultimage = defaultimage;
            this._quantity = Quantity;
            this._retailprice = retailprice;
            this._ourprice = ourprice;
            //this._lineTotal = Quantity * compPrice;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Product ID
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public int ProductID
        {
            get { return this._productID; }
            set { this._productID = value; }
        }
        public string Productnumber
        {
            get { return this._productnumber; }
            set { this._productnumber = value; }
        }
        /// <summary>
        /// Product name
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
        }

        /// <summary>
        /// URL of the product image
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        //public string defaultimage
        //{
        //    get { return this._defaultimage; }
        //    set { this._defaultimage = value; }
        //}

        /// <summary>
        /// Quantity required
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public decimal Quantity
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        /// <summary>
        /// Product price
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public decimal ourprice
        {
            get { return this._ourprice; }
            set { this._ourprice = value; }
        }

        /// <summary>
        /// our price
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public decimal retailprice
        {
            get { return this._retailprice; }
            set { this._retailprice = value; }
        }

        /// <summary>
        /// Line total
        /// </summary>
        /// <value></value>
        /// <remarks>Read only</remarks>
        //public decimal LineTotal
        //{
        //    get { return (this._quantity * this._compprice); }
        //}
        #endregion
    }
    /////////////////////////////////////////////////////
    /// <summary>
    /// The shopping cart
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class WishListCart
    {
        // percentage discount given to members
        //private const decimal MemberDiscountPercentage = 0.1m;

        #region Private Member Variables

        private DateTime _dateCreated;
        private List<WishItem> _items;
        private DateTime _lastUpdate;

        #endregion

        #region Constructors

        /// <summary>
        /// Createa new instance of the cart
        /// </summary>
        /// <remarks></remarks>
        public WishListCart()
        {
            if (this._items == null)
            {
                this._items = new List<WishItem>();
                this._dateCreated = DateTime.Now;
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// The items contained within the cart
        /// </summary>
        /// <value></value>
        /// <remarks>Contains a <see cref="System.Collections.Generic.List(Of CartItem)">List</see> of <paramref name="CartItem">CartItem</paramref> objects</remarks>
        public List<WishItem> Items
        {
            get { return this._items; }
            set { this._items = value; }
        }

        /// <summary>
        /// Returns the amount of the member discount 
        /// </summary>
        /// <value></value>
        /// <remarks>0 if the user is not a member. Read only</remarks>
        //public decimal MemberDiscount
        //{
        //    get
        //    {
        //        if (HttpContext.Current.User.IsInRole("shopMember"))
        //        {
        //            return (this.SubTotal * MemberDiscountPercentage);
        //        }
        //        return 0;
        //    }
        //}

        /// <summary>
        /// Returns the subtotal of the order
        /// </summary>
        /// <value></value>
        /// <remarks>Read only</remarks>
        //public decimal SubTotal
        //{
        //    get
        //    {
        //        if (this._items == null)
        //            return 0;

        //        decimal t = 0;
        //        foreach (CartItem item in _items)
        //            t += item.LineTotal;

        //        return t;
        //    }
        //}

        /// <summary>
        /// Returns the total order value, including member discount
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        //public decimal Total
        //{
        //    get { return (this.SubTotal - this.MemberDiscount); }
        //}

        #endregion

        #region Methods
        /// <summary>
        /// Insert an item into the cart
        /// </summary>
        /// <param name="ProductID">The <see cref="System.Integer">product ID</see></param>
        /// <param name="Price">The <see cref="System.Double">price of the product</see></param>
        /// <param name="Quantity">The <see cref="System.Integer">quantity required</see></param>
        /// <param name="ProductName">The <see cref="System.String">product name</see></param>
        /// <param name="ProductImageUrl">The <see cref="System.String">URL of the product image</see></param>
        /// <remarks></remarks>
        public void Insert(int ProductID,string Productnumber, decimal retailprice, decimal ourprice, int Quantity, string ProductName)
        {
            int ItemIndex = this.ItemIndexOfID(ProductID);
            if (ItemIndex == -1)
            {
                WishItem NewItem = new WishItem();
                NewItem.ProductID = ProductID;
                NewItem.Productnumber = Productnumber;
                NewItem.Quantity = Quantity;
                NewItem.retailprice = retailprice;
                NewItem.ourprice = ourprice;
                NewItem.ProductName = ProductName;
                //NewItem.defaultimage = defaultimage;
                this._items.Add((WishItem)NewItem);
            }
            else
            {
                _items[ItemIndex].Quantity += Quantity;
            }
            this._lastUpdate = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowID">The <see cref="System.Integer">row id</see></param>
        /// <param name="ProductID">The <see cref="System.Integer">product ID</see></param>
        /// <param name="Quantity">The <see cref="System.Integer">quantity required</see></param>
        /// <param name="Price">The <see cref="System.Double">price of the product</see></param>
        /// <remarks></remarks>
        public void Update(int RowID, int ProductID, int Quantity, decimal retailprice,decimal ourprice)
        {
            WishItem Item = this._items[RowID];
            Item.ProductID = ProductID;
            Item.Quantity = Quantity;
            Item.retailprice = retailprice;
            Item.ourprice = ourprice;
            this._lastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Returns the index of the item in the collection
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        /// <remarks>-1 if the item isn't found</remarks>
        private int ItemIndexOfID(int ProductID)
        {
            int index = 0;

            foreach (WishItem item in _items)
            {
                if (item.ProductID == ProductID)
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Deletes an item form the cart
        /// </summary>
        /// <param name="rowID">The <see cref="System.Integer">row ID of the item to delete</see></param>
        /// <remarks></remarks>
        public void DeleteItem(int rowID)
        {
            this._items.RemoveAt(rowID);
            this._lastUpdate = DateTime.Now;
        }

        #endregion
    }
}