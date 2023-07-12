namespace Exe_1.Models
{
    public class Vacation
    {
        DBservices dbs = new DBservices();

        public int Id { get; set; }
        public string UserId { get; set; }
        public int FlatId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        static List<Vacation> OrdersList = new List<Vacation>();

        //Returns the vacation list
        public List<Vacation> Read() 
        {
            return dbs.ReadVacations();
        }

        //Inserts value to the list and returns 1/0
        public bool Insert()
        {
            bool flag = false;


            foreach (var order in OrdersList)//cheaking if there is a flat with same ID and the values of the dates
            {
                if (order.Id == this.Id || this.StartDate > this.EndDate)
                {
                    flag = true;
                }

                if (order.FlatId == this.FlatId)// we want to order the same flat
                {
                    //if we trying to order a vacation but the dates wasent availble
                    if (order.StartDate <= StartDate && order.EndDate >= StartDate ||/*Starts between*/
                    order.StartDate <= EndDate && order.EndDate >= EndDate ||/*Ends between*/
                    order.StartDate >= StartDate && order.EndDate <= EndDate)/*Bigger*/
                    {
                        flag = true;
                    }
                }
            }

            if (flag == false)
            {
                dbs.InsertVacation(this);
                OrdersList.Add(this);
                return true;
            }
            else
                return false;

        }

        public Object getAvgPricePerCityByMonth(int month)
        {
            return dbs.getAvgPricePerCityByMonth(month);

        }


        //Returns the vacation list by the wanted dates
        public List<Vacation> ordersByDates(DateTime startDate, DateTime endDate)
        {
            List<Vacation> ordersByDates = new List<Vacation>();

            foreach (Vacation v in OrdersList)
            {
                if (v.StartDate>startDate && v.EndDate<endDate)
                {
                    ordersByDates.Add(v);
                }
            }
            return ordersByDates;
        }
    }
}
        


    


