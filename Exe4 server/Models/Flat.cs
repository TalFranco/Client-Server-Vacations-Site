namespace Exe_1.Models
{
    public class Flat
    {
        DBservices dbs = new DBservices();

        private double price;

        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int NumOfRooms { get; set; }

        public double Price
        {
            get => price;
            set => price = Discount(NumOfRooms, value);
        }

        static List<Flat> FlatsList = new List<Flat>();

        //Inserts value to the list and returns 1/0
        public bool Insert()
        {
            int num = dbs.InsertFlat(this);
            if (num!=0)
            {
                FlatsList.Add(this);

                return true;

            }

            else return false;
           
        }

        //Returns the flats list
        public List<Flat> Read() {

          return dbs.ReadFlats();
        }

        //Makes 10% discount
        public double Discount(int numOfRooms, double price)
        {
            double discountVlue = .9;
            if (numOfRooms > 1 && price > 100)
            {
                return discountVlue * price;
            }

            return price;
        }

        //Returns all the flats that are under the wanted price & city
        public List<Flat> FlatByPrice(string city, double maxPrice)
        {
            List<Flat> flatByPriceList = new List<Flat>();
            foreach (Flat flat in FlatsList)
            {
                if (flat.price< maxPrice && flat.City==city)
                {
                    flatByPriceList.Add(flat);
                }
            }
            return flatByPriceList;


        }
    }
}
