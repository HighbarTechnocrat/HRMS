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
    public class CartItem
    {
        #region Private Member Variables
        // private member variables: the private storage for the properties
        private decimal _lineTotal;
        private decimal _ourprice;
        private decimal _retailprice;
        private int _productID;
        private string _productnumber;
        // private string _defaultimage;
        private string _productName;
        private decimal _quantity = 2;
        private int _addressid;
        #endregion

        #region Constructors

        /// <summary>
        /// Create a new cart item
        /// </summary>
        /// <remarks></remarks>
        public CartItem()
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
        public CartItem(int ProductID, string Productnumber, string ProductName, decimal Quantity, decimal retailprice, decimal ourprice, int Addressid)
        {
            this._productID = ProductID;
            this._productnumber = Productnumber;
            this._productName = ProductName;
            //this._defaultimage = defaultimage;
            this._quantity = Quantity;
            this._retailprice = retailprice;
            this._ourprice = ourprice;
            if (this._ourprice == 0)
            {
                this._lineTotal = Quantity * retailprice;
            }
            else
            {
                this._lineTotal = Quantity * ourprice;
            }
            this._addressid = Addressid;
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
        /// <summary>
        /// Product Number
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
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
        /// 
        public decimal ourprice
        {
            get { return this._ourprice; }
            set { this._ourprice = value; }
        }
        public decimal retailprice
        {
            get { return this._retailprice; }
            set { this._retailprice = value; }
        }
        public int Addressid
        {
            get { return this._addressid; }
            set { this._addressid = value; }
        }
        /// <summary>
        /// Line total
        /// </summary>
        /// <value></value>
        /// <remarks>Read only</remarks>
        public decimal LineTotal
        {
            get
            {
                if (this._ourprice == 0)
                {
                    return (this._quantity * this._retailprice);
                }
                else
                {
                    return (this._quantity * this._ourprice);
                }

            }
        }
        #endregion
    }

    /// <summary>
    /// The shopping cart
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    public class ShoppingCart
    {
        // percentage discount given to members

        #region Private Member Variables

        private DateTime _dateCreated;
        private List<CartItem> _items;
        private DateTime _lastUpdate;

        #endregion

        #region Constructors

        /// <summary>
        /// Createa new instance of the cart
        /// </summary>
        /// <remarks></remarks>
        public ShoppingCart()
        {
            if (this._items == null)
            {
                this._items = new List<CartItem>();
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
        public List<CartItem> Items
        {
            get { return this._items; }
            set { this._items = value; }
        }
        /// <summary>
        /// Returns the amount of the member discount 
        /// </summary>
        /// <value></value>
        /// <remarks>0 if the user is not a member. Read only</remarks>


        /// <summary>
        /// Returns the subtotal of the order
        /// </summary>
        /// <value></value>
        /// <remarks>Read only</remarks>
        public decimal SubTotal
        {
            get
            {
                if (this._items == null)
                    return 0;

                decimal t = 0;
                foreach (CartItem item in _items)
                    t += item.LineTotal;

                return t;
            }
        }
        /// <summary>
        /// Returns the total order value, including member discount
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>


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
        public void Insert(int ProductID, string Productnumber, decimal retailprice, decimal ourprice, int Quantity, string ProductName, int AddressId)
        {
            int ItemIndex = this.ItemIndexOfID(ProductID);
            //if (ItemIndex == -1)
            //{
            CartItem NewItem = new CartItem();
            NewItem.ProductID = ProductID;
            NewItem.Productnumber = Productnumber;
            NewItem.Quantity = Quantity;
            NewItem.ourprice = ourprice;
            NewItem.retailprice = retailprice;
            NewItem.ProductName = ProductName;
            NewItem.Addressid = AddressId;

            // NewItem.defaultimage = defaultimage;
            this._items.Add((CartItem)NewItem);
            //}
            //else
            //{
            //   // _items[ItemIndex].Quantity++;
            //    _items[ItemIndex].Quantity += Quantity;
            //}
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
        public void Update(int RowID, int ProductID, int Quantity, decimal retailprice, decimal ourprice, int AddressId)
        {
            CartItem Item = this._items[RowID];
            Item.ProductID = ProductID;
            Item.Quantity = Quantity;
            Item.retailprice = retailprice;
            Item.ourprice = ourprice;
            Item.Addressid = AddressId;
            this._lastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Returns the index of the item in the collection
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        /// <remarks>-1 if the item isn't found</remarks>
        public int ItemIndexOfID(int ProductID)
        {
            int index = 0;

            foreach (CartItem item in _items)
            {
                if (item.ProductID == ProductID)
                    return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Returns the count of the item in the collection
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>

        public int ItemCount(int ProductID)
        {
            int count = 0;

            foreach (CartItem item in _items)
            {
                if (item.ProductID == ProductID)
                    count++;
            }
            return count;
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